using api;
using infrastructure;
using Microsoft.EntityFrameworkCore;
using service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

//saves connection string
builder.Services.AddNpgsqlDataSource(Utilities.MySqlConnectionString, 
    dataSourceBuilder => dataSourceBuilder.EnableParameterLogging());

try
{
    var connString = new SecretService().GetSecret();
    if (string.IsNullOrEmpty(connString))
    {
        // Azure Key Vault is not accessible or returned an empty string, use a default connection string
        connString = Utilities.MySqlConnectionString;
    }

    // Register the retrieved or default connection string as a singleton service
    builder.Services.AddSingleton(provider => connString);
}
catch (Exception ex)
{
    // Log the exception or handle it as per your application's error handling strategy
    Console.WriteLine($"Error occurred while retrieving connection string from Azure Key Vault: {ex.Message}");

    // Use a default connection string as a fallback
    var connString = Utilities.MySqlConnectionString;

    // Register the default connection string as a singleton service
    builder.Services.AddSingleton(provider => connString);
}

builder.Services.AddSingleton<CurrencyRepo>();
builder.Services.AddSingleton<CurrencyService>();
builder.Services.AddSingleton<FeatureHubService>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder =>
        {
            builder
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
        });
});

var app = builder.Build();

app.UseCors("AllowAll");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();