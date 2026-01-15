using CodeGen;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;

namespace CodeGen
{
    public class UpdateFieldsLineHandler : ITemplateLineHandler //        var artikel = Artikel.Create(request.Name!, request.Description);
    {
        public void DoTemplating(BuilderParams Params, ref string templateLine)
        {
            string fieldList = string.Empty;

            foreach (PropertyInfo pInfo in Params.PropertyInfos)
            {
                if (pInfo.CustomAttributes.Any(a => a.AttributeType == typeof(KeyAttribute))) //PK fields
                    continue;

                if (Attribute.IsDefined(pInfo, typeof(InversePropertyAttribute)))
                {
                    if (Attribute.IsDefined(pInfo, typeof(ForeignKeyAttribute)))
                    {
                        fieldList += $"request.{pInfo.Name}.Adapt<{pInfo.PropertyType.Name}>(), ";
                        //Console.WriteLine("Skip");
                    }
                    else
                        fieldList += $"request.{pInfo.Name}.Adapt<ICollection<{pInfo.PropertyType.GenericTypeArguments.First().Name}>>(), ";
                }
                else
                    fieldList += $"request.{pInfo.Name}, ";

                if (Attribute.IsDefined(pInfo, typeof(ForeignKeyAttribute)))
                    continue;
            }
            fieldList = fieldList.Substring(0, fieldList.Length - 2);

            templateLine = templateLine.Replace(EnumExtensions.GetEnumValue(TemplateVarsEnum.UpdateFields), fieldList);
        }
    }
}
//https://stackoverflow.com/questions/51899876/how-to-retrieve-entity-configuration-from-fluent-api

