using System;

namespace Enterra.V8x1C.DOM
{
    /// <summary>
    /// ОбъектМетаданныхКонфигурация (ConfigurationMetadataObject)
    /// </summary>
    public class ConfigurationMetadataObject : BaseSessionObject
    {
        internal ConfigurationMetadataObject(Session session, object ptr)
            : base(session, ptr)
        {
        }

        /// <summary>
        /// Метаданные документов
        /// </summary>
        public MetadataObjectCollection Documents
        {
            get
            {

                return (MetadataObjectCollection)GetProperty(
                    "Documents",
                    ptr => new MetadataObjectCollection(this.Session, MetadataType.DocumentCollection, ptr)
                    );
            }
        }

        /// <summary>
        /// Метаданные справочников
        /// </summary>
        public MetadataObjectCollection Catalogs
        {
            get
            {
                return (MetadataObjectCollection)GetProperty(
                   "Catalogs",
                   ptr => new MetadataObjectCollection(this.Session, MetadataType.CatalogCollection, ptr)
                   );
            }
        }

        /// <summary>
        /// Метаданные регистров сведений
        /// </summary>
        public MetadataObjectCollection InformationRegisters
        {
            get
            {
                return (MetadataObjectCollection)GetProperty(
                   "InformationRegisters",
                   ptr => new MetadataObjectCollection(this.Session, MetadataType.InformationRegisterCollection, ptr)
                   );
            }
        }
        
        /// <summary>
        /// Метаданные регистров накоплений
        /// </summary>
        public MetadataObjectCollection AccumulationRegisters
        {
            get
            {
                return (MetadataObjectCollection)GetProperty(
                   "AccumulationRegisters",
                   ptr => new MetadataObjectCollection(this.Session, MetadataType.AccumulationRegisterCollection, ptr)
                   );
            }
        }
        
        /// <summary>
        /// Метаданные регистров бухгалтерии
        /// </summary>
        public MetadataObjectCollection AccountingRegisters
        {
            get
            {
                return (MetadataObjectCollection)GetProperty(
                   "AccountingRegisters",
                   ptr => new MetadataObjectCollection(this.Session, MetadataType.AccountingRegisterCollection, ptr)
                   );
            }
        }

        
        /// <summary>
        /// Метаданные регистров расчета
        /// </summary>
        public MetadataObjectCollection CalculationRegisters
        {
            get
            {
                return (MetadataObjectCollection)GetProperty(
                   "CalculationRegisters",
                   ptr => new MetadataObjectCollection(this.Session, MetadataType.CalculationRegisterCollection, ptr)
                   );
            }
        }

        /// <summary>
        /// Метаданные перечислений
        /// </summary>
        public MetadataObjectCollection Enums
        {
            get
            {
                return (MetadataObjectCollection)GetProperty(
                   "Enums",
                   ptr => new MetadataObjectCollection(this.Session, MetadataType.EnumCollection, ptr)
                   );
            }
        }

        /// <summary>
        /// Метаданные журналов документов
        /// </summary>
        public MetadataObjectCollection DocumentJournals
        {
            get
            {
                return (MetadataObjectCollection)GetProperty(
                  "DocumentJournals",
                  ptr => new MetadataObjectCollection(this.Session, MetadataType.DocumentJournalCollection, ptr)
                  );
            }
        }

        /// <summary>
        /// Метаданные констант
        /// </summary>
        public MetadataObjectCollection Constants
        {
            get
            {
                return (MetadataObjectCollection)GetProperty(
                  "Constants",
                  ptr => new MetadataObjectCollection(this.Session, MetadataType.ConstantCollection, ptr)
                  );
            }
        }

        /// <summary>
        /// НайтиПоТипу (FindByType)
        /// </summary>
        /// <param name="typeV8"></param>
        /// <returns></returns>
        public MetadataObject FindByType(TypeV8 typeV8)
        {
            object ptr = InvokeV8Method("НайтиПоТипу", typeV8.Ptr);
            if (ptr == null)
            {
                return null;
            }
            return MetadataObject.Create(Session, ptr);
        }

    }
}
