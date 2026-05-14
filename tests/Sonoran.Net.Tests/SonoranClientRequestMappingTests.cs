using System.Net;
using System.Text;
using System.Text.Json.Nodes;
using Sonoran;

namespace Sonoran.Net.Tests;

public sealed class SonoranClientRequestMappingTests
{
    [Fact]
    public async Task GetLoginPageV2_UsesPublicQueryWithoutAuth()
    {
        var handler = new RecordingHandler();
        handler.QueueJson(HttpStatusCode.OK, """{"ok":true}""");

        using var client = CreateClient(handler);
        Assert.NotNull(client.Cad);
        var response = await client.Cad.getLoginPageV2(new GetLoginPageV2Query
        {
            Url = "https://example.com/callback",
            CommunityId = "abc 123"
        });

        var request = Assert.Single(handler.Requests);
        Assert.Equal(HttpMethod.Get, request.Method);
        Assert.Equal("https://api.sonorancad.com/v2/general/login-page?communityId=abc%20123&url=https%3A%2F%2Fexample.com%2Fcallback", GetEscapedUrl(request));
        Assert.Null(request.Headers.Authorization);
        Assert.True(response.success);
    }

    [Fact]
    public async Task HeartbeatV2_UsesServerPathAndBody()
    {
        var handler = new RecordingHandler();
        handler.QueueJson(HttpStatusCode.OK, """{"ok":true}""");

        using var client = CreateClient(handler);
        _ = await client.Cad.heartbeatV2(8, 24);

        var request = Assert.Single(handler.Requests);
        Assert.Equal("https://api.sonorancad.com/v2/general/servers/8/heartbeat", GetEscapedUrl(request));
        Assert.Equal("""{"playerCount":24}""", await request.Content!.ReadAsStringAsync());
    }

    [Fact]
    public async Task GetTurnCredentialsV2_UsesOptionalQuerySerialization()
    {
        var handler = new RecordingHandler();
        handler.QueueJson(HttpStatusCode.OK, """{"ok":true}""");

        using var client = CreateClient(handler);
        _ = await client.Cad.getTurnCredentialsV2(new GetTurnCredentialsV2Query
        {
            UserId = "unit/1"
        });

        var request = Assert.Single(handler.Requests);
        Assert.Equal(HttpMethod.Get, request.Method);
        Assert.Equal("https://api.sonorancad.com/v2/general/turn?userId=unit%2F1", GetEscapedUrl(request));
    }

    [Fact]
    public async Task GetCallsV2_UsesTypedQuerySerialization()
    {
        var handler = new RecordingHandler();
        handler.QueueJson(HttpStatusCode.OK, """{"ok":true}""");

        using var client = CreateClient(handler);
        _ = await client.Cad.getCallsV2(new GetCallsV2Query
        {
            ServerId = 10,
            ClosedLimit = 5,
            ClosedOffset = 2,
            Type = "dispatch"
        });

        var request = Assert.Single(handler.Requests);
        Assert.Equal("https://api.sonorancad.com/v2/emergency/servers/10/calls?closedLimit=5&closedOffset=2&type=dispatch", GetEscapedUrl(request));
    }

    [Fact]
    public async Task CreateEmergencyCallV2_UsesTypedRequestBody()
    {
        var handler = new RecordingHandler();
        handler.QueueJson(HttpStatusCode.OK, """{"ok":true}""");

        using var client = CreateClient(handler);
        _ = await client.Cad.createEmergencyCallV2(new CreateEmergencyCallV2Request
        {
            ServerId = 4,
            IsEmergency = true,
            Caller = "caller",
            Location = "loc",
            Description = "desc"
        });

        var request = Assert.Single(handler.Requests);
        Assert.Equal("https://api.sonorancad.com/v2/emergency/servers/4/calls/911", GetEscapedUrl(request));
        Assert.Equal("""{"isEmergency":true,"caller":"caller","location":"loc","description":"desc"}""", await request.Content!.ReadAsStringAsync());
    }

