using System;
using System.Linq;

namespace CodeGen
{
    public class Root_NamespaceLineHandler : ITemplateLineHandler
    {

        public void DoTemplating(BuilderParams builderparams, ref string templateLine)
        {
            templateLine = templateLine.Replace(EnumExtensions.GetEnumValue(TemplateVarsEnum.Root_Namespace), builderparams.Root_Namespace);
        }
    }
}
