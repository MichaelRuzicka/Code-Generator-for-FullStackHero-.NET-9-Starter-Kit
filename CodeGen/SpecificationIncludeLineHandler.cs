using CodeGen;
using DevExpress.DataAccess.Native.Web;
using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;


public class SpecificationIncludeLineHandler : ITemplateLineHandler //      
{
    public void DoTemplating(BuilderParams Params, ref string templateLine)
    {
        string includeList = string.Empty;
        foreach (PropertyInfo pInfo in Params.PropertyInfos.Where(p => p.CustomAttributes.Any()))
        {

            if (pInfo.Name == "Id" && pInfo.PropertyType.FullName == "System.Guid")
            {
                includeList += "Query.Where(p => p.Id == id)";
            }

            if (pInfo.PropertyType.Namespace == "System.Collections.Generic")
            {
                if (pInfo.PropertyType.GenericTypeArguments.First().Namespace == "SaleSnap.WebApi.Pos.Domain")
                {
                    includeList += $".Include(p => p.{pInfo.PropertyType.GenericTypeArguments.First().Name}{Params.PluralEx})";
                }
            }
        }
        includeList += ";";
        templateLine = templateLine.Replace(EnumExtensions.GetEnumValue(TemplateVarsEnum.SpecificationInclude), includeList);
    }
}
   