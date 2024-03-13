using Common.Constants;
using Common.Dto;
using Common.Exceptions;
using Common.Extensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TA.Data.Contracts.Dto;
using TA.Data.Contracts.Entities;

namespace TA.Data.Repositories
{
    public interface IStudentAnswersRepository
    {
        public Task<List<Guid>> GetCompletedTests(Guid userId);
        public Task<PaginatedResponse<TestsForReviewResponseDto>> GetTestDescriptionsForReview(PaginationRequest<Guid> paginationRequest);
        public Task SaveAnswers(SaveAnswersDto dto, Guid userId);
        public Task<bool> CheckForAssignmentCompletion(List<Guid> userIds, Guid assignmentImmutableId);
        public Task UpdateState(int id, StudentAnswerState requiredState);
        public Task<List<StudentAnswer>> GetAllByAssignmentImmutableId(Guid assignmentImmutableId);

    }
    public class StudentAnswersRepository : IStudentAnswersRepository
    {
        private readonly AssignedTestsContext _context;
        public StudentAnswersRepository(AssignedTestsContext context)
        {
            _context = context;
        }

        public async Task UpdateState(int id, StudentAnswerState requiredState)
        {
            StudentAnswer res = _context.StudentAnswers.FirstOrDefault(a => a.Id == id)
                ?? throw new EntityNotFoundException($"StudentAnswer with id = {id} was not found");
            res.StudentAnswerState = requiredState;

            await _context.SaveChangesAsync();
        }
        public async Task<List<Guid>> GetCompletedTests(Guid userId)
        {
            return await _context.StudentAnswers
                .Where(a => a.UserId == userId)
                .Select(res => res.AssignedTestImmutableId)
                .ToListAsync();
        }

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

        public async Task<bool> CheckForAssignmentCompletion(List<Guid> userIds, Guid assignmentImmutableId)
        {
            var res = await _context.StudentAnswers.Where(a => userIds.Contains(a.UserId)
                && a.AssignedTestImmutableId == assignmentImmutableId).CountAsync();

            return res == userIds.Count;
        }

        public async Task<List<StudentAnswer>> GetAllByAssignmentImmutableId(Guid assignmentImmutableId)
        {
            return await _context.StudentAnswers.Where(a => a.AssignedTestImmutableId == assignmentImmutableId).ToListAsync();
        }

    }
}
