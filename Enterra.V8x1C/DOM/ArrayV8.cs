using System;
using System.Collections.Generic;
using System.Text;
using Enterra.V8x1C.Routines;
using Enterra.V8x1C.Routines.V8Parsing;

namespace Enterra.V8x1C.DOM
{
    /// <summary>
    /// Массив (Array)
    /// </summary>
    public class ArrayV8 : BaseSessionObject, IV8Serializable, ICacheLoadable
    {
        private readonly Converter<object, object> _byIndexConverter = null;
        private readonly TypeDescription _elementTypeDesc = null;


        internal ArrayV8(
            Session session, 
            object ptr, 
            Converter<object, object> byIndexConverter
            )
            : base(session, ptr)
        {
            _byIndexConverter = byIndexConverter;
        }

        internal ArrayV8(
            Session session,
            object ptr,
            TypeDescription elementTypeDesc
            )
            : base(session, ptr)
        {
            _elementTypeDesc = elementTypeDesc;
        }

        /// <summary>
        /// Получить по индексу
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public object this[int index]
        {
            get
            {
                return GetFromCache(
                    index.ToString(),
                    delegate
                    {
                        var obj = InvokeV8Method("Get", index);

                        if (obj != null)
                        {
                            if (_byIndexConverter != null)
                            {
                                obj = _byIndexConverter.Invoke(obj);
                            }
                            else if (_elementTypeDesc != null)
                            {
                                obj = ConvertV8Value(obj, _elementTypeDesc);
                            }
                            else
                            {
                                obj = ConvertV8Value(obj);
                            }
                        }
                        return obj;
                    }
                    );
            }
            private set
            {
                PutToCache(index.ToString(), value);
            }
        }

        /// <summary>
        /// Количество
        /// </summary>
        public int Count
        {
            get
            {
                return (int)GetProperty("Count", true, null);
            }
            private set
            {
                PutToCache("Count", value);
            }
        }

        /// <summary>
        /// Load cache
        /// </summary>
        public void LoadCache()
        {
            Session.LoadObjectCache(this);
        }

        /// <summary>
        /// Load from V8 string
        /// </summary>
        /// <param name="v8String"></param>
        void IV8Serializable.LoadFromV8String(string v8String)
        {
            var root = LexemV8Parser.Parse(v8String);

            if (root == null || root.LexemType != LexemV8Type.Brace)
            {
                return;
            }

            root = root.FindFirstChild(LexemV8Type.Brace);

            string stCount = root.ChildLexemList[0].Data;
            Count = int.Parse(stCount);

            if (_elementTypeDesc != null && _elementTypeDesc.Types.Length == 1)
            {
                var valTypeInfo = _elementTypeDesc.Types[0];

                if (valTypeInfo.IsPrimitiveType)
                {
                    for (int i = 1; i < root.ChildLexemList.Count; i++)
                    {
                        var valLex = root.ChildLexemList[i];

                        object val;
                        if (valTypeInfo.TryParseValueFromV8String(Session, valLex.ToString(), out val))
                        {
                            this[i - 1] = val;
                        }
                    }
                }
            }
        }
    }
}
