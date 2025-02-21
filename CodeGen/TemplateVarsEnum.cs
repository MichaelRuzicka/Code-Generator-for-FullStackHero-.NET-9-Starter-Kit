using System;
using System.Linq;
using System.Runtime.Serialization;

namespace CodeGen
{
    public enum TemplateVarsEnum
    {
        [EnumMember(Value = "[Module]")] // Default: zb.Catalog
        Module,

        [EnumMember(Value = "[Module_Namespace]")] // Default: FSH.Starter.WebApi
        Module_Namespace,

        [EnumMember(Value = "[EntitySet]")]
        EntitySet,

        [EnumMember(Value = "[Entity]")]
        Entity,

        [EnumMember(Value = "[Entity_]")]
        Entity_,

        [EnumMember(Value = "[Entity_PropertyId]")]
        Entity_PropertyId,

        [EnumMember(Value = "[DataType]")]
        DataType,

        [EnumMember(Value = "[PropertyName]")]
        PropertyName,

        [EnumMember(Value = "[DefaultValue]")]
        DefaultValue,

        [EnumMember(Value = "[RequestFields]")]
        RequestFields,

        [EnumMember(Value = "[ServiceKey]")]
        ServiceKey,

        [EnumMember(Value = "[PropertyListing]")]
        PropertyListing,

        [EnumMember(Value = "//[Routes]")] //Enhance Routes Section in Module
        Routes,

        [EnumMember(Value = "//[Services]")] //Enhance Service Section in Module
        Services

    }
}
