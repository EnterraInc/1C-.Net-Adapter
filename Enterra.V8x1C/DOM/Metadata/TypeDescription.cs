using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace Enterra.V8x1C.DOM
{
    /// <summary>
    /// ОписаниеТипов (TypeDescription)
    /// </summary>
    public class TypeDescription : BaseSessionObject, IV8XmlSerializable, ICacheLoadable
    {

        private const string V8XmlTagName = "TypeDescription";
        private const string V8XmlTypesTagName = "Types";
        private const string V8XmlTypeTagName = "Type";
        private const string V8XmlD2P1NamespaceName = "d2p1";
        private const string V8XmlP1NamespaceName = "p1";
        
        private const string V8XmlXsiTypeAttr = "xsi:type";
        private const string V8XmlXsiNilAttr = "xsi:nil";


        private TypeInfo[] _types = new TypeInfo[0];

        internal TypeDescription(Session session, object ptr)
            : base(session, ptr)
        {
            LoadCache();
        }

        /// <summary>
        /// КвалификаторыДаты (DateQualifiers)
        /// </summary>
        public DateQualifiers DateQualifiers
        {
            get
            {
                return (DateQualifiers)GetProperty("КвалификаторыДаты", 
                    ptr => new DateQualifiers(Session, ptr));
            }
        }

        /// <summary>
        /// КвалификаторыСтроки (StringQualifiers)
        /// </summary>
        public StringQualifiers StringQualifiers
        {
            get
            {
                return (StringQualifiers)GetProperty("КвалификаторыСтроки",
                    ptr => new StringQualifiers(Session, ptr));
            }
        }

        /// <summary>
        /// КвалификаторыЧисла (NumberQualifiers)
        /// </summary>
        public NumberQualifiers NumberQualifiers
        {
            get
            {
                return (NumberQualifiers)GetProperty("КвалификаторыЧисла",
                    ptr => new NumberQualifiers(Session, ptr));
            }
        }

        /// <summary>
        /// Типы (Возможные типы значений для объекта данного типа)
        /// </summary>
        public TypeInfo[] Types
        {
            get
            {
                return (TypeInfo[])_types.Clone();
            }
        }

        
        /// <summary>
        /// Типы (Types).
        /// Доступ закрыт так как API 1C не предоставляет описания.
        /// Вместо этого используем св-во Types
        /// </summary>
        private ArrayV8 TypesV8
        { 
            get
            {
                return (ArrayV8)GetProperty(
                    "Types", 
                    true,
                    ptr => new ArrayV8(Session, ptr, ptrArg => new TypeV8(Session, ptrArg))
                    );
            }
        }

        /// <summary>
        /// СодержитТип (ContainsType)
        /// </summary>
        /// <param name="typeV8"></param>
        /// <returns></returns>
        public bool ContainsType(TypeV8 typeV8)
        {
            return (bool)InvokeV8Method("СодержитТип", typeV8.Ptr);
        }

        /// <summary>
        /// Load cache
        /// </summary>
        public void LoadCache()
        {
            LoadFromV8Xml(Session.ToXML(this));
        }


        /// <summary>
        /// Load from V8 xml
        /// </summary>
        /// <param name="xml"></param>
        private void LoadFromV8Xml(string xml)
        {
            //D
            //System.IO.File.AppendAllText("TempXml.xml", "\r\n" + xml);
            
            XmlDocument doc = new XmlDocument();
            doc.InnerXml = xml;
            
            if (doc.DocumentElement.Name != V8XmlTagName)
            {
                return;
            }

            (this as IV8XmlSerializable).LoadFromV8Xml(doc.DocumentElement);
        }

        /// <summary>
        /// Load from V8 xml
        /// </summary>
        /// <param name="node"></param>
        void IV8XmlSerializable.LoadFromV8Xml(XmlNode node)
        {
            XmlElement typeDescTag = (XmlElement)node;

            List<TypeInfo> typesList = new List<TypeInfo>();


            foreach (var nd1 in typeDescTag.ChildNodes)
            {
                XmlElement typesTag = nd1 as XmlElement;
                
               
                if (typesTag == null || 
                    !(typesTag.Name == V8XmlTypesTagName ||
                    typesTag.Name == V8XmlD2P1NamespaceName + ":" + V8XmlTypesTagName ||
                    typesTag.Name == V8XmlP1NamespaceName + ":" + V8XmlTypesTagName
                    )
                    )
                {
                    continue;
                }


                foreach (var nd2  in typesTag.ChildNodes)
                {
                    XmlElement typeTag = nd2 as XmlElement;

                    if (typeTag == null ||
                        !(typeTag.Name == V8XmlTypeTagName ||
                        typeTag.Name == V8XmlD2P1NamespaceName + ":" + V8XmlTypeTagName ||
                        typeTag.Name == V8XmlP1NamespaceName + ":" + V8XmlTypeTagName
                        )
                        )
                    {
                        continue;
                    }

                    TypeInfo typeInfo = TypeInfo.LoadFromV8Xml(typeTag.InnerXml);
                    if (typeInfo != null)
                    {
                        typesList.Add(typeInfo);
                    }
                }
            }
            
            _types = typesList.ToArray();
        }

        /// <summary>
        /// Parse V8Xml value
        /// </summary>
        /// <param name="valueTag"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        internal bool TryParseV8XmlValue(XmlElement valueTag, out object value)
        {
            if (valueTag.GetAttribute(V8XmlXsiNilAttr) == "true")
            {
                value = null;
                return true;
            }

            string typeStr = valueTag.GetAttribute(V8XmlXsiTypeAttr);
            {
                TypeInfo valueType = TypeInfo.LoadFromV8Xml(typeStr);
                if (valueType != null &&
                    valueType.Type != TypeEnum.Unknown &&
                    valueType.TryParseValueFromV8Xml(Session, valueTag.InnerText, out value)
                    )
                {
                    return true;
                }
            }

            foreach (var valueType in Types)
            {
                if (valueType.TryParseValueFromV8Xml(Session, valueTag.InnerText, out value))
                {
                    return true;
                }
            }

            value = null;
            return false;
        }
    }
}
