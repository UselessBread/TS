using Common.Dto;
using Common.Exceptions;
using Microsoft.AspNetCore.Mvc;
using TS.Data.Contracts.DTO;
using TS.Data.Contracts.Entities;
using TS.Data.Repositories;

namespace TS.Services
{
    /// <summary>
    /// Basic service for controller
    /// </summary>
    public interface ITestsService
    {
        /// <summary>
        /// Creates new test
        /// </summary>
        /// <param name="dto">dto with test's description</param>
        /// <param name="userId">Id of the creator</param>
        /// <returns><see cref="TestDescriptions"/>Description of the newly created Test</returns>
        public Task<TestDescriptions> CreateNewTest(CreateNewTestDto dto, Guid userId);

        /// <summary>
        /// Get all TestDescriptions, created by specified user
        /// </summary>
        /// <param name="paginationRequest">pagination info</param>
        /// <param name="userId">Id of the creator</param>
        /// <returns>paginated response</returns>
        public Task<PaginatedResponse<TestDescriptions>> GetAllDescriptions(PaginationRequest paginationRequest, Guid userId);

        /// <summary>
        /// Get TestContent by ImmutableId of the TestDescription
        /// </summary>
        /// <param name="testDescriptionImmutableId">ImmutableId of the TestDescription</param>
        /// <returns>found TestContent</returns>
        public Task<TestsContent> GetTestContentByDescriptionsImmutableId([FromQuery] Guid testDescriptionImmutableId);

        /// <summary>
        /// Get TestContent by Id of the TestDescription
        /// </summary>
        /// <param name="taskDescriptionId">Id of the TestDescription</param>
        /// <returns>Found TestContent</returns>
        public Task<TestsContent> GetTestContentByDescriptionsId([FromQuery] int taskDescriptionId);

        /// <summary>
        /// Get TestDescription by Id
        /// </summary>
        /// <param name="testDescriptionId">Id of the TestDescription</param>
        /// <returns>Found TestDescription</returns>
        public Task<TestDescriptions> GetTestDescriptionById([FromQuery] int testDescriptionId);

        /// <summary>
        /// Update whole test (description and content)
        /// </summary>
        /// <param name="dto">New information to update</param>
        /// <returns></returns>
        public Task Update(UpdateTestDto dto);
    }

    /// <summary>
    /// Basic service for controller
    /// </summary>
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


        /// <inheritdoc/>
        public async Task<TestDescriptions> CreateNewTest(CreateNewTestDto dto, Guid userId)
        {
            ValidateTasks(dto.Tasks);
            Guid immutableId = await _testsContentRepository.Create(dto.Tasks);
            TestsContent res = await _testsContentRepository.GetByImmutableId(immutableId);
            Guid testDescriptionGuid = await _testsDescriptionsRepository.Create(res.Id, res.ImmutableId, dto, userId);
            return await _testsDescriptionsRepository.GetByimmutableId(testDescriptionGuid);
        }

        /// <inheritdoc/>
        public async Task<PaginatedResponse<TestDescriptions>> GetAllDescriptions(PaginationRequest paginationRequest, Guid userId)
        {
            return await _testsDescriptionsRepository.GetAllDescriptions(userId, paginationRequest);
        }

        /// <inheritdoc/>
        public async Task<TestsContent> GetTestContentByDescriptionsImmutableId([FromQuery] Guid testDescriptionImmutableId)
        {
            var description = await _testsDescriptionsRepository.GetByimmutableId(testDescriptionImmutableId);
            return await _testsContentRepository.GetByImmutableId(description.TestContentImmutableId);
        }

        /// <inheritdoc/>
        public async Task<TestsContent> GetTestContentByDescriptionsId([FromQuery] int taskDescriptionId)
        {
            var description = await _testsDescriptionsRepository.GetById(taskDescriptionId);
            return await _testsContentRepository.GetById(description.TestContentId);
        }

        /// <inheritdoc/>
        public async Task<TestDescriptions> GetTestDescriptionById([FromQuery] int testDescriptionId)
        {
            return await _testsDescriptionsRepository.GetById(testDescriptionId);
        }

        /// <inheritdoc/>
        public async Task Update(UpdateTestDto dto)
        {
            ValidateTasks(dto.Tasks);
            TestDescriptions existingDescription = await _testsDescriptionsRepository.GetByimmutableId(dto.TestDescriptionImmutableId);
            TestsContent exisitingContent = await _testsContentRepository.GetByImmutableId(dto.TestContentImmutableId);
            await _testsContentRepository.Update(exisitingContent, dto.Tasks);
            var updatedContent = await _testsContentRepository.GetByImmutableId(exisitingContent.ImmutableId);
            await _testsDescriptionsRepository.Update(existingDescription, updatedContent.Id, updatedContent.ImmutableId, dto.TestName);
        }

        /// <summary>
        /// Validates Tasks of the created/updated test
        /// </summary>
        /// <param name="tasks">tasks to validate</param>
        /// <exception cref="InvalidContentException">if position is not incremented by 1 or if metadata for tasks is wrong</exception>
        private void ValidateTasks(List<TaskDto> tasks)
        {
            var orderedTasks = tasks.OrderBy(t => t.Position);
            int prevPosition = -1;
            foreach (var task in orderedTasks)
            {
                if (task.Position != prevPosition + 1)
                    throw new InvalidContentException("invalid task positions");

                prevPosition = task.Position;

                switch (task.Type)
                {
                    case Common.Constants.TestTypes.Text:
                        if (task.RightAnswers != null || task.Answers != null)
                            throw new InvalidContentException($"invalid data content for the task with the type == {task.Type}");
                        break;
                    case Common.Constants.TestTypes.SingleOption:
                        if ((task.RightAnswers == null || task.Answers == null) || (task.RightAnswers.Count != 1 && task.Answers.Count != 1))
                            throw new InvalidContentException($"invalid data content for the task with the type == {task.Type}");

                        if (task.RightAnswers.First() >= task.Answers.Count)
                            throw new InvalidContentException($"invalid data content for the task with the type == {task.Type}");

                        break;
                    case Common.Constants.TestTypes.MultipleOptions:
                        if ((task.RightAnswers == null || task.Answers == null) || (task.RightAnswers.Count == 0 && task.Answers.Count == 0))
                            throw new InvalidContentException($"invalid data content for the task with the type == {task.Type}");

                        foreach (int rightAnswer in task.RightAnswers)
                        {
                            if (rightAnswer >= task.Answers.Count)
                                throw new InvalidContentException($"invalid data content for the task with the type == {task.Type}");
                        }
                        break;
                }
            }
        }
    }
}
