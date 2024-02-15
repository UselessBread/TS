using Common.Dto;
using Ts.Tests.Constants;
using Ts.Tests.Mocks;
using TS.Data.Contracts.Entities;
using TS.Data.Repositories;

namespace Ts.Tests
{
    public class TestsRepositoryTests
    {
        private readonly TestsRepository _repos;
        private readonly TestsContextMock _contextMock;
        public TestsRepositoryTests()
        {
            _contextMock = new TestsContextMock();
            _repos = new TestsRepository(_contextMock.CreateMock());
        }

        [Fact]
        public async Task GetTestDescriptionById_Ok()
        {
            TestDescriptions res = await _repos.GetTestDescriptionById(1);

            Assert.True(true);
        }

        [Fact]
        public async Task GetAllDescriptions_moreThanPage_Ok()
        {
            PaginatedResponse<TestDescriptions> res = await _repos.GetAllDescriptions(TSConstants.FirstUserGuid,
                                                                                      new PaginationRequest(1, 10));
            Assert.True(res.AllEntriesCount > 0 && res.Result.Count == res.AllEntriesCount);
            Assert.True(res.Result.All(r => r.CrreatedBy == TSConstants.FirstUserGuid));
        }

        [Fact]
        public async Task GetAllDescriptions_LessthanPage_Ok()
        {
            int pageLength = 2;
            PaginatedResponse<TestDescriptions> res = await _repos.GetAllDescriptions(TSConstants.FirstUserGuid,
                                                                                      new PaginationRequest(1, pageLength));
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
            PaginatedResponse<TestDescriptions> res = await _repos.GetAllDescriptions(TSConstants.FirstUserGuid,
                                                                                      new PaginationRequest(pageCount, pageLength));
            Assert.True(res.AllEntriesCount > 0
                && res.Result.Count < res.AllEntriesCount
                && res.Result.Count < pageCount * pageLength);
            Assert.True(res.Result.All(r => r.CrreatedBy == TSConstants.FirstUserGuid));
        }
    }
}