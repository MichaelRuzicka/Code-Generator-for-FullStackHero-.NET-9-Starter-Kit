using [Root_Namespace].[Module_Namespace].[Module].Domain.Events;
using MediatR;
using Microsoft.Extensions.Logging;

namespace [Root_Namespace].[Module_Namespace].[Module].Application.[EntitySet].EventHandlers;

public class [Entity]CreatedEventHandler(ILogger<[Entity]CreatedEventHandler> logger) : INotificationHandler<[Entity]Created>
{
    public async Task Handle([Entity]Created notification, CancellationToken cancellationToken)
    {
        logger.LogInformation("handling [Entity_] created domain event..");
        await Task.FromResult(notification);
        logger.LogInformation("finished handling [Entity_] created domain event..");
    }
}
