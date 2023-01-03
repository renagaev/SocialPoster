using Microsoft.EntityFrameworkCore;
using SocialPoster.ImageProviders;
using SocialPoster.Instagram;
using SocialPoster.Jobs;
using SocialPoster.Models;
using SocialPoster.Services;
using SocialPoster.Storage;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;
var services = builder.Services;

services.AddDbContext<AppDbContext>(o => o.UseNpgsql(config["DbConnectionString"]));

services.AddImageProviders();
services.AddJobs(config);
services.AddInstagramServices(config["InstagrapiUrl"]);
services.AddSingleton<FontProvider>();
services.AddScoped<QuoteService>();
services.Configure<QuotesJobOptions>(config.GetSection("Quotes"));
services.Configure<VersesJobOptions>(config.GetSection("Verses"));


var app = builder.Build();


app.MapGet("/", () => "Hello World!");

app.Run();