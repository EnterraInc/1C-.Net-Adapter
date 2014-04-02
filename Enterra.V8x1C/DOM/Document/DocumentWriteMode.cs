using System;
using System.Collections.Generic;
using System.Text;
using Enterra.V8x1C.Routines;

namespace Enterra.V8x1C.DOM
{
    /// <summary>
    /// РежимЗаписиДокумента (DocumentWriteMode)
    /// </summary>
    [V8TypeCode(Code = "27c7756c-e1cc-435e-962f-22df9eeee925")]
    public enum DocumentWriteMode
    {
        /// <summary>
        /// Запись (Write)
        /// </summary>
        Write = 0,

        /// <summary>
        /// Проведение (Posting)
        /// </summary>
        Posting = 1,
        
        /// <summary>
        /// ОтменаПроведения (UndoPosting)
        /// </summary>
        UndoPosting = 2
    }
}
