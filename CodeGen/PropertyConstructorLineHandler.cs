using System;
using System.Linq;
using System.Reflection;

namespace CodeGen
{
    public class PropertyConstructorLineHandler : ITemplateLineHandler //        var artikel = Artikel.Create(request.Name!, request.Description);
    {
        public void DoTemplating(BuilderParams Params, ref string templateLine)
        {
            string propertyConstructor = string.Empty;
            string optionalPropertys = string.Empty;
            foreach (PropertyInfo pInfo in Params.PropertyInfos)
            {
                if (pInfo.CustomAttributes != null && pInfo.CustomAttributes.Any(a => a.AttributeType.Name == "NullableAttribute"))
                {
                   optionalPropertys =  $"{pInfo.PropertyType}? {pInfo.Name} = null, {Environment.NewLine}";
                }
                else
                    propertyConstructor += $"{pInfo.PropertyType} {pInfo.Name}, {Environment.NewLine}";
            }
            propertyConstructor +=optionalPropertys;
            propertyConstructor = propertyConstructor.Substring(0, propertyConstructor.Length - 4);

            templateLine = templateLine.Replace(EnumExtensions.GetEnumValue(TemplateVarsEnum.PropertyConstructor), propertyConstructor);
        }
    }
}

//  [property: DefaultValue("Your default value"] string? Description = null