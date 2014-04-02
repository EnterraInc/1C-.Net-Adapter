using System;


namespace Enterra.V8x1C.DOM
{

    /// <summary>
    /// СправочникиМенеджер (CatalogsManager)
    /// </summary>
    public class CatalogsManager : BaseSessionObject
    {
        internal CatalogsManager(Session session, object ptr)
            : base(session, ptr)
        {}

        /// <summary>
        /// СправочникМенеджер (CatalogManager)
        /// </summary>
        /// <param name="catalogName"></param>
        /// <returns></returns>
        public CatalogManager this[string catalogName]
        {
            get
            {
                return (CatalogManager)GetProperty(catalogName, ptr => new CatalogManager(this, catalogName, ptr));
            }
        }
    }
}
