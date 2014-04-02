using System;
using System.Collections.Generic;
using System.Text;

namespace Enterra.V8x1C.DOM
{
    /// <summary>
    /// Базовые свойства документа
    /// </summary>
    public abstract class BaseDocumentProperties : BaseObjectProperties
    {
        public const string DatePropertyName = "Date";
        public const string NumberPropertyName = "Number";
        public const string PostedPropertyName = "Posted";
        
        internal BaseDocumentProperties(
            Session session, 
            string documentName,
            object ptr
            )
            : base(session, documentName ,ptr)
        {}
        
        
        /// <summary>
        /// Номер (Number)
        /// </remarks>
        public object Number
        {
            get
            {
                return GetV8Property(NumberPropertyName);
            }
            set
            {
                SetProperty(NumberPropertyName, value);
            }
        }

        /// <summary>
        /// Дата (Date)
        /// </summary>
        public DateTime Date
        {
            get
            {
                return (DateTime)GetProperty(DatePropertyName);
            }
            set
            {
                SetProperty(DatePropertyName, value);
            }
        }

        
        /// <summary>
        /// Проведен (Posted)
        /// </summary>
        public bool Posted
        {
            get
            {
                return (bool)GetProperty(PostedPropertyName);
            }
            set
            {
                SetProperty(PostedPropertyName, value);
            }
        }

        /// <summary>
        /// Ссылка (Ref)
        /// </summary>
        public DocumentRef Ref
        {
            get
            {
                return (DocumentRef)GetProperty(
                    RefPropertyName,
                    ptr => new DocumentRef(Session, ObjectTypeName, ptr)
                    );
            }
        }
    }
}
