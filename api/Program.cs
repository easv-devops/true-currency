using infrastructure;
using Serilog;
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

//gets connection string to db
builder.Services.AddSingleton(provider => Utilities.MySqlConnectionString);


builder.Services.AddSingleton(provider => new CurrencyRepo(provider.GetRequiredService<string>()));

builder.Services.AddSingleton<CurrencyService>();

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

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowAll");

app.MapControllers();

Log.CloseAndFlush();

app.Run();

