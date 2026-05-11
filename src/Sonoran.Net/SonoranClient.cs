using System.Globalization;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;

namespace Sonoran;

public sealed partial class SonoranClient : IDisposable
{
    private const int CadV2RateLimitMaxRetries = 2;
    private const int CadV2RateLimitDefaultDelayMs = 1000;
    private const int CadV2RateLimitMaxDelayMs = 10000;

    private static readonly HttpMethod PatchMethod = new("PATCH");

    private static readonly JsonSerializerSettings SerializerSettings = new()
    {
        ContractResolver = new DefaultContractResolver
        {
            NamingStrategy = new CamelCaseNamingStrategy
            {
                OverrideSpecifiedNames = false
            }
        },
        NullValueHandling = NullValueHandling.Ignore,
        // https://github.com/advisories/GHSA-5crp-9r3c-p9vr
        MaxDepth = 128
    };

    private readonly HttpClient _httpClient;
    private readonly bool _ownsHttpClient;
    private readonly Func<TimeSpan, CancellationToken, Task> _delay;
    private readonly string _apiUrl;

    private readonly Dictionary<SonoranProduct, string> AllowedCommonNames = new()
    {
        { SonoranProduct.CAD, "api.sonorancad.com" },
        { SonoranProduct.CMS, "api.sonorancms.com" },
        { SonoranProduct.RADIO, "api.sonoranradio.com" }
    };

    public SonoranClient(SonoranClientOptions options, HttpClient? httpClient = null, Func<TimeSpan, CancellationToken, Task>? delay = null)
    {
        Options = options ?? throw new ArgumentNullException(nameof(options));
        if (Options.product is null)
        {
            throw new ArgumentException("product is required when instancing.", nameof(options));
        }

        if (Options.product != SonoranProduct.CAD && Options.product != SonoranProduct.CMS && Options.product != SonoranProduct.RADIO)
        {
            throw new NotSupportedException("Only SonoranProduct.CAD, SonoranProduct.CMS, and SonoranProduct.RADIO are currently supported in Sonoran.Net.");
        }

        bool httpClientProvided = httpClient != null;

        if (!httpClientProvided)
        {
            // Thanks to FiveM's Mono, we have to do certificate validation ourselves
            ServicePointManager.ServerCertificateValidationCallback = (sender, cert, chain, sslPolicyErrors) =>
            {
                if (DateTime.Parse(cert!.GetExpirationDateString()) < DateTime.Now)
                {
                    throw new HttpRequestException($"Certificate expired: {cert.GetExpirationDateString()}");
                }

                string? commonName = ExtractCommonName(cert.Subject);

                if (string.IsNullOrEmpty(commonName) || !AllowedCommonNames.ContainsValue(commonName!))
                {
                    throw new HttpRequestException("Certificate subject mismatch");
                }

                return true;
            };
        }

        _httpClient = httpClientProvided ? httpClient! : new HttpClient();
        _ownsHttpClient = httpClient is null;
        _httpClient.Timeout = options.timeout;
        _delay = delay ?? Task.Delay;
        _apiUrl = !string.IsNullOrWhiteSpace(options.apiUrl)
            ? options.apiUrl
            : Options.product switch
            {
                SonoranProduct.CMS => "https://api.sonorancms.com",
                SonoranProduct.RADIO => "https://api.sonoranradio.com",
                _ => "https://api.sonorancad.com"
            };
        Cad = new SonoranCadClient(this);
        Cms = new SonoranCmsClient(this);
        Radio = new SonoranRadioClient(this);
    }

    public SonoranClientOptions Options { get; }
    public SonoranCadClient Cad { get; }
    public SonoranCmsClient Cms { get; }
    public SonoranRadioClient Radio { get; }

    public void Dispose()
    {
        if (_ownsHttpClient)
        {
            _httpClient.Dispose();
        }
    }

