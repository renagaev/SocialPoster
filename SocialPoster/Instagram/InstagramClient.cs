
using System.Net.Http.Headers;

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
        var res = await _client.PostAsync("/auth/settings/set", content);
        var resContent = await res.Content.ReadAsStringAsync();
        res.EnsureSuccessStatusCode();
    }

    public async Task UploadPost(string sessionId, byte[] image, string caption)
    {
        var content = new MultipartFormDataContent();
        var imageContent = new ByteArrayContent(image);
        imageContent.Headers.ContentType = MediaTypeHeaderValue.Parse("image/jpeg");

        content.Add(new StringContent(sessionId), "sessionid");
        content.Add(new StringContent(caption), "caption");
        content.Add(imageContent, "file", "image.jpeg");

        var res = await _client.PostAsync("/photo/upload", content);
        var resContent = await res.Content.ReadAsStringAsync();
        res.EnsureSuccessStatusCode();
    }

    public async Task UploadStory(string sessionId, byte[] image, string caption)
    {
        var content = new MultipartFormDataContent();
        content.Add(new StringContent(sessionId), "sessionid");
        content.Add(new StringContent(caption), "caption");
        content.Add(new ByteArrayContent(image), "file", "image.jpeg");

        await _client.PostAsync("/photo/upload_to_story", content);
    }
}