    [Fact]
    public async Task CreateDispatchCallV2_UsesTypedRequestBody()
    {
        var handler = new RecordingHandler();
        handler.QueueJson(HttpStatusCode.OK, """{"ok":true}""");

        using var client = CreateClient(handler);
        _ = await client.Cad.createDispatchCallV2(new CreateDispatchCallV2Request
        {
            ServerId = 11,
            Origin = 1,
            Status = 2,
            Priority = 1,
            Block = "123",
            Address = "Main",
            Postal = "100",
            Title = "Call",
            Code = "TS",
            Description = "desc",
            Notes = []
        });

        var request = Assert.Single(handler.Requests);
        Assert.Equal("https://api.sonorancad.com/v2/emergency/servers/11/dispatch-calls", GetEscapedUrl(request));
        Assert.Equal("""{"origin":1,"status":2,"priority":1,"block":"123","address":"Main","postal":"100","title":"Call","code":"TS","description":"desc","notes":[]}""", await request.Content!.ReadAsStringAsync());
    }

    [Fact]
    public async Task SetUnitStatusV2_StripsServerIdFromBody()
    {
        var handler = new RecordingHandler();
        handler.QueueJson(HttpStatusCode.OK, """{"ok":true}""");

        using var client = CreateClient(handler);
        _ = await client.Cad.setUnitStatusV2(new SetUnitStatusV2Request
        {
            ServerId = 5,
            Roblox = 123456789,
            Status = 2
        });

        var request = Assert.Single(handler.Requests);
        Assert.Equal("https://api.sonorancad.com/v2/emergency/servers/5/units/status", GetEscapedUrl(request));
        Assert.Equal("""{"roblox":123456789,"status":2}""", await request.Content!.ReadAsStringAsync());
    }

    [Fact]
    public async Task UpdateUnitLocationsV2_SupportsRobloxTargets()
    {
        var handler = new RecordingHandler();
        handler.QueueJson(HttpStatusCode.OK, """{"ok":true}""");

        using var client = CreateClient(handler);
        _ = await client.Cad.updateUnitLocationsV2(new UpdateUnitLocationsV2Request
        {
            ServerId = 5,
            Updates =
            [
                new UnitLocationUpdateV2
                {
                    Roblox = 123456789,
                    Location = "Mission Row"
                }
            ]
        });

        var request = Assert.Single(handler.Requests);
        Assert.Equal("https://api.sonorancad.com/v2/emergency/servers/5/unit-locations", GetEscapedUrl(request));
        Assert.Equal("""{"updates":[{"roblox":123456789,"location":"Mission Row"}]}""", await request.Content!.ReadAsStringAsync());
    }

    [Fact]
    public async Task SetStationsV2_UsesTopLevelPayloadShape()
    {
        var handler = new RecordingHandler();
        handler.QueueJson(HttpStatusCode.OK, """{"ok":true}""");

        using var client = CreateClient(handler);
        _ = await client.Cad.setStationsV2(new StationConfigV2
        {
            Locations =
            [
                new StationLocationV2
                {
                    Name = "Mission Row",
                    Coordinates = new BlipCoordinatesV2
                    {
                        X = 425.1,
                        Y = -979.2,
                        Z = 30.7,
                        W = 0.0
                    },
                    Doors = ["bay_1", "bay_2"],
                    Icon = "fas fa-building"
                }
            ],
            Tones = ["tone_station_open.mp3"],
            UnitColors = ["#2563eb", "#ef4444"]
        }, 7);

        var request = Assert.Single(handler.Requests);
        Assert.Equal("https://api.sonorancad.com/v2/emergency/servers/7/stations", GetEscapedUrl(request));
        Assert.Equal("""{"locations":[{"name":"Mission Row","coordinates":{"x":425.1,"y":-979.2,"z":30.7,"w":0.0},"doors":["bay_1","bay_2"],"icon":"fas fa-building"}],"tones":["tone_station_open.mp3"],"unitColors":["#2563eb","#ef4444"]}""", await request.Content!.ReadAsStringAsync());
    }

