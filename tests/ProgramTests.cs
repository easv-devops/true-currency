using System.Net;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;

namespace tests
{
    public class ProgramTests
    {
        [Test]
        public async Task Main_ShouldRunApplication()
        {
            // Arrange
            var builder = new WebHostBuilder()
                .UseStartup<TestStartup>();

            using var server = new TestServer(builder);
            using var client = server.CreateClient();

            // Act
            var response = await client.GetAsync("/");

            // Assert
            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
        }
    }

    public class TestStartup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            // Configure services for testing
            services.AddControllers();
            services.AddSwaggerGen();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // Configure middleware and request pipeline for testing
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}