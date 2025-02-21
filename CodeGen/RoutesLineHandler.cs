using System;
using System.Linq;
using System.Text;

namespace CodeGen
{
    public class RoutesLineHandler : ITemplateLineHandler
    {
        public void DoTemplating(BuilderParams Params, ref string templateLine)
        {
            string entitySetLowerCase = Params.EntitySet.ToLower();
             StringBuilder routes = new StringBuilder();
            routes.Append("var [Entity_]Group = app.MapGroup(\"[EntitySet]\").WithTags(\"[EntitySet]\");");
            routes.Append(Environment.NewLine).Append("\t").Append("\t");

            routes.Append("[Entity_]Group.Map[Entity]CreationEndpoint();");
            routes.Append(Environment.NewLine).Append("\t").Append("\t");

            routes.Append("[Entity_]Group.MapGet[Entity]Endpoint();");
            routes.Append(Environment.NewLine).Append("\t").Append("\t");

            routes.Append("[Entity_]Group.MapGet[Entity]ListEndpoint();");
            routes.Append(Environment.NewLine).Append("\t").Append("\t");

            routes.Append("[Entity_]Group.Map[Entity]UpdateEndpoint();");
            routes.Append(Environment.NewLine).Append("\t").Append("\t");

            routes.Append("[Entity_]Group.Map[Entity]DeleteEndpoint();");
            routes.Append(Environment.NewLine).Append(Environment.NewLine).Append("\t").Append("\t");

            routes.Append("//[Routes]");
            routes.Append(Environment.NewLine).Append(Environment.NewLine);

            routes.Replace("[EntitySet]", entitySetLowerCase);
            routes.Replace("[Entity_]", Params.Entity_);
            routes.Replace("[Entity]", Params.Entity);
            templateLine = templateLine.Replace(EnumExtensions.GetEnumValue(TemplateVarsEnum.Routes), routes.ToString());
        }
    }
}


