using Common.Constants;
using Common.Dto;
using Common.Exceptions;
using Moq;
using System.Linq.Expressions;
using Ts.Tests.Constants;
using Ts.Tests.Mocks;
using TS.Data;
using TS.Data.Contracts.DTO;
using TS.Data.Contracts.Entities;
using TS.Data.Repositories;
using TS.Services;

namespace Ts.Tests
{
    public class TestsRepositoryTests
    {
        private TestsService _service;
        private TestsService _mockedService;
        private TestsContentRepository _contentRepos;
        private TestDescriptionsRepository _descriptionsRepos;
        private Mock<TestsContext> _contextMock;
        public TestsRepositoryTests()
        {
            _contextMock = new TestsContextMock().CreateMock();
            _contentRepos = new TestsContentRepository(_contextMock.Object);
            _descriptionsRepos = new TestDescriptionsRepository(_contextMock.Object);
            _service = new TestsService(null, _contentRepos, _descriptionsRepos);

            var contentMock = new Mock<ITestsContentRepository>();
            contentMock.Setup(m => m.Create(It.IsAny<List<TaskDto>>()))
                .Returns(Task.FromResult(Guid.NewGuid()));
            contentMock.Setup(m => m.GetByImmutableId(It.IsAny<Guid>()))
                .Returns(Task.FromResult(new TestsContent()));

            var descrMock = new Mock<ITestDescriptionsRepository>();
            _mockedService = new TestsService(null, contentMock.Object, descrMock.Object);
        }

        [Fact]
        public async Task GetTestDescriptionById_Deleted_Ok()
        {
            var res = await _service.GetTestDescriptionById(TSConstants.FirstContextDescription.Id);

            Assert.True(res.Id == TSConstants.FirstContextDescription.Id && res.ImmutableId == TSConstants.FirstContextDescription.ImmutableId);
        }

        [Fact]
        public async Task GetTestDescriptionById_Actual_Ok()
        {
            TestDescriptions res = await _service.GetTestDescriptionById(TSConstants.FirstUpdContextDescription.Id);

            Assert.True(res.Id == TSConstants.FirstUpdContextDescription.Id && res.ImmutableId == TSConstants.FirstUpdContextDescription.ImmutableId);
        }

        [Fact]
        public async Task GetTestDescriptionById_WrongId_ThrowsException()
        {
            await Assert.ThrowsAsync<EntityNotFoundException>(() => _service.GetTestDescriptionById(int.MaxValue));
        }

        [Fact]
        public async Task GetAllDescriptions_moreThanPage_Ok()
        {
            PaginatedResponse<TestDescriptions> res = await _service.GetAllDescriptions(new PaginationRequest(1, 10), TSConstants.FirstUserGuid);
            Assert.True(res.AllEntriesCount > 0 && res.Result.Count == res.AllEntriesCount);
            Assert.True(res.Result.All(r => r.CrreatedBy == TSConstants.FirstUserGuid));
        }

        [Fact]
        public async Task GetAllDescriptions_LessthanPage_Ok()
        {
            int pageLength = 2;
            PaginatedResponse<TestDescriptions> res = await _service.GetAllDescriptions(new PaginationRequest(1, pageLength), TSConstants.FirstUserGuid);
            Assert.True(res.AllEntriesCount > 0
            && res.Result.Count < res.AllEntriesCount
            && res.Result.Count == pageLength);
            Assert.True(res.Result.All(r => r.CrreatedBy == TSConstants.FirstUserGuid));
        }

        [Fact]
        public async Task GetAllDescriptions_SecondAndLastPage_Ok()
        {
            int pageLength = 2;
            int pageCount = 2;
            PaginatedResponse<TestDescriptions> res = await _service.GetAllDescriptions(new PaginationRequest(pageCount, pageLength), TSConstants.FirstUserGuid);
            Assert.True(res.AllEntriesCount > 0
                && res.Result.Count < res.AllEntriesCount
                && res.Result.Count < pageCount * pageLength);
            Assert.True(res.Result.All(r => r.CrreatedBy == TSConstants.FirstUserGuid));
        }

