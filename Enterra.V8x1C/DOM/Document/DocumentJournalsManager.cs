using System;
using System.Collections.Generic;
using System.Text;

namespace Enterra.V8x1C.DOM
{
    /// <summary>
    /// ЖурналыДокументовМенеджер (DocumentJournalsManager)
    /// </summary>
    public class DocumentJournalsManager : BaseSessionObject
    {
        internal DocumentJournalsManager(Session session, object ptr)
            : base(session, ptr)
        {
        }

        /// <summary>
        /// Получить менеджер журнала документов
        /// </summary>
        /// <param name="documentName"></param>
        /// <returns></returns>
        public DocumentJournalManager this[string documentName]
        {
            get
            {
                return (DocumentJournalManager)GetProperty(
                    documentName,
                    ptr => new DocumentJournalManager(Session, ptr)
                    );
            }
        }
    }
}
