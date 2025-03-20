using CodeGen;
using DevExpress.DataAccess.Native.Web;
using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;


public class PrimaryKeyWithDataTypeLowerCaseLineHandler : ITemplateLineHandler //      
{
    public void DoTemplating(BuilderParams Params, ref string templateLine)
    {
        string primaryKeyFieldList = string.Empty;
        foreach (PropertyInfo pInfo in Params.PropertyInfos.Where(p => p.CustomAttributes.Any()))
        {
            foreach (var customAttribute in pInfo.CustomAttributes.Where(a => a.AttributeType == typeof(KeyAttribute)))  // if instead of foreach?
            {
                primaryKeyFieldList += $"{pInfo.PropertyType} {pInfo.Name.ToLower()}, "; //Muss erweitert werden für multi natural keys
            }
        }
        primaryKeyFieldList = primaryKeyFieldList.Substring(0, primaryKeyFieldList.Length - 2);
        templateLine = templateLine.Replace(EnumExtensions.GetEnumValue(TemplateVarsEnum.PrimaryKeyWithDataTypeLowerCase), primaryKeyFieldList);
    }
}
