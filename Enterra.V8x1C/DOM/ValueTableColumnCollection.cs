using System;
using System.Collections.Generic;
using System.Text;

namespace Enterra.V8x1C.DOM
{
    /// <summary>
    /// КоллекцияКолонокТаблицыЗначений (ValueTableColumnCollection)
    /// </summary>
    public class ValueTableColumnCollection : BaseSessionObject
    {
        internal ValueTableColumnCollection(Session session, object ptr)
            : base(session, ptr)
        {
        }

        /// <summary>
        /// Получить колонку
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public ValueTableColumn this[int index]
        {
            get
            {
                return (ValueTableColumn)GetFromCache(index.ToString(),
                                     delegate()
                                     {
                                         object ptr = InvokeV8Method("Get", index);
                                         if (ptr != null)
                                         {
                                             var column = new ValueTableColumn(this, ptr);
                                             PutToIndexerCache(column.Name, column);
                                             return column;
                                         }
                                         return null;
                                     }
                    );
            }
        }

        /// <summary>
        /// Получить колонку
        /// </summary>
        /// <param name="columnName"></param>
        /// <returns></returns>
        public ValueTableColumn this[string columnName]
        {
            get
            {
                return (ValueTableColumn)GetIndexerFromCache(columnName,
                                     delegate()
                                         {
                                             object ptr = InvokeV8Method("Find", columnName);
                                             if (ptr != null)
                                             {
                                                 var column = new ValueTableColumn(this, ptr);
                                                 PutToCache(column.Index.ToString(), column);
                                                 return column;
                                             }
                                             return null;
                                         }
                    );
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
        /// Индекс (IndexOf)
        /// </summary>
        /// <returns></returns>
        public int IndexOf(ValueTableColumn column)
        {
            return (int)InvokeV8Method("IndexOf", column.Ptr);
        }
    }
}