    private async Task<SonoranResponse> RequestAsync(HttpMethod method, string path, object? body = null, IReadOnlyDictionary<string, object?>? query = null, bool authenticated = true, CancellationToken cancellationToken = default)
    {
        for (var attempt = 0; attempt <= CadV2RateLimitMaxRetries; attempt++)
        {
            using var request = new HttpRequestMessage(method, BuildUrl(path, query));
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            if (authenticated)
            {
                if (string.IsNullOrWhiteSpace(Options.apiKey))
                {
                    throw new InvalidOperationException("apiKey is required for authenticated requests.");
                }

                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", Options.apiKey);
            }

            if (Options.headers is not null)
            {
                foreach (var header in Options.headers)
                {
                    request.Headers.TryAddWithoutValidation(header.Key, header.Value);
                }
            }

            if (body is not null)
            {
                request.Content = new StringContent(JsonConvert.SerializeObject(body, SerializerSettings), Encoding.UTF8, "application/json");
            }

            using var response = await _httpClient.SendAsync(request, cancellationToken).ConfigureAwait(false);
            var parsed = await ParseResponseAsync(response, cancellationToken).ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                return new SonoranResponse
                {
                    success = true,
                    data = parsed
                };
            }

            if (response.StatusCode == (HttpStatusCode)429 && attempt < CadV2RateLimitMaxRetries)
            {
                var delayMs = ResolveRetryDelayMs(response, attempt);
                if (delayMs > 0)
                {
                    await _delay(TimeSpan.FromMilliseconds(delayMs), cancellationToken).ConfigureAwait(false);
                }
                continue;
            }

            return new SonoranResponse
            {
                success = false,
                reason = ToReason(parsed)
            };
        }

