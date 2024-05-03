using Common.Dto;
using Common.Exceptions;
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
            ReviewRepository reviewRepository = new ReviewRepository(obj);
            _service = new TestAssignerService(assignmentRepository, studentAnswersRepository, reviewRepository, null,null);
        }

        [Fact]
        public async Task GetAssignedTests_Ok()
        {
            PaginatedResponse<AssisgnedTestResponseDto> res = await _service.GetAssignedTests(DbMockConstants.FirstStudentImmutableId, new PaginationRequest(1, 10));
            Assert.True(res.Result.Count == 2);
            Assert.True(res.Result.Any(r =>
                r.AssignmentImmutableId == DbMockConstants.second.ImmutableId) &&
                res.Result.Any(r => r.AssignmentImmutableId == DbMockConstants.fourth.ImmutableId));
        }

        [Fact]
        public async Task GetAssignedTests_WrongStudent_Ok()
        {
            PaginatedResponse<AssisgnedTestResponseDto> res = await _service.GetAssignedTests(Guid.NewGuid(), new PaginationRequest(1, 10));
            Assert.True(res.Result.Count == 0);
        }

        [Fact]
        public async Task AssignTest_AlreadyAssignedTestToGroup_TrowsException()
        {
            var dto = new AssignTestRequestDto
            {
                GroupImmutableId = DbMockConstants.FirstGroupImmutableId,
                TestDescriptionId = 1
            };


            await Assert.ThrowsAsync<BadRequestException>(() => _service.AssignTest(dto, DbMockConstants.FirstTeacherImmutableId));
        }

        [Fact]
        public async Task AssignTest_NoGroupOrUser_TrowsException()
        {
            var dto = new AssignTestRequestDto
            {
                TestDescriptionId = 10
            };


            await Assert.ThrowsAsync<BadRequestException>(() => _service.AssignTest(dto, DbMockConstants.FirstTeacherImmutableId));
        }

        [Fact]
        public async Task AssignTest_Ok()
        {
            await _service.AssignTest(new AssignTestRequestDto
            {
                GroupImmutableId = DbMockConstants.FirstGroupImmutableId,
                TestDescriptionId = 10
            }, DbMockConstants.FirstTeacherImmutableId);

            Assert.True(true);
        }

        [Fact]
        public async Task SaveAnswers_Ok()
        {
            await _service.SaveAnswers(new SaveAnswersDto
            {
                AssignedTestImmutableId = DbMockConstants.second.ImmutableId,
                Tasks = new List<Answer>
                {
                    new Answer
                    {
                        Position = 0,
                        Answers = new List<string>{"1", "2", "3"}
                    }
                }
            }, Guid.NewGuid());

            Assert.True(true);
        }

        [Fact]
        public async Task SaveAnswers_WrongPosition_ThrowsException()
        {
            var dto = new SaveAnswersDto
            {
                AssignedTestImmutableId = DbMockConstants.second.ImmutableId,
                Tasks = new List<Answer>
                {
                    new Answer
                    {
                        Position = 0,
                        Answers = new List<string>{"1", "2", "3"}
                    },
                    new Answer
                    {
                        Position = 2,
                        Answers = new List<string>{"1", "2", "3"}
                    }
                }
            };

            await Assert.ThrowsAsync<Common.Exceptions.InvalidContentException>(() => _service.SaveAnswers(dto, Guid.NewGuid()));

            
        }
    }
}