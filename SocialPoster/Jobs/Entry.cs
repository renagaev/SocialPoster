using Quartz;

namespace SocialPoster.Jobs;

public static class Entry
{
    public static IServiceCollection AddJobs(this IServiceCollection services, IConfiguration config)
    {
        services.AddQuartz(q =>
        {
            q.UseMicrosoftDependencyInjectionJobFactory();

            void AddJob<T>(string cron, string name) where T : IJob
            {
                var key = new JobKey(name);
                q.AddJob<T>(key, o => o.StoreDurably());
                q.AddTrigger(o => o
                    .ForJob(key)
                    .WithCronSchedule(cron)
                    .WithIdentity(new TriggerKey(name)));
            }

            AddJob<InstagramVerseJob>(config["Verses:Cron"]!, "verse");
            AddJob<QuoteJob>(config["Quotes:Cron"]!, "quote");
        });
        return services.AddQuartzHostedService(q => q.WaitForJobsToComplete = true);
    }
}