    [Fact]
    public async Task SetAvailableCalloutsV2_UsesBackendCalloutShape()
    {
        var handler = new RecordingHandler();
        handler.QueueJson(HttpStatusCode.OK, """{"ok":true}""");

        using var client = CreateClient(handler);
        _ = await client.Cad.setAvailableCalloutsV2(
        [
            new AvailableCalloutV2
            {
                Id = "armed_suspect",
                Data = new AvailableCalloutDataV2
                {
                    PedActionOnNoActionFound = "Flee",
                    PedActionMinimumTimeoutInMs = 2000,
                    PedChanceToFleeFromPlayer = 50,
                    PedChanceToObtainWeapons = 30,
                    CalloutName = "Armed Suspect",
                    CalloutDescriptions = ["Reports of an armed suspect in the area."],
                    PedChanceToAttackPlayer = 20,
                    PedActionMaximumTimeoutInMs = 10000,
                    Enabled = true,
                    CalloutLocations = [],
                    PedChanceToSurrender = 30,
                    PedWeaponData = ["WEAPON_PISTOL"],
                    CalloutUnitsRequired = new AvailableCalloutUnitsRequiredV2
                    {
                        TowRequired = false,
                        FireRequired = false,
                        Description = "Single suspect, use caution.",
                        PoliceRequired = true,
                        AmbulanceRequired = false
                    }
                }
            }
        ], 7);

        var request = Assert.Single(handler.Requests);
        Assert.Equal("https://api.sonorancad.com/v2/emergency/servers/7/callouts", GetEscapedUrl(request));
        Assert.Equal("""{"callouts":[{"id":"armed_suspect","data":{"PedActionOnNoActionFound":"Flee","PedActionMinimumTimeoutInMs":2000,"PedChanceToFleeFromPlayer":50,"PedChanceToObtainWeapons":30,"CalloutName":"Armed Suspect","CalloutDescriptions":["Reports of an armed suspect in the area."],"PedChanceToAttackPlayer":20,"PedActionMaximumTimeoutInMs":10000,"Enabled":true,"CalloutLocations":[],"PedChanceToSurrender":30,"PedWeaponData":["WEAPON_PISTOL"],"CalloutUnitsRequired":{"towRequired":false,"fireRequired":false,"description":"Single suspect, use caution.","policeRequired":true,"ambulanceRequired":false}}}]}""", await request.Content!.ReadAsStringAsync());
    }

    [Fact]
    public async Task SetPagerConfigV2_UsesBackendConfigShape()
    {
        var handler = new RecordingHandler();
        handler.QueueJson(HttpStatusCode.OK, """{"ok":true}""");

        using var client = CreateClient(handler);
        _ = await client.Cad.setPagerConfigV2(new SetPagerConfigV2Request
        {
            ServerId = 7,
            NatureWords = new Dictionary<string, string>
            {
                ["Emergency"] = "Emergency",
                ["NonEmergency"] = "Non-Emergency",
                ["Administrative"] = "Administrative"
            },
            MaxAddresses = 5,
            MaxBodyLength = 250,
            Nodes =
            [
                new PagerNodeV2
                {
                    Id = "root-1",
                    Name = "Fire",
                    Description = "Fire services",
                    Permission = "fire",
                    Address = "FIRE-01",
                    ShortCode = "F1",
                    Kind = "group",
                    Children = []
                }
            ]
        });

        var request = Assert.Single(handler.Requests);
        Assert.Equal("https://api.sonorancad.com/v2/emergency/servers/7/pager-config", GetEscapedUrl(request));
        Assert.Equal("""{"natureWords":{"Emergency":"Emergency","NonEmergency":"Non-Emergency","Administrative":"Administrative"},"maxAddresses":5,"maxBodyLength":250,"nodes":[{"id":"root-1","name":"Fire","description":"Fire services","permission":"fire","address":"FIRE-01","shortCode":"F1","kind":"group","children":[]}]}""", await request.Content!.ReadAsStringAsync());
    }

