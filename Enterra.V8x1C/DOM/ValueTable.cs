using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Enterra.V8x1C.DOM
{
    /// <summary>
    /// ТаблицаЗначений (ValueTable)
    /// </summary>
    public class ValueTable : BaseSessionObject, ICacheLoadable
    {
        internal ValueTable(Session session, object ptr)
            : base(session, ptr)
        {
        }

        /// <summary>
        /// Колонки (Columns)
        /// </summary>
        public ValueTableColumnCollection Columns
        {
            get
            {
                return (ValueTableColumnCollection)GetProperty(
                    "Columns",
                    ptr => new ValueTableColumnCollection(Session, ptr)
                    );
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
        /// Получить строку
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public ValueTableRow this[int index]
        {
            get
            {
                return (ValueTableRow)GetIndexerFromCache(index.ToString(),
                                    delegate
                                    {
                                        object ptr = InvokeV8Method("Get", index);
                                        if (ptr == null)
                                        {
                                            return null;
                                        }
                                        return new ValueTableRow(Session, ptr);
                                    }
                    );
            }
        }

        /// <summary>
        /// ВыгрузитьКолонку (UnloadColumn)
        /// </summary>
        /// <param name="column"></param>
        /// <returns></returns>
        public ArrayV8 UnloadColumn(ValueTableColumn column)
        {
            var ptr = InvokeV8Method("UnloadColumn", column.Ptr);

            if (ptr == null)
            {
                return null;
            }

            return new ArrayV8(Session, ptr, column.ValueType);
        }

        /// <summary>
        /// ВыгрузитьКолонку (UnloadColumn)
        /// </summary>
        /// <param name="columnIndex"></param>
        /// <returns></returns>
        public ArrayV8 UnloadColumn(int columnIndex)
        {
            return UnloadColumn(Columns[columnIndex]);
        }

        /// <summary>
        /// ВыгрузитьКолонку (UnloadColumn)
        /// </summary>
        /// <param name="columnName"></param>
        /// <returns></returns>
        public ArrayV8 UnloadColumn(string columnName)
        {
            return UnloadColumn(Columns[columnName]);
        }

        /// <summary>
        /// Load cache
        /// </summary>
        /// <returns>Return cached columns</returns>
        public ValueTableColumn[] LoadCache()
        {
            Dictionary<ValueTableColumn, ArrayV8> columnValuesByIndex = new Dictionary<ValueTableColumn, ArrayV8>();
            
            for (int i = 0; i < Columns.Count; i++)
            {
                var col = Columns[i];

                try
                {
                    var valueTypes = col.ValueType.Types;
                    if (valueTypes.Length != 1 || valueTypes[0].Type == TypeEnum.Unknown)
                    {
                        continue;
                    }
                }
                catch
                {
                    continue;
                }

                
                ArrayV8 columnValuesArr = UnloadColumn(col);
                columnValuesArr.LoadCache();
                columnValuesByIndex[col] = columnValuesArr;
            }

            if (columnValuesByIndex.Count == 0)
            {
                return new ValueTableColumn[0];
            }

            int rowsCount = this.Count;
            for (int i = 0; i < rowsCount; i++)
            {
                ValueTableRow row = (ValueTableRow)GetIndexerFromCache(
                    i.ToString(),
                    () => new ValueTableRow(this, i)
                    );

                foreach (KeyValuePair<ValueTableColumn, ArrayV8> pair in columnValuesByIndex)
                {
                    row[pair.Key] = pair.Value[i];
                }
            }

            ValueTableColumn[] cachedColumns = new ValueTableColumn[columnValuesByIndex.Count];
            columnValuesByIndex.Keys.CopyTo(cachedColumns, 0);
            Array.Sort(cachedColumns, (a, b) => System.Collections.Comparer.DefaultInvariant.Compare(a.Index, b.Index));
            
            return cachedColumns;
        }

        void ICacheLoadable.LoadCache()
        {
            this.LoadCache();
        }

        /// <summary>
        /// Convert to DataTable
        /// </summary>
        /// <param name="fillRows"></param>
        /// <param name="columns"></param>
        /// <returns></returns>
        public DataTable ToDataTable(bool fillRows, ValueTableColumn[] columns)
        {
            DataTable dataTable = new DataTable();
            
            for (int i = 0; i < columns.Length; i++)
            {
                var column = columns[i];

                var tableColumn = column.ToDataColumn();

                dataTable.Columns.Add(tableColumn);
            }

            if (fillRows)
            {
                int rowsCount = this.Count;
                for (int i = 0; i < rowsCount; i++)
                {
                    var v8Row = this[i];

                    DataRow dataRow = dataTable.NewRow();
                    for (int c = 0; c < columns.Length; c++ )
                    {
                        var column = columns[c];
                        dataRow[c] = v8Row[column.Index];
                    }
                    dataTable.Rows.Add(dataRow);
                }
            }

            return dataTable;
        }
    }
}
