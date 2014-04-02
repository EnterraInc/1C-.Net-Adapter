using System;
using System.Collections.Generic;
using System.Text;

namespace Enterra.V8x1C.DOM
{
    /// <summary>
    /// RegisterMetadataBase
    /// </summary>
    public abstract class RegisterMetadataBase : MetadataObject
    {
        protected RegisterMetadataBase(Session session, MetadataType metadataType, object ptr)
            : base(session, metadataType, ptr)
        {
        }

        /// <summary>
        /// Измерения (Measures)
        /// </summary>
        public MetadataObjectCollection Measures
        {
            get
            {
                return (MetadataObjectCollection)GetProperty(
                                                          RussianConsts.Measures,
                                                          ptr =>
                                                          new MetadataObjectCollection(this.Session, DOM.MetadataType.MeasureCollection, ptr)
                                                          );
            }
        }

        /// <summary>
        /// Ресурсы (Resources)
        /// </summary>
        public MetadataObjectCollection Resources
        {
            get
            {
                return (MetadataObjectCollection)GetProperty(
                                                          RussianConsts.Resources,
                                                          ptr =>
                                                          new MetadataObjectCollection(this.Session, DOM.MetadataType.ResourceCollection, ptr)
                                                          );
            }
        }
    }
}
