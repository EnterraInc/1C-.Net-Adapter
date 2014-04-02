using System;
using System.Collections.Generic;
using System.Text;

namespace Enterra.V8x1C.DOM
{
    /// <summary>
    /// Базовые свойства справочника
    /// </summary>
    public abstract class BaseCatalogProperties : BaseObjectProperties
    {
        public const string CodePropertyName = "Code";
        public const string DescriptionPropertyName = "Description";

        internal BaseCatalogProperties(
            Session session, 
            string catalogName,
            object ptr
            )
            : base(session, catalogName, ptr)
        {
        }

        /// <summary>
        /// Метаданные (Metadata)
        /// </summary>
        public MetadataObject Metadata
        {
            get { return Session.Metadata.Catalogs[ObjectTypeName]; }
        }

        /// <summary>
        /// Код (Code).
        /// Содержит код элемента справочника. Строка или число в зависимости от настроек справочника в конфигураторе.
        /// </summary>
        public object Code
        {
            get
            {
                return GetProperty(CodePropertyName);
            }
            set
            {
                SetProperty(CodePropertyName, value);
            }
        }

        /// <summary>
        /// Наименование (Description)
        /// </summary>
        public string Description
        {
            get
            {
                return GetProperty(DescriptionPropertyName) as string;
            }
            set
            {
                SetProperty(DescriptionPropertyName, value);
            }
        }

        /// <summary>
        /// ЭтоГруппа (IsFolder)
        /// </summary>
        public bool IsFolder
        {
            get
            {
                return (bool)GetProperty("IsFolder");
            }
        }
        
        /// <summary>
        /// Владелец (Owner)
        /// </summary>
        public CatalogRef Owner
        {
            get
            {
                return GetRefProperty("Owner");
            }
            set
            {
                SetProperty("Owner", value);
            }
        }

        /// <summary>
        /// Родитель (Parent)
        /// </summary>
        public CatalogRef Parent
        {
            get
            {
                return GetRefProperty("Parent");
            }
            set
            {
                SetProperty("Parent", value);
            }
        }

        /// <summary>
        /// Ссылка (Ref)
        /// </summary>
        public CatalogRef Ref
        {
            get
            {
                return GetRefProperty("Ref");
            }
        }

        private CatalogRef GetRefProperty(string propName)
        {
            return (CatalogRef)GetProperty(
                    propName,
                    ptr => new CatalogRef(Session, ObjectTypeName, ptr)
                    );
        }
    }
}
