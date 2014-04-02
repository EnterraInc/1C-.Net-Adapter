using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace Enterra.V8x1C.Routines
{
    /// <summary>
    /// Настройки соединения с 1С:Предприятие
    /// </summary>
    [Serializable]
    public struct ConnectionSettings
    {
        [XmlIgnore]
        private ConnectionType _connectionType;
        [XmlIgnore]
        private string _server;
        [XmlIgnore]
        private string _file;
        [XmlIgnore]
        private string _base;
        [XmlIgnore]
        private string _userName;
        [XmlIgnore]
        private string _password;

        /// <summary>
        /// Тип соединения
        /// </summary>
        [XmlAttribute]
        public ConnectionType ConnectionType
        {
            get { return _connectionType; }
            set { _connectionType = value; }
        }

        /// <summary>
        /// Путь к каталогу базы
        /// </summary>
        [XmlAttribute]
        public string File
        {
            get { return _file; }
            set { _file = value; }
        }

        /// <summary>
        /// Имя БД (для подключения к серверу)
        /// </summary>
        [XmlAttribute]
        public string Base
        {
            get { return _base; }
            set { _base = value; }
        }

        /// <summary>
        /// Сервер
        /// </summary>
        [XmlAttribute]
        public string Server
        {
            get { return _server; }
            set { _server = value; }
        }

        /// <summary>
        /// Имя пользователя
        /// </summary>
        [XmlAttribute]
        public string UserName
        {
            get { return _userName;}
            set { _userName = value; }
        }

        /// <summary>
        /// Пароль
        /// </summary>
        [XmlAttribute]
        public string Password
        {
            get { return _password; }
            set { _password = value; }
        }


        /// <summary>
        /// Путь к источнику не задан
        /// </summary>
        [XmlIgnore]
        public bool SourceIsEmpty
        {
            get
            {
                return (ConnectionType == ConnectionType.File && string.IsNullOrEmpty(File)) ||
                       (ConnectionType == ConnectionType.Server &&
                        (string.IsNullOrEmpty(Base) || string.IsNullOrEmpty(Server))
                        );
            }
        }

        /// <summary>
        /// Получить строку соединения
        /// </summary>
        /// <returns></returns>
        public string ToConnectionString()
        {
            string connectionString = string.Empty;

            switch (ConnectionType)
            {
                case ConnectionType.File:
                    connectionString = string.Format("File=\"{0}\";Usr=\"{1}\";Pwd=\"{2}\";", File, UserName, Password);
                    break;
                case ConnectionType.Server:
                    connectionString = string.Format("Srvr=\"{0}\";Ref=\"{1}\";Usr=\"{2}\";Pwd=\"{3}\";", Server, Base, UserName, Password);
                    break;
            }

            return connectionString;
        }
    }
}
