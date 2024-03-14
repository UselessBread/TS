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
    /// Repository for working with the AssignedTests table
    /// </summary>
    public interface IAssignmentRepository
    {
        /// <summary>
        /// Assigns test for a gruop or a selected individual
        /// </summary>
        /// <param name="dto">request</param>
        /// <param name="userId">id of the user, who assignes test</param>
        /// <returns></returns>
        public Task AssignTest(AssignTestRequestDto dto, Guid userId);

        /// <summary>
        /// Get all tests, that were assigned by loged-in user
        /// </summary>
        /// <param name="groups">student groups</param>
        /// <param name="userId">concrete single student, if the assignment is assigned for a single student only</param>
        /// <param name="request">info for pagination</param>
        /// <param name="completedTests">completed assignments to filter them off</param>
        /// <returns>paginated results</returns>
        public Task<PaginatedResponse<AssisgnedTestResponseDto>> GetAssignedTests(List<Guid>? groups,
                                                                                  Guid userId,
                                                                                  PaginationRequest request,
                                                                                  List<Guid> completedTests);

        /// <summary>
        /// Get actual Assignment by it's immutab;e id
        /// </summary>
        /// <param name="immutableId">Assignment's immutable id</param>
        /// <returns></returns>
        public Task<AssignedTests> GetByImmutableId(Guid immutableId);

        /// <summary>
        /// Change state of the assignment
        /// </summary>
        /// <param name="entity">assignment's entity</param>
        /// <param name="state">desired state</param>
        /// <returns></returns>
        public Task ChangeState(AssignedTests entity, AssignedTestState state);

        /// <summary>
        /// Change state of the assignment
        /// </summary>
        /// <param name="immutableId">Assignment's immutable id</param>
        /// <param name="state">desired state</param>
        /// <returns></returns>
        public Task ChangeState(Guid immutableId, AssignedTestState state);
    }

    /// <summary>
    /// Repository for working with the AssignedTests table
    /// </summary>
    public class AssignmentRepository : IAssignmentRepository
    {
        private readonly AssignedTestsContext _context;

        public AssignmentRepository(AssignedTestsContext context)
        {
            _context = context;
        }

        /// <inheritdoc/>
        public async Task AssignTest(AssignTestRequestDto dto, Guid userId)
        {
            DateTime currentTime = DateTime.Now.ToUniversalTime();
            if (_context.AssignedTests.Any(t => t.GroupImmutableId == dto.GroupImmutableId
                && t.StudentImmutableId == dto.StudentImmutableId
                && t.TestDescriptionId == dto.TestDescriptionId
                && t.DeletionDate == null))
                throw new BadRequestException("Test has been already assigned to this group");

            _context.AssignedTests.Add(new AssignedTests
            {
                AssignedTime = currentTime,
                CreationDate = currentTime,
                GroupImmutableId = dto.GroupImmutableId,
                ImmutableId = Guid.NewGuid(),
                Version = 1,
                State = AssignedTestState.Assigned,
                StudentImmutableId = dto.StudentImmutableId,
                TestDescriptionId = dto.TestDescriptionId,
                AssignedBy = userId
            });

            await _context.SaveChangesAsync();
        }

        /// <inheritdoc/>
        public async Task<PaginatedResponse<AssisgnedTestResponseDto>> GetAssignedTests(List<Guid>? groups,
                                                                                        Guid userId,
                                                                                        PaginationRequest request,
                                                                                        List<Guid> completedTests)
        {
            IQueryable<AssignedTests> resultQuery = _context.AssignedTests.Where(t => t.DeletionDate == null
            && (t.StudentImmutableId == userId || t.AssignedBy == userId));

            if (groups != null && groups.Any())
            {
                IQueryable<AssignedTests> addition = _context.AssignedTests.Where(t => t.DeletionDate == null && t.GroupImmutableId.HasValue
            && groups.Contains(t.GroupImmutableId.Value));
                resultQuery = resultQuery.Union(addition);
            }

            resultQuery = resultQuery.Where(r => !completedTests.Contains(r.ImmutableId));

            return await resultQuery.Select(r => new AssisgnedTestResponseDto
            {
                AssignedTime = r.AssignedTime,
                DueTo = r.DueTo,
                TestDescriptionId = r.TestDescriptionId,
                TeacherId = r.AssignedBy,
                AssignmentImmutableId = r.ImmutableId,
                State = r.State
            })
                .PaginateResult(request);
        }

        /// <inheritdoc/>
        public async Task<AssignedTests> GetByImmutableId(Guid immutableId)
        {
            return await _context.AssignedTests.FirstOrDefaultAsync(a => a.ImmutableId == immutableId && a.DeletionDate == null)
                ?? throw new EntityNotFoundException($"Cannot find AssignedTests entity with ImmutableId = {immutableId}");
        }

        /// <inheritdoc/>
        public async Task ChangeState(Guid immutableId, AssignedTestState state)
        {
            var res = await GetByImmutableId(immutableId);
            await ChangeState(res, state);
        }

        /// <inheritdoc/>
        public async Task ChangeState(AssignedTests entity, AssignedTestState state)
        {
            DateTime current = DateTime.Now.ToUniversalTime();
            entity.DeletionDate = current;

            AssignedTests updated = new AssignedTests
            {
                AssignedBy = entity.AssignedBy,
                AssignedTime = entity.AssignedTime,
                CreationDate = current,
                DueTo = entity.DueTo,
                TestDescriptionId = entity.TestDescriptionId,
                GroupImmutableId = entity.GroupImmutableId,
                ImmutableId = entity.ImmutableId,
                State = state,
                StudentImmutableId = entity.StudentImmutableId,
                Version = entity.Version + 1
            };

            await _context.AddAsync(updated);
            await _context.SaveChangesAsync();
        }

    }
}
