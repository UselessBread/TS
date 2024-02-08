using Common.Dto;
using TA.Data.Contracts.Dto;
using TA.Data.Repositories;
using TA.RestClients;

namespace TA.Services
{
    public interface ITestAssignerService
    {
        public Task AssignTest(AssignTestRequestDto dto, Guid userId);
        public Task<PaginatedResponse<AssisgnedTestResponseDto>> GetAssignedTests(Guid userId, PaginationRequest request);
        public Task SaveAnswers(SaveAnswersDto dto, Guid userId);
    }

    public class TestAssignerService : ITestAssignerService
    {
        private readonly IAssignedTestsRepository _repository;
        private readonly ITAClient _client;

        public TestAssignerService(IAssignedTestsRepository repository, ITAClient client)
        {
            _repository = repository;
            _client = client;
        }

        public async Task AssignTest(AssignTestRequestDto dto, Guid userId)
        {
            await _repository.AssignTest(dto, userId);
        }

        public async Task<PaginatedResponse<AssisgnedTestResponseDto>> GetAssignedTests(Guid userId, PaginationRequest request)
        {
            List<Guid>? res = await _client.GetGroupsForUser(userId);

            return await _repository.GetAssignedTests(res, userId, request);
        }

        public async Task SaveAnswers(SaveAnswersDto dto, Guid userId)
        {
            await _repository.SaveAnswers(dto, userId);
        }
    }
}
