using System;
using System.Collections.Generic;
using System.Text;

namespace Enterra.V8x1C.DOM
{
    /// <summary>
    /// IV8XmlSerializable
    /// </summary>
    internal interface IV8XmlSerializable
    {
        /// <summary>
        /// Load from V8 xml
        /// </summary>
        /// <param name="node"></param>
        void LoadFromV8Xml(System.Xml.XmlNode node);
    }
}
