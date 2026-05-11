# Sonoran.Net

`Sonoran.Net` is the official C# SDK for Sonoran CAD, CMS, and Radio API integrations.

The package supports `.NET Framework 4.5.2` for FiveM resources and `.NET 8` for modern .NET applications.

## Install

```sh
dotnet add package Sonoran.Net
```

## Create a Client

```csharp
using Sonoran;

using var sonoran = new SonoranClient(new SonoranClientOptions
{
    product = SonoranProduct.CAD,
    apiKey = "your-api-key",
    communityId = "your-community-id",
    defaultServerId = 1
});
```

Use `SonoranProduct.CAD`, `SonoranProduct.CMS`, or `SonoranProduct.RADIO` for the API product you want to call. The client sets the default API URL for the selected product. You can override it with `apiUrl` when needed.

## Client Options

```csharp
var options = new SonoranClientOptions
{
    product = SonoranProduct.CAD,
    apiKey = "your-api-key",
    communityId = "your-community-id",
    apiUrl = "https://api.sonorancad.com",
    defaultServerId = 1,
    timeout = TimeSpan.FromSeconds(30),
    headers = new Dictionary<string, string>
    {
        ["X-Trace-Id"] = Guid.NewGuid().ToString("N")
    }
};
```

For Sonoran Radio, set the public community ID on `communityId` and the default room on `roomId` when creating the client:

```csharp
using var radio = new SonoranClient(new SonoranClientOptions
{
    product = SonoranProduct.RADIO,
    apiKey = "your-radio-api-key",
    communityId = "your-community-id",
    roomId = 1
});
```

| Property | Description |
| --- | --- |
| `product` | API product to use: `CAD`, `CMS`, or `RADIO`. |
| `apiKey` | API key used for authenticated requests. |
| `communityId` | Community identifier for your integration. Required for Radio v2 helpers unless supplied on an individual radio request. |
| `roomId` | Default Radio room ID for room-scoped Radio v2 helpers and request bodies. |
| `apiUrl` | Optional API base URL override. |
| `defaultServerId` | Default numeric server ID for CAD/CMS helpers that use server-scoped routes. Radio v2 helpers resolve the community route from `communityId`. |
| `timeout` | HTTP request timeout. |
| `headers` | Optional custom headers added to each request. |

## Response Handling

All SDK methods return `Task<SonoranResponse>`.

```csharp
var response = await sonoran.Cad.getVersionV2();

if (response.success)
{
    Console.WriteLine(response.data);
}
else
{
    Console.WriteLine(response.reason);
}
```

`data` contains the response body for successful requests. `reason` contains the error response for failed requests when one is available.

## Examples

```csharp
var emergencyCall = await sonoran.Cad.createEmergencyCallV2(new CreateEmergencyCallV2Request
{
    ServerId = 1,
    IsEmergency = true,
    Caller = "John Doe",
    Location = "101 Alta Street",
    Description = "Structure fire with visible smoke.",
    DeleteAfterMinutes = 30
});

var unitStatus = await sonoran.Cad.setUnitStatusV2(new SetUnitStatusV2Request
{
    ServerId = 1,
    Roblox = 123456789,
    Status = 2
});

var uploadedBodycam = await sonoran.Cad.uploadBodycamRecordingV2(new UploadBodycamRecordingV2Request
{
    AccountUuid = "USER_ACCOUNT_UUID",
    DurationMs = 90000,
    IdentId = 123,
    UnitNumber = "1A-12",
    UnitLocation = "Senora Fwy / Route 68",
    FileName = "bodycam-clip.webm",
    FileContent = await File.ReadAllBytesAsync("bodycam-clip.webm"),
    ContentType = "video/webm"
});
```

## CAD Methods

CAD methods are available through `sonoran.Cad`.

