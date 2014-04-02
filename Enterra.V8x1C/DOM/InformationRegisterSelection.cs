using System;
using System.Collections.Generic;
using System.Text;

namespace Enterra.V8x1C.DOM
{
    /// <summary>
    /// РегистрСведенийВыборка (InformationRegisterSelection)
    /// </summary>
    public class InformationRegisterSelection : BaseSessionObject
    {
        internal InformationRegisterSelection(Session session, object ptr)
            : base(session, ptr)
        {
        }

        /// <summary>
        /// Активность (Active)
        /// </summary>
        public bool Active
        {
            get
            {
                return (bool)GetProperty("Active");
            }
        }

        /// <summary>
        /// НомерСтроки (LineNumber)
        /// </summary>
        public int LineNumber
        {
            get
            {
                return (int)GetProperty("LineNumber");
            }
        }


        /// <summary>
        /// Период (Period)
        /// </summary>
        public DateTime Period
        {
            get
            {
                return (DateTime)GetProperty("Period");
            }
        }

        /// <summary>
        /// Регистратор (Recorder)
        /// </summary>
        public DocumentRef Recorder
        {
            get
            {
                return (DocumentRef)GetProperty("Recorder");
            }
        }

        /// <summary>
        /// Получить значение измерения, значение реквизита, значение ресурса.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public object this[string name]
        {
            get
            {
                return GetProperty(name);
            }
        }


        /// <summary>
        /// Следующий (Next)
        /// </summary>
        /// <returns></returns>
        public bool Next()
        {
            return (bool)InvokeMethod("Next");
        }

    }
}
