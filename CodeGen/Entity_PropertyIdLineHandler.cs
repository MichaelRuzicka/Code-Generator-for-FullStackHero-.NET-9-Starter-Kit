using DevExpress.DataAccess.Native.Web;
using DevExpress.DataProcessing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using static DevExpress.XtraBars.Docking2010.Views.BaseRegistrator;
using System.Threading;
using MediatR;
using System.Threading.Tasks;

namespace CodeGen
{
    public class Entity_PropertyIdLineHandler : ITemplateLineHandler //product.Id
    {
        public void DoTemplating(BuilderParams Params, ref string templateLine)
        {
            templateLine = templateLine.Replace(EnumExtensions.GetEnumValue(TemplateVarsEnum.Entity_PropertyId), $"{Params.Entity_}.Id");
        }
    }
}
//[ModuleName] zb.Catalog
//[Module_Namespace]   Default: FSH.Starter.WebApi.Catalog
//[EntitySet] Default: Entity Plural zb.Brands
//[Entity]
//[Entity_]
//[Entity_PropertyId]
//[DataType]
//[PropertyName] Default: zb.Description
//[DefaultValue]

