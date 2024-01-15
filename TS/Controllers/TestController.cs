using Microsoft.AspNetCore.Mvc;

namespace TS.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TestController : Controller
    {
        private readonly ILogger<TestController> _logger;

        public TestController(ILogger<TestController> logger)
        {
            _logger = logger;
        }

        [HttpGet("hello")]
        public async Task<string> Hello() =>
            "hello";

        [HttpPost("create")]
        public async Task CreateNewTest()
        {
        }
    }
}