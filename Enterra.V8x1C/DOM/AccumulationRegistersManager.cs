using System;
using System.Collections.Generic;
using System.Text;

namespace Enterra.V8x1C.DOM
{
    /// <summary>
    /// РегистрыНакопленияМенеджер (AccumulationRegistersManager)
    /// </summary>
    public class AccumulationRegistersManager : BaseSessionObject
    {
        internal AccumulationRegistersManager(Session session, object ptr)
            : base(session, ptr)
        {
        }

        /// <summary>
        /// Получить менеджер регистров накоплений
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public AccumulationRegisterManager this[string name]
        {
            get
            {
                return (AccumulationRegisterManager)GetIndexerFromCache(
                    name,
                    () => GetProperty(name, ptr => new AccumulationRegisterManager(Session, ptr))
                    );
            }
        }
    }
}
