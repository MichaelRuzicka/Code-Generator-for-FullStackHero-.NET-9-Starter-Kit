using CodeGen;
using DevExpress.Utils.Drawing.Animation;
using DevExpress.XtraEditors;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;


public class PrimaryKeyWhereLineHandler : ITemplateLineHandler //      
{
    public void DoTemplating(BuilderParams Params, ref string templateLine)
    {
        string primaryKeyFieldList = string.Empty;
        foreach (PropertyInfo pInfo in Params.PropertyInfos.Where(p => p.CustomAttributes.Any()))
        {
            foreach (var customAttribute in pInfo.CustomAttributes.Where(a => a.AttributeType == typeof(KeyAttribute)))
            {
                primaryKeyFieldList += $"{pInfo.Name} == {pInfo.Name.ToLower()} && ";
            }
        }
        try
        {
            primaryKeyFieldList = primaryKeyFieldList.Substring(0, primaryKeyFieldList.Length - 4);
        }
        catch (Exception ex)
        {
            XtraMessageBox.Show(ex.Message + Environment.NewLine + "DataAnnotation Attributes set?");
        }
        templateLine = templateLine.Replace(EnumExtensions.GetEnumValue(TemplateVarsEnum.PrimaryKeyWhere), primaryKeyFieldList);
    }
}
