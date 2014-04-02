using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Reflection;
using System.Xml;

namespace Enterra.V8x1C.DOM
{
    /// <summary>
    /// Base 1C object
    /// </summary>
    public abstract class BaseObject
    {
        protected delegate object GetValueHandler();

        private object _ptr = null;

        private readonly Dictionary<string, object> _cache = new Dictionary<string, object>();

        ~BaseObject()
        {
            try
            {
                Release();
            }
            catch
            {
            }
        }

        /// <summary>
        /// Ptr (ComObject).
        /// </summary>
        public object Ptr
        {
            get
            {
                if (_ptr == null)
                {
                    OnPtrRequired();
                }

                return _ptr;
            }
            protected set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("");
                }

                if (_ptr != null)
                {
                    throw new Exception("Ptr set already.");
                }

                _ptr = value;
            }
        }

        /// <summary>
        /// Invoke method
        /// </summary>
        /// <param name="name"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        internal object InvokeV8Method(string name, params object[] args)
        {
            return InvokeV8Method(Ptr, name, args);
        }

        /// <summary>
        /// Invoke method
        /// </summary>
        /// <param name="target"></param>
        /// <param name="name"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        internal static object InvokeV8Method(object target, string name, object[] args)
        {
            try
            {
                return target.GetType().InvokeMember(
                    name,
                    BindingFlags.Public | BindingFlags.InvokeMethod,
                    null,
                    target, args
                    );
            }
            catch (TargetInvocationException exc)
            {
                throw exc.InnerException;
            }
        }

        /// <summary>
        /// Get property (return value type or _COMObject)
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        internal object GetV8Property(string name)
        {
            return GetV8Property(Ptr, name);
        }

        
        /// <summary>
        /// Get property and convert
        /// </summary>
        /// <param name="name"></param>
        /// <param name="converter"></param>
        /// <returns></returns>
        internal object GetProperty(string name, Converter<object, object> converter)
        {
            return GetProperty(name, false, converter);
        }
        
        /// <summary>
        /// Get property and convert
        /// </summary>
        /// <param name="name"></param>
        /// <param name="byInvokeV8"></param>
        /// <param name="converter"></param>
        /// <returns></returns>
        internal object GetProperty(string name, bool byInvokeV8, Converter<object, object> converter)
        {
            object obj;
            if (_cache.TryGetValue(name, out obj))
            {
                return obj;
            }
            if (byInvokeV8)
            {
                obj = InvokeV8Method(name);
            }
            else
            {
                obj = GetV8Property(name);
            }
            if (obj != null && converter != null)
            {
                obj = converter(obj);
            }
            _cache[name] = obj;
            return obj;
        }

        /// <summary>
        /// Set property
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        internal void SetProperty(string name, object value)
        {
            object ptr;
            if (value is BaseObject)
            {
                ptr = (value as BaseObject).Ptr;
            }
            else
            {
                ptr = value;
            }

            SetV8Property(name, ptr);
            PutToCache(name, value);
        }

        /// <summary>
        /// Set property
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        internal void SetV8Property(string name, object value)
        {
            SetV8Property(Ptr, name, value);
        }
        
        /// <summary>
        /// Clear cache
        /// </summary>
        public void ClearCache()
        {
            _cache.Clear();
        }
       
        
        /// <summary>
        /// Get property
        /// </summary>
        /// <param name="target"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        internal static object GetV8Property(object target, string name)
        {
            try
            {
                return target.GetType().InvokeMember(name,
                                                   BindingFlags.Public | BindingFlags.GetProperty,
                                                   null, target,
                                                   null);
            }
            catch (TargetInvocationException exc)
            {
                throw exc.InnerException;
            }
        }

        /// <summary>
        /// Set property
        /// </summary>
        /// <param name="target"></param>
        /// <param name="name"></param>
        /// <param name="value"></param>
        internal static void SetV8Property(object target, string name, object value)
        {
            try
            {
                target.GetType().InvokeMember(name,
                    BindingFlags.Public | BindingFlags.SetProperty,
                    null, target,
                    new object[] { value }
                    );
            }
            catch (TargetInvocationException exc)
            {
                throw exc.InnerException;
            }
        }
        
        public override bool Equals(object obj)
        {
            if (obj is BaseObject)
            {
                return Ptr == (obj as BaseObject).Ptr;
            }

            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            if (Ptr != null)
            {
                return Ptr.GetHashCode();
            }

            return base.GetHashCode();
        }

        /// <summary>
        /// Get from cache
        /// </summary>
        /// <param name="name"></param>
        /// <param name="getValue"></param>
        /// <returns></returns>
        protected object GetFromCache(string name, GetValueHandler getValue)
        {
            object obj;
            if (_cache.TryGetValue(name, out obj))
            {
                return obj;
            }

            if (getValue != null)
            {
                obj = getValue.Invoke();

                _cache[name] = obj;
            }

            return obj;
        }

        /// <summary>
        /// Get indexer value from cache
        /// </summary>
        /// <param name="name"></param>
        /// <param name="getValue"></param>
        /// <returns></returns>
        protected object GetIndexerFromCache(string name, GetValueHandler getValue)
        {
            return GetFromCache("this." + name, getValue);
        }

        /// <summary>
        /// Put to cache
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        protected void PutToCache(string name, object value)
        {
            _cache[name] = value;
        }

        /// <summary>
        /// Put to indexer cache
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        protected void PutToIndexerCache(string name, object value)
        {
            PutToCache("this." + name, value);
        }


        /// <summary>
        /// Raise on get Ptr property when there is null
        /// </summary>
        protected virtual void OnPtrRequired()
        {
        }



        /// <summary>
        /// Release
        /// </summary>
        private void Release()
        {
            if (_ptr != null)
            {
                Marshal.ReleaseComObject(_ptr);
                _ptr = null;
            }
        }
    }
}
