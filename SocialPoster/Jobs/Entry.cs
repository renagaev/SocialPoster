using Quartz;

namespace SocialPoster.Jobs;

public static class Entry
{
    public static IServiceCollection AddJobs(this IServiceCollection services, IConfiguration config)
    {
        services.AddQuartz(q =>
        {
            q.UseMicrosoftDependencyInjectionJobFactory();

            void AddJob<T>(string cron) where T : IJob
            {
                var key = new JobKey(typeof(T).FullName);
                q.AddJob<T>(key, o => o.StoreDurably());
                q.AddTrigger(o => o.ForJob(key).WithCronSchedule(cron));
            }

            AddJob<InstagramVerseJob>(config["Verses:Cron"]!);
            AddJob<QuoteJob>(config["Quotes:Cron"]!);
        });
        return services.AddQuartzHostedService(q => q.WaitForJobsToComplete = true);
    }
}