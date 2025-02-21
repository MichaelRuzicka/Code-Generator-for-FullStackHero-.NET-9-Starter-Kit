using System;
using System.Linq;

namespace CodeGen
{
    public class EntityLineHandler : ITemplateLineHandler
    {
        public void DoTemplating(BuilderParams Params, ref string templateLine)
        {
            templateLine = templateLine.Replace(EnumExtensions.GetEnumValue(TemplateVarsEnum.Entity), Params.Entity);
        }
    }
}
