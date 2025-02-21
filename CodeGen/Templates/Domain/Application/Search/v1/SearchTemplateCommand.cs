using [Root_Namespace].Framework.Core.Paging;
using [Root_Namespace].[Module_Namespace].[Module].Application.[EntitySet].Get.v1;
using MediatR;

namespace [Root_Namespace].[Module_Namespace].[Module].Application.[EntitySet].Search.v1;

public class Search[EntitySet]Command : PaginationFilter, IRequest<PagedList<[Entity]Response>>
{
[PropertyListing]
}

