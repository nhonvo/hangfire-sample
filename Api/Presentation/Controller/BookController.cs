using Api.ApplicationLogic.Services;
using Api.Core.Entities;
using Api.Infrastructure.IService;
using Microsoft.AspNetCore.Mvc;

namespace Api.Presentation.Controller
{
    [ApiController]
    [Route("[controller]")]
    public class BookController : ControllerBase
    {
        private readonly IBookReadService _bookReadService;
        private readonly IBookWriteService _bookWriteService;
        public BookController(IBookReadService bookReadService, IBookWriteService bookWriteService)
        {
            _bookReadService = bookReadService;
            _bookWriteService = bookWriteService;
        }

        [HttpGet("seed")]
        public async Task<IActionResult> Seed()
        {
            await _bookReadService.Seed();
            return Ok("Seed data successfully");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
            => Ok(await _bookReadService.Get(id));

        [HttpGet]
        public async Task<IActionResult> Get(int pageIndex = 0, int pageSize = 10)
            => Ok(await _bookReadService.Get(pageIndex, pageSize));

        [HttpPost]
        public async Task<IActionResult> Add(BookDTO request)
            => Ok(await _bookWriteService.Add(request));

        [HttpPut]
        public async Task<IActionResult> Update(Book request)
            => Ok(await _bookWriteService.Update(request));

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
            => Ok(await _bookWriteService.Delete(id));
    }
}