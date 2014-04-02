using System;
using System.Collections.Generic;
using System.Text;

namespace Enterra.V8x1C.DOM
{
    /// <summary>
    /// ПеречислениеСсылка (EnumRef)
    /// </summary>
    public class EnumRef : BaseSessionObject, IObjectRef
    {
        private readonly string _enumName;

        internal EnumRef(Session session, string enumName, object ptr)
            : base(session, ptr)
        {
            _enumName = enumName;
        }

        internal EnumRef(Session session, string enumName, string valueName)
            : base(session, null)
        {
            _enumName = enumName;

            ValueName = valueName;
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

        /// <summary>
        /// Имя значения перечисления
        /// </summary>
        public string ValueName
        {
            get
            {
                return GetFromCache("ValueName_1212rqwrqw21e1",
                    GetEnumValueName
                    ) 
                    as string;
            }
            private set
            {
                PutToCache("ValueName_1212rqwrqw21e1", value);
            }
        }

        #region IObjectRef Members

        UUID IObjectRef.Uuid
        {
            get
            {
                return null;
            }
        }

        object IObjectRef.Code
        {
            get { return ValueName; }
        }

        /// <summary>
        /// Метаданные (Metadata)
        /// </summary>
        public MetadataObject Metadata
        {
            get
            {
                return Session.Metadata.Enums[_enumName];
            }
        }

        #endregion

        public override string ToString()
        {
            return string.Format("{0}, {1}", _enumName, ValueName);
        }

        protected override void OnPtrRequired()
        {
            string valueName = this.ValueName;

            if (valueName == null)
            {
                return;
            }

            var enumManager = Session.Enums[_enumName];
            if (enumManager == null)
            {
                return;
            }
            var refV = enumManager[valueName];
            if (refV == null)
            {
                return;
            }
            Ptr = refV.Ptr;
        }

        private string GetEnumValueName()
        {
            var xml = Session.ToXML(Ptr);
            using (var xmlReader = System.Xml.XmlReader.Create(new System.IO.StringReader(xml)))
            {
                xmlReader.ReadStartElement();
                return xmlReader.ReadString();
            }
        }
    }
}
