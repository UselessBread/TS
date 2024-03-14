using Common.Exceptions;
using Microsoft.EntityFrameworkCore;
using TS.Data.Contracts.DTO;
using TS.Data.Contracts.Entities;

namespace TS.Data.Repositories
{
    /// <summary>
    /// Repository for TestsContent table
    /// </summary>
    public interface ITestsContentRepository
    {
        /// <summary>
        /// Creates new TestContent
        /// </summary>
        /// <param name="dto">tasks description</param>
        /// <returns>ImmutableId of the newly created TestContent</returns>
        public Task<Guid> Create(List<TaskDto> dto);

        /// <summary>
        /// Get actual TestContent by immutable Id
        /// </summary>
        /// <param name="immuutableId">ImmutableId of the TestContent</param>
        /// <returns>Found Entity</returns>
        public Task<TestsContent> GetByImmutableId(Guid immuutableId);

        /// <summary>
        /// Delete specified entuty
        /// </summary>
        /// <param name="entity">Entity to delete</param>
        /// <returns></returns>
        public Task Delete(TestsContent entity);

        /// <summary>
        /// Update Entity
        /// </summary>
        /// <param name="entity">entity to update</param>
        /// <param name="tasks">tasks description</param>
        /// <returns></returns>
        public Task Update(TestsContent entity, List<TaskDto> tasks);

        /// <summary>
        /// Get Entity by Id
        /// </summary>
        /// <param name="id">Id of the entity</param>
        /// <returns>Found entity</returns>
        public Task<TestsContent> GetById(int id);
    }

    /// <summary>
    /// Repository for TestsContent table
    /// </summary>
    public class TestsContentRepository:ITestsContentRepository
    {
        private readonly TestsContext _context;

        public TestsContentRepository(TestsContext context)
        {
            _context = context;
        }

        /// <inheritdoc/>
        public async Task<Guid> Create(List<TaskDto> dto)
        {
            DateTime creatonTime = DateTime.Now.ToUniversalTime();
            Guid immutableId = Guid.NewGuid();
            TestsContent testsContent = new TestsContent()
            {
                Tasks = dto,
                CreationDate = creatonTime,
                ImmutableId = immutableId,
                Version = 1,
            };
            _context.TestsContent.Add(testsContent);
            await _context.SaveChangesAsync();

            return immutableId;
        }

        /// <inheritdoc/>
        public async Task<TestsContent> GetByImmutableId(Guid immuutableId)
        {
            return await _context.TestsContent.FirstOrDefaultAsync(cont => cont.ImmutableId == immuutableId
            && cont.DeletionDate == null) ??
                throw new EntityNotFoundException($"No TestsContent with ImmutableId = {immuutableId} was found");
        }

        /// <inheritdoc/>
        public async Task Delete(TestsContent entity)
        {
            entity.DeletionDate = DateTime.Now.ToUniversalTime();
            await _context.SaveChangesAsync();
        }

        /// <inheritdoc/>
        public async Task Update(TestsContent entity, List<TaskDto> tasks)
        {
            DateTime creatonTime = DateTime.Now.ToUniversalTime();
            entity.DeletionDate = creatonTime;

            TestsContent createdContent = new TestsContent()
            {
                Tasks = tasks,
                CreationDate = creatonTime,
                ImmutableId = entity.ImmutableId,
                Version = entity.Version + 1,
            };

            _context.TestsContent.Add(createdContent);
            await _context.SaveChangesAsync();
        }

        /// <inheritdoc/>
        public async Task<TestsContent> GetById(int id)
        {
            return await _context.TestsContent.FirstOrDefaultAsync(d => d.Id == id) ??
                throw new EntityNotFoundException($"No TestsContent with Id = {id} was found");
        }
    }
}
