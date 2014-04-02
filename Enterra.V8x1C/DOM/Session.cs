using System;
using System.Collections.Generic;
using System.Xml;
using Enterra.V8x1C.Routines;

namespace Enterra.V8x1C.DOM
{
    /// <summary>
    /// Сессия 1C:Предприятие. Глобальный контекст (Session)
    /// </summary>
    public class Session : BaseObject
    {
        private readonly Connector _connector;
        private readonly string _connectionString;

        /// <summary>
        /// Ключ лицензии
        /// </summary>
        public static string LicenseKey
        {
            set; 
            internal get;
        }


        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="version"></param>
        /// <param name="connectionSettings"></param>
        public Session(V8Version version, ConnectionSettings connectionSettings)
            : this(version, connectionSettings.ToConnectionString())
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="version"></param>
        /// <param name="connectionString"></param>
        public Session(V8Version version, string connectionString)
        {
            System.ComponentModel.LicenseManager.Validate(typeof(Session), this);

            _connector = new Connector(version);

            object v8 = _connector.InvokeV8Method("Connect", connectionString);

            Ptr = v8;

            _connectionString = connectionString;
        }

        /// <summary>
        /// Получить список установленных версий 1С:Предприятие
        /// </summary>
        /// <returns></returns>
        public static V8Version[] GetAvailableVersions()
        {
            V8Version[] versions = (V8Version[])Enum.GetValues(typeof(V8Version));
            List<V8Version> availableVersions = new List<V8Version>();
            foreach (V8Version version in versions)
            {
                try
                {
                    new Connector(version);
                    availableVersions.Add(version);
                }
                catch
                {}
            }

            return availableVersions.ToArray();
        }

        /// <summary>
        /// Версия 1С:Предприятие
        /// </summary>
        public V8Version Version
        {
            get
            {
                return _connector.Version;
            }
        }

        /// <summary>
        /// Строка соединения (ConnectionString)
        /// </summary>
        public string ConnectionString
        {
            get { return _connectionString; }
        }

        /// <summary>
        /// Метаданные (Metadata)
        /// </summary>
        public ConfigurationMetadataObject Metadata
        {
            get
            {
                return (ConfigurationMetadataObject)GetProperty(
                    "Metadata",
                    ptr => new ConfigurationMetadataObject(this, ptr)
                    );
            }
        }

        /// <summary>
        /// Справочники (Catalogs)
        /// </summary>
        public CatalogsManager Catalogs
        {
            get
            {
                return (CatalogsManager)GetProperty(
                    "Catalogs",
                    ptr => new CatalogsManager(this, ptr)
                    );
            }
        }

        /// <summary>
        /// Документы (Documents)
        /// </summary>
        public DocumentsManager Documents
        {
            get
            {
                return (DocumentsManager)GetProperty(
                    "Documents",
                    ptr => new DocumentsManager(this, ptr)
                    );
            }
        }

        /// <summary>
        /// ЖурналыДокументов (DocumentJournalCollection)
        /// </summary>
        public DocumentJournalsManager DocumentJournals
        {
            get
            {
                return (DocumentJournalsManager)GetProperty(
                    "DocumentJournalCollection",
                    ptr => new DocumentJournalsManager(this, ptr)
                    );
            }
        }

        /// <summary>
        /// РегистрыБухгалтерии (AccountingRegisters)
        /// </summary>
        public AccountingRegistersManager AccountingRegisters
        {
            get
            {
                return (AccountingRegistersManager)GetProperty(
                   "AccountingRegisters",
                   ptr => new AccountingRegistersManager(this, ptr)
                   );
            }
        }