| Method |
| --- |
| `getLoginPageV2(GetLoginPageV2Query? query = null, CancellationToken cancellationToken = default)` |
| `checkApiIdV2(string apiId, CancellationToken cancellationToken = default)` |
| `applyPermissionKeyV2(ApplyPermissionKeyV2Request request, CancellationToken cancellationToken = default)` |
| `banUserV2(BanUserV2Request request, CancellationToken cancellationToken = default)` |
| `setPenalCodesV2(IReadOnlyList<PenalCodeV2> codes, CancellationToken cancellationToken = default)` |
| `setApiIdsV2(SetApiIdsV2Request request, CancellationToken cancellationToken = default)` |
| `getTemplatesV2(int? recordTypeId = null, CancellationToken cancellationToken = default)` |
| `createRecordV2(CreateRecordV2Request request, CancellationToken cancellationToken = default)` |
| `updateRecordV2(int recordId, UpdateRecordV2Request request, CancellationToken cancellationToken = default)` |
| `removeRecordV2(int recordId, CancellationToken cancellationToken = default)` |
| `sendRecordDraftV2(SendRecordDraftV2Request request, CancellationToken cancellationToken = default)` |
| `lookupV2(LookupV2Request request, CancellationToken cancellationToken = default)` |
| `lookupByValueV2(LookupByValueV2Request request, CancellationToken cancellationToken = default)` |
| `lookupCustomV2(LookupCustomV2Request request, CancellationToken cancellationToken = default)` |
| `getAccountV2(GetAccountV2Query? query = null, CancellationToken cancellationToken = default)` |
| `getAccountsV2(GetAccountsV2Query? query = null, CancellationToken cancellationToken = default)` |
| `createCommunityLinkV2(CommunityLinkV2Request request, CancellationToken cancellationToken = default)` |
| `checkCommunityLinkV2(CommunityLinkV2Request request, CancellationToken cancellationToken = default)` |
| `setAccountPermissionsV2(SetAccountPermissionsV2Request request, CancellationToken cancellationToken = default)` |
| `heartbeatV2(int? serverId, int playerCount, CancellationToken cancellationToken = default)` |
| `getVersionV2(CancellationToken cancellationToken = default)` |
| `getTurnCredentialsV2(GetTurnCredentialsV2Query? query = null, CancellationToken cancellationToken = default)` |
| `getServersV2(CancellationToken cancellationToken = default)` |
| `setServersV2(IReadOnlyList<CadServerV2> servers, bool deployMap = false, CancellationToken cancellationToken = default)` |
| `verifySecretV2(string secret, CancellationToken cancellationToken = default)` |
| `authorizeStreetSignsV2(int? serverId = null, CancellationToken cancellationToken = default)` |
| `setPostalsV2(IReadOnlyList<PostalV2> postals, CancellationToken cancellationToken = default)` |
| `sendPhotoV2(SendPhotoV2Request request, CancellationToken cancellationToken = default)` |
| `uploadBodycamRecordingV2(UploadBodycamRecordingV2Request request, CancellationToken cancellationToken = default)` |
| `getInfoV2(CancellationToken cancellationToken = default)` |
| `getCharactersV2(GetCharactersV2Query? query = null, CancellationToken cancellationToken = default)` |
| `removeCharacterV2(int characterId, CancellationToken cancellationToken = default)` |
| `setSelectedCharacterV2(SetSelectedCharacterV2Request request, CancellationToken cancellationToken = default)` |
| `getCharacterLinksV2(GetCharacterLinksV2Query? query = null, CancellationToken cancellationToken = default)` |
| `addCharacterLinkV2(string syncId, CharacterLinkTargetV2Request request, CancellationToken cancellationToken = default)` |
| `removeCharacterLinkV2(string syncId, CharacterLinkTargetV2Request request, CancellationToken cancellationToken = default)` |
| `getUnitsV2(GetUnitsV2Query? query = null, CancellationToken cancellationToken = default)` |
| `getCallsV2(GetCallsV2Query? query = null, CancellationToken cancellationToken = default)` |
| `getCurrentCallV2(string accountUuid, CancellationToken cancellationToken = default)` |
| `updateUnitLocationsV2(UpdateUnitLocationsV2Request request, CancellationToken cancellationToken = default)` |
| `setUnitPanicV2(SetUnitPanicV2Request request, CancellationToken cancellationToken = default)` |
| `setUnitStatusV2(SetUnitStatusV2Request request, CancellationToken cancellationToken = default)` |
| `kickUnitV2(KickUnitV2Request request, CancellationToken cancellationToken = default)` |
| `getIdentifiersV2(string accountUuid, CancellationToken cancellationToken = default)` |
| `getAccountUnitsV2(GetAccountUnitsV2Query request, CancellationToken cancellationToken = default)` |
| `selectIdentifierV2(string accountUuid, int identId, CancellationToken cancellationToken = default)` |
| `createIdentifierV2(string accountUuid, IdentifierV2Request request, CancellationToken cancellationToken = default)` |
| `updateIdentifierV2(string accountUuid, int identId, IdentifierV2Request request, CancellationToken cancellationToken = default)` |
| `deleteIdentifierV2(string accountUuid, int identId, CancellationToken cancellationToken = default)` |
| `addIdentifiersToGroupV2(AddIdentifiersToGroupV2Request request, CancellationToken cancellationToken = default)` |
| `createEmergencyCallV2(CreateEmergencyCallV2Request request, CancellationToken cancellationToken = default)` |
| `deleteEmergencyCallV2(int callId, int? serverId = null, CancellationToken cancellationToken = default)` |
| `createDispatchCallV2(CreateDispatchCallV2Request request, CancellationToken cancellationToken = default)` |
| `updateDispatchCallV2(int callId, UpdateDispatchCallV2Request request, CancellationToken cancellationToken = default)` |
| `attachUnitsToDispatchCallV2(int callId, DispatchAttachmentV2Request request, CancellationToken cancellationToken = default)` |
| `detachUnitsFromDispatchCallV2(DispatchAttachmentV2Request request, CancellationToken cancellationToken = default)` |
| `setDispatchPostalV2(int callId, string postal, int? serverId = null, CancellationToken cancellationToken = default)` |
| `setDispatchPrimaryV2(int callId, int identId, bool trackPrimary = false, int? serverId = null, CancellationToken cancellationToken = default)` |
| `addDispatchNoteV2(int callId, AddDispatchNoteV2Request request, CancellationToken cancellationToken = default)` |
| `closeDispatchCallsV2(IReadOnlyList<int> callIds, int? serverId = null, CancellationToken cancellationToken = default)` |
| `updateStreetSignsV2(UpdateStreetSignsV2Request request, CancellationToken cancellationToken = default)` |
| `setStreetSignConfigV2(IReadOnlyList<Dictionary<string, object?>> signs, int? serverId = null, CancellationToken cancellationToken = default)` |
| `setAvailableCalloutsV2(IReadOnlyList<AvailableCalloutV2> callouts, int? serverId = null, CancellationToken cancellationToken = default)` |
| `getPagerConfigV2(int? serverId = null, CancellationToken cancellationToken = default)` |
| `setPagerConfigV2(SetPagerConfigV2Request request, CancellationToken cancellationToken = default)` |
| `setStationsV2(StationConfigV2 config, int? serverId = null, CancellationToken cancellationToken = default)` |
| `getBlipsV2(int? serverId = null, CancellationToken cancellationToken = default)` |
| `createBlipV2(CreateBlipV2Request request, CancellationToken cancellationToken = default)` |
| `updateBlipV2(int blipId, UpdateBlipV2Request request, CancellationToken cancellationToken = default)` |
| `deleteBlipsV2(IReadOnlyList<int> ids, int? serverId = null, CancellationToken cancellationToken = default)` |

