using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using Enterra.V8x1C.Routines;
using System.Globalization;
using Enterra.V8x1C.Routines.V8Parsing;

namespace Enterra.V8x1C.DOM
{
    /// <summary>
    /// Описание типа
    /// </summary>
    public class TypeInfo
    {
        private TypeEnum _type = TypeEnum.Unknown;
        private string _referenceTypeName = null;
        
        /// <summary>
        /// Type
        /// </summary>
        public TypeEnum Type
        {
            get
            {
                return _type;
            }
            private set
            {
                _type = value;
            }
        }

        /// <summary>
        /// Reference Type Name
        /// </summary>
        public string ReferenceTypeName
        {
            get
            {
                return _referenceTypeName;
            }
            private set
            {
                _referenceTypeName = value;
            }
        }

        /// <summary>
        /// Used managed type
        /// </summary>
        public Type ManagedType
        {
            get
            {
                switch (Type)
                {
                    case TypeEnum.Boolean:
                        return typeof (bool);
                    case TypeEnum.Date:
                        return typeof (DateTime);
                    case TypeEnum.String:
                        return typeof(string);
                    case TypeEnum.Decimal:
                        return typeof(decimal);
                    case TypeEnum.CatalogRef:
                        return typeof(CatalogRef);
                    case TypeEnum.DocumentRef:
                        return typeof(DocumentRef);
                    case TypeEnum.EnumRef:
                        return typeof(EnumRef);
                }

                return typeof (object);
            }
        }

        /// <summary>
        /// Is primitive type
        /// </summary>
        public bool IsPrimitiveType
        {
            get
            {
                switch (Type)
                {
                    case TypeEnum.Boolean:
                    case TypeEnum.Date:
                    case TypeEnum.String:
                    case TypeEnum.Decimal:
                        return true;
                }

                return false;
            }
        }

        

        internal static TypeInfo LoadFromV8Xml(string v8Xml)
        {
            if (string.IsNullOrEmpty(v8Xml) || 
                string.Equals(v8Xml, "Null", StringComparison.InvariantCultureIgnoreCase) ||
                string.Equals(v8Xml, "p1:Null", StringComparison.InvariantCultureIgnoreCase) ||
                string.Equals(v8Xml, "d2p1:Null", StringComparison.InvariantCultureIgnoreCase)
                )
            {
                return null;
            }

            TypeInfo typeIbfo = new TypeInfo();
            typeIbfo.Type = ParseTypeEnumFromV8Xml(v8Xml);

            typeIbfo.ReferenceTypeName = null;

            if (typeIbfo.Type == TypeEnum.CatalogRef ||
                typeIbfo.Type == TypeEnum.DocumentRef ||
                typeIbfo.Type == TypeEnum.EnumRef
                )
            {
                int dotPos = v8Xml.IndexOf('.');
                if (dotPos >= 0)
                {
                    typeIbfo.ReferenceTypeName = v8Xml.Substring(dotPos + 1);
                }
            }

            return typeIbfo;
        }

        internal bool TryParseValueFromV8Xml(Session session, string strValue, out object value)
        {
            value = null;

            switch (Type)
            {
                case TypeEnum.Boolean:
                    {
                        bool bv;
                        if (bool.TryParse(strValue, out bv))
                        {
                            value = bv;
                            return true;
                        }
                    }
                    break;
                case TypeEnum.Date:
                    {
                        DateTime dt;
                        if (DateTime.TryParse(strValue, out dt))
                        {
                            value = dt;
                            return true;
                        }
                    }
                    break;
                case TypeEnum.String:
                    value = strValue;
                    return true;

                case TypeEnum.Decimal:
                    {
                        decimal dc;
                        if (decimal.TryParse(strValue, NumberStyles.Number, CultureInfo.InvariantCulture, out dc))
                        {
                            value = dc;
                            return true;
                        }
                    }
                    break;

                case TypeEnum.CatalogRef:
                    {
                        if (string.IsNullOrEmpty(strValue))
                        {
                            value = null;
                            return true;
                        }

                        value = new CatalogRef(session, ReferenceTypeName, strValue);
                    }
                    return true;
                case TypeEnum.DocumentRef:
                    {
                        if (string.IsNullOrEmpty(strValue))
                        {
                            value = null;
                            return true;
                        }

                        value = new DocumentRef(session, ReferenceTypeName, strValue);
                    }
                    return true;
                case TypeEnum.EnumRef:
                    {
                        if (string.IsNullOrEmpty(strValue))
                        {
                            value = null;
                            return true;
                        }

                        value = new EnumRef(session, ReferenceTypeName, strValue);
                    }
                    return true;
            }
            
            return false;
        }

        internal bool TryParseValueFromV8String(Session session, string strValue, out object value)
        {
            return TryParseValueFromV8String(session, LexemV8Parser.Parse(strValue), out value);
        }


        internal bool TryParseValueFromV8String(Session session, LexemV8 lexem, out object value)
        {
            value = null;
            if (!IsPrimitiveType
                || lexem == null
                || lexem.LexemType != LexemV8Type.Brace
                || lexem.ChildLexemList == null
                || lexem.ChildLexemList.Count != 2)
            {
                return false;
            }
            string dataValue = lexem.ChildLexemList[1].Data.Trim();

            switch (Type)
            {
                case TypeEnum.Boolean:
                    value = !(dataValue == "0");
                    return true;
                case TypeEnum.Date:
                    DateTime dt;
                    if (DateTime.TryParseExact(dataValue, "yyyyMMddHHmmss", CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out dt))
                    {
                        value = dt;
                        return true;
                    }
                    break;
                case TypeEnum.String:
                    if (dataValue.StartsWith("\""))
                    {
                        dataValue = dataValue.Remove(0, 1);
                    }
                    if (dataValue.EndsWith("\""))
                    {
                        dataValue = dataValue.Remove(dataValue.Length-1, 1);
                    }
                    value = dataValue;
                    return true;
                case TypeEnum.Decimal:
                    decimal dc;
                    if (decimal.TryParse(dataValue, NumberStyles.Number, CultureInfo.InvariantCulture, out dc))
                    {
                        value = dc;
                        return true;
                    }
                    break;
            }
            return false;
        }

        private static TypeEnum ParseTypeEnumFromV8Xml(string value)
        {
            switch (value)
            {
                case "p1:string":
                case "xsd:string":
                    return TypeEnum.String;
                case "p1:dateTime":
                case "xsd:dateTime":
                    return TypeEnum.Date;
                case "p1:decimal":
                case "xsd:decimal":
                    return TypeEnum.Decimal;
                case "p1:boolean":
                case "xsd:boolean":
                    return TypeEnum.Boolean;
                    
            }

            if (value.StartsWith("CatalogRef"))
            {
                return TypeEnum.CatalogRef;
            }
            else if (value.StartsWith("DocumentRef"))
            {
                return TypeEnum.DocumentRef;
            }
            else if (value.StartsWith("EnumRef"))
            {
                return TypeEnum.EnumRef;
            }

            return TypeEnum.Unknown;
        }
    }

    /// <summary>
    /// Type enum
    /// </summary>
    public enum TypeEnum
    {
        Decimal,
        String,
        Date,
        Boolean,
        CatalogRef,
        DocumentRef,
        EnumRef,
        Unknown
    }
}
