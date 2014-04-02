using System;
using System.Collections.Generic;
using System.Text;

namespace Enterra.V8x1C.DOM
{
    /// <summary>
    /// Базовый класс метаданных
    /// </summary>
    public abstract class MetadataBase : BaseSessionObject
    {
        protected MetadataType _metadataType;

        internal MetadataBase(Session session, MetadataType metadataType, object ptr)
            : base(session, ptr)
        {
            _metadataType = metadataType;
        }

        internal MetadataBase(Session session, object ptr)
            : base(session, ptr)
        {
            _metadataType = MetadataType.Unknown;
        }

        /// <summary>
        /// Тип метаданных
        /// </summary>
        public MetadataType MetadataType
        {
            get
            {
                return _metadataType;
            }
        }

        /// <summary>
        /// Имя (Name)
        /// </summary>
        public virtual string Name
        {
            get
            {
                return string.Empty;
            }
        }

    }
}
