using CodeGen;
using DevExpress.DataProcessing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;

namespace CodeGen
{
    public class CreateFieldsLineHandler : ITemplateLineHandler //        var artikel = Artikel.Create(request.Name!, request.Description);
    {
        public void DoTemplating(BuilderParams Params, ref string templateLine)
        {
            string fieldList = string.Empty;

            foreach (PropertyInfo pInfo in Params.PropertyInfos)
            {
                if (Attribute.IsDefined(pInfo, typeof(ForeignKeyAttribute)))
                    continue;

                if (pInfo.CustomAttributes.Any(a => a.AttributeType == typeof(KeyAttribute))) //PK fields
                    continue;
                if (Attribute.IsDefined(pInfo, typeof(InversePropertyAttribute)))
                {
                    fieldList += $"request.{pInfo.Name}.Adapt<ICollection<{pInfo.PropertyType.GenericTypeArguments.First().Name}>>(), ";
                }
                else
                    fieldList += $"request.{pInfo.Name}, ";

            }
            fieldList = fieldList.Substring(0, fieldList.Length - 2);

            templateLine = templateLine.Replace(EnumExtensions.GetEnumValue(TemplateVarsEnum.CreateFields), fieldList);
        }
    }
}
//    request.KitDetails
//request.KitDetails.Adapt<ICollection<KitDetail>>(),