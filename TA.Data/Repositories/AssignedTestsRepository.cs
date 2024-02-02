using Common.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TA.Data.Contracts.Dto;
using TA.Data.Contracts.Entities;

namespace TA.Data.Repositories
{
    public interface IAssignedTestsRepository
    {
        public Task AssignTest(AssignTestRequestDto dto);
    }

    public class AssignedTestsRepository : IAssignedTestsRepository
    {
        private readonly AssignedTestsContext _context;
        public AssignedTestsRepository(AssignedTestsContext context)
        {
            _context = context;
        }

        public async Task AssignTest(AssignTestRequestDto dto)
        {
            DateTime currentTime = DateTime.Now.ToUniversalTime();

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
            });

            await _context.SaveChangesAsync();
        }
    }
}