        return new SonoranResponse
        {
            success = false,
            reason = "Request was rate limited."
        };
    }

    private async Task<SonoranResponse> RequestMultipartAsync(HttpMethod method, string path, Func<HttpContent> contentFactory, IReadOnlyDictionary<string, object?>? query = null, bool authenticated = true, CancellationToken cancellationToken = default)
    {
        for (var attempt = 0; attempt <= CadV2RateLimitMaxRetries; attempt++)
        {
            using var request = new HttpRequestMessage(method, BuildUrl(path, query));
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            if (authenticated)
            {
                if (string.IsNullOrWhiteSpace(Options.apiKey))
                {
                    throw new InvalidOperationException("apiKey is required for authenticated requests.");
                }

                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", Options.apiKey);
            }

            if (Options.headers is not null)
            {
                foreach (var header in Options.headers)
                {
                    request.Headers.TryAddWithoutValidation(header.Key, header.Value);
                }
            }

            request.Content = contentFactory();

            using var response = await _httpClient.SendAsync(request, cancellationToken).ConfigureAwait(false);
            var parsed = await ParseResponseAsync(response, cancellationToken).ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                return new SonoranResponse
                {
                    success = true,
                    data = parsed
                };
            }

            if (response.StatusCode == (HttpStatusCode)429 && attempt < CadV2RateLimitMaxRetries)
            {
                var delayMs = ResolveRetryDelayMs(response, attempt);
                if (delayMs > 0)
                {
                    await _delay(TimeSpan.FromMilliseconds(delayMs), cancellationToken).ConfigureAwait(false);
                }
                continue;
            }

            return new SonoranResponse
            {
                success = false,
                reason = ToReason(parsed)
            };
        }

        return new SonoranResponse
        {
            success = false,
            reason = "Request was rate limited."
        };
    }

    private static async Task<JToken?> ParseResponseAsync(HttpResponseMessage response, CancellationToken cancellationToken)
    {
        if (response.StatusCode == HttpStatusCode.NoContent || response.Content is null)
        {
            return null;
        }

        var rawBody =
#if NET8_0_OR_GREATER
            await response.Content.ReadAsStringAsync(cancellationToken).ConfigureAwait(false);
#else
            await response.Content.ReadAsStringAsync().ConfigureAwait(false);
#endif
        if (string.IsNullOrWhiteSpace(rawBody))
        {
            return null;
        }

        var contentType = response.Content.Headers.ContentType?.MediaType ?? string.Empty;
        if (contentType.StartsWith("application/json", StringComparison.OrdinalIgnoreCase))
        {
            try
            {
                return JToken.Parse(rawBody);
            }
            catch (JsonException)
            {
            }
        }

        return new JValue(rawBody);
    }

    private string BuildUrl(string path, IReadOnlyDictionary<string, object?>? query)
    {
        var builder = new StringBuilder();
        builder.Append(_apiUrl.TrimEnd('/'));
        builder.Append('/');
        builder.Append(path.TrimStart('/'));

        if (query is null || query.Count == 0)
        {
            return builder.ToString();
        }

        var parts = new List<string>();
        foreach (var key in query.Keys.OrderBy(static key => key, StringComparer.Ordinal))
        {
            AppendQueryParts(parts, key, query[key]);
        }

        if (parts.Count == 0)
        {
            return builder.ToString();
        }

        builder.Append('?');
        builder.Append(string.Join("&", parts));
        return builder.ToString();
    }

    private static void AppendQueryParts(List<string> parts, string key, object? value)
    {
        if (value is null)
        {
            return;
        }

        if (value is not string && value is System.Collections.IEnumerable enumerable)
        {
            foreach (var entry in enumerable)
            {
                AppendQueryParts(parts, key, entry);
            }
            return;
        }

        var stringValue = value switch
        {
            bool boolValue => boolValue ? "true" : "false",
            _ => Convert.ToString(value, CultureInfo.InvariantCulture) ?? string.Empty
        };

        parts.Add($"{Uri.EscapeDataString(key)}={Uri.EscapeDataString(stringValue)}");
    }

    private static int ResolveRetryDelayMs(HttpResponseMessage response, int attempt)
    {
        if (response.Headers.TryGetValues("Retry-After", out var values))
        {
            var retryAfter = values.FirstOrDefault();
            if (double.TryParse(retryAfter, NumberStyles.Float, CultureInfo.InvariantCulture, out var retryAfterSeconds) && retryAfterSeconds >= 0)
            {
                return Math.Min((int)Math.Round(retryAfterSeconds * 1000d), CadV2RateLimitMaxDelayMs);
            }
        }

        return Math.Min(CadV2RateLimitDefaultDelayMs * (1 << attempt), CadV2RateLimitMaxDelayMs);
    }

    private int ResolveServerId(int? serverId)
    {
        var resolvedServerId = serverId ?? Options.defaultServerId;
        AssertPositiveInteger(resolvedServerId, nameof(serverId));
        return resolvedServerId;
    }

    private string ResolveRadioCommunityId(string? communityId = null, int? serverId = null)
    {
        if (!string.IsNullOrWhiteSpace(communityId))
        {
            return communityId;
        }

        if (serverId is not null)
        {
            AssertPositiveInteger(serverId.Value, nameof(serverId));
            return serverId.Value.ToString(CultureInfo.InvariantCulture);
        }

        if (!string.IsNullOrWhiteSpace(Options.communityId))
        {
            return Options.communityId;
        }

        return ResolveServerId(null).ToString(CultureInfo.InvariantCulture);
    }

    private static void AssertPositiveInteger(int value, string name)
    {
        if (value < 1)
        {
            throw new ArgumentException($"{name} must be a positive integer.", name);
        }
    }

    private static string EncodePathSegment(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException("Value cannot be null or whitespace.", nameof(value));
        }

        return Uri.EscapeDataString(value);
    }

    private static IReadOnlyDictionary<string, object?>? ToQueryDictionary<T>(T? value)
    {
        if (value is null)
        {
            return null;
        }

        var node = JObject.FromObject(value, JsonSerializer.Create(SerializerSettings));
        if (node is null || node.Count == 0)
        {
            return null;
        }

        return node.Properties().ToDictionary(static property => property.Name, static property => ToObject(property.Value));
    }

    private static object WithoutServerId<T>(T value) where T : notnull => WithoutKeys(value, "ServerId");

    private static object WithoutKeys<T>(T value, params string[] propertyNames) where T : notnull
    {
        var node = JObject.FromObject(value, JsonSerializer.Create(SerializerSettings));
        foreach (var propertyName in propertyNames)
        {
            var jsonName = char.ToLowerInvariant(propertyName[0]) + propertyName.Substring(1);
            node.Remove(jsonName);
        }
        return node;
    }

    private static object? ToObject(JToken? node)
    {
        return node switch
        {
            null => null,
            JValue value => value.Value,
            JArray array => array.Select(ToObject).ToList(),
            JObject obj => obj.Properties().ToDictionary(static property => property.Name, static property => ToObject(property.Value)),
            _ => node.ToString(Formatting.None)
        };
    }

    private static object? ToReason(JToken? node)
    {
        if (node is JValue { Value: string stringReason })
        {
            return stringReason;
        }

        return node;
    }

    private static string? ExtractCommonName(string subject)
    {
        string[] parts = subject.Split(',');

        foreach (string part in parts)
        {
            if (part.Trim().StartsWith("CN=", StringComparison.OrdinalIgnoreCase))
            {
                return part.Trim().Substring(3);  // Return the value after "CN="
            }
        }

        return null;
    }
}
