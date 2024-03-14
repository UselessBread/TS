using Common.Constants;
using Common.Dto;
using Common.Exceptions;
using Common.Extensions;
using Microsoft.EntityFrameworkCore;
using TA.Data.Contracts.Dto;
using TA.Data.Contracts.Entities;

namespace TA.Data.Repositories
{
    /// <summary>
    /// Repository for working with StudentAnswers table
    /// </summary>
    public interface IStudentAnswersRepository
    {
        /// <summary>
        /// Get submitted StudentAnswers for user 
        /// </summary>
        /// <param name="userId">id of the student</param>
        /// <returns>list of immutablle ids of submitted StudentAnswers</returns>
        public Task<List<Guid>> GetCompletedTests(Guid userId);

        /// <summary>
        /// Get student answers by AssignedTestImmutableId
        /// </summary>
        /// <param name="paginationRequest">request with AssignedTestImmutableId</param>
        /// <returns>paginated result of <see cref="TestsForReviewResponseDto"/</returns>
        public Task<PaginatedResponse<TestsForReviewResponseDto>> GetTestDescriptionsForReview(PaginationRequest<Guid> paginationRequest);

        /// <summary>
        /// Save user answers as a StudentAnswer
        /// </summary>
        /// <param name="dto">answers with metadata</param>
        /// <param name="userId">user, who send answers</param>
        /// <returns></returns>
        public Task SaveAnswers(SaveAnswersDto dto, Guid userId);

        /// <summary>
        /// Find out if all users, that have been provided, completed the assignment
        /// </summary>
        /// <param name="userIds">users to check</param>
        /// <param name="assignmentImmutableId">ImmutableId of the assignment</param>
        /// <returns>true if completed</returns>
        public Task<bool> CheckForAssignmentCompletion(List<Guid> userIds, Guid assignmentImmutableId);

        /// <summary>
        /// Update the state of the StudentAnswer
        /// </summary>
        /// <param name="id">Id of the answer</param>
        /// <param name="requiredState">requred state that assignment must have</param>
        /// <returns></returns>
        public Task UpdateState(int id, StudentAnswerState requiredState);

        /// <summary>
        /// Get all StudentAnswers by the assignmentImmutableId
        /// </summary>
        /// <param name="assignmentImmutableId">Immutable id of the assignment</param>
        /// <returns></returns>
        public Task<List<StudentAnswer>> GetAllByAssignmentImmutableId(Guid assignmentImmutableId);

    }

    /// <summary>
    /// Repository for working with StudentAnswers table
    /// </summary>
    public class StudentAnswersRepository : IStudentAnswersRepository
    {
        private readonly AssignedTestsContext _context;
        public StudentAnswersRepository(AssignedTestsContext context)
        {
            _context = context;
        }

        /// <inheritdoc/>
        public async Task UpdateState(int id, StudentAnswerState requiredState)
        {
            StudentAnswer res = _context.StudentAnswers.FirstOrDefault(a => a.Id == id)
                ?? throw new EntityNotFoundException($"StudentAnswer with id = {id} was not found");
            res.StudentAnswerState = requiredState;

            await _context.SaveChangesAsync();
        }

        /// <inheritdoc/>
        public async Task<List<Guid>> GetCompletedTests(Guid userId)
        {
            return await _context.StudentAnswers
                .Where(a => a.UserId == userId)
                .Select(res => res.AssignedTestImmutableId)
                .ToListAsync();
        }

        /// <inheritdoc/>
        public async Task SaveAnswers(SaveAnswersDto dto, Guid userId)
        {
            _context.StudentAnswers.Add(new StudentAnswer
            {
                Answers = dto.Tasks,
                AssignedTestImmutableId = dto.AssignedTestImmutableId,
                UserId = userId,
                StudentAnswerState = StudentAnswerState.OnReview
            });

            await _context.SaveChangesAsync();
        }

        /// <inheritdoc/>
        public async Task<PaginatedResponse<TestsForReviewResponseDto>> GetTestDescriptionsForReview(PaginationRequest<Guid> paginationRequest)
        {
            return await _context.StudentAnswers.Where(a => a.AssignedTestImmutableId == paginationRequest.Request &&
            a.StudentAnswerState == StudentAnswerState.OnReview).Select(a=>new TestsForReviewResponseDto()
            {
                Id = a.Id,
                Answers = a.Answers,
                AssignedTestImmutableId = a.AssignedTestImmutableId,  
                UserId = a.UserId,
            }).PaginateResult(paginationRequest);
        }

        /// <inheritdoc/>
        public async Task<bool> CheckForAssignmentCompletion(List<Guid> userIds, Guid assignmentImmutableId)
        {
            var res = await _context.StudentAnswers.Where(a => userIds.Contains(a.UserId)
                && a.AssignedTestImmutableId == assignmentImmutableId).CountAsync();

            return res == userIds.Count;
        }

        /// <inheritdoc/>
        public async Task<List<StudentAnswer>> GetAllByAssignmentImmutableId(Guid assignmentImmutableId)
        {
            return await _context.StudentAnswers.Where(a => a.AssignedTestImmutableId == assignmentImmutableId).ToListAsync();
        }

    }
}
