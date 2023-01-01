using SixLabors.ImageSharp;

namespace SocialPoster.ImageProviders;

public class RandomWideImageProvider : IImageProvider
{
    private readonly string _collectionId;

    public RandomWideImageProvider(IConfiguration configuration)
    {
        _collectionId = configuration["UnsplashCollectionId"]!;
    }

    public async Task<Image> GetImage()
    {
        var cl = new HttpClient();
        var res = await cl.GetByteArrayAsync($"https://source.unsplash.com/collection/{_collectionId}/735x1335");
        return Image.Load(res);
    }
}