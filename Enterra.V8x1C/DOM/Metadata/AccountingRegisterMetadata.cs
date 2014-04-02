using System;
using System.Collections.Generic;
using System.Text;

namespace Enterra.V8x1C.DOM
{
    /// <summary>
    /// Метаданные регистра бухгалтерии
    /// </summary>
    public class AccountingRegisterMetadata : RegisterMetadataBase
    {
        internal AccountingRegisterMetadata(Session session, object ptr)
            :base(session, MetadataType.AccountingRegister,  ptr)
        {
        }
    }
}
