using System;
using System.Linq;
using System.Reflection;

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
