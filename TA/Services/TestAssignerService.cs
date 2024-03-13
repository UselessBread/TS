using Common.Constants;
using Common.Dto;
using Common.Exceptions;
using IdentityService.Data.Contracts.DTO;
using TA.Data.Contracts.Dto;
using TA.Data.Contracts.Entities;
using TA.Data.Repositories;
using TA.RestClients;

namespace TA.Services
{
    public interface ITestAssignerService
    {
        public Task AssignTest(AssignTestRequestDto dto, Guid userId);
        public Task<PaginatedResponse<AssisgnedTestResponseDto>> GetAssignedTests(Guid userId, PaginationRequest request);
        public Task<PaginatedResponse<TestsForReviewResponseDto>> GetTestDescriptionsForReview(PaginationRequest<Guid> paginationRequest);
        public Task<AssisgnedTestResponseDto> GetAssignmentByImmutableId(Guid immutableId);
        public Task SaveAnswers(SaveAnswersDto dto, Guid userId);
        public Task SaveReview(AssignedTestReviewSaveRequestDto requestDto);
    }

    public class TestAssignerService : ITestAssignerService
    {
        private readonly IAssignmentRepository _assignmentRepository;
        private readonly IStudentAnswersRepository _studentAnswersRepository;
        private readonly IReviewRepository _reviewRepository;
        private readonly ITAClient _client;

        public TestAssignerService(ITAClient client,
                                   IAssignmentRepository assignmentRepository,
                                   IStudentAnswersRepository studentAnswersRepository,
                                   IReviewRepository reviewRepository)
        {
            _client = client;
            _assignmentRepository = assignmentRepository;
            _studentAnswersRepository = studentAnswersRepository;
            _reviewRepository = reviewRepository;
        }

        public async Task SaveReview(AssignedTestReviewSaveRequestDto requestDto)
        {
            await _reviewRepository.SaveReview(requestDto);
            // update state of the answer
            await _studentAnswersRepository.UpdateState(requestDto.StudentAnswerId, StudentAnswerState.Reviewed);
            // if no more answers to review, change state of the assignment
            // find all answers for the assignment
            List<StudentAnswer> res = await _studentAnswersRepository.GetAllByAssignmentImmutableId(requestDto.AssignedTestImmutableId);
            // if they are all reviewed, change state of the assignment
            if (res.All(a => a.StudentAnswerState == StudentAnswerState.Reviewed))
                await _assignmentRepository.ChangeState(requestDto.AssignedTestImmutableId, AssignedTestState.Reviewed);
        }

        public async Task AssignTest(AssignTestRequestDto dto, Guid userId)
        {
            if (dto.GroupImmutableId == null && dto.StudentImmutableId == null)
            {
                throw new BadRequestException("group or student must be assigned");
            }
            await _assignmentRepository.AssignTest(dto, userId);
        }

        public async Task<PaginatedResponse<AssisgnedTestResponseDto>> GetAssignedTests(Guid userId, PaginationRequest request)
        {
            List<Guid>? res = await _client.GetGroupsForUser(userId);
            List<Guid> completedTests = await _studentAnswersRepository.GetCompletedTests(userId);

            return await _assignmentRepository.GetAssignedTests(res, userId, request, completedTests);
        }

        public async Task<PaginatedResponse<TestsForReviewResponseDto>> GetTestDescriptionsForReview(PaginationRequest<Guid> paginationRequest)
        {
            return await _studentAnswersRepository.GetTestDescriptionsForReview(paginationRequest);
        }

        public async Task<AssisgnedTestResponseDto> GetAssignmentByImmutableId(Guid immutableId)
        {
            var res = await _assignmentRepository.GetByImmutableId(immutableId);

            return new AssisgnedTestResponseDto
            {
                AssignedTime = res.AssignedTime,
                AssignmentImmutableId = res.ImmutableId,
                DueTo = res.DueTo,
                State = res.State,
                TeacherId = res.AssignedBy,
                TestDescriptionId = res.TestDescriptionId
            };
        }

        // TODO: Add more checks for answers to be right according to the test types
        public async Task SaveAnswers(SaveAnswersDto dto, Guid userId)
        {
            IOrderedEnumerable<Answer> orederdAnswers = dto.Tasks.OrderBy(a => a.Position);
            int position = -1;
            foreach (var answer in orederdAnswers)
            {
                if (answer.Position == position + 1)
                {
                    position = answer.Position;
                }
                else
                {
                    throw new InvalidContentException("wrong positions");
                }
            }
            await _studentAnswersRepository.SaveAnswers(dto, userId);

            AssignedTests assignment = await _assignmentRepository.GetByImmutableId(dto.AssignedTestImmutableId);

            if (assignment.GroupImmutableId.HasValue)
            {
                GetGroupInfoResponseDto groupInfo = await _client.GetGroupInfoById(assignment.GroupImmutableId.Value);
                if (await _studentAnswersRepository.CheckForAssignmentCompletion(groupInfo.StudentIds, assignment.ImmutableId))
                {
                    await _assignmentRepository.ChangeState(assignment, Common.Constants.AssignedTestState.OnReview);
                }
            }
            else
            {
                await _assignmentRepository.ChangeState(assignment, Common.Constants.AssignedTestState.OnReview);
            }
        }
    }
}
