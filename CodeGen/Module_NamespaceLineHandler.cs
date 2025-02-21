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
