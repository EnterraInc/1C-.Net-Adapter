using System;

namespace Enterra.V8x1C.DOM
{
    /// <summary>
    /// СправочникМенеджер (CatalogManager)
    /// </summary>
    public class CatalogManager : BaseSessionObject
    {
        private readonly string _catalogName;
        private readonly CatalogsManager _catalogsManager;

        internal CatalogManager(CatalogsManager catalogsManager, string catalogName, object ptr)
            : base(catalogsManager.Session, ptr)
        {
            _catalogsManager = catalogsManager;
            _catalogName = catalogName;
        }

        /// <summary>
        /// Catalog name
        /// </summary>
        public string CatalogName
        {
            get { return _catalogName; }
        }

        /// <summary>
        /// Catalogs manager
        /// </summary>
        public CatalogsManager CatalogsManager
        {
            get { return _catalogsManager; }
        }

        

        /// <summary>
        /// Метаданные документа
        /// </summary>
        public MetadataObject CatalogMetadata
        {
            get
            {
                return Session.Metadata.Documents[CatalogName];
            }
        }

        /// <summary>
        /// Выбрать (Select)
        /// </summary>
        public CatalogSelection Select()
        {
            return Select(null);
        }

        /// <summary>
        /// Выбрать (Select)
        /// </summary>
        /// <param name="order">
        /// Строка с именем реквизита справочника, определяющая упорядочивание элементов в выборке. Может быть указано "Код", "Наименование" или имя одного из реквизитов примитивного типа (Число, Строка, Дата, Булево), для которого установлен признак "Индексирование" в значение "Индексировать" или в "Индексировать с дополнительным упорядочиванием" в конфигураторе. После имени реквизита через пробел может быть указано направление сортировки. Направление определяется: "Убыв" ("Desc") - упорядочивать по убыванию; "Возр" ("Asc") - упорядочивать по возрастанию. По умолчанию сортировка производится по возрастанию. Если параметр не указан, то порядок определяется основным представлением справочника.
        /// </param>
        /// <returns></returns>
        public CatalogSelection Select(string order)
        {
            object ptr = InvokeV8Method("Выбрать", null, null, null, order);

            if (ptr == null)
            {
                return null;
            }

            return new CatalogSelection(this, ptr);
        }

        /// <summary>
        /// ВыбратьИерархически (SelectHierarchically)
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        public CatalogSelection SelectHierarchically(string order)
        {
            object ptr = InvokeV8Method("ВыбратьИерархически", null, null, null, order);

            if (ptr == null)
            {
                return null;
            }

            return new CatalogSelection(this, ptr);
        }

        /// <summary>
        /// НайтиПоКоду (FindByCode)
        /// </summary>
        /// <param name="code"></param>
        /// <param name="byFullCode"></param>
        /// <param name="parent"></param>
        /// <param name="owner"></param>
        /// <returns></returns>
        public CatalogRef FindByCode(object code, bool byFullCode, CatalogRef parent, CatalogRef owner)
        {
            object ptr = InvokeV8Method("FindByCode", 
                code, 
                byFullCode,
                parent != null ? parent.Ptr : null, 
                owner != null ? owner.Ptr : null
                );

            return ptr != null ? new CatalogRef(Session, CatalogName, ptr) : null;
        }

        /// <summary>
        /// НайтиПоКоду (FindByCode)
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public CatalogRef FindByCode(object code)
        {
            object ptr = InvokeV8Method("FindByCode", code);
            return ptr != null ? new CatalogRef(Session, CatalogName, ptr) : null;
        }

        /// <summary>
        /// НайтиПоНаименованию (FindByDescription)
        /// </summary>
        /// <param name="desc"></param>
        /// <returns></returns>
        public CatalogRef FindByDescription(string desc)
        {
            object ptr = InvokeV8Method("FindByDescription", desc);
            return ptr != null ? new CatalogRef(Session, CatalogName, ptr) : null;
        }

        /// <summary>
        /// НайтиПоНаименованию (FindByDescription)
        /// </summary>
        /// <param name="desc"></param>
        /// <param name="exactlyEqual"></param>
        /// <param name="parent"></param>
        /// <param name="owner"></param>
        /// <returns></returns>
        public CatalogRef FindByDescription(string desc, bool exactlyEqual, CatalogRef parent, CatalogRef owner)
        {
            object ptr = InvokeV8Method("FindByDescription",
               desc,
               exactlyEqual,
               parent != null ? parent.Ptr : null,
               owner != null ? owner.Ptr : null
               );
            return ptr != null ? new CatalogRef(Session, CatalogName, ptr) : null;
        }

        /// <summary>
        /// НайтиПоРеквизиту (FindByAttribute)
        /// </summary>
        /// <param name="attributeName"></param>
        /// <param name="value"></param>
        /// <param name="parent"></param>
        /// <param name="owner"></param>
        /// <returns></returns>
        public CatalogRef FindByAttribute(string attributeName, object value, CatalogRef parent, CatalogRef owner)
        {
            object ptr = InvokeV8Method("FindByAttribute",
               attributeName,
               value,
               parent != null ? parent.Ptr : null,
               owner != null ? owner.Ptr : null
               );
            return ptr != null ? new CatalogRef(Session, CatalogName, ptr) : null;
        }

        /// <summary>
        /// НайтиПоРеквизиту (FindByAttribute)
        /// </summary>
        /// <param name="attributeName"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public CatalogRef FindByAttribute(string attributeName, object value)
        {
            object ptr = InvokeV8Method("FindByAttribute",
               attributeName,
               value
               );
            return ptr != null ? new CatalogRef(Session, CatalogName, ptr) : null;
        }

        /// <summary>
        /// СоздатьГруппу (CreateFolder)
        /// </summary>
        /// <returns></returns>
        public CatalogObject CreateFolder()
        {
            object ptr = InvokeV8Method("CreateFolder");
            return new CatalogObject(Session, CatalogName, ptr);
        }

        /// <summary>
        /// СоздатьЭлемент (CreateItem)
        /// </summary>
        /// <returns></returns>
        public CatalogObject CreateItem()
        {
            object ptr = InvokeV8Method("CreateItem");
            return new CatalogObject(Session, CatalogName, ptr);
        }


        /// <summary>
        /// ПолучитьСсылку (GetRef)
        /// </summary>
        /// <param name="uuid"></param>
        /// <returns></returns>
        public CatalogRef GetRef(UUID uuid)
        {
            object ptr = GetRefInternal(uuid);

            if (ptr == null)
            {
                return null;
            }
            else
            {
                return new CatalogRef(Session, CatalogName, ptr);
            }
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
