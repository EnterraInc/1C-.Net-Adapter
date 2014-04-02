using System;
using System.Collections.Generic;
using System.Text;

namespace Enterra.V8x1C.DOM
{
    /// <summary>
    /// ОписаниеПараметровЗапроса (QueryParametersDescription)
    /// </summary>
    public class QueryParametersDescription : BaseSessionObject
    {
        internal QueryParametersDescription(Session session, object ptr)
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
                return (int) GetProperty("Count", true, null);
            }
        }

        /// <summary>
        /// Найти (Find)
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public QueryParameterDescription Find(string name)
        {
            object ptr = InvokeV8Method("Find", name);
            return ptr != null ? new QueryParameterDescription(Session, ptr) : null;
        }
    }
}
