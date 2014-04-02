using System;
using System.Collections.Generic;
using System.Text;

namespace Enterra.V8x1C.DOM
{
    /// <summary>
    /// ISelection
    /// </summary>
    public interface ISelection<T> : ISelection
        where T : BaseSessionObject
    {
        /// <summary>
        /// ПолучитьОбъект (GetObject)
        /// </summary>
        /// <returns></returns>
        new T GetObject();
    }

    /// <summary>
    /// ISelection
    /// </summary>
    public interface ISelection
    {
        /// <summary>
        /// Session
        /// </summary>
        Session Session
        { 
            get;
        }

        /// <summary>
        /// Следующий (Next)
        /// </summary>
        /// <returns> Истина - следующий элемент выбран; Ложь - достигнут конец выборки. </returns>
        bool Next();


        /// <summary>
        /// ПолучитьОбъект (GetObject)
        /// </summary>
        /// <returns></returns>
        BaseSessionObject GetObject();
    }
}
