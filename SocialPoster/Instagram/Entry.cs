namespace SocialPoster.Instagram;

public static class Entry
{
    public static IServiceCollection AddInstagramServices(this IServiceCollection services, string apiUrl)
    {
        services.AddHttpClient<InstagramClient>(cl =>
        {
            cl.BaseAddress = new Uri(apiUrl);
        });
        services.AddScoped<IInstagramProvider, InstagramProvider>();
        return services;
    }
}