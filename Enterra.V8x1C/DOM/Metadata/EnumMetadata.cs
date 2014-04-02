using System;
using System.Collections.Generic;
using System.Text;

namespace Enterra.V8x1C.DOM
{
    /// <summary>
    /// Метаданные перечисления
    /// </summary>
    public class EnumMetadata : MetadataObject
    {
        internal EnumMetadata(Session session, object ptr)
            : base(session, MetadataType.Enum, ptr)
        {
        }
    }
}
