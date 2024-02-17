using Common.Dto;
using Moq;
using TA.Data;
using TA.Data.Contracts.Dto;
using TA.Data.Repositories;
using TA.Services;
using TA.Tests.Constants;
using TA.Tests.Mocks;

namespace TA.Tests
{
    public class TaTests
    {
        private readonly TestAssignerService _service;

        public TaTests()
        {
            TaMocks mocks = new TaMocks();
            Mock<AssignedTestsContext> mock = mocks.CreateDbMock();
            AssignedTestsContext obj = mock.Object;
            AssignmentRepository assignmentRepository = new AssignmentRepository(obj);
            StudentAnswersRepository studentAnswersRepository = new StudentAnswersRepository(obj);
            _service = new TestAssignerService(mocks.CreateClientMock().Object, assignmentRepository, studentAnswersRepository);
        }

        [Fact]
        public async Task GetAssignedTests_Ok()
        {
            PaginatedResponse<AssisgnedTestResponseDto> res = await _service.GetAssignedTests(DbMockConstants.FirstStudentImmutableId, new PaginationRequest(1, 10));

            Assert.True(true);
        }
    }
}