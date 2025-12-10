using Microsoft.AspNetCore.Authentication;
using SampleApiService;
using Serilog.Events;
using Serilog;
using Elastic.Serilog.Sinks;
using Elastic.Ingest.Elasticsearch.DataStreams;
using Elastic.Ingest.Elasticsearch;
using Elastic.Channels;
using SampleApiService.Services;
using Repository.Config;
using OcelotGatewayService;
using MassTransit;

using Microsoft.Data.SqlClient;
using System.Data;
using Repository.DapperRepo;

var builder = WebApplication.CreateBuilder(args);
MyConfigurationManager.SetConfiguration(builder.Configuration);

builder.Services.AddControllers();

// Add CORS - Read allowed origins from configuration
var allowedOrigins = builder.Configuration.GetSection("Cors:AllowedOrigins").Get<string[]>() 
    ?? new[] { "http://localhost:5173", "http://localhost:3000", "http://localhost:5174" };

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp", policy =>
    {
        policy.WithOrigins(allowedOrigins)
              .AllowAnyMethod()
              .AllowAnyHeader()
              .AllowCredentials();
    });
});

builder.Services.AddScoped<IDbConnection>(sp =>
{
    var connectionString = sp.GetRequiredService<IConfiguration>().GetConnectionString("Db");
    if (string.IsNullOrEmpty(connectionString))
    {
        throw new InvalidOperationException("Connection string 'Db' is not configured.");
    }
    return new SqlConnection(connectionString);
});

// Register your dependency
builder.Services.AddScoped<IProfileService, ProfileService>();
builder.Services.AddScoped<IProfileRepository, ProfileRepository>();



Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)  // Ignore most Microsoft logs below Warning
    .MinimumLevel.Override("Microsoft.AspNetCore.Hosting", LogEventLevel.Warning) // ignores request starting logs
    .MinimumLevel.Override("Microsoft.AspNetCore.Authentication", LogEventLevel.Warning) // ignore auth success logs
    .MinimumLevel.Override("Ocelot", LogEventLevel.Warning) // ignore auth success logs
    .Enrich.FromLogContext()
    //.WriteTo.Elasticsearch(new[] { new Uri(MyConfigurationManager.AppSettings["ElasticLog:ElasticUri"].ToString()) },
    //opts =>
    //{
    //    opts.DataStream = new DataStreamName(MyConfigurationManager.AppSettings["ElasticLog:DataStream:Type"].ToString(), MyConfigurationManager.AppSettings["ElasticLog:DataStream:Dataset"].ToString(), MyConfigurationManager.AppSettings["ElasticLog:DataStream:NameSpace"].ToString());
    //    opts.BootstrapMethod = BootstrapMethod.Failure;
    //    opts.ConfigureChannel = channelOpts =>
    //    {
    //        channelOpts.BufferOptions = new BufferOptions
    //        {
    //            ExportMaxConcurrency = 20
    //        };
    //    };
    //})
    .CreateLogger();

// Configure MassTransit with RabbitMQ
builder.Services.AddMassTransit(x =>
{
    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host(MyConfigurationManager.AppSettings["RabbitMQ:Uri"].ToString(), "/", h =>
        {
            h.Username(MyConfigurationManager.AppSettings["RabbitMQ:Username"].ToString());
            h.Password(MyConfigurationManager.AppSettings["RabbitMQ:Password"].ToString());
        });
    });
});

//string CorrelationId = Guid.NewGuid().ToString();
//Serilog.Context.LogContext.PushProperty("CorrelationId", CorrelationId);
//Console.WriteLine($"Incoming request for CorrelationId: {CorrelationId}");

builder.Host.UseSerilog(); // Use Serilog for the app

// Add Authentication scheme for Basic. It will invalidate DownstreamHeaderTransform
builder.Services.AddAuthentication("BasicAuth")
    .AddScheme<AuthenticationSchemeOptions, BasicAuthHandler>("BasicAuth", null);

var allowedClientIds = builder.Configuration.GetSection("AllowedClientIds").Get<List<string>>();
var app = builder.Build();

// Example debug log
var logger = app.Services.GetRequiredService<ILogger<Program>>();
logger.LogDebug("Sample API Service Application started");

// Enable CORS
app.UseCors("AllowReactApp");

//
app.UseMiddleware<CorrelationIdMiddleware>();
app.UseMiddleware<RequestLoggingMiddleware>();
app.UseMiddleware<ResponseLoggingMiddleware>();
app.UseDefaultFiles();
app.UseStaticFiles();
app.MapFallbackToFile("index.html");
app.UseRouting();

app.MapControllers();
app.Run();
