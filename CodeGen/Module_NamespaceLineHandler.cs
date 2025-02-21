using System;
using System.Linq;

namespace CodeGen
{
    public class Module_NamespaceLineHandler : ITemplateLineHandler
    {

        public void DoTemplating(BuilderParams builderparams, ref string templateLine)
        {
            templateLine = templateLine.Replace(EnumExtensions.GetEnumValue(TemplateVarsEnum.Module_Namespace), builderparams.Module_Namespace);
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