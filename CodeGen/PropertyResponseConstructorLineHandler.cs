using DevExpress.CodeParser;
using DevExpress.Xpo;
using System;
using System.DirectoryServices.ActiveDirectory;
using System.Linq;
using System.Reflection;

namespace CodeGen
{
    public class PropertyResponseConstructorLineHandler : ITemplateLineHandler //        var artikel = Artikel.Create(request.Name!, request.Description);
    {
        public void DoTemplating(BuilderParams Params, ref string templateLine)
        {
            string propertyConstructor = string.Empty;
            string optionalPropertys = string.Empty;
            foreach (PropertyInfo pInfo in Params.PropertyInfos.Where(pi => !IsOptionalCtorParam(pi))) //Keine Nullable / optionale
                propertyConstructor = ContructorBuilder(propertyConstructor, pInfo, Params);

            foreach (PropertyInfo pInfo in Params.PropertyInfos.Where(pi => IsOptionalCtorParam(pi)))//Nullable / optionale
                propertyConstructor = ContructorBuilder(propertyConstructor, pInfo, Params);

            propertyConstructor += optionalPropertys;
            propertyConstructor = propertyConstructor.Substring(0, propertyConstructor.Length - 4);

            templateLine = templateLine.Replace(EnumExtensions.GetEnumValue(TemplateVarsEnum.PropertyResponseConstructor), propertyConstructor);
            
        }

        private static string ContructorBuilder(string propertyConstructor, PropertyInfo pInfo, BuilderParams Params)
        {

            if (pInfo.ToString().Contains("SaleSnap.WebApi.Pos.Domain"))//Check if Navigation Data Type Property of Response Template
            {

                if (pInfo.CustomAttributes != null && pInfo.CustomAttributes.Any(a => a.AttributeType.Name == "NullableAttribute" || a.AttributeType.Name == "InversePropertyAttribute"))
                {
                    var navPropOrg = $"{pInfo.PropertyType.ToString().Replace("`1", "").Replace("[", "<").Replace("]", ">?")} {pInfo.Name} = null, {Environment.NewLine}";
                    if (pInfo.PropertyType.GenericTypeArguments.Count() > 0)//ICollection?
                    {
                        string singularTypeName = pInfo.PropertyType.GenericTypeArguments.First().Name;
                        navPropOrg = navPropOrg.Replace($"{singularTypeName}>", $"{singularTypeName}Response>");
                        navPropOrg = navPropOrg.Replace("SaleSnap.WebApi.Pos.Domain", $"{singularTypeName}{Params.PluralEx}.Get.v1"); //Return Navigation as Response Datatype
                    }
                    else
                    {
                        navPropOrg = navPropOrg.Replace($"SaleSnap.WebApi.Pos.Domain.{pInfo.PropertyType.Name}",$"{pInfo.PropertyType.Name}{Params.PluralEx}.Get.v1.{pInfo.PropertyType.Name}Response"); 


                    }
                    propertyConstructor += navPropOrg;
                }
                else
                if (pInfo.PropertyType.Name.Contains("Nullable`1"))
                    propertyConstructor += $"{pInfo.PropertyType.ToString().Replace("System.Nullable`1[", "").Replace("]", "?")} {pInfo.Name}, {Environment.NewLine}";
                else
                    propertyConstructor += $"{pInfo.PropertyType} {pInfo.Name}, {Environment.NewLine}";
            }
            else
            {
                if (pInfo.CustomAttributes != null && pInfo.CustomAttributes.Any(a => a.AttributeType.Name == "NullableAttribute" || a.AttributeType.Name == "InversePropertyAttribute"))
                    propertyConstructor += $"{pInfo.PropertyType.ToString().Replace("`1", "").Replace("[", "<").Replace("]", ">?")} {pInfo.Name} = null, {Environment.NewLine}";
                else
                    if (pInfo.PropertyType.Name.Contains("Nullable`1"))
                    propertyConstructor += $"{pInfo.PropertyType.ToString().Replace("System.Nullable`1[", "").Replace("]", "?")} {pInfo.Name}, {Environment.NewLine}";
                else
                    propertyConstructor += $"{pInfo.PropertyType} {pInfo.Name}, {Environment.NewLine}";
            }
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
//System.Collections.Generic.ICollection<SaleSnap.WebApi.Pos.Domain.LieferantenBank>? LieferantenBanks = null,
//System.Collections.Generic.ICollection<LieferantenBanks.Get.v1.LieferantenBankResponse>? LieferantenBanks = null, 


//SaleSnap.WebApi.Pos.Domain.ProductVariant Variant = null
//ProductVariants.Get.v1.ProductVariantResponse Variant = null);