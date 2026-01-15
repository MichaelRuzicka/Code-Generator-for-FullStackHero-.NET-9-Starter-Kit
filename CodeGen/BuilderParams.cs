using DevExpress.CodeParser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows.Documents;

namespace CodeGen
{
    public class BuilderParams
    {
        private string entity;

        public string TemplateName { get; set; } //zb: MediatRCreate
        public List<string> TemplatePaths { get; set; } = new List<string>();

        private List<PropertyInfo> propertyInfos;

        public string ModuleName { get; set; }

        public string Root_Namespace { get; set; }
        public string Module_Namespace { get; set; }
        public string EntitySet { get; set; }
        public string Entity
        {
            get => entity;
            set
            {
                entity = value;
                Entity_ = value.ToLower();
                Entity_PropertyId = entity + ".Id";
            }
        }

        public bool EntityIsIAggregateRoot { get; set; }

        public string Entity_ { get; private set; }

        public string Entity_PropertyId { get; private set; }

        public string OutputPath { get; set; }

        public string PluralEx { get; set; }

        //public string DataType { get; set; }

        //public string PropertyName { get; set; }

        //public string DefaultValue { get; set; }

        public List<PropertyInfo> PropertyInfos
        {
            get => propertyInfos;
            set { 
                propertyInfos = value;
            }
        }
        public OutputEnum OutputDestination { get; set; }

    }
}
