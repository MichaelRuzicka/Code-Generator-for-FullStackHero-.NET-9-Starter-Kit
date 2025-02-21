using Ardalis.Specification;
using FSH.Framework.Core.Paging;
using FSH.Framework.Core.Specifications;
using [Module_Namespace].[Module].Application.[EntitySet].Get.v1;
using [Module_Namespace].[Module].Domain;

namespace [Module_Namespace].[Module].Application.[EntitySet].Search.v1;
public class Search[Entity]Specs : EntitiesByPaginationFilterSpec<[Entity], [Entity]Response>
{
    public Search[Entity]Specs(Search[EntitySet]Command command)
        : base(command) =>
        Query
            .OrderBy(c => c.Name, !command.HasOrderBy())
            .Where(b => b.Name.Contains(command.Keyword), !string.IsNullOrEmpty(command.Keyword));
}
