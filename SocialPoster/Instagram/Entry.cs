namespace SocialPoster.Instagram;

public static class Entry
{
    public static IServiceCollection AddInstagramServices(this IServiceCollection services)
    {
        services.AddSingleton<IInstagramProvider, InstagramProvider>();
        services.AddSingleton<InstagramClient>();
        return services;
    }
}