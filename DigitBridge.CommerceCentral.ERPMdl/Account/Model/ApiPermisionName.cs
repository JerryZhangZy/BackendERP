using System;
using System.Collections.Generic;
using System.Text;

namespace DigitBridge.CommerceCentral.ERPMdl
{
    public partial class ApiPermisionName
    {
        public const string ManageOrders = "10001";
        public const string ManageDistributionCenters = "10002";
        public const string ManageProfileUser = "10003";
        public const string UserPermission = "10004";
        public const string Channels = "10005";
        public const string ManageShippingSetting = "10006";
        public const string ManageExport = "10007";
        public const string ManageProductElements = "10008";

        public const string ManageWarehouses = "40001";
        public const string ManageWarehouseLocations = "40002";
        public const string ManageWarehouseProducts = "40003";
        public const string ManageWarehousePOs = "40004";
        public const string ManageWarehouseInvenory = "40005";
        public const string ManageWarehouseSalesOrders = "40006";
        public const string ManageWarehouseRoutesRules = "40007";
        public const string ManageWarehouseShipments = "40008";
        public const string ManageWarehouseTransactionReasons = "40009";

        public const string ManageWarehouseAudits = "40010";
        public const string ManageWarehouseLotNumbers = "40011";
        public const string ManageWarehouseKits = "40012";
        public const string ManageWarehouseQualityControl = "40013";
        public const string ManageWarehouseSerialNumbers = "40014";
        public const string ManageWarehouseAssignments = "40015";
    }

    public enum ApiPermissionLevel
    {
        None = 0,
        View = 1,
        ViewEdit = 2,
        Admin = 4
    }
}
