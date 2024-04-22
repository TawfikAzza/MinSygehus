using Microsoft.Extensions.Options;
using PatientService.PatientContext;
using PatientService.Repository;
using PatientService.Service;

var builder = WebApplication.CreateBuilder(args);

var allowOriginsPolicy = "_allowOriginsPolicy";

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
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: allowOriginsPolicy,
        policy  =>
        {
            policy.WithOrigins("http://localhost:8088", //DoctorUI PROD
                    "http://localhost:5173") //DEV
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});

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

app.UseCors(allowOriginsPolicy);

app.Run();