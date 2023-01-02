namespace SocialPoster.Instagram;

public class InstagramClient
{
    private readonly HttpClient _client;

    public InstagramClient(HttpClient client)
    {
        _client = client;
    }


    public async Task<string> GetSettings(string sessionId)
    {
        var q = new QueryString().Add("sessionId", sessionId);
        return await _client.GetStringAsync("/auth/settings/get?" + q);
    }

    public async Task SetSettings(string sessionId, string settingsJson)
    {
        var content = new FormUrlEncodedContent(new Dictionary<string, string>
        {
            ["sessionid"] = sessionId,
            ["settings"] = settingsJson
        });
        await _client.PostAsync("/auth/settings/set", content);
    }

    public async Task UploadPost(string sessionId, byte[] image, string caption)
    {
        var content = new MultipartFormDataContent();
        content.Add(new StringContent(sessionId), "sessionId");
        content.Add(new StringContent(caption), "caption");
        content.Add(new ByteArrayContent(image), "file");

        await _client.PostAsync("/photo/upload", content);
    }

    public async Task UploadStory(string sessionId, byte[] image, string caption)
    {
        var content = new MultipartFormDataContent();
        content.Add(new StringContent(sessionId), "sessionId");
        content.Add(new StringContent(caption), "caption");
        content.Add(new ByteArrayContent(image), "file");

        await _client.PostAsync("/photo/upload_to_story", content);
    }
}