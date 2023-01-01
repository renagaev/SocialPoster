using SixLabors.ImageSharp;

namespace SocialPoster.ImageProviders;

public class PaperImageProvider : IImageProvider
{
    public Task<Image> GetImage() => Image.LoadAsync("Assets/paper.jpg");
}