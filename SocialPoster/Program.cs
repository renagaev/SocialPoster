using Microsoft.EntityFrameworkCore;
using SocialPoster.ImageProviders;
using SocialPoster.Jobs;
using SocialPoster.Models;
using SocialPoster.Storage;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;
var services = builder.Services;

services.AddDbContext<AppDbContext>(o => o.UseNpgsql(config["DbConnectionString"]));

services.AddImageProviders();
services.AddJobs(config);

services.Configure<QuotesJobOptions>(config.GetSection("Quotes"));
services.Configure<QuotesJobOptions>(config.GetSection("Verses"));


var app = builder.Build();


app.MapGet("/", () => "Hello World!");

app.Run();