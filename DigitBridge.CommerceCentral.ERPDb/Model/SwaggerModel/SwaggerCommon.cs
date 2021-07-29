using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using DigitBridge.Base.Utility;
using DigitBridge.CommerceCentral.YoPoco;

namespace DigitBridge.CommerceCentral.ERPDb
{ 
    public class SwaggerOne<T>
    {
        /// <summary>
        /// Add T Dto in your Payload class
        /// eg. If T is  OrderShipmentDataDto then add  OrderShipmentDataDto Dto to ShipmentPayload
        /// </summary>
        public T Dto { get; set; }
    } 

    public class SwaggerList<T>  
    {
        /// <summary>
        /// Add T Dtos in your Payload class
        /// eg. If T is OrderShipmentDataDto[] then add  List<OrderShipmentDataDto> Dtos to ShipmentPayload
        /// </summary>
        public T Dtos { get; set; }
    } 
}
