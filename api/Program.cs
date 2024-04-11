using infrastructure;
using Infrastructure;
using MySqlConnector;
using Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllers();
builder.Services.AddSwaggerGen();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();




//saves connection string
builder.Services.AddNpgsqlDataSource(Utilities.ProperlyFormattedConnectionString, 
    dataSourceBuilder => dataSourceBuilder.EnableParameterLogging());

//gets connection string to db
builder.Services.AddSingleton(provider => Utilities.MySqlConnectionString);


builder.Services.AddSingleton(provider => new CurrencyRepo(provider.GetRequiredService<string>()));

builder.Services.AddSingleton<CurrencyService>();

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
