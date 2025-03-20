using DevExpress.Xpo;
using System;
using System.Linq;
using System.Reflection;

namespace CodeGen
{
    public class PropertyListingLineHandler : ITemplateLineHandler //        var artikel = Artikel.Create(request.Name!, request.Description);
    {
        public void DoTemplating(BuilderParams Params, ref string templateLine)
        {
            string propertyList = string.Empty;
            foreach (PropertyInfo pInfo in Params.PropertyInfos)
            {


                if (pInfo.CustomAttributes!= null && pInfo.CustomAttributes.Any(a => a.AttributeType.Name == "NullableAttribute" || a.AttributeType.Name == "InversePropertyAttribute"))
                    propertyList += $"\tpublic {pInfo.PropertyType.ToString().Replace("`1","").Replace("[","<").Replace("]",">?")} {pInfo.Name} {{ get; set; }}{Environment.NewLine}";
                else
                       if (pInfo.PropertyType.Name.Contains("Nullable`1"))
                    propertyList += $"\tpublic {pInfo.PropertyType.ToString().Replace("System.Nullable`1[","").Replace("]","?")} {pInfo.Name} {{ get; set; }}{Environment.NewLine}";
                else
                    propertyList += $"\tpublic {pInfo.PropertyType} {pInfo.Name} {{ get; set; }}{Environment.NewLine}";
            }
            templateLine = templateLine.Replace(EnumExtensions.GetEnumValue(TemplateVarsEnum.PropertyListing), propertyList);
        }



        private bool IsNullable<T>(T value)
        {
            return Nullable.GetUnderlyingType(typeof(T)) != null;
        }
    }
}

