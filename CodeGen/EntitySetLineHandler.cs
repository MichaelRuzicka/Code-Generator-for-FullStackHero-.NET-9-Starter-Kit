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
