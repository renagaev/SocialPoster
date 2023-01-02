using SixLabors.ImageSharp;

namespace SocialPoster.Instagram;

public class InstagramUploader : IInstagramUploader
{
    private readonly InstagramClient _client;
    private readonly string _sessionId;

    public InstagramUploader(InstagramClient client, string sessionId)
    {
        _client = client;
        _sessionId = sessionId;
    }

    public async Task UploadStory(Image image, string caption)
    {
        var stream = new MemoryStream();
        await image.SaveAsJpegAsync(stream);
        await _client.UploadStory(_sessionId, stream.ToArray(), caption);
    }

    public async Task UploadPost(Image image, string caption)
    {
        var stream = new MemoryStream();
        await image.SaveAsJpegAsync(stream);
        await _client.UploadPost(_sessionId, stream.ToArray(), caption);
    }
}