using Hangfire.Dashboard;

namespace Api.Infrastructure
{
    public class HangfireAuthorizationFilter : IDashboardAuthorizationFilter
    {
        public bool Authorize(DashboardContext context)
        {
            // Allow unrestricted access to the Hangfire Dashboard
            return true;
        }
    }
}