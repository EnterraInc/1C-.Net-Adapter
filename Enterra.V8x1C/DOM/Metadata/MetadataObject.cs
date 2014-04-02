using System;

namespace Enterra.V8x1C.DOM
{
    /// <summary>
    /// ОбъектМетаданных (MetadataObject)
    /// </summary>
    public class MetadataObject : MetadataBase
    {
        public const string FullNameProperty = "FullName";


        internal MetadataObject(Session session, MetadataType metadataType, object ptr)
            : base(session, metadataType, ptr)
        { }

        /// <summary>
        /// Create
        /// </summary>
        /// <param name="session"></param>
        /// <param name="ptr"></param>
        /// <returns></returns>
        internal static MetadataObject Create(Session session, object ptr)
        {
            string fullName = GetV8Property(ptr, FullNameProperty) as string;

            MetadataObject meta;

            if (fullName.StartsWith(RussianConsts.Catalog))
            {
                meta = new MetadataObject(session, MetadataType.Catalog, ptr);
            }
            else if (fullName.StartsWith(RussianConsts.Document))
            {
                meta = new DocumentMetadata(session, ptr);
            }
            else
            {
                meta = new MetadataObject(session, MetadataType.Unknown, ptr);
            }

            meta.PutToCache(FullNameProperty, fullName);
            
            return meta;
        }

        /// <summary>
        /// Тип
        /// </summary>
        public TypeDescription Type
        {
            get
            {
                return (TypeDescription)GetProperty("Тип", ptr => new TypeDescription(Session, ptr));
            }
        }
        
        /// <summary>
        /// Имя (Name)
        /// </summary>
        public override string Name
        {
            get
            {
                return GetProperty("Name") as string;
            }
        }

        /// <summary>
        /// ПолноеИмя (FullName)
        /// </summary>
        public string FullName
        {
            get
            {
                return GetProperty(FullNameProperty, true, null) as string;
            }
        }

        /// <summary>
        /// Представление (Синоним) (Presentation)
        /// </summary>
        public string Presentation
        {
            get
            {
                return GetProperty("Представление", true, null) as string;
            }
        }

        /// <summary>
        /// Имеет реквизиты
        /// </summary>
        public bool HasRequisites
        {
            get
            {
                switch (MetadataType)
                {
                    case MetadataType.Document:
                    case MetadataType.Catalog:
                    case MetadataType.InformationRegister:
                        return true;
                }
                return false;
            }
        }

        /// <summary>
        /// Имеет табличные части
        /// </summary>
        public bool HasTableParts
        {
            get
            {
                switch (MetadataType)
                {
                    case MetadataType.Document:
                    case MetadataType.Catalog:
                        return true;
                }
                return false;
            }
        }

        /// <summary>
        /// Реквизиты. (Только для метаданных справочника, документа)
        /// </summary>
        public MetadataObjectCollection Requisites
        {
            get
            {
                if (HasRequisites)
                {

                    return (MetadataObjectCollection) GetProperty(
                                                          RussianConsts.Requisites,
                                                          ptr =>
                                                          new MetadataObjectCollection(this.Session,
                                                                                       DOM.MetadataType.
                                                                                           RequisiteCollection, ptr)
                                                          );
                }

                return null;
            }
        }

        /// <summary>
        /// Табличные части. (Только для метаданных справочника, документа)
        /// </summary>
        public MetadataObjectCollection TableParts
        {
            get
            {
                if (HasTableParts)
                {
                    return (MetadataObjectCollection) GetProperty(
                                                          RussianConsts.TableParts,
                                                          ptr =>
                                                          new MetadataObjectCollection(this.Session,
                                                                                       DOM.MetadataType.
                                                                                           TablePartCollection, ptr)
                                                          );
                }

                return null;
            }
        }
    }
}
