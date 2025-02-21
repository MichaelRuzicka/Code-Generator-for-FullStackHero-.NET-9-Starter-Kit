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
                if (pInfo.CustomAttributes!= null && pInfo.CustomAttributes.Any(a => a.AttributeType.Name == "NullableAttribute"))
                    propertyList += $"\tpublic {pInfo.PropertyType}? {pInfo.Name} {{ get; set; }}{Environment.NewLine}";
                else
                    propertyList += $"\tpublic {pInfo.PropertyType} {pInfo.Name} {{ get; set; }}{Environment.NewLine}";
            }
            //propertyList = propertyList.Substring(0, propertyList.Length - 2);

            templateLine = templateLine.Replace(EnumExtensions.GetEnumValue(TemplateVarsEnum.PropertyListing), propertyList);
        }
    }
}

