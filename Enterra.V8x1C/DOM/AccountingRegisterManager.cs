using System;
using System.Collections.Generic;
using System.Text;

namespace Enterra.V8x1C.DOM
{
    /// <summary>
    /// РегистрБухгалтерииМенеджер (AccountingRegisterManager)
    /// </summary>
    public class AccountingRegisterManager : BaseSessionObject
    {
        internal AccountingRegisterManager(Session session, object ptr)
            : base(session, ptr)
        {
        }

        /// <summary>
        /// Выбрать (Select)
        /// </summary>
        public AccountingRegisterSelection Select()
        {
            object ptr = InvokeMethod("Select");
            if (ptr == null)
            {
                return null;
            }
            return new AccountingRegisterSelection(Session, ptr);
        }
    }

}
