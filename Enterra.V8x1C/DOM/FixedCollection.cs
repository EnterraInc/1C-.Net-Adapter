using System;
using System.Collections.Generic;
using System.Text;

namespace Enterra.V8x1C.DOM
{
    /// <summary>
    /// ФиксированнаяКоллекция (FixedCollection)
    /// </summary>
    public class FixedCollection : BaseSessionObject
    {
        internal FixedCollection(Session session, object ptr)
            : base(session, ptr)
        {
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
        /// Найти (Find)
        /// </summary>
        /// <param name="elementName"></param>
        /// <returns></returns>
        public object Find(string elementName)
        {
            return InvokeMethod("Find", elementName);
        }

        /// <summary>
        /// Получить (Get)
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public object Get(int index)
        {
            return InvokeMethod("Get", index);
        }

        /// <summary>
        /// Индекс (IndexOf)
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public int IndexOf(object item)
        {
            if (item is BaseObject)
            {
                item = (item as BaseObject).Ptr;
            }

            return (int)InvokeV8Method("IndexOf", item);
        }

    }
}
