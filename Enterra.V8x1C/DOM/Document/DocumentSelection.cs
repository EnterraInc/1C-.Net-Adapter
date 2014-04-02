using System;
using System.Collections.Generic;
using System.Text;

namespace Enterra.V8x1C.DOM
{
    /// <summary>
    /// ДокументВыборка (DocumentSelection)
    /// </summary>
    public class DocumentSelection : BaseDocumentProperties, ISelection<DocumentObject>
    {
        internal DocumentSelection(DocumentManager documentManager, object ptr)
            : base(documentManager.Session, documentManager.DocumentName, ptr)
        {
        }
       
        /// <summary>
        /// Следующий (Next)
        /// </summary>
        /// <returns> Истина - следующий элемент выбран; Ложь - достигнут конец выборки. </returns>
        public bool Next()
        {
            return (bool)InvokeV8Method("Next");
        }

        /// <summary>
        /// ПолучитьОбъект (GetObject)
        /// </summary>
        /// <returns></returns>
        public DocumentObject GetObject()
        {
            return (DocumentObject) GetProperty(
                "GetObject",
                true,
                ptr => new DocumentObject(this.Session, ObjectTypeName, ptr)
                );
        }

        BaseSessionObject ISelection.GetObject()
        {
            return GetObject();
        }

    }
}
