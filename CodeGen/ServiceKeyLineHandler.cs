using System;
using System.Linq;

namespace CodeGen
{
    public class ServiceKeyLineHandler : ITemplateLineHandler
    {
        public void DoTemplating(BuilderParams Params, ref string templateLine)
        {
            templateLine = templateLine.Replace(EnumExtensions.GetEnumValue(TemplateVarsEnum.ServiceKey),  $"{Params.ModuleName.ToLower()}:{Params.EntitySet.ToLower()}");
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