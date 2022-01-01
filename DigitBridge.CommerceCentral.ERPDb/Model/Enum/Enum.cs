using System;
using System.Collections.Generic;
using System.Text;

namespace DigitBridge.CommerceCentral.ERPDb
{
    /// <summary>
    /// All Const variable
    /// </summary>
    public enum TransTypeEnum: int
    {
        /// <summary>
        /// invoice payment
        /// </summary>
        Payment=1,
        /// <summary>
        /// inovice return
        /// </summary>
        Return=2,

    }

    /// <summary>
    /// Sku type
    /// </summary>
    public enum SKUType : int
    {
        /// <summary>
        ///Key: SKU Warehouse
        /// </summary>
        GeneralMerchandise = 0,
        /// <summary>
        ///Key: SKU Color Size Width  Warehouse
        /// </summary>
        ApparelAndShoes = 1,

        /// <summary>
        ///Key: SKU Lot Warehouse
        /// </summary>
        FoodAndVitamin = 2,

        /// <summary>
        ///Key: SKU Warehouse Bundle
        /// </summary>
        ElectronicAndComputer = 3,

        /// <summary>
        ///Key: SKU Warehouse SN
        /// </summary>
        Application = 4,

        /// <summary>
        ///Key: SKU Warehouse Bundle
        /// </summary>
        Furniture = 5

    }
}
