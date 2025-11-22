using DevExpress.Xpo;
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
            foreach (PropertyInfo pInfo in Params.PropertyInfos.Where(pi=> !IsOptionalCtorParam(pi))) //Keine Nullable / optionale
                propertyConstructor = ContructorBuilder(propertyConstructor, pInfo);

            foreach (PropertyInfo pInfo in Params.PropertyInfos.Where(pi => IsOptionalCtorParam(pi)))//Nullable / optionale
                propertyConstructor = ContructorBuilder(propertyConstructor, pInfo);

            propertyConstructor += optionalPropertys;
            propertyConstructor = propertyConstructor.Substring(0, propertyConstructor.Length - 4);

            templateLine = templateLine.Replace(EnumExtensions.GetEnumValue(TemplateVarsEnum.PropertyConstructor), propertyConstructor);
        }

        private static string ContructorBuilder(string propertyConstructor, PropertyInfo pInfo)
        {
            if (pInfo.CustomAttributes != null && pInfo.CustomAttributes.Any(a => a.AttributeType.Name == "NullableAttribute" || a.AttributeType.Name == "InversePropertyAttribute"))
                propertyConstructor += $"{pInfo.PropertyType.ToString().Replace("`1", "").Replace("[", "<").Replace("]", ">?")} {pInfo.Name} = null, {Environment.NewLine}";
            else
                if (pInfo.PropertyType.Name.Contains("Nullable`1"))
                propertyConstructor += $"{pInfo.PropertyType.ToString().Replace("System.Nullable`1[", "").Replace("]", "?")} {pInfo.Name}, {Environment.NewLine}";
            else
                propertyConstructor += $"{pInfo.PropertyType} {pInfo.Name}, {Environment.NewLine}";
            return propertyConstructor;
        }

        private static bool IsOptionalCtorParam(PropertyInfo pInfo)
        {
            if (pInfo.CustomAttributes.Any(s => s.AttributeType == typeof(System.Runtime.CompilerServices.NullableAttribute))) //Check Nullable
                return true;
            return false;
        }

    }
}

