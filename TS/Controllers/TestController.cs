using Common.Dto;
using Common.Exceptions;
using Common.Web.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        private readonly ITestsContentRepository _testsContentRepository;
        private readonly ITestDescriptionsRepository _testsDescriptionsRepository;

        public TestController(ILogger<TestController> logger, ITestsContentRepository testsContentRepository, ITestDescriptionsRepository testsDescriptionsRepository)
        {
            _logger = logger;
            _testsDescriptionsRepository = testsDescriptionsRepository;
            _testsContentRepository = testsContentRepository;
        }

        [Authorize (Roles = "Admin, Teacher")]
        [HttpPost("create")]
        public async Task CreateNewTest(CreateNewTestDto dto)
        {
            Guid userId = JwtTokenHelpers.GetUserIdFromToken(Request);
            Guid immutableId = await _testsContentRepository.Create(dto.Tasks);
            TestsContent res = await _testsContentRepository.GetByImmutableId(immutableId);
            await _testsDescriptionsRepository.Create(res.Id, res.ImmutableId, dto, userId);
        }

        //TODO: Use Pagination
        [Authorize(Roles = "Admin, Teacher")]
        [HttpPost("descriptions")]
        public async Task<PaginatedResponse<TestDescriptions>> GetAllDescriptions(PaginationRequest paginationRequest)
        {
            Guid userId = JwtTokenHelpers.GetUserIdFromToken(Request);
            return await _testsDescriptionsRepository.GetAllDescriptions(userId, paginationRequest);
            //return await _testsRepository.GetAllDescriptions(userId, paginationRequest);
        }

        [HttpGet("content")]
        public async Task<TestsContent> GetTestContentByDescriptionsImmutableId([FromQuery] Guid testDescriptionImmutableId)
        {
            var description = await _testsDescriptionsRepository.GetByimmutableId(testDescriptionImmutableId);
            return await _testsContentRepository.GetByImmutableId(description.TestContentImmutableId);
            //return await _testsRepository.GetTestContentByDescriptionsImmutableId(testDescriptionImmutableId);
        }

        [HttpGet("contentbyid")]
        public async Task<TestsContent> GetTestContentByDescriptionsId([FromQuery] int taskDescriptionId)
        {
            var description = await _testsDescriptionsRepository.GetById(taskDescriptionId);
            return await _testsContentRepository.GetById(description.TestContentId);
            //return await _testsRepository.GetTestContentByDescriptionsId(taskDescriptionId);
        }

        [HttpGet("descriptionbyid")]
        public async Task<TestDescriptions> GetTestDescriptionById([FromQuery] int testDescriptionId)
        {
            return await _testsDescriptionsRepository.GetById(testDescriptionId);
            //return await _testsRepository.GetTestDescriptionById(testDescriptionId);
        }

        [Authorize(Roles = "Admin, Teacher")]
        [HttpPost("update")]
        public async Task Update(UpdateTestDto dto)
        {
            TestDescriptions existingDescription = await _testsDescriptionsRepository.GetByimmutableId(dto.TestDescriptionImmutableId);
            TestsContent exisitingContent = await _testsContentRepository.GetByImmutableId(dto.TestContentImmutableId);
            await _testsContentRepository.Update(exisitingContent, dto.Tasks);
            var updatedContent = await _testsContentRepository.GetByImmutableId(exisitingContent.ImmutableId);
            await _testsDescriptionsRepository.Update(existingDescription, updatedContent.Id, updatedContent.ImmutableId, dto.TestName);
            
            //await _testsRepository.Update(dto);
        }
    }
}