using System;
using System.Collections.Generic;
using System.Text;

namespace Enterra.V8x1C.DOM
{
    /// <summary>
    /// Метаданные регистра сведений
    /// </summary>
    public class InformationRegisterMetadata : RegisterMetadataBase
    {
        internal InformationRegisterMetadata(Session session, object ptr)
            :base(session, MetadataType.InformationRegister,  ptr)
        {
        }
    }
}
