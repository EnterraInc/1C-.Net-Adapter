using System;
using System.Collections.Generic;
using System.Text;

namespace Enterra.V8x1C.DOM
{
    /// <summary>
    /// Строка табличной части (TablePartRow)
    /// </summary>
    public class TablePartRow : BaseSessionObject
    {
        internal TablePartRow(Session session, object ptr)
            : base(session, ptr)
        {
        }

        /// <summary>
        /// НомерСтроки (LineNumber)
        /// </summary>
        public int LineNumber
        {
            get
            {
                return (int)GetProperty("LineNumber");
            }
        }

        /// <summary>
        /// Значение колонки
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public object this[string name]
        {
            get
            {
                return GetProperty(name);
            }
            set
            {
                SetProperty(name, value);
            }
        }
    }
}
