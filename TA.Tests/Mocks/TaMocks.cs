using Moq;
using Moq.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TA.Data;
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
    }
}
