using FSH.Framework.Core.Paging;
using FSH.Framework.Core.Persistence;
using [Module_Namespace].[Module].Application.[EntitySet].Get.v1;
using [Module_Namespace].[Module].Domain;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace [Module_Namespace].[Module].Application.[EntitySet].Search.v1;

public sealed class Search[EntitySet]Handler([FromKeyedServices("[ServiceKey]")] IReadRepository<[Entity]> repository) : IRequestHandler<Search[EntitySet]Command, PagedList<[Entity]Response>>
{
    public async Task<PagedList<[Entity]Response>> Handle(Search[EntitySet]Command request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        var spec = new Search[Entity]Specs(request);

        var items = await repository.ListAsync(spec, cancellationToken).ConfigureAwait(false);

        var totalCount = await repository.CountAsync(spec, cancellationToken).ConfigureAwait(false);

        return new PagedList<[Entity]Response>(items, request!.PageNumber, request!.PageSize, totalCount);
    }
}
