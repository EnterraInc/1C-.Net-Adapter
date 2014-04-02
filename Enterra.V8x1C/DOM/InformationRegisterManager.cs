using System;
using System.Collections.Generic;
using System.Text;

namespace Enterra.V8x1C.DOM
{
    /// <summary>
    /// РегистрСведенийМенеджер (InformationRegisterManager)
    /// </summary>
    public class InformationRegisterManager : BaseSessionObject
    {
        internal InformationRegisterManager(Session session, object ptr)
            : base(session, ptr)
        {
        }

        /// <summary>
        /// Выбрать (Select)
        /// </summary>
        public InformationRegisterSelection Select()
        {
            object ptr = InvokeMethod("Select");
            if (ptr == null)
            {
                return null;
            }
            return new InformationRegisterSelection(Session, ptr);
        }
    }
}
