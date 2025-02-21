using FSH.Framework.Core.Persistence;
using [Module_Namespace].[ModuleName].Domain;
using [Module_Namespace].[ModuleName].Domain.Exceptions;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace [Module_Namespace].[ModuleName].Application.[EntitySet].Delete.v1;

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
