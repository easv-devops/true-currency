using FeatureHubSDK;
using infrastructure;
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

FeatureLogging.DebugLogger += (sender, s) => Console.WriteLine("DEBUG: " + s);
FeatureLogging.TraceLogger += (sender, s) => Console.WriteLine("TRACE: " + s);
FeatureLogging.InfoLogger += (sender, s) => Console.WriteLine("INFO: " + s);
FeatureLogging.ErrorLogger += (sender, s) => Console.WriteLine("ERROR: " + s);

var config = new EdgeFeatureHubConfig("http://featurehub:8085",
    "5c0f0b36-21ed-4da1-bb6c-2ef1316ea865/J3tcF5V9eHZBwrw9IVgOaHMTDthmnCZi6claDzSw");
var fh = await config.NewContext().Build();

Console.WriteLine(fh["History"].IsEnabled);

app.Run();