        /// <summary>
        /// РегистрыНакопления (AccumulationRegisters)
        /// </summary>
        public AccumulationRegistersManager AccumulationRegisters
        {
            get
            {
                return (AccumulationRegistersManager)GetProperty(
                    "AccumulationRegisters",
                    ptr => new AccumulationRegistersManager(this, ptr)
                    );
            }
        }

        
        /// <summary>
        /// РегистрыСведений (InformationRegisters)
        /// </summary>
        public InformationRegistersManager InformationRegisters
        {
            get
            {
                return (InformationRegistersManager)GetProperty(
                    "InformationRegisters", 
                    ptr => new InformationRegistersManager(this, ptr)
                    );
            }
        }

        /// <summary>
        /// РегистрыРасчета (CalculationRegisters)
        /// </summary>
        public CalculationRegistersManager CalculationRegisters
        {
            get
            {
                return (CalculationRegistersManager)GetProperty(
                    "CalculationRegisters",
                    ptr => new CalculationRegistersManager(this, ptr)
                    );
            }
        }

        /// <summary>
        /// Перечисления (Enums)
        /// </summary>
        public EnumsManager Enums
        {
            get
            {
                return (EnumsManager)GetProperty(
                    "Enums",
                    ptr => new EnumsManager(this, ptr)
                    );
            }
        }

        /// <summary>
        /// Константы (Constants)
        /// </summary>
        public ConstantsManager Constants
        {
            get
            {
                return (ConstantsManager)GetProperty(
                    "Constants",
                    ptr => new ConstantsManager(this, ptr)
                    );
            }
        }

        /// <summary>
        /// ЗаписатьXML (WriteXML)
        /// </summary>
        /// <param name="xmlWriter"></param>
        /// <param name="value"></param>
        public void WriteXML(XMLWriter xmlWriter, object value)
        {
            if (value is BaseObject)
            {
                value = (value as BaseObject).Ptr;
            }

            InvokeV8Method("ЗаписатьXML", xmlWriter.Ptr, value);
        }
        
        /// <summary>
        /// Load object cache
        /// </summary>
        public void LoadObjectCache(BaseSessionObject sessionObject)
        {
            CheckSession(sessionObject);

            if (sessionObject is IV8XmlSerializable)
            {
                XmlDocument doc = new XmlDocument();
                var root = doc.CreateElement("root");
                root.InnerXml = ToXML(this);

                (sessionObject as IV8XmlSerializable).LoadFromV8Xml(root.FirstChild);
            }
            else if (sessionObject is IV8Serializable)
            {
                string str = ValueToStringInternal(sessionObject.Ptr);
                (sessionObject as IV8Serializable).LoadFromV8String(str);
            }
        }

        /// <summary>
        /// Load objects cache
        /// </summary>
        /// <param name="sessionObjects"></param>
        public void LoadObjectsCache(IEnumerable<BaseSessionObject> sessionObjects)
        {
            var enmrtr = sessionObjects.GetEnumerator();

            XMLWriter xmlWriter = null;
            List<BaseSessionObject> xmlLoadedObjects = null;


            while (enmrtr.MoveNext())
            {
                var sessionObject = enmrtr.Current;

                CheckSession(sessionObject);

                if (sessionObject is IV8XmlSerializable)
                {
                    if (xmlWriter == null)
                    {
                        xmlWriter = new XMLWriter(this);
                        xmlWriter.SetString();
                        xmlLoadedObjects = new List<BaseSessionObject>();
                    }

                    WriteXML(xmlWriter, sessionObject);
                    xmlLoadedObjects.Add(sessionObject);
                }
                else if (sessionObject is IV8Serializable)
                {
                    string str = ValueToStringInternal(sessionObject.Ptr);
                    (sessionObject as IV8Serializable).LoadFromV8String(str);
                }
            }

            if (xmlWriter != null)
            {
                string xml = xmlWriter.Close();

                XmlDocument doc = new XmlDocument();
                var root = doc.CreateElement("root");
                root.InnerXml = xml;

                int objectId = 0;
                foreach (var node in root.ChildNodes)
                {
                    if (!(node is XmlElement))
                    {
                        continue;
                    }

                    XmlElement objectTag = (XmlElement) node;
                    BaseSessionObject v8Object = xmlLoadedObjects[objectId++];

                    (v8Object as IV8XmlSerializable).LoadFromV8Xml(objectTag);
                }
            }
        }

