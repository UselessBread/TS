using Common.MassTransit;
using IdentityService.Services;
using MassTransit;

namespace IdentityService.Consumers
{
    public class GetGroupsForUserConsumer : IConsumer<GetGroupsForUserRequestMessage>
    {
        private readonly IUserService _userService;

        public GetGroupsForUserConsumer(IUserService userService)
        {
            _userService = userService;
        }

        public async Task Consume(ConsumeContext<GetGroupsForUserRequestMessage> context)
        {
            List<Guid> res = await _userService.GetGroupsForUser(context.Message.UserId);
            await context.RespondAsync(new GetGroupsForUserResponseMessage
            {
                Groups = res
            });
        }
    }

    public class GetGroupsForUserConsumerDefinition : ConsumerDefinition<GetGroupsForUserConsumer>
    {
        public GetGroupsForUserConsumerDefinition() { }

        protected override void ConfigureConsumer(IReceiveEndpointConfigurator endpointConfigurator, IConsumerConfigurator<GetGroupsForUserConsumer> consumerConfigurator, IRegistrationContext context)
        {
            if (endpointConfigurator is not IRabbitMqReceiveEndpointConfigurator endpoint)
                throw new Exception();

            endpoint.Durable = true;
            endpoint.AutoDelete = false;
            endpoint.PrefetchCount = 1;
            endpoint.ConcurrentMessageLimit = 1;
        }
    }
}
