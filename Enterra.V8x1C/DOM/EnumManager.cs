using System;
using System.Collections.Generic;
using System.Text;

namespace Enterra.V8x1C.DOM
{
    /// <summary>
    /// ПеречислениеМенеджер (EnumManager)
    /// </summary>
    public class EnumManager : BaseSessionObject
    {
        private readonly string _enumName;

        internal EnumManager(Session session, string enumName, object ptr)
            : base(session, ptr)
        {
            _enumName = enumName;
        }

        /// <summary>
        /// Enum name
        /// </summary>
        public string EnumName
        {
            get
            {
                return _enumName;
            }
        }

        /// <summary>
        /// Количество (Count)
        /// </summary>
        public int Count
        {
            get
            {
                return (int)GetProperty("Count", true, null);
            }
        }

        /// <summary>
        /// Получить значения перечисления
        /// </summary>
        /// <param name="enumValueName"></param>
        /// <returns></returns>
        public EnumRef this[string enumValueName]
        {
            get
            {
                return (EnumRef) GetProperty(
                    enumValueName,
                    ptr => new EnumRef(Session, _enumName, ptr)
                    );
            }
        }

        public EnumRef this[int index]
        {
            get
            {
                return (EnumRef)GetIndexerFromCache(index.ToString(),
                    () => Get(index)
                    );
            }
        }

        /// <summary>
        /// ПустаяСсылка 
        /// </summary>
        public EnumRef EmptyRef
        {
            get
            {
                return (EnumRef)GetProperty(
                    "EmptyRef",
                    true,
                    ptr => new EnumRef(Session, _enumName, ptr)
                    );
            }
        }

        /// <summary>
        /// Индекс (IndexOf)
        /// </summary>
        /// <param name="enumRef"></param>
        /// <returns></returns>
        public int IndexOf(EnumRef enumRef)
        {
            return (int)Session.InvokeV8Method("IndexOf", enumRef.Ptr);
        }

        //Получить (Get)
        public EnumRef Get(int index)
        {
            object ptr = InvokeV8Method("Get", index);
            return ptr != null ? new EnumRef(Session, EnumName, ptr) : null;
        }

    }
}
