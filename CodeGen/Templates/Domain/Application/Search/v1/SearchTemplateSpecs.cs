using Ardalis.Specification;
using [Root_Namespace].Framework.Core.Paging;
using [Root_Namespace].Framework.Core.Specifications;
using [Root_Namespace].[Module_Namespace].[Module].Application.[EntitySet].Get.v1;
using [Root_Namespace].[Module_Namespace].[Module].Domain;

namespace [Root_Namespace].[Module_Namespace].[Module].Application.[EntitySet].Search.v1;
public class Search[Entity]Specs : EntitiesByPaginationFilterSpec<[Entity], [Entity]Response>
{
    public Search[Entity]Specs(Search[EntitySet]Command command)
        : base(command) =>
        Query
            .OrderBy(c => c.Name, !command.HasOrderBy())
            .Where(b => b.Name.Contains(command.Keyword), !string.IsNullOrEmpty(command.Keyword));
}
