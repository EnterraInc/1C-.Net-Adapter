using System;
using System.Collections.Generic;
using System.Text;

namespace Enterra.V8x1C.DOM
{
    /// <summary>
    /// СправочникСсылка (CatalogRef)
    /// </summary>
    public class CatalogRef : BaseCatalogProperties, IObjectRef
    {
        private readonly string _uuidStr;
        
        internal CatalogRef(
            Session session,
            string catalogName,
            string uuidStr
            )
            : base(session, catalogName, null)
        {
            _uuidStr = uuidStr;
        }

        internal CatalogRef(
            Session session, 
            string catalogName,
            object ptr
            )
            : base(session, catalogName, ptr)
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
        /// ПолныйКод (FullCode)
        /// </summary>
        public string FullCode
        {
            get
            {
                if (IsEmpty)
                {
                    return null;
                }

                return GetProperty("FullCode", true, null) as string;
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

        public override string ToString()
        {
            return string.Format("{0}, {1}", ObjectTypeName, Uuid.ToString());
        }

        protected override void OnPtrRequired()
        {
            this.Uuid = new UUID(Session, _uuidStr);
            var catManager = Session.Catalogs[ObjectTypeName];
            Ptr = catManager.GetRefInternal(Uuid);
        }
    }
}
