using System;
using System.Collections.Generic;
using System.Text;

namespace Enterra.V8x1C.DOM
{
    /// <summary>
    /// Метаданные документа
    /// </summary>
    public class DocumentMetadata : MetadataObject
    {
        internal DocumentMetadata(Session session, object ptr)
            : base(session, MetadataType.Document, ptr)
        {}

        /// <summary>
        /// КонтрольУникальности (UniqueControl)
        /// </summary>
        public bool UniqueControl
        {
            get
            {
                return (bool)GetProperty("КонтрольУникальности");
            }
        }

    }
}
