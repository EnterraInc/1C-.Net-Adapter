using System;
using System.Collections.Generic;
using System.Text;

namespace Enterra.V8x1C.DOM
{
    /// <summary>
    /// КонстантыМенеджер (ConstantsManager)
    /// </summary>
    public class ConstantsManager : BaseSessionObject
    {
        internal ConstantsManager(Session session, object ptr)
            : base(session, ptr)
        {
        }

        /// <summary>
        /// Получить менеджер констант
        /// </summary>
        /// <param name="constantName"></param>
        /// <returns></returns>
        public ConstantManager this[string constantName]
        {
            get
            {
                return (ConstantManager)GetProperty(
                    constantName,
                    ptr => new ConstantManager(Session, ptr)
                    );
            }
        }
    }
}
