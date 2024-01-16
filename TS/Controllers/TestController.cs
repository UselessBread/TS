using Microsoft.AspNetCore.Mvc;
using TS.Data;
using TS.DTO;

namespace TS.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TestController : Controller
    {
        private readonly ILogger<TestController> _logger;
        private readonly TestsContext _context;

        public TestController(ILogger<TestController> logger, TestsContext context)
        {
            _logger = logger;
            _context = context;
        }

        //[HttpGet("hello")]
        //public async Task<string> Hello() =>
        //    "hello";

        [HttpPost("create")]
        public async Task CreateNewTest(CreateNewTestDto dto)
        {
            if (dto.TestName == null || dto.Tasks == null)
                throw new Exception();

            DateTime creatonTame = DateTime.Now.ToUniversalTime();

            TestDescriptions testDescription = new TestDescriptions
            {
                Name = dto.TestName,
                ImmutableId = Guid.NewGuid(),
                CreationDate = creatonTame,
                Version = 1,
            };

            _context.TestDescriptions.Add(testDescription);
            await _context.SaveChangesAsync();

            int res = _context.TestDescriptions.First(descr => descr.ImmutableId == testDescription.ImmutableId).Id;
            TestsContent testsContent = new TestsContent()
            {
                Tasks = dto.Tasks,
                CreationDate = creatonTame,
                ImmutableId = Guid.NewGuid(),
                TestDescriptionImmutableId = testDescription.ImmutableId,
                TestDescriptionId = res,
                Version = 1,
            };

            _context.TestsContent.Add(testsContent);
            await _context.SaveChangesAsync();
        }
    }
}