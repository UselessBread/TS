using Microsoft.AspNetCore.Mvc;
using TA.Data.Contracts.Dto;
using TA.Services;

namespace TA.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TestAssignerController : Controller
    {
        private readonly ITestAssignerService _service;

        public TestAssignerController(ITestAssignerService service)
        {
            _service = service;
        }

        [HttpPost("assign")]
        public async Task AssignTest(AssignTestRequestDto dto)
        {
            await _service.AssignTest(dto);
        }
    }
}
