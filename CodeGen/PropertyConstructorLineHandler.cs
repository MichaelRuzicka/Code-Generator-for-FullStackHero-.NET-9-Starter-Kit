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
                if (pInfo.CustomAttributes != null && pInfo.CustomAttributes.Any(a => a.AttributeType.Name == "NullableAttribute" || a.AttributeType.Name == "InversePropertyAttribute"))
                    propertyConstructor += $"{pInfo.PropertyType.ToString().Replace("`1", "").Replace("[", "<").Replace("]", ">?")} {pInfo.Name} = null, {Environment.NewLine}";
                else
                    if (pInfo.PropertyType.Name.Contains("Nullable`1"))
                    propertyConstructor += $"{pInfo.PropertyType.ToString().Replace("System.Nullable`1[", "").Replace("]", "?")} {pInfo.Name}, {Environment.NewLine}";
                else
                    propertyConstructor += $"{pInfo.PropertyType} {pInfo.Name}, {Environment.NewLine}";
            }
            propertyConstructor += optionalPropertys;
            propertyConstructor = propertyConstructor.Substring(0, propertyConstructor.Length - 4);

            templateLine = templateLine.Replace(EnumExtensions.GetEnumValue(TemplateVarsEnum.PropertyConstructor), propertyConstructor);
        }
    }
}

//  System.Nullable`1[System.Decimal] BaseEkNetto, 
//System.Collections.Generic.ICollection`1[SaleSnap.WebApi.Pos.Domain.ProductVariant]? ProductVariants = null, 