using Moq;
using Moq.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TA.Data;
using TA.RestClients;
using TA.Tests.Constants;

namespace TA.Tests.Mocks
{
    public class TaMocks
    {
        public Mock<AssignedTestsContext> CreateDbMock()
        {
            Mock<AssignedTestsContext> mock = new Mock<AssignedTestsContext>();
            mock.Setup(m => m.AssignedTests).ReturnsDbSet(DbMockConstants.AssignedTests);
            mock.Setup(m => m.StudentAnswers).ReturnsDbSet(DbMockConstants.StudentAnswers);
            return mock;
        }

        public Mock<ITAClient> CreateClientMock()
        {
            Mock<ITAClient> clientMock = new Mock<ITAClient>();
            clientMock.Setup(m => m.GetGroupsForUser(It.Is<Guid>(a => a == DbMockConstants.FirstStudentImmutableId)))
                .Returns(Task.FromResult(new List<Guid> { DbMockConstants.FirstGroupImmutableId }));

            clientMock.Setup(m => m.GetGroupInfoById(It.Is<Guid>(a => a == DbMockConstants.FirstGroupImmutableId)))
                .Returns(Task.FromResult(new IdentityService.Data.Contracts.DTO.GetGroupInfoResponseDto
                {
                    GroupName = "",
                    StudentIds = new List<Guid> { DbMockConstants.FirstStudentImmutableId }
                }));

            return clientMock;
        }
    }
}
