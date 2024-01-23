using Common.Exceptions;
using TS.Data.Contracts.DTO;
using TS.Data.Contracts.Entities;

namespace TS.Data.Repositories
{
    public interface ITestsRepository
    {
        public Task CreateNewTest(CreateNewTestDto dto);
        public Task<List<TestDescriptions>> GetAllDescriptions();

        public Task<TestsContent> GetTestContentByDescriptionsId(Guid testDescriptionImmutableId);
        public Task Update(UpdateTestDto dto);
    }

    public class TestsRepository : ITestsRepository
    {
        private readonly TestsContext _context;

        public TestsRepository(TestsContext context)
        {
            _context = context;
        }

        public async Task CreateNewTest(CreateNewTestDto dto)
        {
            DateTime creatonTime = DateTime.Now.ToUniversalTime();
            
            TestsContent testsContent = new TestsContent()
            {
                Tasks = dto.Tasks,
                CreationDate = creatonTime,
                ImmutableId = Guid.NewGuid(),
                Version = 1,
            };
            _context.TestsContent.Add(testsContent);
            await _context.SaveChangesAsync();

            var foundContent = _context.TestsContent.FirstOrDefault(cont => cont.ImmutableId == testsContent.ImmutableId) ??
                throw new EntityNotFoundException($"No testContent with ImmutableId = {testsContent.ImmutableId} was found");
            int createdContentId = foundContent.Id;
            TestDescriptions testDescription = new TestDescriptions
            {
                Name = dto.TestName,
                TestContentImmutableId = testsContent.ImmutableId,
                TestContentId = createdContentId,
                ImmutableId = Guid.NewGuid(),
                CreationDate = creatonTime,
                Version = 1,
            };

            _context.TestDescriptions.Add(testDescription);
            await _context.SaveChangesAsync();
        }

        public async Task<List<TestDescriptions>> GetAllDescriptions()
        {
            return _context.TestDescriptions.Where(d => d.DeletionDate == null).OrderBy(d => d.Id).ToList();
        }

        //TODO: Get content directly by content ID
        public async Task<TestsContent> GetTestContentByDescriptionsId(Guid testDescriptionImmutableId)
        {
            var res = _context.TestDescriptions.FirstOrDefault(d=>d.ImmutableId == testDescriptionImmutableId && d.DeletionDate == null) ??
                throw new EntityNotFoundException($"No testContent with ImmutableId = {testDescriptionImmutableId} was found");

            return _context.TestsContent.OrderBy(c => c.Id).Last(c => c.ImmutableId == res.TestContentImmutableId);
        }

        public async Task Update(UpdateTestDto dto)
        {
            DateTime creatonTime = DateTime.Now.ToUniversalTime();
            TestDescriptions existingDescription = _context.TestDescriptions.FirstOrDefault(d => d.ImmutableId == dto.TestDescriptionImmutableId
                && d.DeletionDate == null) ??
                    throw new EntityNotFoundException($"No testDescription with ImmutableId = {dto.TestDescriptionImmutableId} was found");

            TestsContent existingContent = _context.TestsContent.FirstOrDefault(d => d.ImmutableId == dto.TestContentImmutableId &&
                d.DeletionDate == null) ??
                    throw new EntityNotFoundException($"No testContent with ImmutableId = {dto.TestContentImmutableId} was found");

            existingDescription.DeletionDate = creatonTime;
            var updatedDescr = _context.TestDescriptions.Update(existingDescription);

            existingContent.DeletionDate = creatonTime;
            _context.TestsContent.Update(existingContent);

            TestsContent createdContent = new TestsContent()
            {
                Tasks = dto.Tasks,
                CreationDate = creatonTime,
                ImmutableId = dto.TestContentImmutableId,
                Version = existingContent.Version + 1,
            };

            _context.TestsContent.Add(createdContent);
            await _context.SaveChangesAsync();

            var foundContent = _context.TestsContent.FirstOrDefault(cont => cont.ImmutableId == dto.TestContentImmutableId &&
            cont.DeletionDate == null) ??
                throw new EntityNotFoundException($"No testContent with ImmutableId = {dto.TestContentImmutableId} was found");
            int res = foundContent.Id;

            TestDescriptions createdDescr = new TestDescriptions
            {
                TestContentImmutableId = createdContent.ImmutableId,
                TestContentId = res,
                CreationDate = creatonTime,
                ImmutableId = dto.TestDescriptionImmutableId,
                Name = dto.TestName,
                Version = existingDescription.Version + 1
            };
            _context.TestDescriptions.Add(createdDescr);
            await _context.SaveChangesAsync();
        }
    }
}
