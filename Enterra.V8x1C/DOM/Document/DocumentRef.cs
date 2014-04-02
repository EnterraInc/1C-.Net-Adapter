using System;
using System.Collections.Generic;
using System.Text;

namespace Enterra.V8x1C.DOM
{
    /// <summary>
    /// ДокументСсылка (DocumentRef)
    /// </summary>
    public class DocumentRef : BaseDocumentProperties, IObjectRef
    {
        private readonly string _uuidStr;

        internal DocumentRef(
            Session session,
            string documentName,
            string uuidStr
            )
            : base(session, documentName, null)
        {
            _uuidStr = uuidStr;
        }
        
        internal DocumentRef(
            Session session,
            string documentName,
            object ptr
            )
            : base(session, documentName, ptr)
        {
            _uuidStr = null;
        }

        /// <summary>
        /// УникальныйИдентификатор (UUID)
        /// </summary>
        public UUID Uuid
        {
            get
            {
                return (UUID)GetProperty(
                    "Uuid",
                    true,
                    ptr => new UUID(Session, ptr)
                    );
            }
            private set
            {
                PutToCache("Uuid", value);
            }
        }
        
        /// <summary>
        /// Пустой (IsEmpty)
        /// </summary>
        public bool IsEmpty
        {
            get
            {
                return (bool)GetProperty("IsEmpty", true, null);
            }
        }


        #region IObjectRef Members

        /// <summary>
        /// Метаданные (Metadata)
        /// </summary>
        public MetadataObject Metadata
        {
            get { return Session.Metadata.Documents[ObjectTypeName]; }
        }

        object IObjectRef.Code
        {
            get
            {
                return Number;
            }
        }

        
        #endregion

        
        /// <summary>
        /// ПолучитьОбъект (GetObject)
        /// </summary>
        /// <returns></returns>
        public DocumentObject GetObject()
        {
            return (DocumentObject)GetProperty(
                "GetObject", 
                true,
                ptr => new DocumentObject(this.Session, ObjectTypeName, ptr)
                );
        }

        public override string ToString()
        {
            return string.Format("{0}, {1}", ObjectTypeName, Uuid.ToString());
        }

        protected override void OnPtrRequired()
        {
            this.Uuid = new UUID(Session, _uuidStr);
            var docManager = Session.Documents[ObjectTypeName];
            Ptr = docManager.GetRefInternal(Uuid);
        }
    }
}
