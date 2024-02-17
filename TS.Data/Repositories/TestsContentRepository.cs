using Common.Exceptions;
using Microsoft.EntityFrameworkCore;
using TS.Data.Contracts.DTO;
using TS.Data.Contracts.Entities;

namespace TS.Data.Repositories
{
    public interface ITestsContentRepository
    {
        public Task<Guid> Create(List<TaskDto> dto);
        public Task<TestsContent> GetByImmutableId(Guid immuutableId);
        public Task Delete(TestsContent entity);
        public Task Update(TestsContent entity, List<TaskDto> tasks);

        public Task<TestsContent> GetById(int id);
    }

    public class TestsContentRepository:ITestsContentRepository
    {
        private readonly TestsContext _context;

        public TestsContentRepository(TestsContext context)
        {
            _context = context;
        }


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

        public async Task<TestsContent> GetByImmutableId(Guid immuutableId)
        {
            return await _context.TestsContent.FirstOrDefaultAsync(cont => cont.ImmutableId == immuutableId
            && cont.DeletionDate == null) ??
                throw new EntityNotFoundException($"No TestsContent with ImmutableId = {immuutableId} was found");
        }

        public async Task Delete(TestsContent entity)
        {
            entity.DeletionDate = DateTime.Now.ToUniversalTime();
            await _context.SaveChangesAsync();
        }

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

        public async Task<TestsContent> GetById(int id)
        {
            return await _context.TestsContent.FirstOrDefaultAsync(d => d.Id == id) ??
                throw new EntityNotFoundException($"No TestsContent with Id = {id} was found");
        }
    }
}
