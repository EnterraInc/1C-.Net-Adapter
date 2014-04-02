using System;
using System.Collections.Generic;
using System.Text;

namespace Enterra.V8x1C.DOM
{
    /// <summary>
    /// ПланОбменаСсылка (ExchangePlanRef)
    /// </summary>
    public class ExchangePlanRef : BaseSessionObject
    {
        internal ExchangePlanRef(Session session, object ptr)
            : base(session, ptr)
        {
        }

        /// <summary>
        /// Код (Code)
        /// </summary>
        public string Code
        {
            get
            {
                return (string)GetProperty("Code");
            }
        }

        /// <summary>
        /// Наименование (Description)
        /// </summary>
        public string Description
        {
            get
            {
                return (string)GetProperty("Description");
            }
        }

        /// <summary>
        /// НомерОтправленного (SentNo)
        /// </summary>
        public int SentNo
        {
            get
            {
                return (int)GetProperty("SentNo");
            }
        }

        /// <summary>
        /// НомерПринятого (ReceivedNo)
        /// </summary>
        public int ReceivedNo
        {
            get
            {
                return (int)GetProperty("ReceivedNo");
            }
        }

        /// <summary>
        /// ПометкаУдаления (DeletionMark)
        /// </summary>
        public bool DeletionMark
        {
            get
            {
                return (bool)GetProperty("DeletionMark");
            }
        }

        /// <summary>
        /// Ссылка (Ref)
        /// </summary>
        public ExchangePlanRef Ref
        {
            get
            {
                return (ExchangePlanRef)GetProperty("Ref");
            }
        }


        /// <summary>
        /// Пустая (IsEmpty)
        /// </summary>
        public bool IsEmpty
        {
            get
            {
                return (bool)GetProperty("IsEmpty", true, null);
            }
        }


    }
}