    [Fact]
    public async Task GetAccountUnitsV2_UsesPathAndQuery()
    {
        var handler = new RecordingHandler();
        handler.QueueJson(HttpStatusCode.OK, """{"ok":true}""");

        using var client = CreateClient(handler);
        _ = await client.Cad.getAccountUnitsV2(new GetAccountUnitsV2Query
        {
            ServerId = 6,
            AccountUuid = "acc/1",
            OnlyOnline = true,
            OnlyUnits = true,
            Limit = 4,
            Offset = 1
        });

        var request = Assert.Single(handler.Requests);
        Assert.Equal("https://api.sonorancad.com/v2/emergency/servers/6/accounts/acc%2F1/units?limit=4&offset=1&onlyOnline=true&onlyUnits=true", GetEscapedUrl(request));
    }

    [Fact]
    public async Task UploadBodycamRecordingV2_UsesMultipartRequest()
    {
        var handler = new RecordingHandler();
        handler.QueueJson(HttpStatusCode.OK, """{"ok":true}""");

        using var client = CreateClient(handler);
        _ = await client.Cad.uploadBodycamRecordingV2(new UploadBodycamRecordingV2Request
        {
            ApiId = "1",
            DurationMs = 90000,
            IdentId = 123,
            UnitNumber = "1A-12",
            UnitLocation = "Senora Fwy / Route 68",
            FileName = "bodycam-clip.webm",
            FileContent = Encoding.UTF8.GetBytes("webm-data")
        });

        var request = Assert.Single(handler.Requests);
        Assert.Equal(HttpMethod.Post, request.Method);
        Assert.Equal("https://api.sonorancad.com/v2/general/bodycam-recordings", GetEscapedUrl(request));
        Assert.Equal("Bearer test-key", request.Headers.Authorization?.ToString());
        Assert.NotNull(request.Content);
        Assert.Equal("multipart/form-data", request.Content!.Headers.ContentType?.MediaType);

        var body = await request.Content.ReadAsStringAsync();
        Assert.Contains("name=communityUserId", body);
        Assert.Contains("name=file; filename=bodycam-clip.webm", body);
        Assert.Contains("webm-data", body);
    }

    [Fact]
    public async Task AddIdentifiersToGroupV2_UsesPathValueAndTypedBody()
    {
        var handler = new RecordingHandler();
        handler.QueueJson(HttpStatusCode.OK, """{"ok":true}""");

        using var client = CreateClient(handler);
        _ = await client.Cad.addIdentifiersToGroupV2(new AddIdentifiersToGroupV2Request
        {
            ServerId = 4,
            GroupName = "A Shift",
            ApiIds = ["1"]
        });

        var request = Assert.Single(handler.Requests);
        Assert.Equal("https://api.sonorancad.com/v2/emergency/servers/4/identifier-groups/A%20Shift", GetEscapedUrl(request));
        Assert.Equal("""{"communityUserIds":["1"]}""", await request.Content!.ReadAsStringAsync());
    }

    [Fact]
    public async Task GetCharactersV2_SupportsRobloxQuery()
    {
        var handler = new RecordingHandler();
        handler.QueueJson(HttpStatusCode.OK, """{"ok":true}""");

        using var client = CreateClient(handler);
        _ = await client.Cad.getCharactersV2(new GetCharactersV2Query
        {
            Roblox = 123456789
        });

        var request = Assert.Single(handler.Requests);
        Assert.Equal("https://api.sonorancad.com/v2/civilian/characters?roblox=123456789", GetEscapedUrl(request));
    }

