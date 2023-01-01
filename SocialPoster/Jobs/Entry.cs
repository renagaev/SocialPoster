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
                var key = new JobKey(Guid.NewGuid().ToString());
                q.AddJob<T>(key);
                q.AddTrigger(o => o.ForJob(key).WithCronSchedule(cron));
            }

            AddJob<InstagramVerseJob>(config["Verses:Cron"]!);
            AddJob<QuoteJob>(config["Quotes:Cron"]!);
        });
        return services.AddQuartzHostedService(q => q.WaitForJobsToComplete = true);
    }
}