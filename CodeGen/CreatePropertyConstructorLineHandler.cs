using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;

namespace CodeGen
{
    public class CreatePropertyConstructorLineHandler : ITemplateLineHandler //        var artikel = Artikel.Create(request.Name!, request.Description);
    {
        public void DoTemplating(BuilderParams Params, ref string templateLine)
        {
            string propertyConstructor = string.Empty;
            string optionalPropertys = string.Empty;
            foreach (PropertyInfo pInfo in Params.PropertyInfos)
            {
                if (Attribute.IsDefined(pInfo, typeof(ForeignKeyAttribute)))
                    continue; //Skip Reverse Navigation in Commands
                var isnullable = IsNullable(pInfo);
                if (IsNullable(pInfo))
                    if (pInfo.PropertyType.ToString().Contains("Nullable`1"))
                        propertyConstructor += $"{pInfo.PropertyType.GenericTypeArguments.First().FullName}? {pInfo.Name}, {Environment.NewLine}";
                    else
                        if (pInfo.PropertyType.IsArray)
                        propertyConstructor += $"{pInfo.PropertyType.ToString().Replace("`1", "")}? {pInfo.Name}, {Environment.NewLine}";
                    else
                        propertyConstructor += $"{pInfo.PropertyType.ToString().Replace("`1", "").Replace("[", "<").Replace("]", ">?")}? {pInfo.Name}, {Environment.NewLine}";
                else
                    if (pInfo.PropertyType.IsGenericType)
                    propertyConstructor += $"{pInfo.PropertyType.ToString().Replace("`1", "").Replace("[", "<").Replace("]", ">?")} {pInfo.Name}, {Environment.NewLine}";
                else
                    propertyConstructor += $"{pInfo.PropertyType} {pInfo.Name}, {Environment.NewLine}";
            }
            propertyConstructor += optionalPropertys;
            propertyConstructor = propertyConstructor.Substring(0, propertyConstructor.Length - 4);

            templateLine = templateLine.Replace(EnumExtensions.GetEnumValue(TemplateVarsEnum.CreatePropertyConstructor), propertyConstructor);
        }


        private static bool IsNullable(PropertyInfo property)
        {
            NullabilityInfoContext nullabilityInfoContext = new NullabilityInfoContext();
            var info = nullabilityInfoContext.Create(property);
            if (info.WriteState == NullabilityState.Nullable || info.ReadState == NullabilityState.Nullable)
                 return true;
            return false;
        }
    }


}