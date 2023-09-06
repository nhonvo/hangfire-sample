using Api.ApplicationLogic;
using Api.Core;
using Api.Infrastructure;
using Api.Infrastructure.Extensions;
using Api.Infrastructure.Persistence;
using Api.Presentation.Middlewares;
using Hangfire;
using Serilog;

namespace Api.Presentation.Extensions
{
    public static class HostingExtensions
    {
        public static WebApplication ConfigureServices(
            this WebApplicationBuilder builder,
            AppConfiguration configuration)
        {
            builder.Host.UseSerilog((context, config) =>
           {
               config.ReadFrom.Configuration(context.Configuration);
           });
            builder.Services.AddInfrastructuresService(configuration);
            builder.Services.AddApplicationService();
            builder.Services.AddWebAPIService(configuration);


            return builder.Build();
        }

        public static async Task<WebApplication> ConfigurePipelineAsync(this WebApplication app, AppConfiguration configuration)
        {
            using var scope = app.Services.CreateScope();
            if (!configuration.UseInMemoryDatabase)
            {
                var initialize = scope.ServiceProvider.GetRequiredService<ApplicationDbContextInitializer>();
                await initialize.InitializeAsync();
            }
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
                Log.Information("Access swagger by  link http://localhost:5253/swagger/index.html");
            }
            // Use Hangfire dashboard
            app.UseHangfireDashboard();

            // Enable Hangfire Dashboard
            app.UseHangfireDashboard("/jobs", new DashboardOptions
            {
                Authorization = new[] { new HangfireAuthorizationFilter() }
            });

            app.UseCors("_myAllowSpecificOrigins");

            app.UseMiddleware<GlobalExceptionMiddleware>();

            app.UseMiddleware<PerformanceMiddleware>();

            app.UseMiddleware<LoggingMiddleware>();

            app.UseResponseCompression();

            app.UseResponseCompression();

            app.UseHttpsRedirection();

            app.MapHealthChecks("/hc");

            app.UseAuthentication();

            app.UseAuthorization();

            app.MapControllers();

            app.UseSerilogRequestLogging();

            return app;
        }
    }
}