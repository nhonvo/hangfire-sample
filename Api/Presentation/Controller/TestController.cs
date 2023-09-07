using Api.ApplicationLogic.Interface;
using Api.ApplicationLogic.Services;
using Hangfire;
using Microsoft.AspNetCore.Mvc;

namespace Api.Presentation.Controller
{
    public class TestController : BaseController
    {
        private readonly ISampleJobs _sampleJobs;
        public TestController(
            ISampleJobs sampleJobs)
        {
            _sampleJobs = sampleJobs;
        }

        [HttpGet]
        public IActionResult Get()
            => Ok(_sampleJobs.Recalculate());

        [HttpPost("enqueue-job")]
        public IActionResult EnqueueJob()
        {
            // Enqueue the job
            BackgroundJob.Enqueue(() => new SampleJob().Recalculate());
            return Ok("Job has been enqueued.");
        }
    }
}