using DevExpress.Xpo;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.DirectoryServices.ActiveDirectory;
using System.Linq;
using System.Reflection;

namespace CodeGen
{
    public class UpdateCommandConstructorLineHandler : ITemplateLineHandler //        var artikel = Artikel.Create(request.Name!, request.Description);
    {
        public void DoTemplating(BuilderParams Params, ref string templateLine)
        {
            string propertyConstructor = string.Empty;
            string optionalPropertys = string.Empty;
            foreach (PropertyInfo pInfo in Params.PropertyInfos)
            {

                if (IsNullable(pInfo))

                    if (pInfo.PropertyType.ToString().Contains("Nullable`1"))
                        propertyConstructor += $"{pInfo.PropertyType.GenericTypeArguments.First().FullName}? {pInfo.Name}, {Environment.NewLine}";
                    else
                    if (pInfo.PropertyType.IsArray)
                        propertyConstructor += $"{pInfo.PropertyType.ToString().Replace("`1", "")}? {pInfo.Name}, {Environment.NewLine}";
                    else
                        propertyConstructor += $"{pInfo.PropertyType.ToString().Replace("`1", "").Replace("[", "<").Replace("]", ">?")}? {pInfo.Name}, {Environment.NewLine}";

                else
                {
                    if (Attribute.IsDefined(pInfo, typeof(InversePropertyAttribute)))
                    {
                        //if (Attribute.IsDefined(pInfo, typeof(ForeignKeyAttribute)))
                        //    continue;
                        var navPropOrg = $"{pInfo.PropertyType.ToString().Replace("`1", "").Replace("[", "<").Replace("]", ">?")} {pInfo.Name} = null, {Environment.NewLine}";
                        if (pInfo.PropertyType.GenericTypeArguments.Count() > 0)//ICollection?
                        {
                            string singularTypeName = pInfo.PropertyType.GenericTypeArguments.First().Name;
                            navPropOrg = navPropOrg.Replace($"{singularTypeName}>", $"{singularTypeName}Response>");
                            navPropOrg = navPropOrg.Replace("SaleSnap.WebApi.Pos.Domain", $"{singularTypeName}{Params.PluralEx}.Get.v1"); //Return Navigation as Response Datatype
                        }
                        else
                        {
                            navPropOrg = navPropOrg.Replace($"SaleSnap.WebApi.Pos.Domain.{pInfo.PropertyType.Name}", $"{pInfo.PropertyType.Name}{Params.PluralEx}.Get.v1.{pInfo.PropertyType.Name}Response");
                        }
                        propertyConstructor += navPropOrg;
                    }
                    else
                    {
                        if (pInfo.PropertyType.IsGenericType)
                            propertyConstructor += $"{pInfo.PropertyType.ToString().Replace("`1", "").Replace("[", "<").Replace("]", ">?")} {pInfo.Name}, {Environment.NewLine}";
                        else
                            propertyConstructor += $"{pInfo.PropertyType} {pInfo.Name}, {Environment.NewLine}";
                    }
                }
            }
            propertyConstructor += optionalPropertys;
            propertyConstructor = propertyConstructor.Substring(0, propertyConstructor.Length - 4);

            templateLine = templateLine.Replace(EnumExtensions.GetEnumValue(TemplateVarsEnum.UpdateCommandConstructor), propertyConstructor);
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
//System.Collections.Generic.ICollection<SaleSnap.WebApi.Pos.Domain.LieferantenBank>? LieferantenBanks = null,
//System.Collections.Generic.ICollection<LieferantenBanks.Get.v1.LieferantenBankResponse>? LieferantenBanks = null, 


//SaleSnap.WebApi.Pos.Domain.ProductVariant Variant = null
//ProductVariants.Get.v1.ProductVariantResponse Variant = null);