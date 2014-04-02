using System;
using System.Collections.Generic;
using System.Text;

namespace Enterra.V8x1C.DOM
{
    /// <summary>
    /// Табличная часть (TablePart)
    /// </summary>
    public class TablePart : BaseSessionObject
    {
        internal TablePart(Session session, object ptr)
            : base(session, ptr)
        {
        }

        /// <summary>
        /// Получить строку
        /// </summary>
        /// <param name="rowIndex"></param>
        /// <returns></returns>
        public TablePartRow this[int rowIndex]
        {
            get
            {
                return (TablePartRow)GetFromCache(
                    rowIndex.ToString(),
                    () => Get(rowIndex)
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
        /// Вставить (Insert)
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public TablePartRow Insert(int index)
        {
            var ptr = InvokeV8Method("Insert", index);
            return ptr != null ? new TablePartRow(Session, ptr) : null;
        }

        /// <summary>
        /// Выгрузить (Unload)
        /// </summary>
        /// <returns></returns>
        public ValueTable Unload()
        {
            var ptr = InvokeV8Method("Unload");
            return new ValueTable(Session, ptr);
        }

        /// <summary>
        /// ВыгрузитьКолонку (UnloadColumn)
        /// </summary>
        /// <param name="columnIndex"></param>
        public ArrayV8 UnloadColumn(int columnIndex)
        {
            var ptr = InvokeV8Method("UnloadColumn", columnIndex);
            return ptr != null ? new ArrayV8(Session, ptr, (Converter<object, object>)null) : null;
        }

        /// <summary>
        /// ВыгрузитьКолонку (UnloadColumn)
        /// </summary>
        /// <param name="columnName"></param>
        public ArrayV8 UnloadColumn(string columnName)
        {
            var ptr = InvokeV8Method("UnloadColumn", columnName);
            return ptr != null ? new ArrayV8(Session, ptr, (Converter<object, object>)null) : null;
        }

        /// <summary>
        /// Добавить (Add)
        /// </summary>
        public TablePartRow Add()
        {
            return new TablePartRow(Session, InvokeV8Method("Add"));
        }

        /// <summary>
        /// Загрузить (Load)
        /// </summary>
        /// <param name="table"></param>
        public void Load(ValueTable table)
        {
            InvokeV8Method("Load", table.Ptr);
        }

        /// <summary>
        /// ЗагрузитьКолонку (LoadColumn)
        /// </summary>
        /// <param name="values"></param>
        /// <param name="columnIndex"></param>
        public void LoadColumn(ArrayV8 values, int columnIndex)
        {
            InvokeV8Method("LoadColumn", values.Ptr, columnIndex);
        }

        /// <summary>
        /// ЗагрузитьКолонку (LoadColumn)
        /// </summary>
        /// <param name="values"></param>
        /// <param name="columnName"></param>
        public void LoadColumn(ArrayV8 values, string columnName)
        {
            InvokeV8Method("LoadColumn", values.Ptr, columnName);
        }

        /// <summary>
        /// Индекс (IndexOf)
        /// </summary>
        /// <param name="tableRow"></param>
        /// <returns></returns>
        public int IndexOf(TablePartRow tableRow)
        {
            return (int)InvokeV8Method("IndexOf", tableRow.Ptr);
        }

        /// <summary>
        /// Итог (Total)
        /// </summary>
        /// <param name="columnIndex"></param>
        /// <returns></returns>
        public object Total(int columnIndex)
        {
            return InvokeV8Method("Total", columnIndex);
        }

        /// <summary>
        /// Итог (Total)
        /// </summary>
        /// <param name="columnName"></param>
        /// <returns></returns>
        public object Total(string columnName)
        {
            return InvokeV8Method("Total", columnName);
        }

        /// <summary>
        /// Найти (Find)
        /// </summary>
        /// <param name="value"></param>
        /// <param name="columnNames"></param>
        /// <returns></returns>
        public TablePartRow Find(object value, string columnNames)
        {
            if (value is BaseObject)
            {
                value = (value as BaseObject).Ptr;
            }
            var ptr = InvokeV8Method("Find", value, columnNames);
            return ptr != null ? new TablePartRow(Session, ptr) : null;
        }

        /// <summary>
        /// НайтиСтроки (FindRows)
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public ArrayV8 FindRows(Structure filter)
        {
            var ptr = InvokeV8Method("FindRows", filter.Ptr);
            if (ptr == null)
            {
                return null;
            }
            return new ArrayV8(
                Session, 
                ptr, 
                pt => new TablePartRow(Session, pt)
                );
        }

        /// <summary>
        /// Очистить (Clear)
        /// </summary>
        public void Clear()
        {
            InvokeV8Method("Clear");
        }

        /// <summary>
        /// Получить (Get)
        /// </summary>
        /// <param name="rowIndex"></param>
        /// <returns></returns>
        public TablePartRow Get(int rowIndex)
        {
            var ptr = InvokeV8Method("Get", rowIndex);
            return ptr != null ? new TablePartRow(Session, ptr) : null;
        }

        /// <summary>
        /// Свернуть (GroupBy)
        /// </summary>
        /// <param name="groupColumnNames"></param>
        /// <param name="sumColumnNames"></param>
        public void GroupBy(string groupColumnNames, string sumColumnNames)
        {
            InvokeV8Method("GroupBy", groupColumnNames, sumColumnNames);
        }

        /// <summary>
        /// Сдвинуть (Move)
        /// </summary>
        /// <param name="rowIndex"></param>
        /// <param name="shift"></param>
        public void Move(int rowIndex, int shift)
        {
            InvokeV8Method("Move", rowIndex, shift);
        }

        /// <summary>
        /// Сдвинуть (Move)
        /// </summary>
        /// <param name="row"></param>
        /// <param name="shift"></param>
        public void Move(TablePartRow row, int shift)
        {
            InvokeV8Method("Move", row.Ptr, shift);
        }

        /// <summary>
        /// Сортировать (Sort)
        /// </summary>
        /// <param name="columnNames"></param>
        public void Sort(string columnNames)
        {
            InvokeV8Method("Sort", columnNames);
        }

        /// <summary>
        /// Удалить (Delete)
        /// </summary>
        /// <param name="rowIndex"></param>
        public void Delete(int rowIndex)
        {
            InvokeV8Method("Delete", rowIndex);
        }

        /// <summary>
        /// Удалить (Delete)
        /// </summary>
        /// <param name="row"></param>
        public void Delete(TablePartRow row)
        {
            InvokeV8Method("Delete", row.Ptr);
        }

    }
}
