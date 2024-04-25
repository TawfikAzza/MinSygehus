using FeatureHubSDK;
using Microsoft.Extensions.Options;
using Monitoring;
using OpenTelemetry.Trace;
using PatientService;
using PatientService.PatientContext;
using PatientService.Repository;
using PatientService.Service;

var builder = WebApplication.CreateBuilder(args);
/* Tracer config **/
var serviceName = "PatientService";
var serviceVersion = "1.0.0";


builder.Services.AddOpenTelemetry().Setup(serviceName, serviceVersion);
builder.Services.AddSingleton(TracerProvider.Default.GetTracer(serviceName));

// FeatureHub setup
var (config, clientContext) = await FeatureHubConfigProvider.InitializeFeatureHub();
builder.Services.AddSingleton<EdgeFeatureHubConfig>(config);
builder.Services.AddSingleton<IClientContext>(clientContext);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient();

builder.Services.Configure<MongoDbSettings>(builder.Configuration.GetSection(nameof(MongoDbSettings)));

builder.Services.AddSingleton<DbContext>(serviceProvider =>
{   
    var settings = serviceProvider.GetRequiredService<IOptions<MongoDbSettings>>().Value;
    return new DbContext(settings.ConnectionString, settings.DatabaseName);
});

builder.Services.AddScoped<PatientManager>();
builder.Services.AddScoped<PatientRepository>();

// Configure CORS policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("_allowOriginsPolicy",
        policy =>
        {
            policy.WithOrigins("http://localhost:9099", //PatientUI PROD
                                "http://localhost:8080", //DoctorUI PROD
                                "http://localhost:5173") //DEV
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

app.UseRouting();
app.UseCors("_allowOriginsPolicy");

//app.UseAuthorization();

app.MapControllers();

app.Run();