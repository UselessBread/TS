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
        private readonly IAssignmentRepository _assignmentRepository;
        private readonly IStudentAnswersRepository _studentAnswersRepository;
        private readonly ITAClient _client;

        public TestAssignerService(ITAClient client,
                                   IAssignmentRepository assignmentRepository,
                                   IStudentAnswersRepository studentAnswersRepository)
        {
            _client = client;
            _assignmentRepository = assignmentRepository;
            _studentAnswersRepository = studentAnswersRepository;
        }

        public async Task AssignTest(AssignTestRequestDto dto, Guid userId)
        {
            await _assignmentRepository.AssignTest(dto, userId);
        }

        public async Task<PaginatedResponse<AssisgnedTestResponseDto>> GetAssignedTests(Guid userId, PaginationRequest request)
        {
            List<Guid>? res = await _client.GetGroupsForUser(userId);
            var completedTests = await _studentAnswersRepository.GetCompletedTests(userId);

            return await _assignmentRepository.GetAssignedTests(res, userId, request, completedTests);
        }

        public async Task SaveAnswers(SaveAnswersDto dto, Guid userId)
        {
            await _studentAnswersRepository.SaveAnswers(dto, userId);
        }
    }
}
