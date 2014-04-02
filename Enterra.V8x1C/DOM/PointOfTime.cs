using System;
using System.Collections.Generic;
using System.Text;

namespace Enterra.V8x1C.DOM
{
    /// <summary>
    /// МоментВремени (PointOfTime)
    /// </summary>
    public class PointOfTime : BaseSessionObject, IComparable<PointOfTime>
    {
        internal PointOfTime(Session session, object ptr)
            : base(session, ptr)
        {
        }

        public PointOfTime(Session session, DateTime date)
            :base(session, null)
        {
            Ptr = session.NewObject("PointOfTime", date);
        }

        /// <summary>
        /// Дата (Date)
        /// </summary>
        public DateTime Date
        {
            get
            {
                return (DateTime)GetProperty("Date");
            }
        }

        /// <summary>
        /// Ссылка (Ref)
        /// </summary>
        public object Ref
        {
            get
            {
                return GetProperty("Ref");
            }
        }



        #region IComparable<PointOfTime> Members

        /// <summary>
        /// Compare
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public int CompareTo(PointOfTime other)
        {
            return (int)InvokeV8Method("Compare", other.Ptr);
        }

        #endregion
    }
}
