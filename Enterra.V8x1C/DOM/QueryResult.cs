using System;
using System.Collections.Generic;
using System.Text;

namespace Enterra.V8x1C.DOM
{
    /// <summary>
    /// РезультатЗапроса (QueryResult)
    /// </summary>
    public class QueryResult : BaseSessionObject
    {
        internal QueryResult(Session session, object ptr)
            : base(session, ptr)
        {
        }

        /// <summary>
        /// Пустой (IsEmpty)
        /// </summary>
        public bool IsEmpty
        {
            get
            {
                return (bool)GetProperty("IsEmpty", true, null);
            }
        }

        /// <summary>
        /// Выбрать (Choose)
        /// </summary>
        /// <returns></returns>
        public QueryResultSelection Choose()
        {
            object ptr = InvokeV8Method("Choose");
            if (ptr == null)
            {
                return null;
            }
            return new QueryResultSelection(Session, ptr);
        }


        /// <summary>
        /// Выгрузить (Unload)
        /// </summary>
        /// <returns></returns>
        public ValueTable Unload()
        {
            object ptr = InvokeV8Method("Unload");

            if (ptr == null)
            {
                return null;
            }
            
            return new ValueTable(Session, ptr);

            //TODO: Unload может возвращать еще тип ДеревоЗначений. См. доку 1С
        }

    }
}
