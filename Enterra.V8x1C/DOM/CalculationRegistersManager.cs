using System;
using System.Collections.Generic;
using System.Text;

namespace Enterra.V8x1C.DOM
{
    /// <summary>
    /// РегистрыРасчетаМенеджер (CalculationRegistersManager)
    /// </summary>
    public class CalculationRegistersManager : BaseSessionObject
    {
        internal CalculationRegistersManager(Session session, object ptr)
            : base(session, ptr)
        {
        }

        /// <summary>
        /// Получить менеджер регистров расчета
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public CalculationRegisterManager this[string name]
        {
            get
            {
                return (CalculationRegisterManager)GetIndexerFromCache(
                    name,
                    () => GetProperty(name, ptr => new CalculationRegisterManager(Session, ptr))
                    );
            }
        }
    }
}
