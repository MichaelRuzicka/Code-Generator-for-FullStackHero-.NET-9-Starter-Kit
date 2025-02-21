using System;
using System.Linq;
using System.Reflection;

namespace CodeGen
{
    public class EntityItemFieldsLineHandler : ITemplateLineHandler
    {
        public void DoTemplating(BuilderParams Params, ref string templateLine)
        {
            string entityFieldList = string.Empty;
            foreach (PropertyInfo pInfo in Params.PropertyInfos)
            {
                entityFieldList += $"{Params.Entity_}Item.{pInfo.Name}, ";
            }
            entityFieldList = entityFieldList.Substring(0, entityFieldList.Length - 2);

            templateLine = templateLine.Replace(EnumExtensions.GetEnumValue(TemplateVarsEnum.EntityItemFields), entityFieldList);
        }
    }
}
