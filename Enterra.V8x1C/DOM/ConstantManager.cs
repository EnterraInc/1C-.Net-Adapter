using System;
using System.Collections.Generic;
using System.Text;

namespace Enterra.V8x1C.DOM
{
    /// <summary>
    /// КонстантаМенеджер (ConstantManager)
    /// </summary>
    public class ConstantManager : BaseSessionObject
    {
        internal ConstantManager(Session session, object ptr)
            : base(session, ptr)
        {
        }

        /// <summary>
        /// Значение константы
        /// </summary>
        public object Value
        {
            get
            {
                return GetProperty("Get", true, null);
            }
        }
    }
}
