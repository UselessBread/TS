using Common.Dto;
using Common.Exceptions;
using Common.Extensions;
using Microsoft.EntityFrameworkCore;
using TS.Data.Contracts.DTO;
using TS.Data.Contracts.Entities;

namespace TS.Data.Repositories
{
    /// <summary>
    /// Repository for TestDescriptions table
    /// </summary>
    public interface ITestDescriptionsRepository
    {
        /// <summary>
        /// Create new TestDescription
        /// </summary>
        /// <param name="contentId">id of the TestContent</param>
        /// <param name="testContentImmutableId">immutable id of the testContent</param>
        /// <param name="dto">additional info about test descripton</param>
        /// <param name="userId">id of the creator</param>
        /// <returns></returns>
        public Task Create(int contentId, Guid testContentImmutableId, CreateNewTestDto dto, Guid userId);

        /// <summary>
        /// Get testDescription by immutableId
        /// </summary>
        /// <param name="immutableId">ImmutableId of the TestDescription</param>
        /// <returns></returns>
        public Task<TestDescriptions> GetByimmutableId(Guid immutableId);

        /// <summary>
        /// Delete TestDescription
        /// </summary>
        /// <param name="entity">Entity to delete</param>
        /// <returns></returns>
        public Task Delete(TestDescriptions entity);

        /// <summary>
        /// Update testDescription
        /// </summary>
        /// <param name="entity">Entity to update</param>
        /// <param name="contentId">id of the testContent</param>
        /// <param name="contentImmutableId">ImmutableId of the TestContent</param>
        /// <param name="testName">Name of the test</param>
        /// <returns></returns>
        public Task Update(TestDescriptions entity, int contentId, Guid contentImmutableId, string testName);

        /// <summary>
        /// Get all TestDescriptions, that were created by a specified user
        /// </summary>
        /// <param name="userId">Creator's id</param>
        /// <param name="paginationRequest">pagination info</param>
        /// <returns>paginated result</returns>
        public Task<PaginatedResponse<TestDescriptions>> GetAllDescriptions(Guid userId, PaginationRequest paginationRequest);

        /// <summary>
        /// get testDescription by id
        /// </summary>
        /// <param name="id">TestDescription's Id</param>
        /// <returns></returns>
        public Task<TestDescriptions> GetById(int id);
    }

    /// <summary>
    /// Repository for TestDescriptions table
    /// </summary>
    public class TestDescriptionsRepository : ITestDescriptionsRepository
    {
        private readonly TestsContext _context;

        public TestDescriptionsRepository(TestsContext context)
        {
            _context = context;
        }

        /// <inheritdoc/>
        public async Task Create(int contentId, Guid testContentImmutableId, CreateNewTestDto dto, Guid userId)
        {
            TestDescriptions testDescription = new TestDescriptions
            {
                Name = dto.TestName,
                TestContentImmutableId = testContentImmutableId,
                TestContentId = contentId,
                ImmutableId = Guid.NewGuid(),
                CreationDate = DateTime.Now.ToUniversalTime(),
                Version = 1,
                CrreatedBy = userId
            };

            _context.TestDescriptions.Add(testDescription);
            await _context.SaveChangesAsync();
        }

        /// <inheritdoc/>
        public async Task<TestDescriptions> GetByimmutableId(Guid immutableId)
        {
            return await _context.TestDescriptions.FirstOrDefaultAsync(d => d.ImmutableId == immutableId
                && d.DeletionDate == null) ??
                    throw new EntityNotFoundException($"No TestDescriptions with ImmutableId = {immutableId} was found");
        }

        /// <inheritdoc/>
        public async Task Delete(TestDescriptions entity)
        {
            entity.DeletionDate = DateTime.Now.ToUniversalTime();
            await _context.SaveChangesAsync();
        }

        /// <inheritdoc/>
        public async Task Update(TestDescriptions entity, int contentId, Guid contentImmutableId, string testName)
        {
            DateTime creatonTime = DateTime.Now.ToUniversalTime();
            entity.DeletionDate = creatonTime;
            
            TestDescriptions createdDescr = new TestDescriptions
            {
                TestContentImmutableId = contentImmutableId,
                TestContentId = contentId,
                CreationDate = creatonTime,
                ImmutableId = entity.ImmutableId,
                Name = testName,
                Version = entity.Version + 1,
                CrreatedBy = entity.CrreatedBy
            };

            _context.TestDescriptions.Add(createdDescr);
            await _context.SaveChangesAsync();
        }

        /// <inheritdoc/>
        public async Task<PaginatedResponse<TestDescriptions>> GetAllDescriptions(Guid userId, PaginationRequest paginationRequest)
        {
            IQueryable<TestDescriptions> resQuery = _context.TestDescriptions
                .Where(d => d.DeletionDate == null && d.CrreatedBy == userId);

            return await resQuery.PaginateResult(paginationRequest);
        }

        /// <inheritdoc/>
        public async Task<TestDescriptions> GetById(int id)
        {
            return await _context.TestDescriptions.FirstOrDefaultAsync(d => d.Id == id) ??
                throw new EntityNotFoundException($"No TestDescriptions with Id = {id} was found");
        }
    }
}
