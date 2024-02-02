using TA.Data.Contracts.Dto;
using TA.Data.Repositories;

namespace TA.Services
{
    public interface ITestAssignerService
    {
        public Task AssignTest(AssignTestRequestDto dto);
    }
    public class TestAssignerService : ITestAssignerService
    {
        private readonly IAssignedTestsRepository _repository;

        public TestAssignerService(IAssignedTestsRepository repository)
        {
            _repository = repository;
        }

        public async Task AssignTest(AssignTestRequestDto dto)
        {
            await _repository.AssignTest(dto);
        }
    }
}
