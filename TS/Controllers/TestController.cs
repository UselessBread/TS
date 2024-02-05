using Common.Dto;
using Common.Web.Helpers;
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
            Guid userId = JwtTokenHelpers.GetUserIdFromToken(Request);
            await _testsRepository.CreateNewTest(dto, userId);
        }

        //TODO: Use Pagination
        [Authorize(Roles = "Admin, Teacher")]
        [HttpPost("descriptions")]
        public async Task<PaginatedResponse<TestDescriptions>> GetAllDescriptions(PaginationRequest paginationRequest)
        {
            Guid userId = JwtTokenHelpers.GetUserIdFromToken(Request);
            return await _testsRepository.GetAllDescriptions(userId, paginationRequest);
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