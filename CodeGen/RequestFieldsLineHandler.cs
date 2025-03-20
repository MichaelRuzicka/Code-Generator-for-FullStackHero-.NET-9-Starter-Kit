using CodeGen;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace CodeGen
{
    public class RequestFieldsLineHandler : ITemplateLineHandler //        var artikel = Artikel.Create(request.Name!, request.Description);
    {
        public void DoTemplating(BuilderParams Params, ref string templateLine)
        {
            string requestFieldList = string.Empty;

            foreach (PropertyInfo pInfo in Params.PropertyInfos)
            {
                if (pInfo.CustomAttributes.Any(a => a.AttributeType == typeof(KeyAttribute))) //PK fields
                    continue;
                requestFieldList += $"request.{pInfo.Name}, ";

            }
            requestFieldList = requestFieldList.Substring(0, requestFieldList.Length - 2);

            templateLine = templateLine.Replace(EnumExtensions.GetEnumValue(TemplateVarsEnum.RequestFields), requestFieldList);
        }
    }
}


//https://stackoverflow.com/questions/51899876/how-to-retrieve-entity-configuration-from-fluent-api

