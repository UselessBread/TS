using Common.Dto;
using Microsoft.AspNetCore.Mvc;
using TS.Data.Contracts.DTO;
using TS.Data.Contracts.Entities;
using TS.Data.Repositories;

namespace TS.Services
{
    public interface ITestsService
    {
        public Task CreateNewTest(CreateNewTestDto dto, Guid userId);
        public Task<PaginatedResponse<TestDescriptions>> GetAllDescriptions(PaginationRequest paginationRequest, Guid userId);
        public Task<TestsContent> GetTestContentByDescriptionsImmutableId([FromQuery] Guid testDescriptionImmutableId);
        public Task<TestsContent> GetTestContentByDescriptionsId([FromQuery] int taskDescriptionId);
        public Task<TestDescriptions> GetTestDescriptionById([FromQuery] int testDescriptionId);
        public Task Update(UpdateTestDto dto);
    }
    public class TestsService : ITestsService
    {
        private readonly ILogger<TestsService> _logger;
        private readonly ITestsContentRepository _testsContentRepository;
        private readonly ITestDescriptionsRepository _testsDescriptionsRepository;

        public TestsService(ILogger<TestsService> logger, ITestsContentRepository testsContentRepository, ITestDescriptionsRepository testsDescriptionsRepository)
        {
            _logger = logger;
            _testsDescriptionsRepository = testsDescriptionsRepository;
            _testsContentRepository = testsContentRepository;
        }


        public async Task CreateNewTest(CreateNewTestDto dto, Guid userId)
        {
            Guid immutableId = await _testsContentRepository.Create(dto.Tasks);
            TestsContent res = await _testsContentRepository.GetByImmutableId(immutableId);
            await _testsDescriptionsRepository.Create(res.Id, res.ImmutableId, dto, userId);
        }

        public async Task<PaginatedResponse<TestDescriptions>> GetAllDescriptions(PaginationRequest paginationRequest, Guid userId)
        {
            return await _testsDescriptionsRepository.GetAllDescriptions(userId, paginationRequest);
        }

        public async Task<TestsContent> GetTestContentByDescriptionsImmutableId([FromQuery] Guid testDescriptionImmutableId)
        {
            var description = await _testsDescriptionsRepository.GetByimmutableId(testDescriptionImmutableId);
            return await _testsContentRepository.GetByImmutableId(description.TestContentImmutableId);
        }

        public async Task<TestsContent> GetTestContentByDescriptionsId([FromQuery] int taskDescriptionId)
        {
            var description = await _testsDescriptionsRepository.GetById(taskDescriptionId);
            return await _testsContentRepository.GetById(description.TestContentId);
        }

        public async Task<TestDescriptions> GetTestDescriptionById([FromQuery] int testDescriptionId)
        {
            return await _testsDescriptionsRepository.GetById(testDescriptionId);
        }

        public async Task Update(UpdateTestDto dto)
        {
            TestDescriptions existingDescription = await _testsDescriptionsRepository.GetByimmutableId(dto.TestDescriptionImmutableId);
            TestsContent exisitingContent = await _testsContentRepository.GetByImmutableId(dto.TestContentImmutableId);
            await _testsContentRepository.Update(exisitingContent, dto.Tasks);
            var updatedContent = await _testsContentRepository.GetByImmutableId(exisitingContent.ImmutableId);
            await _testsDescriptionsRepository.Update(existingDescription, updatedContent.Id, updatedContent.ImmutableId, dto.TestName);
        }
    }
}
