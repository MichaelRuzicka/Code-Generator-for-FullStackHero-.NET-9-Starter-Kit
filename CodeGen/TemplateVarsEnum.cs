using System;
using System.Linq;
using System.Runtime.Serialization;

namespace CodeGen
{
    public enum TemplateVarsEnum
    {
        [EnumMember(Value = "[Root_Namespace]")] // Default: FSH
        Root_Namespace,

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


        [EnumMember(Value = "[CreateFields]")]
        CreateFields,

        [EnumMember(Value = "[UpdateFields]")]
        UpdateFields,

        [EnumMember(Value = "[EntityItemFields]")] //zb. customerItem.Name
        EntityItemFields,

        [EnumMember(Value = "[ServiceKey]")]
        ServiceKey,

        [EnumMember(Value = "[PropertyListing]")]
        PropertyListing,

        [EnumMember(Value = "[CreatePropertyConstructor]")]
        CreatePropertyConstructor,

        [EnumMember(Value = "[UpdateCommandConstructor]")]
        UpdateCommandConstructor,

        [EnumMember(Value = "[ResponsePropertyConstructor]")]
        ResponsePropertyConstructor,

        [EnumMember(Value = "[PrimaryKeyWithProperty]")]
        PrimaryKeyWithProperty,

        [EnumMember(Value = "[PrimaryKeyWithDataTypeLowerCase]")]
        PrimaryKeyWithDataTypeLowerCase,

        [EnumMember(Value = "[PrimaryKeyWithDataTypeUpperCase]")]
        PrimaryKeyWithDataTypeUpperCase,

        [EnumMember(Value = "[PrimaryKeyFieldNameLowerCase]")]
        PrimaryKeyFieldNameLowerCase,

        [EnumMember(Value = "[PrimaryKeyFieldNameUpperCase]")]
        PrimaryKeyFieldNameUpperCase,

        [EnumMember(Value = "[PrimaryKeyFieldDataTypeLowerCase]")]
        PrimaryKeyFieldDataTypeLowerCase,

        [EnumMember(Value = "[SpecificationInclude]")]
        SpecificationInclude,

        [EnumMember(Value = "//[Routes]")] //Enhance Routes Section in Module
        Routes,

        [EnumMember(Value = "//[Services]")] //Enhance Service Section in Module
        Services


    }
}
