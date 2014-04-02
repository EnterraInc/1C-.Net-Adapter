using System;
using System.Collections.Generic;
using System.Text;

namespace Enterra.V8x1C.DOM
{
    /// <summary>
    /// Метаданные регистра накоплений
    /// </summary>
    public class AccumulationRegisterMetadata : RegisterMetadataBase
    {
        internal AccumulationRegisterMetadata(Session session, object ptr)
            :base(session, MetadataType.AccumulationRegister,  ptr)
        {
        }
    }
}
