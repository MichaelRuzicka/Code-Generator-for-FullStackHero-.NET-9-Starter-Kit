using [Root_Namespace].Framework.Core.Persistence;
using [Root_Namespace].[Module_Namespace].[Module].Domain;
using [Root_Namespace].[Module_Namespace].[Module].Domain.Exceptions;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace [Root_Namespace].[Module_Namespace].[Module].Application.[EntitySet].Delete.v1;

public sealed class Delete[Entity]Handler(ILogger<Delete[Entity]Handler> logger,[FromKeyedServices([ServiceKey])] IRepository<[Entity]> repository):IRequestHandler<Delete[Entity]Command>
{
    public async Task Handle(Delete[Entity]Command request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        var [Entity_] = await repository.GetByIdAsync(request.Id, cancellationToken);

        _ = [Entity_] ?? throw new [Entity]NotFoundException(request.Id);

        await repository.DeleteAsync([Entity_], cancellationToken);

        logger.LogInformation("[Entity] with id : {[Entity]Id} deleted", [Entity_].Id);
    }
}