    [Fact]
    public async Task RetriesRateLimitedRequests()
    {
        var delays = new List<TimeSpan>();
        var handler = new RecordingHandler();
        handler.QueueJson((HttpStatusCode)429, """{"error":"bad request"}""", retryAfter: "1");
        handler.QueueJson((HttpStatusCode)429, """{"error":"bad request"}""");
        handler.QueueJson(HttpStatusCode.OK, """{"ok":true}""");

        using var client = CreateClient(handler, delays);
        var response = await client.Cad.getVersionV2();

        Assert.True(response.success);
        Assert.Equal(3, handler.Requests.Count);
        Assert.Equal(2, delays.Count);
        Assert.True(delays[0] >= TimeSpan.FromSeconds(1));
        Assert.True(delays[1] >= TimeSpan.FromSeconds(2));
    }

    [Fact]
    public async Task HandlesNoContentAndPlainTextErrors()
    {
        var handler = new RecordingHandler();
        handler.Queue(HttpStatusCode.NoContent, string.Empty, null);
        handler.Queue(HttpStatusCode.InternalServerError, "plain error", "text/plain");

        using var client = CreateClient(handler);
        var success = await client.Cad.getVersionV2();
        var failure = await client.Cad.getInfoV2();

        Assert.True(success.success);
        Assert.Null(success.data);
        Assert.False(failure.success);
        Assert.Equal("plain error", Assert.IsType<string>(failure.reason));
    }

    [Fact]
    public async Task RadioGetConnectedUsersV2_UsesRadioServerPath()
    {
        var handler = new RecordingHandler();
        handler.QueueJson(HttpStatusCode.OK, """{"ok":true}""");

        using var client = CreateRadioClient(handler);
        _ = await client.Radio.getConnectedUsersV2("9");

        var request = Assert.Single(handler.Requests);
        Assert.Equal("https://api.sonoranradio.com/v2/servers/9/connected-users", GetEscapedUrl(request));
        Assert.Equal("Bearer radio-key", request.Headers.Authorization?.ToString());
    }

    [Fact]
    public async Task RadioGetMembersV2_UsesTypedQuerySerialization()
    {
        var handler = new RecordingHandler();
        handler.QueueJson(HttpStatusCode.OK, """{"ok":true}""");

        using var client = CreateRadioClient(handler);
        _ = await client.Radio.getMembersV2(new GetMembersV2Query
        {
            CommunityId = "radio-community",
            Page = 1,
            PerPage = 25,
            Status = "approved",
            Search = "dispatch"
        });

        var request = Assert.Single(handler.Requests);
        Assert.Equal("https://api.sonoranradio.com/v2/servers/radio-community/members?page=1&perPage=25&search=dispatch&status=approved", GetEscapedUrl(request));
    }

    [Fact]
    public async Task RadioSetServerIpV2_AddsConfiguredRoomId()
    {
        var handler = new RecordingHandler();
        handler.QueueJson(HttpStatusCode.OK, """{"ok":true}""");

        using var client = CreateRadioClient(handler);
        _ = await client.Radio.setServerIpV2(new SetServerIpV2Request
        {
            CommunityId = "radio-community",
            ServerPort = 30120,
            PushUrl = "http://127.0.0.1:30120/sonoranradio",
            Nickname = "Patrol"
        });

        var request = Assert.Single(handler.Requests);
        Assert.Equal("https://api.sonoranradio.com/v2/servers/radio-community/server-ip", GetEscapedUrl(request));
        Assert.Equal("""{"roomId":2,"serverPort":30120,"pushUrl":"http://127.0.0.1:30120/sonoranradio","nickname":"Patrol"}""", await request.Content!.ReadAsStringAsync());
    }

    [Fact]
    public async Task RadioRoomScopedMethods_UseConfiguredRoomId()
    {
        var handler = new RecordingHandler();
        handler.QueueJson(HttpStatusCode.OK, """{"ok":true}""");

        using var client = CreateRadioClient(handler);
        _ = await client.Radio.getConnectedUserV2("user-1");

        var request = Assert.Single(handler.Requests);
        Assert.Equal("https://api.sonoranradio.com/v2/servers/radio-community/rooms/2/users/user-1", GetEscapedUrl(request));
    }

