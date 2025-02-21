using FSH.Framework.Core.Persistence;
using [Module_Namespace].[ModuleName].Application.[EntitySet].Create.v1;
using [Module_Namespace].[ModuleName].Domain;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace [Module_Namespace].[ModuleName].Application.[EntitySet].Create.v1;
public sealed class CreateArtikelHandler(ILogger<CreateArtikelHandler> logger, [FromKeyedServices("[ServiceKey]")] IRepository<[Entity]> repository) 
: IRequestHandler<Create[Entity]Command, Create[Entity]Response>
{
    public async Task<Create[Entity]Response> Handle(Create[Entity]Command request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);
        var [Entity_] = [Entity].Create([RequestFields]);
		
        await repository.AddAsync([Entity_], cancellationToken);
        logger.LogInformation("[Entity_] created {EntityId}", [Entity_PropertyId]);
        return new Create[Entity]Response([Entity_PropertyId]);
    }
}
