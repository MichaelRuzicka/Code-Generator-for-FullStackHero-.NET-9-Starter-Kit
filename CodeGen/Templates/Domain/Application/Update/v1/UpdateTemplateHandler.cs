using [Root_Namespace].Framework.Core.Persistence;
using [Root_Namespace].[Module_Namespace].[Module].Domain;
using [Root_Namespace].[Module_Namespace].[Module].Domain.Exceptions;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace [Root_Namespace].[Module_Namespace].[Module].Application.[EntitySet].Update.v1;

public sealed class Update[Entity]Handler(ILogger<Update[Entity]Handler> logger, [FromKeyedServices("[ServiceKey]")] IRepository<[Entity]> repository) : IRequestHandler<Update[Entity]Command, Update[Entity]Response>
{
    public async Task<Update[Entity]Response> Handle(Update[Entity]Command request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        var [Entity_] = await repository.GetByIdAsync(request.Id, cancellationToken);

        _ = [Entity_] ?? throw new [Entity]NotFoundException(request.Id);

        var updated[Entity] = [Entity_].Update([RequestFields]);

        await repository.UpdateAsync(updated[Entity], cancellationToken);

        logger.LogInformation("[Entity] with id : {[Entity]Id} updated.", [Entity_].Id);

        return new Update[Entity]Response([Entity_].Id);
    }
}
