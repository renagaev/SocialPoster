using System.Data;
using Npgsql;
using SocialPoster.ImageProviders;
using SocialPoster.Jobs;
using SocialPoster.Models;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;
var services = builder.Services;

services.AddScoped<IDbConnection>(_ => new NpgsqlConnection(config["DbConnectionString"]));

services.AddImageProviders();
services.AddJobs(config);

services.Configure<QuotesJobOptions>(config.GetSection("Quotes"));
services.Configure<QuotesJobOptions>(config.GetSection("Verses"));


var app = builder.Build();


app.MapGet("/", () => "Hello World!");

app.Run();