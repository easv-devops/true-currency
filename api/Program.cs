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
var connString = new SecretService().GetSecret();
if (ReferenceEquals(connString, null))
{
    //gets connection string to db
    builder.Services.AddSingleton(provider => Utilities.MySqlConnectionString);
}
else
{
    //gets connection string to db
    builder.Services.AddSingleton(provider => connString);
}

Console.WriteLine("currency-conn fra Util "+Utilities.MySqlConnectionString);
Console.WriteLine("currency-conn fra Azure Key Vault "+connString);

builder.Services.AddSingleton(provider => new CurrencyRepo(provider.GetRequiredService<string>()));
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