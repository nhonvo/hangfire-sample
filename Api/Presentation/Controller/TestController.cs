using Api.ApplicationLogic.Interface;
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

    }
}