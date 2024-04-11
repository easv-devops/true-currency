using infrastructure;
using Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Service;

namespace tests
{
    public class ProgramTests
    {
        [Test]
        public void ConfigureServices_ShouldRegisterServices()
        {
            // Arrange
            var services = new ServiceCollection();
            var builder = WebApplication.CreateBuilder();

            // Act
            builder.Services.AddControllers();
            builder.Services.AddSwaggerGen();
            builder.Services.AddSingleton(provider => Utilities.MySqlConnectionString);
            builder.Services.AddSingleton(provider => new CurrencyRepo(provider.GetRequiredService<string>()));
            builder.Services.AddSingleton<CurrencyService>();

            var app = builder.Build();

            // Assert
            var serviceProvider = app.Services;
            var currencyRepo = serviceProvider.GetService<CurrencyRepo>();
            var currencyService = serviceProvider.GetService<CurrencyService>();

            Assert.NotNull(currencyRepo);
            Assert.NotNull(currencyService);
        }
    }
}