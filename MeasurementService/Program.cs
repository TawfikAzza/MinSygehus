using MongoDB.Driver;
using Domain;
using MeasurementService.Repository;
using MeasurementService.Context;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<DatabaseContext>();

var connectionString = "mongodb://measurement-db:27017";
var databaseName = "measurement-db";
var allowOriginsPolicy = "_allowOriginsPolicy";

var client = new MongoClient(connectionString);
var database = client.GetDatabase(databaseName);
var measurementsCollection = database.GetCollection<Measurement>("measurements");

builder.Services.AddSingleton(measurementsCollection);
builder.Services.AddSingleton<IMongoClient>(client);

// Register MeasurementRepository
builder.Services.AddScoped<MeasurementRepository>(provider => 
{
    var connectionString = "mongodb://measurement-db:27017";
    var databaseName = "measurement-db";
    var client = new MongoClient(connectionString);
    var database = client.GetDatabase(databaseName);
    return new MeasurementRepository(client, database);
});

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: allowOriginsPolicy,
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

// Configure the HTTP request pipeline.

    app.UseSwagger();
    app.UseSwaggerUI();


//app.UseHttpsRedirection();

//app.UseAuthorization();

app.MapControllers();

app.UseCors(allowOriginsPolicy);

app.Run();