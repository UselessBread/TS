using Moq;
using Moq.EntityFrameworkCore;
using Ts.Tests.Constants;
using TS.Data;
using TS.Data.Contracts.Entities;

namespace Ts.Tests.Mocks
{
    public class TestsContextMock
    {
        
        public Mock<TestsContext> CreateMock()
        {
            List<TestsContent> testsContents =
            [
                TSConstants.OldSingleOptionContent,
                TSConstants.UpdSingleOptionContent,
                TSConstants.MultipleOptionsContent,
                TSConstants.TextOptionContent,
                TSConstants.TextOptionContentByFirstUser,
                TSConstants.TextOptionContentByFirstUserThird
            ];

            List<TestDescriptions> testsDescriptions =
            [
                TSConstants.FirstContextDescription,
                TSConstants.FirstUpdContextDescription,
                TSConstants.SecondContextDescription,
                TSConstants.ThirdContextDescription,
                TSConstants.SecondContextDescriptionByFirstUser,
                TSConstants.ThirdContextDescriptionByFirstUser
            ];

            var testsContextMock = new Mock<TestsContext>();
            testsContextMock
                .Setup(x=>x.TestDescriptions).ReturnsDbSet(testsDescriptions);
            testsContextMock.Setup(x => x.TestsContent).ReturnsDbSet(testsContents);

            return testsContextMock;
        }
    }
}
