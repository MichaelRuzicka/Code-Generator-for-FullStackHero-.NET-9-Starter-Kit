using CodeGen;
using DevExpress.CodeParser;
using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

public class PrimaryKeyWithPropertyLineHandler : ITemplateLineHandler //      
{
    public void DoTemplating(BuilderParams Params, ref string templateLine)
    {
        string primaryKeyFieldList = string.Empty;
        foreach (PropertyInfo pInfo in Params.PropertyInfos.Where(p => p.CustomAttributes.Any()))
        {
            foreach (var customAttribute in pInfo.CustomAttributes.Where(a => a.AttributeType == typeof(KeyAttribute)))
            {
                primaryKeyFieldList += $"public {pInfo.PropertyType} {pInfo.Name} {{ get; set; }}{Environment.NewLine}";
                primaryKeyFieldList += $"\tpublic Get{Params.Entity}Request ({pInfo.PropertyType} {pInfo.Name.ToLower()}) => {pInfo.Name} = {pInfo.Name.ToLower()};{Environment.NewLine}";
            }
        }
        //primaryKeyFieldList = primaryKeyFieldList.Substring(0, primaryKeyFieldList.Length - 2);

        templateLine = templateLine.Replace(EnumExtensions.GetEnumValue(TemplateVarsEnum.PrimaryKeyWithProperty), primaryKeyFieldList);
    }
}
