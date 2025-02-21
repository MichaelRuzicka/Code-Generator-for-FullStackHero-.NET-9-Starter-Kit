using FSH.Framework.Core.Paging;
using [Module_Namespace].[Module].Application.[EntitySet].Get.v1;
using MediatR;

namespace [Module_Namespace].[Module].Application.[EntitySet].Search.v1;

public class Search[EntitySet]Command : PaginationFilter, IRequest<PagedList<[Entity]Response>>
{
  [PropertyListing]
//public string? Name { get; set; }
//public string? Description { get; set; }
}
