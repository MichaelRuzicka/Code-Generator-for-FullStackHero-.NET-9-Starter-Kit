using CodeGen;
using Microsoft.CSharp;
using System;
using System.CodeDom;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Reflection.Metadata;



public class PrimaryKeyFieldDataTypeLowerCaseLineHandler : ITemplateLineHandler //      
{
    public void DoTemplating(BuilderParams Params, ref string templateLine)
    {
        string primaryKeyFieldList = string.Empty;
        foreach (PropertyInfo pInfo in Params.PropertyInfos.Where(p => p.CustomAttributes.Any()))
        {
            foreach (var customAttribute in pInfo.CustomAttributes.Where(a => a.AttributeType == typeof(KeyAttribute)))
            {
                var clrType = Extensions.GetClrType(pInfo.PropertyType);
                primaryKeyFieldList += $"{clrType}";

            }
        }

        templateLine = templateLine.Replace(EnumExtensions.GetEnumValue(TemplateVarsEnum.PrimaryKeyFieldDataTypeLowerCase), primaryKeyFieldList);
    }



}


public static class Extensions
{
    public static string GetClrType(Type type)
    {
        using (var provider = new CSharpCodeProvider())
        {
            var typeRef = new CodeTypeReference(type);
            var typeName = provider.GetTypeOutput(typeRef);
            return typeName;
        }
    }
}