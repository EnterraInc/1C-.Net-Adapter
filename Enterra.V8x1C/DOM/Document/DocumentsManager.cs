using System;
using System.Collections.Generic;
using System.Text;

namespace Enterra.V8x1C.DOM
{
    /// <summary>
    /// DocumentsManager
    /// </summary>
    public class DocumentsManager : BaseSessionObject
    {
        internal DocumentsManager(Session session, object ptr)
            : base(session, ptr)
        {
            
        }

        /// <summary>
        /// ДокументМенеджер (DocumentsManager)
        /// </summary>
        /// <param name="documentName"></param>
        /// <returns></returns>
        public DocumentManager this[string documentName]
        {
            get
            {
                return (DocumentManager)GetProperty(documentName, ptr => new DocumentManager(this, documentName, ptr));
            }
        }
    }
}
