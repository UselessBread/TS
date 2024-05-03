using Common.MassTransit;
using IdentityService.Services;
using MassTransit;

namespace IdentityService.Consumers
{
    public class GetGroupInfoByIdConsumer : IConsumer<GetGroupInfoByIdRequestMessage>
    {
        private readonly IUserService _userService;

        public GetGroupInfoByIdConsumer(IUserService userService)
        {
            _userService = userService;
        }

        public async Task Consume(ConsumeContext<GetGroupInfoByIdRequestMessage> context)
        {
            await context.RespondAsync(await _userService.GetGroupInfoById(context.Message.ImmutableId));
        }
    }

    public class GetGroupInfoByIdConsumerDefinition: ConsumerDefinition<GetGroupInfoByIdConsumer>
    {
        public GetGroupInfoByIdConsumerDefinition() { }

        protected override void ConfigureConsumer(IReceiveEndpointConfigurator endpointConfigurator, IConsumerConfigurator<GetGroupInfoByIdConsumer> consumerConfigurator, IRegistrationContext context)
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
