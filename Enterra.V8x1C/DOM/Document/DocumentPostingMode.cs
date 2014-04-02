using System;
using System.Collections.Generic;
using System.Text;
using Enterra.V8x1C.Routines;

namespace Enterra.V8x1C.DOM
{
    /// <summary>
    /// РежимПроведенияДокумента (DocumentPostingMode)
    /// </summary>
    [V8TypeCode(Code = "76665f04-21b6-4b9c-abce-ed319bde7c6e")]
    public enum DocumentPostingMode
    {
        /// <summary>
        /// Неоперативный (NonOperational)
        /// </summary>
        NonOperational = 0,

        /// <summary>
        /// Оперативный (Operational)
        /// </summary>
        Operational = 1

    }
}
