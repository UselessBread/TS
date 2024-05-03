using Common.MassTransit;
using IdentityService.Data.Contracts.DTO;
using MassTransit;
using MassTransit.Testing;
using Microsoft.Extensions.DependencyInjection;
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

        public ITestHarness InitMassTransitHarness()
        {
            ServiceProvider provider = new ServiceCollection()
                .AddMassTransitTestHarness(cfg =>
                {
                    cfg.AddHandler<GetGroupInfoByIdRequestMessage>(async ctx =>
                    {
                        if (ctx.Message.ImmutableId == DbMockConstants.FirstGroupImmutableId)
                            await ctx.RespondAsync(new GetGroupInfoResponseDto
                            {
                                GroupName = "",
                                StudentIds = new List<Guid> { DbMockConstants.FirstStudentImmutableId }
                            });
                    });

                    cfg.AddHandler<GetGroupsForUserRequestMessage>(async ctx =>
                    {
                        if (ctx.Message.UserId == DbMockConstants.FirstStudentImmutableId)
                            await ctx.RespondAsync(new GetGroupsForUserResponseMessage
                            {
                                Groups = new List<Guid> { DbMockConstants.FirstGroupImmutableId }
                            });
                        else
                            await ctx.RespondAsync(new GetGroupsForUserResponseMessage
                            {
                                Groups = new List<Guid>()
                            });
                    });
                })
                .BuildServiceProvider(true);

            return provider.GetRequiredService<ITestHarness>();
        }
    }
}
