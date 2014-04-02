using System;
using System.Collections.Generic;
using System.Text;

namespace Enterra.V8x1C.DOM
{
    /// <summary>
    /// ПараметрыОбменаДанными (DataExchangeParameters)
    /// </summary>
    public class DataExchangeParameters : BaseSessionObject
    {
        internal DataExchangeParameters(Session session, object ptr)
            : base(session, ptr)
        {
        }

        /// <summary>
        /// Загрузка (Load)
        /// </summary>
        public bool Load
        {
            get
            {
                return (bool)GetProperty("Load");
            }
        }

        /// <summary>
        /// Отправитель (Sender)
        /// </summary>
        public ExchangePlanRef Sender
        {
            get
            {
                return (ExchangePlanRef)GetProperty(
                    "Sender",
                    ptr => new ExchangePlanRef(Session, ptr)
                    );
            }
            set
            {
                SetProperty("Sender", value);
            }
        }
    }
}
