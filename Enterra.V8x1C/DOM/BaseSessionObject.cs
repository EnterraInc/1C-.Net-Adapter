using System;
using System.Xml;

namespace Enterra.V8x1C.DOM
{
    /// <summary>
    /// Base sessioned 1C object
    /// </summary>
    public abstract class BaseSessionObject : BaseObject
    {
        private readonly Session _session = null;

        internal BaseSessionObject(Session session, object ptr)
        {
            if (session == null)
            {
                throw new ArgumentNullException("session");
            }

            _session = session;

            if (ptr != null)
            {
                Ptr = ptr;
            }
        }

        /// <summary>
        /// Session
        /// </summary>
        public Session Session
        {
            get { return _session; }
        }
        
        /// <summary>
        /// Get property. Use only for value types
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        internal object GetProperty(string name)
        {
            return GetProperty(name, ConvertV8Value);
        }
        
        /// <summary>
        /// Invoke method
        /// </summary>
        /// <param name="name"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        internal object InvokeMethod(string name, params object[] args)
        {
            return ConvertV8Value(InvokeV8Method(name, args));
        }

        internal protected object ConvertV8Value(object obj)
        {
            return ConvertV8Value(obj, null);
        }

        internal protected object ConvertV8Value(object obj, TypeDescription typeDescription)
        {
            if (!IsInternalV8Object(obj))
            {
                return obj;
            }


            if (typeDescription != null)
            {
                var types = typeDescription.Types;

                if (types.Length == 1)
                {
                    var typeInfo = types[0];

                    switch (typeInfo.Type)
                    {
                        case TypeEnum.DocumentRef:
                            return new DocumentRef(this.Session, typeInfo.ReferenceTypeName, obj);
                        case TypeEnum.CatalogRef:
                            return new CatalogRef(this.Session, typeInfo.ReferenceTypeName, obj);
                        case TypeEnum.EnumRef:
                            return new EnumRef(this.Session, typeInfo.ReferenceTypeName, obj);
                    }
                }
            }
            
            object ptrMeta;
            try
            {
                ptrMeta = InvokeV8Method(obj, RussianConsts.Metadata, new object[0]);
            }
            catch
            {
                ptrMeta = null;
            }

            if (ptrMeta == null)
            {
                return null;
            }


            MetadataObject metadata = MetadataObject.Create(this.Session, ptrMeta);

            switch (metadata.MetadataType)
            {
                case MetadataType.Catalog:
                    return new CatalogRef(this.Session, metadata.Name, obj);

                case MetadataType.Document:
                    return new DocumentRef(this.Session, metadata.Name, obj);
            }

            //not implemented

            return null;
        }

        internal protected static bool IsInternalV8Object(object val)
        {
            return (val is MarshalByRefObject);
        }

    }
}
