using System;
using System.Collections.Generic;
using System.Text;

namespace Enterra.V8x1C.DOM
{
    /// <summary>
    /// ДокументМенеджер (DocumentManager)
    /// </summary>
    public class DocumentManager : BaseSessionObject
    {
        private readonly string _documentName;
        private readonly DocumentsManager _documentsManager;

        internal DocumentManager(DocumentsManager documentsManager, string documentName, object ptr)
            : base(documentsManager.Session, ptr)
        {
            _documentsManager = documentsManager;
            _documentName = documentName;
        }

        /// <summary>
        /// Document name
        /// </summary>
        public string DocumentName
        {
            get
            {
                return _documentName;
            }
        }

        /// <summary>
        /// Метаданные документа
        /// </summary>
        public MetadataObject DocumentMetadata
        {
            get
            {
                return Session.Metadata.Documents[_documentName];
            }
        }

        /// <summary>
        /// Documents manager
        /// </summary>
        public DocumentsManager DocumentsManager
        {
            get
            {
                return _documentsManager;
            }
        }
        
        /// <summary>
        /// ПустаяСсылка
        /// </summary>
        public DocumentRef EmptyRef
        {
            get
            {
                return (DocumentRef)GetProperty(
                    "EmptyRef",
                    true,
                    ptr => new DocumentRef(Session, _documentName, ptr)
                    );
            }
        }

        /// <summary>
        /// Выбрать (Select)
        /// </summary>
        public DocumentSelection Select()
        {
            return Select(null);
        }
        
        /// <summary>
        /// Выбрать (Select)
        /// </summary>
        /// <param name="order">
        /// Строка с именем реквизита документа, определяющая упорядочивание документов в выборке. Может быть указано поле "Дата" или имя реквизита документа, для которого признак индексирования в конфигураторе установлен в значения "Индексировать" или "Индексировать с доп. упорядочиванием". После указания имени через пробел может быть указано направление сортировки. Направление определяется: "Убыв" ("Desc") - упорядочивать по убыванию, и "Возр" ("Asc") - упорядочивать по возрастанию. По умолчанию выборка упорядочивается по возрастанию.
        /// </param>
        /// <returns></returns>
        public DocumentSelection Select(string order)
        {
            object ptr = InvokeV8Method("Выбрать", null, null, null, order);

            if (ptr == null)
            {
                return null;
            }

            return new DocumentSelection(this, ptr);
        }

        /// <summary>
        /// ПолучитьСсылку (GetRef)
        /// </summary>
        /// <param name="uuid"></param>
        /// <returns></returns>
        public DocumentRef GetRef(UUID uuid)
        {
            object ptr = GetRefInternal(uuid);

            if (ptr == null)
            {
                return null;
            }
            else
            {
                return new DocumentRef(Session, DocumentName, ptr);
            }
        }

        /// <summary>
        /// НайтиПоНомеру (FindByNumber)
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public DocumentRef FindByNumber(object number)
        {
            object ptr = InvokeV8Method("FindByNumber", number);
            return ptr != null ? new DocumentRef(Session, DocumentName, ptr) : null;
        }

        /// <summary>
        /// НайтиПоНомеру (FindByNumber)
        /// </summary>
        /// <param name="number"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        public DocumentRef FindByNumber(object number, DateTime date)
        {
            object ptr = InvokeV8Method("FindByNumber", number, date);
            return ptr != null ? new DocumentRef(Session, DocumentName, ptr) : null;
        }

        /// <summary>
        /// НайтиПоРеквизиту (FindByAttribute)
        /// </summary>
        /// <param name="attributeName"></param>
        /// <param name="value"></param>
        public DocumentRef FindByAttribute(string attributeName, object value)
        {
            if (value is BaseObject)
            {
                value = (value as BaseObject).Ptr;
            }
            object ptr = InvokeV8Method("FindByAttribute", attributeName, value);
            return ptr != null ? new DocumentRef(Session, DocumentName, ptr) : null;
        }

        /// <summary>
        /// СоздатьДокумент (CreateDocument)
        /// </summary>
        /// <returns></returns>
        public DocumentObject CreateDocument()
        {
            object ptr = InvokeV8Method("CreateDocument");
            return new DocumentObject(Session, DocumentName, ptr);
        }


        /// <summary>
        /// ПолучитьСсылку (GetRef)
        /// </summary>
        /// <param name="uuid"></param>
        /// <returns></returns>
        internal object GetRefInternal(UUID uuid)
        {
            return InvokeV8Method("ПолучитьСсылку", uuid == null ? null : uuid.Ptr);
        }
    }
}
