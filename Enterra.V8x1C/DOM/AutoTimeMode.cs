using System;
using System.Collections.Generic;
using System.Text;
using Enterra.V8x1C.Routines;

namespace Enterra.V8x1C.DOM
{
    /// <summary>
    /// РежимАвтоВремя (AutoTimeMode)
    /// </summary>
    [V8TypeCode(Code = "adeb08a0-415c-11d6-b9d1-0050bae0a95d")]
    public enum AutoTimeMode
    {
        /// <summary>
        /// НеИспользовать (DontUse)
        /// </summary>
        DontUse = 0,
        
        /// <summary>
        /// Первым (First)
        /// </summary>
        First = 2,
        
        /// <summary>
        /// Последним (Last)
        /// </summary>
        Last = 1,
        
        /// <summary>
        /// ТекущееИлиПервым (CurrentOrFirst)
        /// </summary>
        CurrentOrFirst = 4,

        /// <summary>
        /// ТекущееИлиПоследним (CurrentOrLast)
        /// </summary>
        CurrentOrLast = 3
    }
}
