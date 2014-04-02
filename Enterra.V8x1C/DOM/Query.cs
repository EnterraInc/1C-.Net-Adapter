using System;
using System.Collections.Generic;
using System.Text;

namespace Enterra.V8x1C.DOM
{
    /// <summary>
    /// Запрос (Query)
    /// </summary>
    public class Query : BaseSessionObject
    {
        public const string TextPropertyName = "Text";

        public Query(Session session) :
            base(session, session.NewObject("Запрос")
             )
        {
        }

        public Query(Session session, string text):
            base(session, session.NewObject("Запрос", text)
            )
        {
            PutToCache(TextPropertyName, text);
        }

        /// <summary>
        /// Текст (Text)
        /// </summary>
        public string Text
        {
            get
            {
                return GetProperty(TextPropertyName) as string;
            }
            set
            {
                SetProperty(TextPropertyName, value);
            }
        }

        /// <summary>
        /// Выполнить (Execute)
        /// </summary>
        /// <returns></returns>
        public QueryResult Execute()
        {
            object ptr = InvokeV8Method("Execute");
            if (ptr == null)
            {
                return null;
            }
            return new QueryResult(Session, ptr);
        }

        /// <summary>
        /// УстановитьПараметр (SetParameter)
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        public void SetParameter(string name, object value)
        {
            if (value is BaseObject)
            {
                value = (value as BaseObject).Ptr;
            }

            InvokeMethod("SetParameter", name, value);
        }

        /// <summary>
        /// НайтиПараметры (FindParameters)
        /// </summary>
        /// <returns></returns>
        public QueryParametersDescription FindParameters()
        {
            object ptr = InvokeV8Method("FindParameters");
            return ptr != null ? new QueryParametersDescription(Session, ptr) : null;
        }

    }
}
