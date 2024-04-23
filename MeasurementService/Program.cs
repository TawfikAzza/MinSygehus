using MeasurementService.Repository;
using MeasurementService.Context;
using MeasurementService.Service;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

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

builder.Services.AddScoped<MeasurementManager>();
builder.Services.AddScoped<MeasurementRepository>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("_allowOriginsPolicy",
        policy  =>
        {
            policy.WithOrigins("http://localhost:9099", //PatientUI PROD
                                "http://localhost:8080", //DoctorUI PROD
                                "http://localhost:5173") //DEV
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseRouting();
app.UseCors("_allowOriginsPolicy");

app.UseAuthorization();

app.MapControllers();

app.Run();