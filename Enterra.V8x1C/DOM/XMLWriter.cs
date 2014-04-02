using System;
using System.Collections.Generic;
using System.Text;

namespace Enterra.V8x1C.DOM
{
    /// <summary>
    /// ЗаписьXML (XMLWriter)
    /// </summary>
    public class XMLWriter : BaseSessionObject
    {
        public XMLWriter(Session session)
            : base(session, null)
        {
            var ptr = session.NewObject("XMLWriter");

            Ptr = ptr;
        }

        /// <summary>
        /// УстановитьСтроку (SetString)
        /// </summary>
        public void SetString()
        {
            InvokeV8Method("SetString");
        }



        /// <summary>
        /// Закрыть (Close)
        /// </summary>
        /// <returns></returns>
        public string Close()
        {
            return (string)InvokeV8Method("Close");
        }
    }
}
