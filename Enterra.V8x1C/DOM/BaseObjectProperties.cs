using System;
using System.Collections.Generic;
using System.Text;

namespace Enterra.V8x1C.DOM
{
    /// <summary>
    /// BaseObjectProperties
    /// </summary>
    public abstract class BaseObjectProperties : BaseSessionObject
    {
        public const string RefPropertyName = "Ref";
        public const string DeletionMarkPropertyName = "DeletionMark";


        private readonly string _objectTypeName;

        internal BaseObjectProperties(
            Session session, 
            string objectTypeName,
            object ptr)
            : base(session, ptr)
        {
            _objectTypeName = objectTypeName;
        }


        /// <summary>
        /// Object type name
        /// </summary>
        public string ObjectTypeName
        {
            get { return _objectTypeName; }
        }
       
        /// <summary>
        /// ПометкаУдаления (DeletionMark)
        /// Содержит признак пометки на удаление.
        /// </summary>
        public bool DeletionMark
        {
            get
            {
                return (bool)GetProperty(DeletionMarkPropertyName);
            }
            set
            {
                SetProperty(DeletionMarkPropertyName, value);
            }
        }

        /// <summary>
        /// Получить реквизит, табличную часть
        /// </summary>
        /// <param name="propName"></param>
        /// <returns></returns>
        public object this[string propName]
        {
            get
            {
                return GetProperty(
                    propName,
                    ptr => ConvertCustomPropertyValue(propName, ptr)
                    );
            }
            set
            {
                SetProperty(propName, value);
            }
        }

        private object ConvertCustomPropertyValue(string propName, object value)
        {
            if (!IsInternalV8Object(value))
            {
                return value;
            }

            MetadataObject metadataObject = null;

            if (!string.IsNullOrEmpty(ObjectTypeName))
            {
                if (this is BaseDocumentProperties)
                {
                    metadataObject = Session.Metadata.Documents[ObjectTypeName];
                }
                else if (this is BaseCatalogProperties)
                {
                    metadataObject = Session.Metadata.Catalogs[ObjectTypeName];
                }
            }

            if (metadataObject == null)
            {
                return ConvertV8Value(value);
            }

            if (metadataObject.HasRequisites)
            {
                var reqMeta = metadataObject.Requisites[propName];
                if (reqMeta != null)
                {
                    return ConvertV8Value(value, reqMeta.Type);
                }
            }
            if (metadataObject.HasTableParts)
            {
                var tablePartMeta = metadataObject.TableParts[propName];
                if (tablePartMeta != null)
                {
                    return new TablePart(Session, value);
                }
            }

            return value;
        }
    }
}
