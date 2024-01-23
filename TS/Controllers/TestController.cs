using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TS.Data.Contracts.DTO;
using TS.Data.Contracts.Entities;
using TS.Data.Repositories;

namespace TS.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TestController : Controller
    {
        private readonly ILogger<TestController> _logger;
        private readonly ITestsRepository _testsRepository;

        public TestController(ILogger<TestController> logger, ITestsRepository testsRepository)
        {
            _logger = logger;
            _testsRepository = testsRepository;
        }

        [Authorize (Roles = "Admin, Teacher")]
        [HttpPost("create")]
        public async Task CreateNewTest(CreateNewTestDto dto)
        {
            await _testsRepository.CreateNewTest(dto);
        }

        //TODO: Use Pagination
        [Authorize(Roles = "Admin")]
        [HttpGet("descriptions")]
        public async Task<List<TestDescriptions>> GetAllDescriptions()
        {
            return await _testsRepository.GetAllDescriptions();
        }

        [HttpGet("content")]
        public async Task<TestsContent> GetTestContentByDescriptionsId([FromQuery] Guid testDescriptionImmutableId)
        {
            return await _testsRepository.GetTestContentByDescriptionsId(testDescriptionImmutableId);
        }

        [Authorize(Roles = "Admin, Teacher")]
        [HttpPost("update")]
        public async Task Update(UpdateTestDto dto)
        {
            await _testsRepository.Update(dto);
        }
    }
}