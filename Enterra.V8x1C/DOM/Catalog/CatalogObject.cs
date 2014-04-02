using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace Enterra.V8x1C.DOM
{
    /// <summary>
    /// СправочникОбъект (CatalogObject)
    /// </summary>
    public class CatalogObject : BaseCatalogProperties, IV8XmlSerializable, ICacheLoadable
    {
        internal CatalogObject(
            Session session,
            string catalogName,
            object ptr)
            : base(session, catalogName, ptr)
        {
        }


        /// <summary>
        /// Предопределенный (Predefined)
        /// </summary>
        public bool Predefined
        {
            get
            {
                return (bool) GetProperty("Predefined");
            }
        }

        /// <summary>
        /// ЭтоГруппа (IsFolder)
        /// </summary>
        public bool IsFolder
        {
            get
            {
                return (bool)GetProperty("IsFolder");
            }
        }

        /// <summary>
        /// Заблокирован (IsLocked)
        /// </summary>
        public bool IsLocked
        {
            get
            {
                return (bool)GetProperty("IsLocked", true, null);
            }
        }

        /// <summary>
        /// ПолноеНаименование (FullDescr)
        /// </summary>
        public string FullDescr
        {
            get
            {
                return GetProperty("FullDescr", true, null) as string;
            }
        }

        /// <summary>
        /// ПолныйКод (FullCode)
        /// </summary>
        public string FullCode
        {
            get
            {
                return GetProperty("FullCode", true, null) as string;
            }
        }

        /// <summary>
        /// Уровень (Level)
        /// </summary>
        public int Level
        {
            get
            {
                return (int)GetProperty("Level", true, null);
            }
        }

        /// <summary>
        /// ЭтоНовый (IsNew)
        /// </summary>
        public bool IsNew
        {
            get
            {
                return (bool)GetProperty("IsNew", true, null);
            }
        }

        /// <summary>
        /// ОбменДанными (DataExchange)
        /// </summary>
        public DataExchangeParameters DataExchange
        {
            get
            {
                return (DataExchangeParameters)GetProperty("DataExchange");
            }
        }


        /// <summary>
        /// Заблокировать (Lock)
        /// </summary>
        public void Lock()
        {
            InvokeMethod("Lock");
        }

        /// <summary>
        /// Записать (Write)
        /// </summary>
        public void Write()
        {
            InvokeMethod("Write");
        }

        /// <summary>
        /// Заполнить (Fill)
        /// </summary>
        /// <param name="value"></param>
        public void Fill(object value)
        {
            if (value is BaseObject)
            {
                value = (value as BaseObject).Ptr;
            }
            InvokeV8Method("Fill", value);
        }

        /// <summary>
        /// ПолучитьСсылкуНового (GetNewObjectRef)
        /// </summary>
        /// <returns></returns>
        public CatalogRef GetNewObjectRef()
        {
            object ptr = InvokeV8Method("GetNewObjectRef");
            return ptr != null ? new CatalogRef(Session, ObjectTypeName, ptr) : null;
        }

        /// <summary>
        /// ПринадлежитЭлементу (BelongsToItem)
        /// </summary>
        /// <param name="catalogRef"></param>
        /// <returns></returns>
        public bool BelongsToItem(CatalogRef catalogRef)
        {
            return (bool) InvokeV8Method("BelongsToItem", catalogRef.Ptr);
        }

        /// <summary>
        /// Прочитать (Read)
        /// </summary>
        public void Read()
        {
            InvokeV8Method("Read");
        }

        /// <summary>
        /// Разблокировать (Unlock)
        /// </summary>
        public void Unlock()
        {
            InvokeV8Method("Unlock");
        }

        /// <summary>
        /// Скопировать (Copy)
        /// </summary>
        /// <returns></returns>
        public CatalogObject Copy()
        {
            object ptr = InvokeV8Method("Copy");
            return ptr != null ? new CatalogObject(Session, ObjectTypeName, ptr) : null;
        }

        /// <summary>
        /// Удалить (Delete)
        /// </summary>
        public void Delete()
        {
            InvokeV8Method("Delete");
        }


        /// <summary>
        /// УстановитьНовыйКод (SetNewCode)
        /// </summary>
        /// <param name="prefixCode"></param>
        public void SetNewCode(string prefixCode)
        {
            InvokeV8Method("SetNewCode", prefixCode);
        }

        /// <summary>
        /// УстановитьПометкуУдаления (SetDeletionMark)
        /// </summary>
        /// <param name="set"></param>
        public void SetDeletionMark(bool set)
        {
            InvokeV8Method("SetDeletionMark", set);
        }

        /// <summary>
        /// УстановитьПометкуУдаления (SetDeletionMark)
        /// </summary>
        /// <param name="set"></param>
        /// <param name="onlyChild"></param>
        public void SetDeletionMark(bool set, bool onlyChild)
        {
            InvokeV8Method("SetDeletionMark", set, onlyChild);
        }

        /// <summary>
        /// УстановитьСсылкуНового (SetNewObjectRef)
        /// </summary>
        /// <param name="catRef"></param>
        public void SetNewObjectRef(CatalogRef catRef)
        {
            InvokeV8Method("SetNewObjectRef", catRef.Ptr);
        }



        /// <summary>
        /// Load cache
        /// </summary>
        public void LoadCache()
        {
            Session.LoadObjectCache(this);
        }



        /// <summary>
        /// Load from V8 xml
        /// </summary>
        /// <param name="node"></param>
        void IV8XmlSerializable.LoadFromV8Xml(System.Xml.XmlNode node)
        {
            XmlElement docTag = (XmlElement)node;

            MetadataObject metadata = this.Metadata;

            foreach (XmlNode nd in docTag.ChildNodes)
            {
                XmlElement valueTag = nd as XmlElement;

                if (valueTag == null)
                {
                    continue;
                }

                if (ReadV8XmlAsSysPropertyValue(valueTag))
                {
                    continue;
                }

                var meta = metadata.Requisites[valueTag.Name];
                if (meta != null)
                {
                    ReadV8XmlAsRequisite(valueTag, meta);
                    continue;
                }

                //TODO:load table parts
            }
        }

        private bool ReadV8XmlAsSysPropertyValue(XmlElement valueTag)
        {
            switch (valueTag.Name)
            {
                case RefPropertyName:
                    return true;

                case CodePropertyName:
                    PutToCache(CodePropertyName, valueTag.InnerText);
                    return true;

                case DescriptionPropertyName:
                    PutToCache(DescriptionPropertyName, valueTag.InnerText);
                    return true;

                case DeletionMarkPropertyName:
                    PutToCache(valueTag.Name, Convert.ToBoolean(valueTag.InnerText));
                    return true;
            }

            return false;
        }

        private void ReadV8XmlAsRequisite(XmlElement valueTag, MetadataObject reqMeta)
        {
            object value;
            if (reqMeta.Type.TryParseV8XmlValue(valueTag, out value))
            {
                PutToCache(valueTag.Name, value);
            }
        }
    }
}
