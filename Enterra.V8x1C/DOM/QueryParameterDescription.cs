using System;
using System.Collections.Generic;
using System.Text;

namespace Enterra.V8x1C.DOM
{
    /// <summary>
    /// ОписаниеПараметраЗапроса (QueryParameterDescription)
    /// </summary>
    public class QueryParameterDescription : BaseSessionObject
    {
        internal QueryParameterDescription(Session session, object ptr)
            : base(session, ptr)
        {
        }

        /// <summary>
        /// Имя (Name)
        /// </summary>
        public string Name
        {
            get
            {
                return GetProperty("Name") as string;
            }
        }

        /// <summary>
        /// ТипЗначения (ValueType)
        /// </summary>
        public TypeDescription ValueType
        {
            get
            {
                return (TypeDescription)GetProperty(
                    "ValueType",
                    ptr => new TypeDescription(Session, ptr)
                    );
            }
        }
    }
}
