using System;
using System.Collections;
using System.Collections.Generic;

namespace Enterra.V8x1C.DOM
{
    /// <summary>
    /// КоллекцияОбъектовМетаданных (MetadataObjectCollection)
    /// </summary>
    public class MetadataObjectCollection : MetadataBase, IEnumerable<MetadataObject>
    {
        internal MetadataObjectCollection(Session session, MetadataType metadataType, object ptr)
            : base(session, metadataType, ptr)
        {}

        internal MetadataObjectCollection(Session session, object ptr)
            : base(session, ptr)
        {}

        /// <summary>
        /// Имя (Name)
        /// </summary>
        public override string Name
        {
            get
            {
                switch (MetadataType)
                {
                    case MetadataType.DocumentCollection:
                        return RussianConsts.Documents;
                        
                    case MetadataType.CatalogCollection:
                        return RussianConsts.Catalogs;
                        
                    case MetadataType.RequisiteCollection:
                        return RussianConsts.Requisites;
                        
                    case MetadataType.InformationRegisterCollection:
                        return RussianConsts.InformationRegisters;

                    case MetadataType.AccumulationRegisterCollection:
                        return RussianConsts.AccumulationRegisters;

                    case MetadataType.AccountingRegisterCollection:
                        return RussianConsts.AccountingRegisters;

                    case MetadataType.CalculationRegisterCollection:
                        return RussianConsts.CalculationRegisters;

                    case MetadataType.EnumCollection:
                        return RussianConsts.Enums;

                    case MetadataType.MeasureCollection:
                        return RussianConsts.Measures;
                    
                    case MetadataType.ResourceCollection:
                        return RussianConsts.Resources;

                    case MetadataType.DocumentJournalCollection:
                        return RussianConsts.DocumentJournals;

                    case MetadataType.ConstantCollection:
                        return RussianConsts.Constants;
                }

                return base.Name;
            }
        }

        /// <summary>
        /// Количество (Count)
        /// </summary>
        public int Count
        {
            get
            {
                return (int)GetProperty("Count", true, null);
            }
        }

        /// <summary>
        /// Получить метаданные
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public MetadataObject this[int index]
        {
            get
            {
                return (MetadataObject)GetFromCache(
                    index.ToString(),
                    () => GetMetadata(InvokeV8Method("Get", index))
                    );
            }
        }

        /// <summary>
        /// Получить метаданные
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public MetadataObject this[string name]
        {
            get
            {
                return (MetadataObject)GetIndexerFromCache(
                    name,
                    () => GetMetadata(InvokeV8Method("Найти", name))
                    );
            }
        }

        private MetadataObject GetMetadata(object ptr)
        {
            if (ptr == null)
            {
                return null;
            }

            MetadataObject meta;
            
            switch (this.MetadataType)
            {
                case MetadataType.DocumentCollection:
                    meta = new DocumentMetadata(this.Session, ptr);
                    break;
                    
                case MetadataType.CatalogCollection:
                    meta = new MetadataObject(this.Session, MetadataType.Catalog, ptr);
                    break;
                
                case MetadataType.InformationRegisterCollection:
                    meta = new InformationRegisterMetadata(this.Session, ptr);
                    break;
                    
                case MetadataType.AccumulationRegisterCollection:
                    meta = new AccumulationRegisterMetadata(this.Session, ptr);
                    break;

                case MetadataType.AccountingRegisterCollection:
                    meta = new AccountingRegisterMetadata(this.Session, ptr);
                    break;

                case MetadataType.CalculationRegisterCollection:
                    meta = new CalculationRegisterMetadata(this.Session, ptr);
                    break;

                case MetadataType.EnumCollection:
                    meta = new EnumMetadata(this.Session, ptr);
                    break;
                    
                case MetadataType.DocumentJournalCollection:
                    meta = new MetadataObject(this.Session, MetadataType.DocumentJournal, ptr);
                    break;
                
                case MetadataType.ConstantCollection:
                    meta = new MetadataObject(this.Session, MetadataType.Constant, ptr);
                    break;
                    
                case MetadataType.TablePartCollection:
                    meta = new MetadataObject(this.Session, MetadataType.TablePart, ptr);
                    break;
                case MetadataType.RequisiteCollection:
                    meta = new MetadataObject(this.Session, MetadataType.Requisite, ptr);
                    break;

                case MetadataType.MeasureCollection:
                    meta = new MetadataObject(this.Session, MetadataType.Measure, ptr);
                    break;
                
                case MetadataType.ResourceCollection:
                    meta = new MetadataObject(this.Session, MetadataType.Resource, ptr);
                    break;

                default:
                    meta = new MetadataObject(this.Session, MetadataType.Unknown, ptr);
                    break;
            }

            if (meta != null)
            {
                PutToIndexerCache(meta.Name, meta);
            }

            return meta;
        }

        #region IEnumerable Members

        public IEnumerator GetEnumerator()
        {
            return new Enumerator(this);
        }

        #endregion

       
        #region IEnumerable<MetadataObject> Members
        
        IEnumerator<MetadataObject>  IEnumerable<MetadataObject>.GetEnumerator()
        {
            return new Enumerator(this);
        }
        
        #endregion
       
        
        private class Enumerator : IEnumerator<MetadataObject>
        {
            private readonly MetadataObjectCollection _collection;
            private int _currentIndex = -1;
            private int _count = 0;
            
            public Enumerator(MetadataObjectCollection collection)
            {
                _collection = collection;
                _count = collection.Count;
            }

            #region IEnumerator Members

            public object Current
            {
                get
                {
                    if (_currentIndex < 0)
                    {
                        return null;
                    }
                    return _collection[_currentIndex];
                }
            }

            public bool MoveNext()
            {
                if (_currentIndex < 0)
                {
                    if (_count > 0)
                    {
                        _currentIndex = 0;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else if (_currentIndex + 1 < _count)
                {
                    _currentIndex++;
                    return true;
                }
                else
                {
                    return false;
                }
            }

            public void Reset()
            {
                _currentIndex = -1;
            }

            #endregion

            #region IEnumerator<MetadataObject> Members

            MetadataObject IEnumerator<MetadataObject>.Current
            {
                get
                {
                    return (MetadataObject)this.Current;
                }
            }

            #endregion

            #region IDisposable Members

            public void Dispose()
            {
            }

            #endregion
        }
    }
}