        [Fact]
        public async Task GetTestContentByDescriptionsId_DeletedDescription_Ok()
        {
            TestsContent res = await _service.GetTestContentByDescriptionsId(TSConstants.FirstContextDescription.Id);

            Assert.True(res.Id == TSConstants.UpdSingleOptionContent.Id && res.ImmutableId == TSConstants.UpdSingleOptionContent.ImmutableId);
        }

        [Fact]
        public async Task GetTestContentByDescriptionsId_ActualDescription_Ok()
        {
            TestsContent res = await _service.GetTestContentByDescriptionsId(TSConstants.FirstUpdContextDescription.Id);
            Assert.True(res.Id == TSConstants.UpdSingleOptionContent.Id && res.ImmutableId == TSConstants.UpdSingleOptionContent.ImmutableId);
        }

        [Fact]
        public async Task GetTestContentByDescriptionsImmutableId_DeletedContext_Ok()
        {
            TestsContent res = await _service.GetTestContentByDescriptionsImmutableId(TSConstants.FirstContextDescription.ImmutableId);

            Assert.True(res.Id == TSConstants.UpdSingleOptionContent.Id && res.ImmutableId == TSConstants.UpdSingleOptionContent.ImmutableId);
        }

        [Fact]
        public async Task GetTestContentByDescriptionsImmutableId_ActualContext_Ok()
        {
            TestsContent res = await _service.GetTestContentByDescriptionsImmutableId(TSConstants.FirstUpdContextDescription.ImmutableId);

            Assert.True(res.Id == TSConstants.UpdSingleOptionContent.Id && res.ImmutableId == TSConstants.UpdSingleOptionContent.ImmutableId);
        }

        [Fact]
        public async Task GetTestContentByDescriptionsImmutableId_WrongImmutableId_ThrowsException()
        {
            await Assert.ThrowsAsync<EntityNotFoundException>(() => _service.GetTestContentByDescriptionsImmutableId(Guid.NewGuid()));
        }

        [Fact]
        public async Task CreateNewTest_Ok()
        {
            Guid userId = Guid.NewGuid();
            CreateNewTestDto idealCreateNewTestDto = new CreateNewTestDto
            {
                TestName = "Created via test",
                Tasks = new List<TaskDto>
                {
                    new TaskDto
                    {
                        Answers = new List<string>{"fst","sec","third"},
                        Position = 0,
                        RightAnswers = new List<int>{1},
                        TaskDescription = "Descr",
                        Type = TestTypes.SingleOption
                    },
                    new TaskDto
                    {
                        Answers = new List<string>{"fst","sec","third"},
                        Position = 1,
                        RightAnswers = new List<int>{1,2},
                        TaskDescription = "Descr",
                        Type = TestTypes.MultipleOptions
                    },
                    new TaskDto
                    {
                        Answers = null,
                        Position = 2,
                        RightAnswers = null,
                        TaskDescription = "Descr",
                        Type = TestTypes.Text
                    }
                }
            };

            TestsContent testsContent = new TestsContent()
            {
                Tasks = idealCreateNewTestDto.Tasks,
                CreationDate = DateTime.Now,
                ImmutableId = Guid.NewGuid(),
                Version = 1,
            };

            Guid id = await _contentRepos.Create(idealCreateNewTestDto.Tasks);
            await _descriptionsRepos.Create(1, id, idealCreateNewTestDto, userId);

            Expression<Func<TestsContent, bool>> contentMatcher = arg => arg.Tasks == idealCreateNewTestDto.Tasks
            && arg.Version == 1;
            _contextMock.Verify(m => m.TestsContent.Add(It.Is(contentMatcher)));

            Expression<Func<TestDescriptions, bool>> descriptionMatcher = arg => arg.CrreatedBy == userId
            && arg.TestContentImmutableId == id
            && arg.Version == 1
            && arg.TestContentId == 1
            && arg.Name == idealCreateNewTestDto.TestName;
            _contextMock.Verify(m => m.TestDescriptions.Add(It.Is(descriptionMatcher)));

            Assert.True(true);
        }

