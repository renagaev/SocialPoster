using SixLabors.ImageSharp;

namespace SocialPoster.ImageProviders;

public interface IImageProvider
{
    Task<Image> GetImage();
}