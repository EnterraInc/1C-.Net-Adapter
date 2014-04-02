using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Enterra.V8x1C.DOM
{
    /// <summary>
    /// КолонкаТаблицыЗначений (ValueTableColumn)
    /// </summary>
    public class ValueTableColumn : BaseSessionObject
    {
        private readonly ValueTableColumnCollection _owner;

        internal ValueTableColumn(ValueTableColumnCollection owner, object ptr)
            : base(owner.Session, ptr)
        {
            _owner = owner;
        }

        /// <summary>
        /// Имя (Name)
        /// </summary>
        public string Name
        {
            get
            {
                return GetProperty("Name") as string;
            }
        }

        /// <summary>
        /// Заголовок (Title)
        /// </summary>
        public string Title
        {
            get
            {
                return GetProperty("Title") as string;
            }
        }

        /// <summary>
        /// ТипЗначения (ValueType)
        /// </summary>
        public TypeDescription ValueType
        {
            get
            {
                return (TypeDescription)GetProperty(
                    "ValueType", 
                    ptr => new TypeDescription(Session, ptr)
                    );
            }
        }

        /// <summary>
        /// Индекс
        /// </summary>
        public int Index
        {
            get
            {
                return (int)GetFromCache("Index",
                    () => _owner.IndexOf(this)
                    );
            }
        }

        /// <summary>
        /// To DataColumn
        /// </summary>
        /// <returns></returns>
        public DataColumn ToDataColumn()
        {
            DataColumn col = new DataColumn(Name);
            col.Caption = Title;

            var colTypes = ValueType.Types;

            if (colTypes.Length == 0 || colTypes.Length > 1)
            {
                col.DataType = typeof (object);
            }
            else
            {
                col.DataType = colTypes[0].ManagedType;
            }
            
            return col;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
