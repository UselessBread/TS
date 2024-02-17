using Common.Dto;
using Common.Exceptions;
using Common.Extensions;
using Microsoft.EntityFrameworkCore;
using TS.Data.Contracts.DTO;
using TS.Data.Contracts.Entities;

namespace TS.Data.Repositories
{
    public interface ITestDescriptionsRepository
    {
        public Task Create(int contentId, Guid testContentImmutableId, CreateNewTestDto dto, Guid userId);
        public Task<TestDescriptions> GetByimmutableId(Guid immutableId);
        public Task Delete(TestDescriptions entity);
        public Task Update(TestDescriptions entity, int contentId, Guid contentImmutableId, string testName);
        public Task<PaginatedResponse<TestDescriptions>> GetAllDescriptions(Guid userId, PaginationRequest paginationRequest);
        public Task<TestDescriptions> GetById(int id);
    }

    public class TestDescriptionsRepository : ITestDescriptionsRepository
    {
        private readonly TestsContext _context;

        public TestDescriptionsRepository(TestsContext context)
        {
            _context = context;
        }

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

        public async Task<TestDescriptions> GetByimmutableId(Guid immutableId)
        {
            return await _context.TestDescriptions.FirstOrDefaultAsync(d => d.ImmutableId == immutableId
                && d.DeletionDate == null) ??
                    throw new EntityNotFoundException($"No TestDescriptions with ImmutableId = {immutableId} was found");
        }

        public async Task Delete(TestDescriptions entity)
        {
            entity.DeletionDate = DateTime.Now.ToUniversalTime();
            await _context.SaveChangesAsync();
        }

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

        public async Task<PaginatedResponse<TestDescriptions>> GetAllDescriptions(Guid userId, PaginationRequest paginationRequest)
        {
            IQueryable<TestDescriptions> resQuery = _context.TestDescriptions
                .Where(d => d.DeletionDate == null && d.CrreatedBy == userId);

            return await resQuery.PaginateResult(paginationRequest);
        }

        public async Task<TestDescriptions> GetById(int id)
        {
            return await _context.TestDescriptions.FirstOrDefaultAsync(d => d.Id == id) ??
                throw new EntityNotFoundException($"No TestDescriptions with Id = {id} was found");
        }
    }
}
