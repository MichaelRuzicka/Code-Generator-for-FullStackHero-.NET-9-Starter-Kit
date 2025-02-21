using System;
using System.Linq;

namespace CodeGen
{
    public class EntitySetLineHandler : ITemplateLineHandler
    {
        public void DoTemplating(BuilderParams Params, ref string templateLine)
        {
            templateLine = templateLine.Replace(EnumExtensions.GetEnumValue(TemplateVarsEnum.EntitySet), Params.EntitySet);
        }
    }
}
//[ModuleName] zb.Catalog
//[Module_Namespace]   Default: FSH.Starter.WebApi.Catalog
//[EntitySet] Default: Entity Plural zb.Brands
//[Entity]
//[Entity_]
//[Entity_PropertyId]
//[DataType]
//[PropertyName] Default: zb.Description
//[DefaultValue]