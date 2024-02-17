using Common.Constants;
using Common.Dto;
using Common.Exceptions;
using Microsoft.EntityFrameworkCore;
using Moq;
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
        TestsService _service;
        public TestsRepositoryTests()
        {
            Mock<TestsContext> contextMock = new TestsContextMock().CreateMock();
            TestsContentRepository contentRepos = new TestsContentRepository(contextMock.Object);
            TestDescriptionsRepository DescriptionsRepos = new TestDescriptionsRepository(contextMock.Object);
            _service = new TestsService(null,contentRepos, DescriptionsRepos);
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

        //[Fact]
        //public async Task CreateNewTest_Ok()
        //{
        //    Guid userId = Guid.NewGuid();
        //    CreateNewTestDto idealCreateNewTestDto = new CreateNewTestDto
        //    {
        //        TestName = "Created via test",
        //        Tasks = new List<TaskDto>
        //        {
        //            new TaskDto
        //            {
        //                Answers = new List<string>{"fst","sec","third"},
        //                Position = 0,
        //                RightAnswers = new List<int>{1},
        //                TaskDescription = "Descr",
        //                Type = TestTypes.SingleOption
        //            },
        //            new TaskDto
        //            {
        //                Answers = new List<string>{"fst","sec","third"},
        //                Position = 1,
        //                RightAnswers = new List<int>{1,2},
        //                TaskDescription = "Descr",
        //                Type = TestTypes.MultipleOptions
        //            },
        //            new TaskDto
        //            {
        //                Answers = null,
        //                Position = 2,
        //                RightAnswers = null,
        //                TaskDescription = "Descr",
        //                Type = TestTypes.Text
        //            }
        //        }
        //    };

        //    //await _repos.CreateNewTest(idealCreateNewTestDto, userId);
        //    TestsContent testsContent = new TestsContent()
        //    {
        //        Tasks = idealCreateNewTestDto.Tasks,
        //        CreationDate = DateTime.Now,
        //        ImmutableId = Guid.NewGuid(),
        //        Version = 1,
        //    };
        //    List<TestsContent> testsContents = new List<TestsContent> { testsContent };
        //    Mock<TestsContext> contextmock = new Mock<TestsContext>();
        //    Mock<DbSet<TestsContent>> mock = new Mock<DbSet<TestsContent>>();
        //    contextmock.Setup(x => x.TestsContent.Add(It.IsNotNull<TestsContent>()));
        //    contextmock.Setup(x => x.TestDescriptions.Add(It.IsNotNull<TestDescriptions>()));


        //    Assert.True(true);
        //}
    }
}