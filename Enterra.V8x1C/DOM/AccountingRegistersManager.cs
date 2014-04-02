using System;
using System.Collections.Generic;
using System.Text;

namespace Enterra.V8x1C.DOM
{
    /// <summary>
    /// РегистрыБухгалтерииМенеджер (AccountingRegistersManager)
    /// </summary>
    public class AccountingRegistersManager : BaseSessionObject
    {
        internal AccountingRegistersManager(Session session, object ptr)
            : base(session, ptr)
        {
        }

        /// <summary>
        /// Получить менеджер регистров бухгалтерии
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public AccountingRegisterManager this[string name]
        {
            get
            {
                return (AccountingRegisterManager)GetIndexerFromCache(
                    name,
                    () => GetProperty(name, ptr => new AccountingRegisterManager(Session, ptr))
                    );
            }
        }
    }
}
