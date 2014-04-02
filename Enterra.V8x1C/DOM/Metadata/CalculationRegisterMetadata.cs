using System;
using System.Collections.Generic;
using System.Text;

namespace Enterra.V8x1C.DOM
{
    /// <summary>
    /// Метаданные регистра расчета
    /// </summary>
    public class CalculationRegisterMetadata : RegisterMetadataBase
    {
        internal CalculationRegisterMetadata(Session session, object ptr)
            :base(session, MetadataType.CalculationRegister,  ptr)
        {
        }
    }
}