    [Fact]
    public async Task SetRoomId_UpdatesRoomScopedRadioPaths()
    {
        var handler = new RecordingHandler();
        handler.QueueJson(HttpStatusCode.OK, """{"ok":true}""");

        using var client = CreateRadioClient(handler);
        client.setRoomId(7);
        _ = await client.Radio.getConnectedUserV2("user-1");

        var request = Assert.Single(handler.Requests);
        Assert.Equal("https://api.sonoranradio.com/v2/servers/radio-community/rooms/7/users/user-1", GetEscapedUrl(request));
    }

    [Fact]
    public async Task SetRoomId_UpdatesRadioRequestBodies()
    {
        var handler = new RecordingHandler();
        handler.QueueJson(HttpStatusCode.OK, """{"ok":true}""");

        using var client = CreateRadioClient(handler);
        client.setRoomId(7);
        _ = await client.Radio.approveMembersV2(["user-1"]);

        var request = Assert.Single(handler.Requests);
        Assert.Equal("""{"roomId":7,"accIds":["user-1"]}""", await request.Content!.ReadAsStringAsync());
    }

    [Fact]
    public void SetRoomId_RequiresPositiveRoomId()
    {
        using var client = CreateRadioClient(new RecordingHandler());

        var exception = Assert.Throws<ArgumentException>(() => client.setRoomId(0));

        Assert.Equal("roomId must be a positive integer. (Parameter 'roomId')", exception.Message);
    }

    [Fact]
    public async Task RadioPlayToneV2_AddsConfiguredRoomId()
    {
        var handler = new RecordingHandler();
        handler.QueueJson(HttpStatusCode.OK, """{"ok":true}""");

        using var client = CreateRadioClient(handler);
        _ = await client.Radio.playToneV2(new PlayToneV2Request
        {
            Tones = [new { tone = "alert" }],
            PlayTo = [new { identity = "user-1" }]
        });

        var request = Assert.Single(handler.Requests);
        Assert.Equal("https://api.sonoranradio.com/v2/servers/radio-community/tones/play", GetEscapedUrl(request));
        Assert.Equal("""{"roomId":2,"tones":[{"tone":"alert"}],"playTo":[{"identity":"user-1"}]}""", await request.Content!.ReadAsStringAsync());
    }

    [Fact]
    public async Task RadioPlayToneV2_AcceptsRadioRoomIdAlias()
    {
        var handler = new RecordingHandler();
        handler.QueueJson(HttpStatusCode.OK, """{"ok":true}""");
        using var client = CreateRadioClient(handler, roomId: null, radioRoomId: 9);

        _ = await client.Radio.playToneV2(new PlayToneV2Request
        {
            Tones = [12],
            PlayTo = [new { type = "channel", value = 101 }]
        });

        var request = Assert.Single(handler.Requests);
        Assert.Equal("""{"roomId":9,"tones":[12],"playTo":[{"type":"channel","value":101}]}""", await request.Content!.ReadAsStringAsync());
    }

    [Fact]
    public async Task RadioPayloadV2Methods_AddConfiguredRoomId()
    {
        var handler = new RecordingHandler();
        handler.QueueJson(HttpStatusCode.OK, """{"ok":true}""");

        using var client = CreateRadioClient(handler);
        _ = await client.Radio.approveMembersV2(["user-1"]);

        var request = Assert.Single(handler.Requests);
        Assert.Equal("https://api.sonoranradio.com/v2/servers/radio-community/members/approve", GetEscapedUrl(request));
        Assert.Equal("""{"roomId":2,"accIds":["user-1"]}""", await request.Content!.ReadAsStringAsync());
    }

    [Fact]
    public void Constructor_RequiresProduct()
    {
        var exception = Assert.Throws<ArgumentException>(() => new SonoranClient(new SonoranClientOptions
        {
            apiKey = "test-key",
            communityId = "community-123"
        }));

        Assert.Contains("product is required when instancing.", exception.Message);
    }

