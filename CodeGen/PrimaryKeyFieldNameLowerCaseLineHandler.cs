using CodeGen;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

public class PrimaryKeyFieldNameLowerCaseLineHandler : ITemplateLineHandler //      
{
    public void DoTemplating(BuilderParams Params, ref string templateLine)
    {
        string primaryKeyFieldList = string.Empty;
        foreach (PropertyInfo pInfo in Params.PropertyInfos.Where(p => p.CustomAttributes.Any()))
        {
            foreach (var customAttribute in pInfo.CustomAttributes.Where(a => a.AttributeType == typeof(KeyAttribute)))
            {
                primaryKeyFieldList += $"{pInfo.Name.ToLower()}";

            }
        }
        //primaryKeyFieldList = primaryKeyFieldList.Substring(0, primaryKeyFieldList.Length - 2);

        templateLine = templateLine.Replace(EnumExtensions.GetEnumValue(TemplateVarsEnum.PrimaryKeyFieldNameLowerCase), primaryKeyFieldList);
    }
}
