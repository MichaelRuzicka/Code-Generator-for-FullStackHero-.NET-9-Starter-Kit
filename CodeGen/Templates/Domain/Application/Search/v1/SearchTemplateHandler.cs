using [Root_Namespace].Framework.Core.Paging;
using [Root_Namespace].Framework.Core.Persistence;
using [Root_Namespace].[Module_Namespace].[Module].Application.[EntitySet].Get.v1;
using [Root_Namespace].[Module_Namespace].[Module].Domain;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace [Root_Namespace].[Module_Namespace].[Module].Application.[EntitySet].Search.v1;

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
