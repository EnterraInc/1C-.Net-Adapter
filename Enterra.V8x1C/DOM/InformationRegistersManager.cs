using System;
using System.Collections.Generic;
using System.Text;

namespace Enterra.V8x1C.DOM
{
    /// <summary>
    /// РегистрыСведенийМенеджер (InformationRegistersManager)
    /// </summary>
    public class InformationRegistersManager : BaseSessionObject
    {
        internal InformationRegistersManager(Session session, object ptr)
            : base(session, ptr)
        {
        }

        /// <summary>
        /// Получить менеджер регистров сведений
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public InformationRegisterManager this[string name]
        {
            get
            {
                return (InformationRegisterManager)GetIndexerFromCache(
                    name,
                    () => GetProperty(name, ptr => new InformationRegisterManager(Session, ptr))
                    );
            }
        }
    }
}
