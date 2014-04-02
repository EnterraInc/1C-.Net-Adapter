using System;
using System.Collections.Generic;
using System.Text;

namespace Enterra.V8x1C.DOM
{
    /// <summary>
    /// РегистрНакопленияМенеджер (AccumulationRegisterManager)
    /// </summary>
    public class AccumulationRegisterManager : BaseSessionObject
    {
        internal AccumulationRegisterManager(Session session, object ptr)
            : base(session, ptr)
        {
        }

        /// <summary>
        /// Выбрать (Select)
        /// </summary>
        public AccumulationRegisterSelection Select()
        {
            object ptr = InvokeMethod("Select");
            if (ptr == null)
            {
                return null;
            }
            return new AccumulationRegisterSelection(Session, ptr);
        }
    }
}
