using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace Enterra.V8x1C.DOM
{
    /// <summary>
    /// ДокументОбъект (DocumentObject)
    /// </summary>
    public class DocumentObject : BaseDocumentProperties, IV8XmlSerializable, ICacheLoadable
    {
        internal DocumentObject(
            Session session, 
            string documentName,
            object ptr
            )
            : base(session, documentName, ptr)
        {
        }

        /// <summary>
        /// Метаданные (Metadata)
        /// </summary>
        public DocumentMetadata Metadata
        {
            get { return Session.Metadata.Documents[ObjectTypeName] as DocumentMetadata; }
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
        /// ПринадлежностьПоследовательностям (BelongingToSequences)
        /// </summary>
        public FixedCollection BelongingToSequences
        {
            get
            {
                return (FixedCollection)GetProperty(
                    "BelongingToSequences",
                    ptr => new FixedCollection(Session, ptr)
                    );
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
        /// Модифицированность (Modified)
        /// </summary>
        public bool Modified
        {
            get
            {
                return (bool)GetProperty("Modified", true, null);
            }
        }

        /// <summary>
        /// МоментВремени (PointOfTime)
        /// </summary>
        public PointOfTime PointOfTime
        {
            get
            {
                return (PointOfTime)GetProperty(
                    "PointOfTime",
                    true,
                    ptr => new PointOfTime(Session, ptr)
                    );
            }
        }

        /// <summary>
        /// ЭтоНовый (IsNew)
        /// </summary>
        public bool IsNew
        {
            get
            {
                return (bool) GetProperty("IsNew", true, null);
            }
        }

        /// <summary>
        /// Движения (Movements)
        /// </summary>
        public FixedCollection Movements
        {
            get
            {
                return (FixedCollection)GetProperty(
                    "Movements",
                    ptr => new FixedCollection(Session, ptr)
                    );
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
        /// Записать (Write)
        /// </summary>
        /// <param name="writeMode"></param>
        /// <param name="postingMode"></param>
        public void Write(DocumentWriteMode writeMode, DocumentPostingMode postingMode)
        {
            InvokeMethod(
                "Write",
                Session.GetV8EnumInstance(writeMode).Ptr,
                Session.GetV8EnumInstance(postingMode).Ptr
                );
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
        public DocumentRef GetNewObjectRef()
        {
            object ptr = InvokeV8Method("GetNewObjectRef");
            return ptr != null ? new DocumentRef(Session, ObjectTypeName, ptr) : null;
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
        public DocumentObject Copy()
        {
            object ptr = InvokeV8Method("Copy");
            return ptr != null ? new DocumentObject(Session, ObjectTypeName, ptr) : null;
        }

        /// <summary>
        /// Удалить (Delete)
        /// </summary>
        public void Delete()
        {
            InvokeV8Method("Delete");
        }

        /// <summary>
        /// УстановитьВремя (SetTime)
        /// </summary>
        public void SetTime()
        {
            InvokeV8Method("SetTime");
        }

        /// <summary>
        /// УстановитьВремя (SetTime)
        /// </summary>
        /// <param name="autoTimeMode"></param>
        /// <param name="useJournals"></param>
        public void SetTime(AutoTimeMode autoTimeMode, bool useJournals)
        {
            InvokeV8Method(
                "SetTime",
                Session.GetV8EnumInstance(autoTimeMode),
                useJournals
                );
        }

        /// <summary>
        /// УстановитьНовыйНомер (SetNewNumber)
        /// </summary>
        /// <param name="numPrefix"></param>
        public void SetNewNumber(string numPrefix)
        {
            InvokeV8Method("SetNewNumber", numPrefix);
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
        /// УстановитьСсылкуНового (SetNewObjectRef)
        /// </summary>
        /// <param name="docRef"></param>
        public void SetNewObjectRef(DocumentRef docRef)
        {
            InvokeV8Method("SetNewObjectRef", docRef.Ptr);
        }


        /// <summary>
        /// Load from V8 xml
        /// </summary>
        /// <param name="node"></param>
        void IV8XmlSerializable.LoadFromV8Xml(System.Xml.XmlNode node)
        {
            XmlElement docTag = (XmlElement) node;

            DocumentMetadata metadata = (DocumentMetadata)this.Metadata;

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
        
        /// <summary>
        /// Load cache
        /// </summary>
        public void LoadCache()
        {
            Session.LoadObjectsCache(new BaseSessionObject[] { this });
        }

        private bool ReadV8XmlAsSysPropertyValue(XmlElement valueTag)
        {
            switch (valueTag.Name)
            {
                case RefPropertyName:
                    return true;
                
                case NumberPropertyName:
                    PutToCache(NumberPropertyName, valueTag.InnerText);
                    return true;

                case DatePropertyName:
                    PutToCache(DatePropertyName, Convert.ToDateTime(valueTag.InnerText));
                    return true;
                
                case DeletionMarkPropertyName:
                case PostedPropertyName:
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
