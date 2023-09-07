using Api.ApplicationLogic.Interface;
using Api.Core;
using Hangfire;
using Hangfire.MemoryStorage;

namespace Api.Infrastructure.Extensions
{
    public static class HangfireExtension
    {
        public static IServiceCollection AddHangFireCustom(this IServiceCollection services)
        {
            // Configure Hangfire to use MemoryStorage
            services.AddHangfire(configuration => configuration
                     .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                     .UseSimpleAssemblyNameTypeSerializer()
                     .UseDefaultTypeSerializer()
                     .UseMemoryStorage());

            services.AddHangfireServer();

            return services;
        }
        public static void RecurringJobsHangfire(AppConfiguration configuration)
        {
            RecurringJob.AddOrUpdate<ISampleJobs>(
                "sample-job",
                job => job.Recalculate(),
                configuration.RecurringJobs.CronjobExpression);
        }
    }
}