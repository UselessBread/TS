using Common.Constants;
using Common.Dto;
using Common.Web.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TS.Data.Contracts.DTO;
using TS.Data.Contracts.Entities;
using TS.Services;

namespace TS.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TestController : Controller
    {
        private readonly ILogger<TestController> _logger;
        private readonly ITestsService _testsService;

        public TestController(ILogger<TestController> logger, ITestsService testsService)
        {
            _logger = logger;
            _testsService = testsService;
        }

        [Authorize (Roles = $"{UserConstants.RoleAdmin}, {UserConstants.RoleTeacher}")]
        [HttpPost("create")]
        public async Task<TestDescriptions> CreateNewTest(CreateNewTestDto dto)
        {
            Guid userId = JwtTokenHelpers.GetUserIdFromToken(Request);
            return await _testsService.CreateNewTest(dto, userId);
        }

        //TODO: Use Pagination
        [Authorize(Roles = $"{UserConstants.RoleAdmin}, {UserConstants.RoleTeacher}")]
        [HttpPost("descriptions")]
        public async Task<PaginatedResponse<TestDescriptions>> GetAllDescriptions(PaginationRequest paginationRequest)
        {
            Guid userId = JwtTokenHelpers.GetUserIdFromToken(Request);
            return await _testsService.GetAllDescriptions(paginationRequest, userId);
        }

        [HttpGet("content")]
        public async Task<TestsContent> GetTestContentByDescriptionsImmutableId([FromQuery] Guid testDescriptionImmutableId)
        {
            return await _testsService.GetTestContentByDescriptionsImmutableId(testDescriptionImmutableId);
        }

        [HttpGet("contentbyid")]
        public async Task<TestsContent> GetTestContentByDescriptionsId([FromQuery] int taskDescriptionId)
        {
            return await _testsService.GetTestContentByDescriptionsId(taskDescriptionId);
        }

        [HttpGet("descriptionbyid")]
        public async Task<TestDescriptions> GetTestDescriptionById([FromQuery] int testDescriptionId)
        {
            return await _testsService.GetTestDescriptionById(testDescriptionId);
        }

        [Authorize(Roles = $"{UserConstants.RoleAdmin}, {UserConstants.RoleTeacher}")]
        [HttpPost("update")]
        public async Task Update(UpdateTestDto dto)
        {
            await _testsService.Update(dto);
        }
    }
}