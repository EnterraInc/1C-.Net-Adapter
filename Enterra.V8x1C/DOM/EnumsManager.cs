using System;
using System.Collections.Generic;
using System.Text;

namespace Enterra.V8x1C.DOM
{
    /// <summary>
    /// ПеречисленияМенеджер (EnumsManager)
    /// </summary>
    public class EnumsManager : BaseSessionObject
    {
        internal EnumsManager(Session session, object ptr)
            : base(session, ptr)
        {
        }

        /// <summary>
        /// Получить менеджер перечисления
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public EnumManager this[string name]
        {
            get
            {
                return (EnumManager)GetIndexerFromCache(
                    name,
                    () => GetProperty(name, ptr => new EnumManager(Session, name, ptr))
                    );
            }
        }
    }
}
