using System;
using System.Collections.Generic;
using System.Text;

namespace Enterra.V8x1C.DOM
{
    /// <summary>
    /// СтрокаТаблицыЗначений (ValueTableRow)
    /// </summary>
    public class ValueTableRow : BaseSessionObject
    {
        private readonly ValueTable _table = null;
        private readonly int _index = -1;

        internal ValueTableRow(Session session, object ptr)
            : base(session, ptr)
        {
        }

        internal ValueTableRow(ValueTable table, int index)
            : base(table.Session, null)
        {
            _table = table;
            _index = index;
        }


        /// <summary>
        /// Получить значение колонки
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public object this[string columnName]
        {
            get
            {
                return GetProperty(columnName);
            }
            set
            {
                PutToCache(columnName, value);
            }
        }

        /// <summary>
        /// Получить значение колонки
        /// </summary>
        /// <param name="columnIndex"></param>
        /// <returns></returns>
        public object this[int columnIndex]
        {
            get
            {
                return GetFromCache(columnIndex.ToString(),
                    () => InvokeMethod("Get", columnIndex)
                    );
            }
            set
            {
                PutToCache(columnIndex.ToString(), value);
            }
        }

        internal object this[ValueTableColumn column]
        {
            set
            {
                PutToCache(column.Name, value);
                PutToCache(column.Index.ToString(), value);
            }
        }

        protected override void OnPtrRequired()
        {
            if (_table != null)
            {
                Ptr = _table.InvokeV8Method("Get", _index);
            }

            base.OnPtrRequired();
        }
    }
}
