using System;
using System.Collections.Generic;
using System.Text;

namespace Enterra.V8x1C.DOM
{
    /// <summary>
    /// Структура (Structure)
    /// </summary>
    public class Structure : BaseSessionObject
    {
        internal Structure(Session session, object ptr)
            : base(session, ptr)
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="session"></param>
        public Structure(Session session) :
            base(session, session.NewObject("Structure"))
        {
        }

        /// <summary>
        /// Значение
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public object this[string name]
        {
            get
            {
                return GetProperty(name);
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
        /// Вставить (Insert)
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        public void Insert(string name, object value)
        {
            if (value is BaseObject)
            {
                value = (value as BaseObject).Ptr;
            }
            InvokeV8Method("Insert", name, value);
        }

        /// <summary>
        /// Очистить (Clear)
        /// </summary>
        public void Clear()
        {
            InvokeV8Method("Clear");
        }

        /// <summary>
        /// Удалить (Delete)
        /// </summary>
        /// <param name="name"></param>
        public void Delete(string name)
        {
            InvokeV8Method("Delete", name);
        }
    }
}
