using DigitBridge.Base.Utility;
using System;
using System.Text;
using System.Text.Json.Serialization;

namespace DigitBridge.CommerceCentral.ERPApiSDK
{
    /// <summary>
    ///  Request payload object
    /// </summary>
    [Serializable()]
    public class WMSSalesOrderRequestPayload : RequestPayloadBase
    {
        // Add more parameters here
    }

    /// <summary>
    ///  Response payload object
    /// </summary>
    [Serializable()]
    public class WMSSalesOrderResponsePayload : ResponsePayloadBase
    {
        #region list service

        /// <summary>
        /// (Response Data) List result which load filter and paging.
        /// </summary>   
        public string SalesOrderOpenList { get; set; }

        /// <summary>
        /// (Response Data) List result count which load filter and paging.
        /// </summary>
        public int SalesOrderOpenListCount { get; set; }

        #endregion list service 
    }
}