        /// <summary>
        /// Selection to objects list
        /// </summary>
        /// <param name="selection"></param>
        /// <returns></returns>
        public static List<BaseSessionObject> SelectionToObjects(ISelection selection)
        {
            List<BaseSessionObject> buffer = new List<BaseSessionObject>();
            while (selection.Next())
            {
                var docObj = selection.GetObject();
                buffer.Add(docObj);
            }
            return buffer;
        }

        /// <summary>
        /// В XML
        /// </summary>
        /// <param name="value"></param>
        public string ToXML(object value)
        {
            XMLWriter xmlWriter = new XMLWriter(this);
            xmlWriter.SetString();
            WriteXML(xmlWriter, value);
            return xmlWriter.Close();
        }
        
        /// <summary>
        /// ЗначениеВСтрокуВнутр (ValueToStringInternal)
        /// </summary>
        /// <returns></returns>
        internal string ValueToStringInternal(object ptr)
        {
            return InvokeV8Method("ValueToStringInternal", ptr) as string;
        }

        /// <summary>
        /// ЗначениеИзСтрокиВнутр (ValueFromStringInternal)
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        internal object ValueFromStringInternal(string str)
        {
            return InvokeV8Method("ValueFromStringInternal", str);
        }

        /// <summary>
        /// Получить COM инстанс для Enum
        /// </summary>
        /// <param name="enumValue"></param>
        /// <returns></returns>
        internal BaseSessionObject GetV8EnumInstance(Enum enumValue)
        {
            Type enumType = enumValue.GetType();
            object[] attrs = enumType.GetCustomAttributes(typeof (V8TypeCodeAttribute), true);
            if (attrs == null || attrs.Length == 0)
            {
                throw new ArgumentException(string.Format("Тип {0} не имеет соответствующего типа V8", enumType.FullName));
            }

            V8TypeCodeAttribute v8TypeCodeAttr = (V8TypeCodeAttribute) attrs[0];

            string internalStr = "{\"#\"," + v8TypeCodeAttr.Code + "," + Convert.ToInt32(enumValue) + "}";
            
            object ptr = ValueFromStringInternal(internalStr);

            return ptr != null ? new EnumObject(this, ptr) : null;
        }

        /// <summary>
        /// Создать (NewObject)
        /// </summary>
        /// <param name="newObjectType"></param>
        /// <returns></returns>
        internal object NewObject(string newObjectType)
        {
            return InvokeV8Method("NewObject", newObjectType);
        }
        /// <summary>
        /// Создать (NewObject)
        /// </summary>
        /// <param name="newObjectType"></param>
        /// <param name="arg1"></param>
        /// <returns></returns>
        internal object NewObject(string newObjectType, object arg1)
        {
            return InvokeV8Method("NewObject", newObjectType, arg1);
        }

        private void CheckSession(BaseSessionObject sessionObject)
        {
            if (sessionObject.Session != this)
            {
                throw new Exception("Property 'Session' must be equal current session");
            }
        }

        class Connector : BaseObject
        {
            private readonly V8Version _version;

            public Connector(V8Version version)
            {
                string progId;

                switch(version)
                {
                    case V8Version.V80:
                        progId = "V8.ComConnector";
                        break;
                    case V8Version.V81:
                        progId = "V81.ComConnector";
                        break;
                    case V8Version.V82:
                        progId = "V82.ComConnector";
                        break;
                    default:
                        throw new NotImplementedException();
                }

                Type comType = Type.GetTypeFromProgID(progId, true);
                Ptr = Activator.CreateInstance(comType);
                _version = version;
            }

            public V8Version Version
            {
                get
                {
                    return _version;
                }
            }
        }

        class EnumObject : BaseSessionObject
        {
            public EnumObject(Session session, object ptr)
                : base(session, ptr)
            {
            }
        }
    }
}