## CMS Methods

CMS methods are available through `sonoran.Cms`.

| Method |
| --- |
| `getCommunityV2(CancellationToken cancellationToken = default)` |
| `getSubVersionV2(CancellationToken cancellationToken = default)` |
| `lookupCommunityV2(object? query = null, CancellationToken cancellationToken = default)` |
| `getDepartmentsV2(CancellationToken cancellationToken = default)` |
| `getProfileFieldsV2(CancellationToken cancellationToken = default)` |
| `getClockInTypesV2(CancellationToken cancellationToken = default)` |
| `getCustomLogTypesV2(CancellationToken cancellationToken = default)` |
| `getPromotionFlowsV2(CancellationToken cancellationToken = default)` |
| `triggerPromotionFlowsV2(object request, CancellationToken cancellationToken = default)` |
| `undoRankChangeV2(string undoId, object? request = null, CancellationToken cancellationToken = default)` |
| `createShortUrlV2(object request, CancellationToken cancellationToken = default)` |
| `getAccountsV2(object? query = null, CancellationToken cancellationToken = default)` |
| `searchAccountsV2(object? query = null, CancellationToken cancellationToken = default)` |
| `getAccountV2(string accountId, CancellationToken cancellationToken = default)` |
| `getAccountRanksV2(string accountId, CancellationToken cancellationToken = default)` |
| `getAccountIdentifiersV2(string accountId, CancellationToken cancellationToken = default)` |
| `registerAccountIdentifiersV2(string accountId, object request, CancellationToken cancellationToken = default)` |
| `setAccountNameV2(string accountId, object request, CancellationToken cancellationToken = default)` |
| `setAccountRanksV2(string accountId, object request, CancellationToken cancellationToken = default)` |
| `editProfileFieldsV2(string accountId, object request, CancellationToken cancellationToken = default)` |
| `clockAccountV2(string accountId, object request, CancellationToken cancellationToken = default)` |
| `getCurrentClockInV2(string accountId, CancellationToken cancellationToken = default)` |
| `getLatestActivityV2(string accountId, object? query = null, CancellationToken cancellationToken = default)` |
| `forceSyncV2(string accountId, object? request = null, CancellationToken cancellationToken = default)` |
| `getServersV2(CancellationToken cancellationToken = default)` |
| `setServersV2(object request, CancellationToken cancellationToken = default)` |
| `addServersV2(object request, CancellationToken cancellationToken = default)` |
| `getAceConfigV2(int serverId, CancellationToken cancellationToken = default)` |
| `setAceConfigV2(int serverId, object request, CancellationToken cancellationToken = default)` |
| `setServerTypeV2(int serverId, object request, CancellationToken cancellationToken = default)` |
| `verifyWhitelistV2(int serverId, object request, CancellationToken cancellationToken = default)` |
| `getWhitelistV2(int serverId, CancellationToken cancellationToken = default)` |
| `createActivityV2(int serverId, object? request = null, CancellationToken cancellationToken = default)` |
| `startActivityV2(int serverId, object? request = null, CancellationToken cancellationToken = default)` |
| `rsvpEventV2(string eventId, object request, CancellationToken cancellationToken = default)` |
| `changeFormStageV2(string formId, object request, CancellationToken cancellationToken = default)` |
| `getFormSubmissionsV2(string templateId, object? query = null, CancellationToken cancellationToken = default)` |
| `getFormLockV2(string templateId, CancellationToken cancellationToken = default)` |
| `setFormLockV2(string templateId, object request, CancellationToken cancellationToken = default)` |
| `getSubmissionV2(string submissionId, CancellationToken cancellationToken = default)` |
| `getRosterV2(string rosterId, object? query = null, CancellationToken cancellationToken = default)` |
| `getDisciplinaryPointsV2(string accountId, CancellationToken cancellationToken = default)` |
| `getDisciplinaryRecordsV2(string accountId, CancellationToken cancellationToken = default)` |
| `addDisciplinaryRecordV2(string accountId, object request, CancellationToken cancellationToken = default)` |
| `setDisciplinaryRecordPointsV2(string recordId, object request, CancellationToken cancellationToken = default)` |
| `setDisciplinaryRecordReasonV2(string recordId, object request, CancellationToken cancellationToken = default)` |
| `setDisciplinaryRecordStatusV2(string recordId, object request, CancellationToken cancellationToken = default)` |
| `getOnlinePlayersV2(object? query = null, CancellationToken cancellationToken = default)` |
| `getPlayerQueueV2(object? query = null, CancellationToken cancellationToken = default)` |
| `addErlcRecordV2(object request, CancellationToken cancellationToken = default)` |
| `executeErlcCommandV2(object request, CancellationToken cancellationToken = default)` |
| `lockTeamV2(object request, CancellationToken cancellationToken = default)` |
| `unlockTeamV2(object request, CancellationToken cancellationToken = default)` |
| `getCurrentSessionV2(int? serverId = null, CancellationToken cancellationToken = default)` |
| `startSessionV2(object request, CancellationToken cancellationToken = default)` |
| `stopSessionV2(object request, CancellationToken cancellationToken = default)` |
| `cancelSessionV2(object request, CancellationToken cancellationToken = default)` |

