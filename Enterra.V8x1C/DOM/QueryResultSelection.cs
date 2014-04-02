using System;
using System.Collections.Generic;
using System.Text;

namespace Enterra.V8x1C.DOM
{
    /// <summary>
    /// ВыборкаИзРезультатаЗапроса (QueryResultSelection)
    /// </summary>
    public class QueryResultSelection : BaseSessionObject
    {
        internal QueryResultSelection(Session session, object ptr)
            :base(session, ptr)
        {
        }

        /// <summary>
        /// Значения полей записи результата запроса
        /// </summary>
        /// <param name="fieldName"></param>
        /// <returns></returns>
        public object this[string fieldName]
        {
            get
            {
                return GetProperty(fieldName);
            }
        }

        /// <summary>
        /// Следующий (Next)
        /// </summary>
        /// <returns></returns>
        public bool Next()
        {
            return (bool)InvokeV8Method("Next");
        }
    }
}
