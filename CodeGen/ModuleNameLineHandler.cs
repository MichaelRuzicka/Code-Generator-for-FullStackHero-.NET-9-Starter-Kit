using System;
using System.Linq;
using System.Runtime.Serialization;

namespace CodeGen
{
    public class ModuleNameLineHandler : ITemplateLineHandler
    {
        public void DoTemplating(BuilderParams Params, ref string templateLine)
        {
            templateLine = templateLine.Replace(EnumExtensions.GetEnumValue(TemplateVarsEnum.Module), Params.ModuleName);
        }
    }
}
//[ModuleName] zb.Pos
//[Module_Namespace]   Default: FSH.Starter.WebApi.Pos
//[EntitySet] Default: Entity Plural zb.Brands
//[Entity]
//[Entity_]
//[Entity_PropertyId]
//[DataType]
//[PropertyName] Default: zb.Description
//[DefaultValue]