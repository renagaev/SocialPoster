using SixLabors.ImageSharp;

namespace SocialPoster.ImageProviders;

public class RandomSquareImageProvider : IImageProvider
{
    private readonly string _collectionId;

    public RandomSquareImageProvider(IConfiguration configuration)
    {
        _collectionId = configuration["UnsplashCollectionId"];
    }

    public async Task<Image> GetImage()
    {
        var cl = new HttpClient();
        var res = await cl.GetByteArrayAsync($"https://source.unsplash.com/collection/{_collectionId}/1080x1080");
        return Image.Load(res);
    }
}