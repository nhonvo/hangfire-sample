using Api.ApplicationLogic.Interface;

namespace Api.ApplicationLogic.Services
{
    public class SampleJob : ISampleJobs
    {
        static int i = 1;
        public Task Recalculate() => Task.Run(() =>
        {
            Console.WriteLine($"hello demo hangfire {i++}");
        });
    }
}