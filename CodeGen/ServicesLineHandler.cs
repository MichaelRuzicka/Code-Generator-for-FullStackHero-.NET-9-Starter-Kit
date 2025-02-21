using System;
using System.Linq;
using System.Text;

namespace CodeGen
{
    public class ServicesLineHandler : ITemplateLineHandler
    {
        public void DoTemplating(BuilderParams Params, ref string templateLine)
        {
            string entitySetLowerCase = Params.EntitySet.ToLower();
            string modulSetLowerCase = Params.ModuleName.ToLower();

            StringBuilder serviceRegistration = new StringBuilder();
            serviceRegistration.Append("builder.Services.AddKeyedScoped<IRepository<[Entity]>, CatalogRepository<[Entity]>>(\"[Module]:[EntitySet]\");");
            serviceRegistration.Append(Environment.NewLine).Append("\t").Append("\t");

            serviceRegistration.Append("builder.Services.AddKeyedScoped<IReadRepository<[Entity]>, CatalogRepository<[Entity]>>(\"[Module]:[EntitySet]\");");
            serviceRegistration.Append(Environment.NewLine).Append(Environment.NewLine).Append("\t").Append("\t");


            serviceRegistration.Append("//[Services]");
            serviceRegistration.Append(Environment.NewLine).Append(Environment.NewLine);

            serviceRegistration.Replace("[EntitySet]", entitySetLowerCase);
            serviceRegistration.Replace("[Module]", modulSetLowerCase);
            serviceRegistration.Replace("[Entity]", Params.Entity);
            templateLine = templateLine.Replace(EnumExtensions.GetEnumValue(TemplateVarsEnum.Services), serviceRegistration.ToString());
        }
    }
}

