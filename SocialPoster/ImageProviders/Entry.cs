namespace SocialPoster.ImageProviders;

public static class Entry
{
    public static IServiceCollection AddImageProviders(this IServiceCollection services)
    {
        services.AddSingleton<RandomSquareImageProvider>();
        services.AddSingleton<PaperImageProvider>();
        services.AddSingleton<RandomWideImageProvider>();
        return services;
    }
}