    [Fact]
    public void Constructor_RejectsUnsupportedProducts()
    {
        var exception = Assert.Throws<NotSupportedException>(() => new SonoranClient(new SonoranClientOptions
        {
            product = (SonoranProduct)999,
            apiKey = "test-key",
            communityId = "community-123"
        }));

        Assert.Equal("Only SonoranProduct.CAD, SonoranProduct.CMS, and SonoranProduct.RADIO are currently supported in Sonoran.Net.", exception.Message);
    }

    private static SonoranClient CreateClient(RecordingHandler handler, List<TimeSpan>? delays = null)
    {
        var httpClient = new HttpClient(handler);
        return new SonoranClient(new SonoranClientOptions
        {
            product = SonoranProduct.CAD,
            apiKey = "test-key",
            communityId = "community-123",
            apiUrl = "https://api.sonorancad.com/",
            defaultServerId = 3,
            timeout = TimeSpan.FromMilliseconds(12345),
            headers = new Dictionary<string, string>
            {
                ["X-Test"] = "yes"
            }
        }, httpClient, (delay, _) =>
        {
            delays?.Add(delay);
            return Task.CompletedTask;
        });
    }

    private static SonoranClient CreateRadioClient(RecordingHandler handler, int? roomId = 2, int? radioRoomId = null)
    {
        var httpClient = new HttpClient(handler);
        return new SonoranClient(new SonoranClientOptions
        {
            product = SonoranProduct.RADIO,
            apiKey = "radio-key",
            communityId = "radio-community",
            roomId = roomId,
            radioRoomId = radioRoomId,
            apiUrl = "https://api.sonoranradio.com/",
            defaultServerId = 3,
            timeout = TimeSpan.FromMilliseconds(12345),
            headers = new Dictionary<string, string>
            {
                ["X-Test"] = "yes"
            }
        }, httpClient);
    }

    private sealed class RecordingHandler : HttpMessageHandler
    {
        private readonly Queue<HttpResponseMessage> _responses = new();

        public List<HttpRequestMessage> Requests { get; } = [];

        public void QueueJson(HttpStatusCode statusCode, string body, string? retryAfter = null)
        {
            var response = new HttpResponseMessage(statusCode)
            {
                Content = new StringContent(body, Encoding.UTF8, "application/json")
            };

            if (!string.IsNullOrWhiteSpace(retryAfter))
            {
                response.Headers.TryAddWithoutValidation("Retry-After", retryAfter);
            }

            _responses.Enqueue(response);
        }

        public void Queue(HttpStatusCode statusCode, string body, string? contentType)
        {
            var response = new HttpResponseMessage(statusCode);
            if (contentType is not null)
            {
                response.Content = new StringContent(body, Encoding.UTF8, contentType);
            }
            else
            {
                response.Content = new StringContent(body);
                response.Content.Headers.ContentType = null;
            }

            _responses.Enqueue(response);
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var clone = new HttpRequestMessage(request.Method, request.RequestUri);
            foreach (var header in request.Headers)
            {
                clone.Headers.TryAddWithoutValidation(header.Key, header.Value);
            }

            if (request.Content is not null)
            {
                var body = await request.Content.ReadAsStringAsync(cancellationToken);
                clone.Content = request.Content.Headers.ContentType?.MediaType is { } mediaType
                    ? new StringContent(body, Encoding.UTF8, mediaType)
                    : new StringContent(body, Encoding.UTF8);
                foreach (var header in request.Content.Headers)
                {
                    clone.Content.Headers.Remove(header.Key);
                    clone.Content.Headers.TryAddWithoutValidation(header.Key, header.Value);
                }
            }

            Requests.Add(clone);
            return _responses.Dequeue();
        }
    }

    private static string GetEscapedUrl(HttpRequestMessage request) =>
        request.RequestUri!.GetComponents(UriComponents.HttpRequestUrl, UriFormat.UriEscaped);
}
