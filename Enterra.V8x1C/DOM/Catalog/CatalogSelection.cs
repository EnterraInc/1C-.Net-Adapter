using System;
using System.Collections.Generic;
using System.Text;

namespace Enterra.V8x1C.DOM
{
    /// <summary>
    /// СправочникВыборка (CatalogSelection)
    /// </summary>
    public class CatalogSelection : BaseCatalogProperties, ISelection<CatalogObject>
    {
        private readonly CatalogManager _catalogManager;

        internal CatalogSelection(CatalogManager catalogManager, object ptr)
            : base(catalogManager.Session, catalogManager.CatalogName, ptr)
        {
            _catalogManager = catalogManager;
        }

        /// <summary>
        /// CatalogManager
        /// </summary>
        public CatalogManager CatalogManager
        {
            get { return _catalogManager; }
        }

        /// <summary>
        /// Следующий (Next)
        /// </summary>
        /// <returns> Истина - следующий элемент выбран; Ложь - достигнут конец выборки. </returns>
        public bool Next()
        {
            return (bool)InvokeV8Method("Next");
        }

        /// <summary>
        /// ПолучитьОбъект (GetObject)
        /// </summary>
        /// <returns></returns>
        public CatalogObject GetObject()
        {
            return (CatalogObject)GetProperty(
                "GetObject",
                true,
                ptr => new CatalogObject(this.Session, ObjectTypeName, ptr)
                );
        }

        BaseSessionObject ISelection.GetObject()
        {
            return GetObject();
        }
    }
}