## Radio Methods

Radio methods are available through `sonoran.Radio`.

| Method |
| --- |
| `getCommunityChannelsV2(string? communityId = null, CancellationToken cancellationToken = default)` |
| `getConnectedUsersV2(string? communityId = null, CancellationToken cancellationToken = default)` |
| `getMembersV2(GetMembersV2Query? query = null, CancellationToken cancellationToken = default)` |
| `getConnectedUserV2(string identity, string? communityId = null, CancellationToken cancellationToken = default)` |
| `setUserChannelsV2(string identity, object? options = null, string? communityId = null, CancellationToken cancellationToken = default)` |
| `setUserDisplayNameV2(SetUserDisplayNameV2Request request, CancellationToken cancellationToken = default)` |
| `approveMembersV2(IReadOnlyList<string> accIds, string? communityId = null, CancellationToken cancellationToken = default)` |
| `kickMembersV2(IReadOnlyList<string> accIds, string? communityId = null, CancellationToken cancellationToken = default)` |
| `banMembersV2(IReadOnlyList<string> accIds, string? communityId = null, CancellationToken cancellationToken = default)` |
| `setMemberDisplayNamesV2(IReadOnlyList<MemberDisplayNameV2Change> accNicknames, string? communityId = null, CancellationToken cancellationToken = default)` |
| `setMemberPermissionsV2(IReadOnlyList<MemberPermissionV2Change> userPerms, string? communityId = null, CancellationToken cancellationToken = default)` |
| `getServerSubscriptionFromIpV2(CancellationToken cancellationToken = default)` |
| `setServerIpV2(SetServerIpV2Request request, CancellationToken cancellationToken = default)` |
| `setInGameSpeakerLocationsV2(IReadOnlyList<object?> locations, string? communityId = null, CancellationToken cancellationToken = default)` |
| `playToneV2(PlayToneV2Request request, CancellationToken cancellationToken = default)` |

## Notes

The client automatically retries `429 Too Many Requests` responses up to 2 times and respects `Retry-After` when it is provided.
