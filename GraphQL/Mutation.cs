using System.Threading;
using System.Threading.Tasks;
using CommanderGQL.Commands;
using CommanderGQL.Data;
using CommanderGQL.GraphQL.Platforms;
using CommanderGQL.Models;
using HotChocolate;
using HotChocolate.Data;
using HotChocolate.Subscriptions;

namespace CommanderGQL.GraphQL
{
    public class Mutation
    {
        [UseDbContext(typeof(AppDbContext))]
        public async Task<AddPlatformPayload> AddPlatformAsync(AddplatformInput input, [ScopedService] AppDbContext context, [Service] ITopicEventSender eventSender, CancellationToken cancellationToken)
        {
            var platform = new Platform { Name = input.Name };
            context.Platforms.Add(platform);
            await context.SaveChangesAsync(cancellationToken);
            await eventSender.SendAsync(nameof(Subscription.OnPlatformAdded), platform, cancellationToken);
            return new AddPlatformPayload(platform);
        }

        [UseDbContext(typeof(AppDbContext))]
        public async Task<AddCommandPayload> AddCommandAsync(AddCommandInput input, [ScopedService] AppDbContext context)
        {
            var command = new Command
            {
                HowTo = input.HowTo,
                CommandLine = input.CommandLine,
                PlatformId = input.PlatformId
            };
            context.Commands.Add(command);
            await context.SaveChangesAsync();
            return new AddCommandPayload(command);
        }
    }
}