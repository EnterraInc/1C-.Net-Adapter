using System;
using System.Collections.Generic;
using System.Text;

namespace Enterra.V8x1C.DOM
{
    /// <summary>
    /// Тип метаданных.
    /// </summary>
    public enum MetadataType
    {
        Unknown,
        DocumentCollection,
        CatalogCollection,
        
        InformationRegisterCollection,
        AccumulationRegisterCollection,
        AccountingRegisterCollection,
        CalculationRegisterCollection,
        EnumCollection,
        DocumentJournalCollection,
        ConstantCollection,

        Catalog,
        Document,
        
        InformationRegister,
        AccumulationRegister,
        AccountingRegister,
        CalculationRegister,
        Enum,
        DocumentJournal,
        Constant,

        RequisiteCollection,
        Requisite,
        TablePartCollection,
        TablePart,
        MeasureCollection,
        Measure,
        ResourceCollection,
        Resource
    }
}
