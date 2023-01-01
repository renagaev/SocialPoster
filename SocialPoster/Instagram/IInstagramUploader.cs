using SixLabors.ImageSharp;

namespace SocialPoster.Instagram;

public interface IInstagramUploader
{
    Task UploadStory(Image image, string caption);
    Task UploadPost(Image image, string caption);
}