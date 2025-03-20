using CodeGen;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;


public class PrimaryKeyFieldNameUpperCaseLineHandler : ITemplateLineHandler //      
{
    public void DoTemplating(BuilderParams Params, ref string templateLine)
    {
        string primaryKeyFieldList = string.Empty;
        foreach (PropertyInfo pInfo in Params.PropertyInfos.Where(p => p.CustomAttributes.Any()))
        {
            if (pInfo.CustomAttributes.Any(a => a.AttributeType == typeof(KeyAttribute))) //PK fields
              primaryKeyFieldList += $"{pInfo.Name}";

        }
        //primaryKeyFieldList = primaryKeyFieldList.Substring(0, primaryKeyFieldList.Length - 2);

        templateLine = templateLine.Replace(EnumExtensions.GetEnumValue(TemplateVarsEnum.PrimaryKeyFieldNameUpperCase), primaryKeyFieldList);
    }
}