        [Fact]
        public async Task TaskDtoValidation_Ok()
        {

            await _mockedService.CreateNewTest(TSConstants.IdealCreateNewTestDto, Guid.NewGuid());
            Assert.True(true);
        }

        [Fact]
        public async Task TaskDtoValidation_WrongPosition_ThrowsException()
        {
            await Assert.ThrowsAsync<InvalidContentException>(() =>
            _mockedService.CreateNewTest(TSConstants.WrongPositionCreateNewTestDto, Guid.NewGuid()));
        }

        [Fact]
        public async Task TaskDtoValidation_HaveRightAnswersInTextOption_ThrowsException()
        {
            await Assert.ThrowsAsync<InvalidContentException>(() =>
            _mockedService.CreateNewTest(TSConstants.HaveRightAnswersInTextCreateNewTestDto, Guid.NewGuid()));
        }

        [Fact]
        public async Task TaskDtoValidation_HaveAnswersInTextOption_ThrowsException()
        {
            await Assert.ThrowsAsync<InvalidContentException>(() =>
            _mockedService.CreateNewTest(TSConstants.HaveAnswersInTextCreateNewTestDto, Guid.NewGuid()));
        }

        [Fact]
        public async Task TaskDtoValidation_DoesNotHaveRightAnsersInSingle_ThrowsException()
        {
            await Assert.ThrowsAsync<InvalidContentException>(() =>
            _mockedService.CreateNewTest(TSConstants.DoesNotHaveRightAnsersInSingleCreateNewTestDto, Guid.NewGuid()));
        }

        [Fact]
        public async Task TaskDtoValidation_DoesNotHaveAnsersInSingle_ThrowsException()
        {
            await Assert.ThrowsAsync<InvalidContentException>(() =>
            _mockedService.CreateNewTest(TSConstants.DoesNotHaveAnsersInSingleCreateNewTestDto, Guid.NewGuid()));
        }

        [Fact]
        public async Task TaskDtoValidation_MultipleRightAnsersInSingle_ThrowsException()
        {
            await Assert.ThrowsAsync<InvalidContentException>(() =>
            _mockedService.CreateNewTest(TSConstants.MultipleRightAnswersSingleCreateNewTestDto, Guid.NewGuid()));
        }

        [Fact]
        public async Task TaskDtoValidation_OutOfRangeRightAnsersInSingle_ThrowsException()
        {
            await Assert.ThrowsAsync<InvalidContentException>(() =>
            _mockedService.CreateNewTest(TSConstants.OutOfRangeRightAnswersSingleCreateNewTestDto, Guid.NewGuid()));
        }

        [Fact]
        public async Task TaskDtoValidation_DoesNotHaveRightAnsersInMult_ThrowsException()
        {
            await Assert.ThrowsAsync<InvalidContentException>(() =>
            _mockedService.CreateNewTest(TSConstants.DoesNotHaveRightAnsersInMultCreateNewTestDto, Guid.NewGuid()));
        }

        [Fact]
        public async Task TaskDtoValidation_DoesNotHaveAnsersInMult_ThrowsException()
        {
            await Assert.ThrowsAsync<InvalidContentException>(() =>
            _mockedService.CreateNewTest(TSConstants.DoesNotHaveAnsersInMultCreateNewTestDto, Guid.NewGuid()));
        }

        [Fact]
        public async Task TaskDtoValidation_OutOfRangeRightAnsersInMult_ThrowsException()
        {
            await Assert.ThrowsAsync<InvalidContentException>(() =>
            _mockedService.CreateNewTest(TSConstants.OutOfRangeRightAnsersInMultCreateNewTestDto, Guid.NewGuid()));
        }
    }
}