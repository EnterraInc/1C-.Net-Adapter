using System;
using System.Collections.Generic;
using System.Text;

namespace Enterra.V8x1C.DOM
{
    /// <summary>
    /// Ссылка
    /// </summary>
    interface IObjectRef
    {
        /// <summary>
        /// УникальныйИдентификатор (UUID)
        /// </summary>
        UUID Uuid { get; }

        /// <summary>
        /// Пустой (IsEmpty)
        /// </summary>
        bool IsEmpty { get; }

        /// <summary>
        /// Код.
        /// Код справочника, номер документа.
        /// </summary>
        object Code { get; }

        /// <summary>
        /// Метаданные (Metadata)
        /// </summary>
        MetadataObject Metadata { get; }
    }
}
