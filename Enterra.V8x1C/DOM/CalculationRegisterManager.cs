using System;
using System.Collections.Generic;
using System.Text;

namespace Enterra.V8x1C.DOM
{
    /// <summary>
    /// РегистрРасчетаМенеджер (CalculationRegisterManager)
    /// </summary>
    public class CalculationRegisterManager : BaseSessionObject
    {
        internal CalculationRegisterManager(Session session, object ptr)
            : base(session, ptr)
        {
        }

        /// <summary>
        /// Выбрать (Select)
        /// </summary>
        public CalculationRegisterSelection Select()
        {
            object ptr = InvokeMethod("Select");
            if (ptr == null)
            {
                return null;
            }
            return new CalculationRegisterSelection(Session, ptr);
        }
    }

}
