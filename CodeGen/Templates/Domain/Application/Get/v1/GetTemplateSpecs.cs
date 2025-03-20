using Ardalis.Specification;
using [Root_Namespace].[Module_Namespace].[Module].Domain;

namespace [Root_Namespace].[Module_Namespace].[Module].Application.[EntitySet].Get.v1;

public class Get[Entity]Specs : Specification<[Entity], [Entity]Response>
{
    public Get[Entity]Specs([PrimaryKeyWithDataTypeLowerCase])
    {
    Query
        .Where(p => p.[PrimaryKeyWhere]);
            //.Include(p => p.Brand);
    }
}


