using DigitBridge.CommerceCentral.CommerceCentralDb;
using DigitBridge.CommerceCentral.CommerceCentralDb.Infrastructure;
using DigitBridge.CommerceCentral.CommerceCentralDb.Model;
using DigitBridge.CommerceCentral.CommerceCentralMdl.Infrastructure;
using DigitBridge.CommerceCentral.CommerceCentralMdl.Model;
using DigitBridge.CommerceCentral.Entity.OrderShipments;
using DigitBridge.Orchestration.DataType;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using UneedgoHelper.DotNet.Common;
using UneedgoHelper.DotNet.Data.MsSql;
using DigitBridge.CommerceCentral.Entity.Orders;
using DigitBridge.Biz.Events.Entities;
using DigitBridge.CommerceCentral.Entity.Events;
using DigitBridge.Biz.Events;
using DigitBridge.CommerceCentral.Entity.Enums;

namespace DigitBridge.CommerceCentral.CommerceCentralMdl.CommerceCentralShipment
{
    public class AddCentralOrderShipment
    {
        private DatabaseInfo _centralDbInfo;
        private string _tokenProvider;
        private string _logConnString;

        protected ComerceCentralOrderShipmentDb _addOrderShipmentDb;
        protected ComerceCentralOrderDCAssignmentDb _assignDb;
        protected ComerceCentralOrderProcessDb _centralOrderDb;

        protected DataTableSetSettingType _addOrderShipmentDtSet;
        protected DataTableSetSettingType _orderDCAssignmentDtSet;
        protected DataTableSetSettingType _centralOrderDtSet;
        protected DataTableSetSettingType _chkAssignedOrderDtSet;

        protected DatabaseInfo _dbInfo;

        private EventConnector _eventConnector;
        public AddCentralOrderShipment(DatabaseInfo cenDbInfo
            , string tokenProvider
            , string logTableConnStrings
            , string universalEventEndpoint
            , string universalEventKey
            )
        {
            _centralDbInfo = cenDbInfo;

            _tokenProvider = tokenProvider;

            _logConnString = logTableConnStrings;

            SetProcessDataSets();

            _eventConnector = new EventConnector()
            {
                TopicEndpoint = universalEventEndpoint,
                TopicKey = universalEventKey
            };

        }

        private void SetProcessDataSets()
        {
            _addOrderShipmentDtSet = new DataTableSetSettingType()
            {
                TableSetName = "OrderShipment",
                Tables = new List<DataTableSettingType>()
                {
                    new DataTableSettingType()
                    {
                        TableName = "OrderShipmentHeader",
                        IdentityColumnName = "OrderShipmentNum",
                        ProcessStatusName = "ProcessStatus",
                        ProcessStatusDateName = "ProcessDateUtc",
                    },
                    new DataTableSettingType()
                    {
                        TableName = "OrderShipmentPackage",
                        IdentityColumnName = "OrderShipmentPackageNum",
                        HasParentIdentities = true,
                        ParentIdentitis = new List<DataTableParentIdentityType>()
                        {
                            new DataTableParentIdentityType()
                            {
                                ParentTableName = "OrderShipmentHeader",
                                IdentityColumnForParent = "OrderShipmentNum",
                            }
                        }
                    },
                    new DataTableSettingType()
                    {
                        TableName = "OrderShipmentShippedItem",
                        IdentityColumnName = "OrderShipmentShippedItemNum",
                        HasParentIdentities = true,
                        ParentIdentitis = new List<DataTableParentIdentityType>()
                        {
                            new DataTableParentIdentityType()
                            {
                                ParentTableName = "OrderShipmentHeader",
                                IdentityColumnForParent = "OrderShipmentNum",
                                TempValue = -999
                            },
                            new DataTableParentIdentityType()
                            {
                                ParentTableName = "OrderShipmentPackage",
                                IdentityColumnForParent = "OrderShipmentPackageNum",
                                TempValue = -888
                            }
                        }
                    },
                    new DataTableSettingType()
                    {
                        TableName = "OrderShipmentCanceledItem",
                        IdentityColumnName = "OrderShipmentCanceledItemNum",
                        HasParentIdentities = true,
                        ParentIdentitis = new List<DataTableParentIdentityType>()
                        {
                            new DataTableParentIdentityType()
                            {
                                ParentTableName = "OrderShipmentHeader",
                                IdentityColumnForParent = "OrderShipmentNum",
                            }
                        }
                    },
                    new DataTableSettingType()
                    {
                        TableName = "view_CommerceCentralOrderShipmentHeader",
                        HasParentIdentities = true,
                        ParentIdentitis = new List<DataTableParentIdentityType>()
                        {
                            new DataTableParentIdentityType()
                            {
                                ParentTableName = "OrderShipmentHeader",
                                IdentityColumnForParent = "OrderShipmentNum",
                            }
                        }
                    },
                    new DataTableSettingType()
                    {
                        TableName = "view_CommerceCentralOrderShipmentPackage",
                        HasParentIdentities = true,
                        ParentIdentitis = new List<DataTableParentIdentityType>()
                        {
                            new DataTableParentIdentityType()
                            {
                                ParentTableName = "OrderShipmentHeader",
                                IdentityColumnForParent = "OrderShipmentNum",
                            }
                        }
                    },
                }
            };

            _orderDCAssignmentDtSet = new DataTableSetSettingType()
            {
                TableSetName = "OrderDCAssignment",
                Tables = new List<DataTableSettingType>()
                {
                    new DataTableSettingType()
                    {
                        TableName = "OrderDCAssignmentHeader",
                        IdentityColumnName = "OrderDCAssignmentNum",
                    },
                    new DataTableSettingType()
                    {
                        TableName = "OrderDCAssignmentLine",
                        HasParentIdentities = true,
                        ParentIdentitis = new List<DataTableParentIdentityType>()
                        {
                            new DataTableParentIdentityType()
                            {
                                ParentTableName = "OrderDCAssignmentHeader",
                                IdentityColumnForParent = "OrderDCAssignmentNum",
                            }
                        }
                    }
                }
            };

            _centralOrderDtSet = new DataTableSetSettingType()
            {
                TableSetName = "CentralOrder",
                Tables = new List<DataTableSettingType>()
                {
                    new DataTableSettingType()
                    {
                        TableName = "OrderHeader",
                        IdentityColumnName = "CentralOrderNum",
                    },
                    new DataTableSettingType()
                    {
                        TableName = "OrderLine",
                        HasParentIdentities = true,
                        ParentIdentitis = new List<DataTableParentIdentityType>()
                        {
                            new DataTableParentIdentityType()
                            {
                                ParentTableName = "OrderHeader",
                                IdentityColumnForParent = "CentralOrderNum",
                            }
                        }
                    }
                }
            };

            _chkAssignedOrderDtSet = new DataTableSetSettingType()
            {
                TableSetName = "CheckOrderDCAssignment",
                Tables = new List<DataTableSettingType>()
                {
                    new DataTableSettingType()
                    {
                        TableName = "OrderDCAssignmentHeader",
                        IdentityColumnName = "CentralOrderNum",
                    },
                    new DataTableSettingType()
                    {
                        TableName = "OrderDCAssignmentLine",
                        HasParentIdentities = true,
                        ParentIdentitis = new List<DataTableParentIdentityType>()
                        {
                            new DataTableParentIdentityType()
                            {
                                ParentTableName = "OrderDCAssignmentHeader",
                                IdentityColumnForParent = "CentralOrderNum",
                            }
                        }
                    }
                }
            };
        }

        protected async Task AddErrorLogAsync(ErrorTypeEnum errorType, string logKey, string logMsg, StackFrame souceSF)
        {
            try
            {
                await AzureStorageUtility.WriteAzureErrorLogAsync(
                      LogContainer.ErrorLogOrderShipment.ToString()
                      , _logConnString
                      , LogTypeEnum.AddCentralOrderShipment
                      , errorType
                      , logKey
                      , logMsg
                      , souceSF);
            }
            catch (Exception ex)
            {
                throw ExceptionUtility.WrapException(MethodBase.GetCurrentMethod(), ex);
            }
        }

        protected void AddErpEventMessage(CentralEventErpType eventMessage)
        {
            try
            {
                if (_eventConnector.TopicEndpoint != "")
                {
                    BizEventCollaborator.PostEvent(eventMessage
                      , ErpEventConst.FlatGridSubject
                      , ErpEventConst.FlatGridEventType
                      , _eventConnector);
                }
            }
            catch (Exception ex)
            {
                throw ExceptionUtility.WrapException(MethodBase.GetCurrentMethod(), ex);
            }
        }

        private CommerceCentralOrderShipmentType FillEmptyOrderDCAssignmentShipmentValue(CommerceCentralOrderShipmentType orderShipment
            , OrderDCAssignmentHeaderType dcOrderHeader
            , List<OrderDCAssignmentLineType> dcOrderLines)
        {
            CommerceCentralOrderShipmentType newOrderShipment = new CommerceCentralOrderShipmentType();

            newOrderShipment.ShipmentHeader = FillEmptyValue(dcOrderHeader, orderShipment.ShipmentHeader);
            newOrderShipment.ShipmentHeader.WarehouseCode = dcOrderHeader.SellerWarehouseID;
            if (orderShipment.PackageItems != null && orderShipment.PackageItems.Count > 0)
            {
                newOrderShipment.PackageItems = new List<CommerceCentralOrderShipmentPackageItemsType>();
                foreach (var pkg in orderShipment.PackageItems)
                {
                    var newShipPkg = FillEmptyValue(dcOrderHeader, pkg.ShipmentPackage);
                    newShipPkg.OrderShipmentUuid = newOrderShipment.ShipmentHeader.OrderShipmentUuid;
                    List<CommerceCentralOrderShipmentShippedItemType> newShippedItems = new List<CommerceCentralOrderShipmentShippedItemType>();
                    foreach (var pkgItem in pkg.ShippedItems)
                    {
                        var line = dcOrderLines.FirstOrDefault(p => p.OrderDCAssignmentLineNum == pkgItem.OrderDCAssignmentLineNum);
                        var newPkgItem = FillEmptyValue(line, pkgItem);
                        newPkgItem.OrderShipmentPackageUuid = newShipPkg.OrderShipmentPackageUuid;
                        newPkgItem.OrderShipmentUuid = newOrderShipment.ShipmentHeader.OrderShipmentUuid;
                        newShippedItems.Add(newPkgItem);
                    }
                    CommerceCentralOrderShipmentPackageItemsType newPkg = new CommerceCentralOrderShipmentPackageItemsType()
                    {
                        ShipmentPackage = newShipPkg,
                        ShippedItems = newShippedItems
                    };
                    newOrderShipment.PackageItems.Add(newPkg);
                }
            }
            if (orderShipment.CanceledItems != null && orderShipment.CanceledItems.Count > 0)
            {
                List<CommerceCentralOrderShipmentCanceledItemType> newCanceledItems = new List<CommerceCentralOrderShipmentCanceledItemType>();
                foreach (var canceledItem in orderShipment.CanceledItems)
                {
                    var line = dcOrderLines.FirstOrDefault(p => p.OrderDCAssignmentLineNum == canceledItem.OrderDCAssignmentLineNum);
                    var newCanceledItem = FillEmptyValue(line, canceledItem);
                    newCanceledItem.OrderShipmentUuid = newOrderShipment.ShipmentHeader.OrderShipmentUuid;
                    newCanceledItems.Add(newCanceledItem);
                }

                newOrderShipment.CanceledItems = newCanceledItems;
            }
            return newOrderShipment;
        }

        private T FillEmptyValue<S, T>(S sourceObj, T destObj)
        {
            Dictionary<string, string> sourceDic = sourceObj.ObjectToDicationary<string>();
            Dictionary<string, string> destDic = destObj.ObjectToDicationary<string>();

            List<string> emptyDestValueFieldNames = destDic.Where(p => string.IsNullOrEmpty(p.Value)).Select(p => p.Key).ToList();
            List<string> notEmptySourceValueFieldNames = sourceDic.Where(p => !string.IsNullOrEmpty(p.Value)).Select(p => p.Key).ToList();
            foreach (string fieldName in emptyDestValueFieldNames)
            {
                if (notEmptySourceValueFieldNames.Contains(fieldName) && destDic[fieldName] != sourceDic[fieldName])
                {
                    destDic[fieldName] = sourceDic[fieldName];
                }
            }
            List<string> zeroDestValueFieldNames = destDic.Where(p => p.Value == "0").Select(p => p.Key).ToList();
            foreach (string fieldName in zeroDestValueFieldNames)
            {
                if (notEmptySourceValueFieldNames.Contains(fieldName) && destDic[fieldName] != sourceDic[fieldName])
                {
                    destDic[fieldName] = sourceDic[fieldName];
                }
            }

            List<string> invalidDateFieldNames = destDic.Where(p => p.Value == "0001-01-01T00:00:00").
                Select(p => p.Key).ToList();
            foreach (string fieldName in invalidDateFieldNames)
            {
                if (fieldName.ToLower().Contains("date"))
                {
                    destDic[fieldName] = DateTime.UtcNow.ToString();
                }
            }

            T newDestObj = destDic.DicationaryToObject<T>();

            return newDestObj;
        }

        private void SetSpecialShipmentValue(ref CommerceCentralOrderShipmentType orderShipment)
        {
            bool hasShipPkgs = orderShipment.PackageItems == null ? false : true;
            bool hasCanceledItems = orderShipment.CanceledItems == null ? false : true;
            if (hasShipPkgs && hasCanceledItems)
            {
                orderShipment.ShipmentHeader.ShipmentStatus = (int)CommerceCentralConst.OrderStatus.PartiallyShipped;
            }
            else if (hasShipPkgs)
            {
                orderShipment.ShipmentHeader.ShipmentStatus = (int)CommerceCentralConst.OrderStatus.Shipped;
            }
            else
            {
                orderShipment.ShipmentHeader.ShipmentStatus = (int)CommerceCentralConst.OrderStatus.Canceled;
            }
            if (orderShipment.ShipmentHeader.ShippingCarrier.ToLower().Contains("fedex"))
            {
                orderShipment.ShipmentHeader.ShippingCarrier = "FedEx";
            }
        }
        private List<string> _cancelCodes;
        private List<string> _carrierCodes;
        private Dictionary<string, List<string>> _carrierServices;
        private List<CommerceCentralOrderShipmentHeaderType> _existOrderShipmentHeaders = new List<CommerceCentralOrderShipmentHeaderType>();
        private List<CommerceCentralOrderShipmentPackageType> _existOrderShipmentPkgs = new List<CommerceCentralOrderShipmentPackageType>();
        private List<CommerceCentralOrderDCAssignmentType> _orderDCAssignments = new List<CommerceCentralOrderDCAssignmentType>();

        public async Task<PostOrderShipmentResponseType> PostOrderDCAssignmentShipmentsAsync(PostOrderShipmentRequestType req)
        {
            bool ret = true;
            string errMsg = "";
            PostOrderShipmentResponseType resp = new PostOrderShipmentResponseType()
            {
                ResultStatus = Entity.Enums.ResultStatusEnum.Success,
                ResultMessage = "",
                SuccessShipmentIDs = new List<string>(),
                FailedShipments = new Dictionary<string, string>()
            };

            MsSqlUniversal msSql = await MsSqlUniversal.CreateAsync(
                _centralDbInfo.ConnectionString
                , _centralDbInfo.UseManagedIdentity
                , _tokenProvider
                , _centralDbInfo.TenantId
                );

            _addOrderShipmentDb = new ComerceCentralOrderShipmentDb(msSql, _addOrderShipmentDtSet);
            _assignDb = new ComerceCentralOrderDCAssignmentDb(msSql, _orderDCAssignmentDtSet);

            try
            {
                _cancelCodes = await _addOrderShipmentDb.LoadDefinedCancelCodesAsync();
                _carrierCodes = await _addOrderShipmentDb.LoadDefinedShippingCarrierAsync();
                _carrierServices = await _addOrderShipmentDb.LoadDefinedShippingCarrierServiceAsync();

                List<string> shipmentIDs = req.InputShipments.Select(p => p.ShipmentHeader.ShipmentID).ToList();
                _existOrderShipmentHeaders = await _addOrderShipmentDb.ExistOrderShipmentsByShipmentIDAsync(shipmentIDs);
                var pkgs = req.InputShipments.Where(p => p.PackageItems != null && p.PackageItems.Count() > 0).SelectMany(p => p.PackageItems).ToList();
                if (pkgs != null && pkgs.Count() > 0)
                {
                    List<string> trkNumbers = pkgs.Select(p => p.ShipmentPackage).Select(p => p.PackageTrackingNumber).ToList();
                    _existOrderShipmentPkgs = await _addOrderShipmentDb.ExistOrderShipmentTrackingNums(trkNumbers);
                }

                List<long> orderDCAssignmentNums = req.InputShipments.Where(p => p.ShipmentHeader.OrderDCAssignmentNum != 0).
                    Select(p => p.ShipmentHeader.OrderDCAssignmentNum).ToList();

                if (orderDCAssignmentNums == null || orderDCAssignmentNums.Count == 0)
                {
                    resp.ResultStatus = Entity.Enums.ResultStatusEnum.Failed;
                    resp.ResultMessage = "No Valid OrderDCAssignmentNum";
                    req.InputShipments.ForEach(p => resp.FailedShipments.Add(p.ShipmentHeader.ShipmentID, "Invalid OrderDCAssignmentNum"));
                    return resp;
                }

                (ret, errMsg, _orderDCAssignments) = await _assignDb.GetOrderDCAssignmentsAsync(orderDCAssignmentNums);
                if (ret == false)
                {
                    resp.ResultStatus = Entity.Enums.ResultStatusEnum.Failed;
                    resp.ResultMessage = errMsg;
                    req.InputShipments.ForEach(p => resp.FailedShipments.Add(p.ShipmentHeader.ShipmentID, errMsg));
                    return resp;
                }

                foreach (var inputShipment in req.InputShipments)
                {
                    string shipmentID = inputShipment.ShipmentHeader.ShipmentID;
                    long orderDCAssignmentNum = inputShipment.ShipmentHeader.OrderDCAssignmentNum;
                    //Verify InputShipments

                    var orderDCAssignment = _orderDCAssignments.FirstOrDefault(p => p.OrderDCAssignmentHeader.OrderDCAssignmentNum == orderDCAssignmentNum);
                    if (orderDCAssignment == null)
                    {
                        resp.FailedShipments.Add(shipmentID, "Cannot find OrderDCAssignment with OrderDCAssignmentNum " + orderDCAssignmentNum);
                        continue;
                    }

                    (ret, errMsg) = await AddOrerShipmentBaseOnOrderDCAssignmentAsync(inputShipment, orderDCAssignment);
                    if (ret)
                    {
                        resp.SuccessShipmentIDs.Add(shipmentID);
                    }
                    else
                    {
                        resp.FailedShipments.Add(shipmentID, errMsg);
                    }
                }

                if (resp.FailedShipments.Count > 0)
                {
                    if (resp.SuccessShipmentIDs.Count > 0)
                    {
                        resp.ResultStatus = Entity.Enums.ResultStatusEnum.PartialFailed;
                    }
                    else
                    {
                        resp.ResultStatus = Entity.Enums.ResultStatusEnum.Failed;
                    }
                    resp.ResultMessage = $"PostFailed shipments: {resp.FailedShipments.Count}. Total PostShipments {req.InputShipments.Count()}.";
                }
                return resp;
            }
            catch (Exception ex)
            {
                StackFrame souceSF = new System.Diagnostics.StackTrace(true).GetFrame(0);

                await AddErrorLogAsync(ErrorTypeEnum.Exception, "", ex.Message, souceSF);

                throw ExceptionUtility.WrapException(MethodBase.GetCurrentMethod(), ex);
            }
            finally
            {
                msSql.Dispose();
            }
        }

        private async Task<(bool, string)> AddOrerShipmentBaseOnOrderDCAssignmentAsync(InputOrderShipmentType shipment
            , CommerceCentralOrderDCAssignmentType orderDCAssignment)
        {
            bool ret = true;
            string errMsg = "";

            string shipmentID = shipment.ShipmentHeader.ShipmentID;

            int chnAccNum = orderDCAssignment.OrderDCAssignmentHeader.ChannelAccountNum;
            try
            {
                (ret, errMsg) = VerifyInputOrderDCAssignmentShipment(shipment, orderDCAssignment);

                if (ret == true)
                {
                    CommerceCentralOrderShipmentType orderShipment = new CommerceCentralOrderShipmentType();
                    //Map inputShipment to orderShipment
                    orderShipment = (shipment.ObjectToString()).StringToObject<CommerceCentralOrderShipmentType>();

                    orderShipment = FillEmptyOrderDCAssignmentShipmentValue(orderShipment
                        , orderDCAssignment.OrderDCAssignmentHeader, orderDCAssignment.OrderDCAssignmentLines);

                    SetSpecialShipmentValue(ref orderShipment);

                    CentralEventErpType eventErp = new CentralEventErpType()
                    {
                        DatabaseNum = orderShipment.ShipmentHeader.DatabaseNum,
                        ErpEventType = ErpEventTypeEnum.ToCreateInvoice,
                        MasterAccountNum = orderShipment.ShipmentHeader.MasterAccountNum,
                        ProfileNum = orderShipment.ShipmentHeader.ProfileNum,
                        ChannelNum = orderShipment.ShipmentHeader.ChannelNum,
                        ChannelAccountNum = orderShipment.ShipmentHeader.ChannelAccountNum,
                        ProcessSource = "OrderShipment",
                        ProcessUuid = orderShipment.ShipmentHeader.OrderShipmentUuid
                    };

                    CommerceCentralOrderActivityLogType orderLog = new CommerceCentralOrderActivityLogType()
                    {
                        DatabaseNum = orderShipment.ShipmentHeader.DatabaseNum,
                        MasterAccountNum = orderShipment.ShipmentHeader.MasterAccountNum,
                        ProfileNum = orderShipment.ShipmentHeader.ProfileNum,
                        ChannelNum = orderShipment.ShipmentHeader.ChannelNum,
                        ChannelAccountNum = orderShipment.ShipmentHeader.ChannelAccountNum,
                        CentralOrderNum = orderShipment.ShipmentHeader.CentralOrderNum,
                        ActivityType = (int)OrderActivityTypeEnum.OrderShipment,
                        ActivityNote = "Created OrderShipment. " +
                            $"ShipmentID: {orderShipment.ShipmentHeader.ShipmentID}. " +
                            $"ShipmentStatus: {orderShipment.ShipmentHeader.ShipmentStatus}. " +
                            (orderShipment.ShipmentHeader.ShipmentStatus == (int)ShipmentStatusEnum.Canceled ? "" : 
                                "TrackingNum: " + orderShipment.ShipmentHeader.MainTrackingNumber),
                        //ActivityNote = $"Get OrderShipment From DistributionCenter {orderShipment.ShipmentHeader.DistributionCenterNum}. " +
                        //                    $"ShipmentID: {orderShipment.ShipmentHeader.ShipmentID} ShipmentStatus: {orderShipment.ShipmentHeader.ShipmentStatus}.",
                        DigitBridgeGuid = orderShipment.ShipmentHeader.OrderShipmentUuid,
                        CentralOrderUuid = ""
                    };

                    (ret, errMsg) = await _addOrderShipmentDb.AddAsync(orderShipment, eventErp, orderLog);
                    if (ret)
                    {
                        //Add ErpEvent To Event Grid
                        AddErpEventMessage(eventErp);
                    }
                }

            }
            catch (Exception ex)
            {
                StackFrame souceSF = new System.Diagnostics.StackTrace(true).GetFrame(0);

                await AddErrorLogAsync(ErrorTypeEnum.Exception, "", ex.Message, souceSF);

                ret = false;
                errMsg = ex.Message;
            }
            if (ret == false)
            {
                errMsg = "ChannelAccountNum: " + chnAccNum + " ShipmentID: " + shipmentID + " " + errMsg;
            }
            return (ret, errMsg);
        }

        public Task GetShippingCarrier()
        {
            throw new NotImplementedException();
        }

        private (bool ret, string errMsg) VerifyInputOrderDCAssignmentShipment(InputOrderShipmentType shipment
            , CommerceCentralOrderDCAssignmentType orderDCAssignment)
        {
            try
            {
                string shipmentID = shipment.ShipmentHeader.ShipmentID;

                int chnAccNum = orderDCAssignment.OrderDCAssignmentHeader.ChannelAccountNum;

                if (_existOrderShipmentHeaders != null && _existOrderShipmentHeaders.Count > 0)
                {
                    var existOrderShipment = _existOrderShipmentHeaders.FirstOrDefault(p => p.ChannelAccountNum == chnAccNum &&
                        p.ShipmentID == shipmentID);
                    if (existOrderShipment != null)
                    {
                        //ordershipment exists
                        return (false, $"OrderShipment Exists");
                    }
                }


                var orderDCAssignmentLines = orderDCAssignment.OrderDCAssignmentLines;
                bool hasShipPkg = false, hasCanceledItems = false;

                if (shipment.PackageItems != null && shipment.PackageItems.Count > 0)
                {
                    if (!_carrierCodes.Contains(shipment.ShipmentHeader.ShippingCarrier, StringComparer.InvariantCultureIgnoreCase))
                    {
                        return (false, "Invalide ShippingCarrier: " + shipment.ShipmentHeader.ShippingCarrier);
                    }
                    var carrierServices = _carrierServices.Where(p => p.Key.ToUpper() == shipment.ShipmentHeader.ShippingCarrier.ToUpper()).First().Value;
                    if (!carrierServices.Contains(shipment.ShipmentHeader.ShippingClass, StringComparer.InvariantCultureIgnoreCase))
                    {
                        return (false, "Invalide ShippingClass: " + shipment.ShipmentHeader.ShippingClass);
                    }

                    hasShipPkg = true;
                    foreach (var pkg in shipment.PackageItems)
                    {
                        if (pkg.ShippedItems == null || pkg.ShippedItems.Count == 0)
                        {
                            return (false, "ShippedItems' cannot be empty in ShipmentPackageID " + pkg.ShipmentPackage.PackageID);
                        }
                        var noShipQtys = pkg.ShippedItems.Where(p => p.ShippedQty == 0).ToList();
                        if (noShipQtys.Count > 0)
                        {
                            return (false, "ShippedItems' Qty cannot be 0 in ShipmentPackageID " + pkg.ShipmentPackage.PackageID);
                        }

                        var noOrderDCAssignmentLineNums = pkg.ShippedItems.Where(p => p.OrderDCAssignmentLineNum == 0).ToList();
                        if (noOrderDCAssignmentLineNums.Count > 0)
                        {
                            return (false, "ShippedItems' OrderDCAssignmentLineNum cannot be 0 in ShipmentPackageID " + pkg.ShipmentPackage.PackageID);
                        }

                        if (_existOrderShipmentPkgs != null && _existOrderShipmentPkgs.Count > 0)
                        {
                            var existTrackingNumber = _existOrderShipmentPkgs.FirstOrDefault(p => p.ChannelAccountNum == chnAccNum &&
                                p.PackageTrackingNumber.Equals(pkg.ShipmentPackage.PackageTrackingNumber, StringComparison.InvariantCultureIgnoreCase));
                            if (existTrackingNumber != null)
                            {
                                return (false, "PackageTrackingNumber exists " + pkg.ShipmentPackage.PackageTrackingNumber);
                            }
                        }

                        foreach (var pkgItem in pkg.ShippedItems)
                        {
                            var orderDCAssignmentLine = orderDCAssignmentLines.FirstOrDefault(p => p.OrderDCAssignmentLineNum == pkgItem.OrderDCAssignmentLineNum);
                            if (orderDCAssignmentLine == null)
                            {
                                return (false, "Cannot find OrderDCAssignmentLine with OrderDCAssignmentLineNum " + pkgItem.OrderDCAssignmentLineNum);
                            }
                        }
                    }
                }
                if (shipment.CanceledItems != null && shipment.CanceledItems.Count > 0)
                {
                    hasCanceledItems = true;
                    var noCancelQtys = shipment.CanceledItems.Where(p => p.CanceledQty == 0).ToList();
                    if (noCancelQtys.Count > 0)
                    {
                        return (false, "CanceledItems' Qty cannot be 0");
                    }
                    var noOrderDCAssignmentLineNums = shipment.CanceledItems.Where(p => p.OrderDCAssignmentLineNum == 0).ToList();
                    if (noOrderDCAssignmentLineNums.Count > 0)
                    {
                        return (false, "CanceledItems' OrderDCAssignmentLineNum cannot be 0");
                    }
                    foreach (var canceledItem in shipment.CanceledItems)
                    {
                        var orderDCAssignmentLine = orderDCAssignmentLines.FirstOrDefault(p => p.OrderDCAssignmentLineNum == canceledItem.OrderDCAssignmentLineNum);
                        if (orderDCAssignmentLine == null)
                        {
                            return (false, "Cannot find OrderDCAssignmentLine with OrderDCAssignmentLineNum " + canceledItem.OrderDCAssignmentLineNum);
                        }
                    }

                    var unknowCancelCodes = shipment.CanceledItems.Where(p => !_cancelCodes.Contains(p.CancelCode, StringComparer.InvariantCultureIgnoreCase)).
                        Select(p => p.CancelCode).Distinct().ToList();
                    if (unknowCancelCodes != null && unknowCancelCodes.Count > 0)
                    {
                        return (false, "Invalide CancelCodes: " + string.Join(", ", unknowCancelCodes));
                    }
                }

                if (hasShipPkg == false && hasCanceledItems == false)
                {
                    return (false, "ShipmentPackage and CanceledItems cannot both be empty.");
                }

                return (true, "");
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }


        private CommerceCentralOrderShipmentType FillEmptyCentralOrderShipmentValue(CommerceCentralOrderShipmentType orderShipment
            , CommerceCentralOrderHeaderRow orderHeader
            , List<CommerceCentralOrderLineRow> orderLines)
        {
            CommerceCentralOrderShipmentType newOrderShipment = new CommerceCentralOrderShipmentType();


            newOrderShipment.ShipmentHeader = FillEmptyValue(orderHeader, orderShipment.ShipmentHeader);
            //newOrderShipment.ShipmentHeader.OrderShipmentUuid = Guid.NewGuid().ToString();
            if (orderShipment.PackageItems != null && orderShipment.PackageItems.Count > 0)
            {
                newOrderShipment.PackageItems = new List<CommerceCentralOrderShipmentPackageItemsType>();
                foreach (var pkg in orderShipment.PackageItems)
                {
                    var newShipPkg = FillEmptyValue(orderHeader, pkg.ShipmentPackage);
                    newShipPkg.OrderShipmentUuid = newOrderShipment.ShipmentHeader.OrderShipmentUuid;

                    List<CommerceCentralOrderShipmentShippedItemType> newShippedItems = new List<CommerceCentralOrderShipmentShippedItemType>();
                    foreach (var pkgItem in pkg.ShippedItems)
                    {
                        var line = orderLines.FirstOrDefault(p => p.CentralOrderLineNum == pkgItem.CentralOrderLineNum);
                        var newPkgItem = FillEmptyValue(line, pkgItem);
                        newPkgItem.OrderShipmentPackageUuid = newShipPkg.OrderShipmentPackageUuid;
                        newPkgItem.OrderShipmentUuid = newOrderShipment.ShipmentHeader.OrderShipmentUuid;
                        newShippedItems.Add(newPkgItem);
                    }
                    CommerceCentralOrderShipmentPackageItemsType newPkg = new CommerceCentralOrderShipmentPackageItemsType()
                    {
                        ShipmentPackage = newShipPkg,
                        ShippedItems = newShippedItems
                    };
                    newOrderShipment.PackageItems.Add(newPkg);
                }
            }
            if (orderShipment.CanceledItems != null && orderShipment.CanceledItems.Count > 0)
            {
                List<CommerceCentralOrderShipmentCanceledItemType> newCanceledItems = new List<CommerceCentralOrderShipmentCanceledItemType>();
                foreach (var canceledItem in orderShipment.CanceledItems)
                {
                    var line = orderLines.FirstOrDefault(p => p.CentralOrderLineNum == canceledItem.CentralOrderLineNum);
                    var newCanceledItem = FillEmptyValue(line, canceledItem);
                    newCanceledItem.OrderShipmentUuid = newOrderShipment.ShipmentHeader.OrderShipmentUuid;
                    newCanceledItems.Add(newCanceledItem);
                }

                newOrderShipment.CanceledItems = newCanceledItems;
            }
            return newOrderShipment;
        }

        private List<CommerceCentralOrderTableType> _centralOrders = new List<CommerceCentralOrderTableType>();

        public async Task<PostOrderShipmentResponseType> PostCentralOrderShipmentsAsync(PostOrderShipmentRequestType req)
        {
            bool ret = true;
            string errMsg = "";
            PostOrderShipmentResponseType resp = new PostOrderShipmentResponseType()
            {
                ResultStatus = Entity.Enums.ResultStatusEnum.Success,
                ResultMessage = "",
                SuccessShipmentIDs = new List<string>(),
                FailedShipments = new Dictionary<string, string>()
            };

            MsSqlUniversal msSql = await MsSqlUniversal.CreateAsync(
                _centralDbInfo.ConnectionString
                , _centralDbInfo.UseManagedIdentity
                , _tokenProvider
                , _centralDbInfo.TenantId
                );

            _addOrderShipmentDb = new ComerceCentralOrderShipmentDb(msSql, _addOrderShipmentDtSet);
            _centralOrderDb = new ComerceCentralOrderProcessDb(msSql, _centralOrderDtSet);
            _assignDb = new ComerceCentralOrderDCAssignmentDb(msSql, _orderDCAssignmentDtSet);

            try
            {
                _cancelCodes = await _addOrderShipmentDb.LoadDefinedCancelCodesAsync();
                _carrierCodes = await _addOrderShipmentDb.LoadDefinedShippingCarrierAsync();
                _carrierServices = await _addOrderShipmentDb.LoadDefinedShippingCarrierServiceAsync();

                List<string> shipmentIDs = req.InputShipments.Select(p => p.ShipmentHeader.ShipmentID).ToList();
                _existOrderShipmentHeaders = await _addOrderShipmentDb.ExistOrderShipmentsByShipmentIDAsync(shipmentIDs);

                var pkgs = req.InputShipments.Where(p => p.PackageItems != null && p.PackageItems.Count() > 0).SelectMany(p => p.PackageItems).ToList();
                if (pkgs != null && pkgs.Count() > 0)
                {
                    List<string> trkNumbers = pkgs.Select(p => p.ShipmentPackage).Select(p => p.PackageTrackingNumber).ToList();
                    _existOrderShipmentPkgs = await _addOrderShipmentDb.ExistOrderShipmentTrackingNums(trkNumbers);
                }

                List<long> centralOrderNums = req.InputShipments.Where(p => p.ShipmentHeader.CentralOrderNum != 0).
                    Select(p => p.ShipmentHeader.CentralOrderNum).ToList();
                if (centralOrderNums == null || centralOrderNums.Count == 0)
                {
                    resp.ResultStatus = Entity.Enums.ResultStatusEnum.Failed;
                    resp.ResultMessage = "No Valid CentralOrderNum";
                    req.InputShipments.ForEach(p => resp.FailedShipments.Add(p.ShipmentHeader.ShipmentID, "Invalid CentralOrderNum"));
                    return resp;
                }

                (ret, errMsg, _centralOrders) = await _centralOrderDb.GetCentralOrdersAsync(centralOrderNums);
                if (ret == false)
                {
                    resp.ResultStatus = Entity.Enums.ResultStatusEnum.Failed;
                    resp.ResultMessage = errMsg;
                    req.InputShipments.ForEach(p => resp.FailedShipments.Add(p.ShipmentHeader.ShipmentID, errMsg));
                    return resp;
                }
                (ret, errMsg, _orderDCAssignments) = await _assignDb.GetAssignedOrdersByCentralOrderNumAsync(centralOrderNums, _chkAssignedOrderDtSet);

                foreach (var inputShipment in req.InputShipments)
                {
                    string shipmentID = inputShipment.ShipmentHeader.ShipmentID;
                    long centralOrderNum = inputShipment.ShipmentHeader.CentralOrderNum;

                    //Verify InputShipments
                    var centralOrderHeader = _centralOrders.SelectMany(p => p.CentralOrderHeaders).FirstOrDefault(p => p.CentralOrderNum == centralOrderNum);
                    var centralOrderLines = _centralOrders.SelectMany(p => p.CentralOrderLines).Where(p => p.CentralOrderNum == centralOrderNum).ToList();
                    if (centralOrderHeader == null)
                    {
                        resp.FailedShipments.Add(shipmentID, "Cannot find CentralOrder with CentralOrderNum " + centralOrderNum);
                        continue;
                    }

                    var orderDCAssignment = _orderDCAssignments.FirstOrDefault(p => p.OrderDCAssignmentHeader.CentralOrderNum == centralOrderNum);
                    if (orderDCAssignment != null)
                    {
                        (ret, errMsg) = await AddOrerShipmentBaseOnOrderDCAssignmentAsync(inputShipment, orderDCAssignment);
                    }
                    else
                    {
                        (ret, errMsg) = await AddOrerShipmentBaseOnCentralOrderAsync(inputShipment, centralOrderHeader, centralOrderLines);
                    }

                    if (ret)
                    {
                        resp.SuccessShipmentIDs.Add(shipmentID);
                    }
                    else
                    {
                        resp.FailedShipments.Add(shipmentID, errMsg);
                    }
                }

                if (resp.FailedShipments.Count > 0)
                {
                    if (resp.SuccessShipmentIDs.Count > 0)
                    {
                        resp.ResultStatus = Entity.Enums.ResultStatusEnum.PartialFailed;
                    }
                    else
                    {
                        resp.ResultStatus = Entity.Enums.ResultStatusEnum.Failed;
                    }
                    resp.ResultMessage = $"PostFailed shipments: {resp.FailedShipments.Count}. Total PostShipments: {req.InputShipments.Count()}.";
                }
                return resp;
            }
            catch (Exception ex)
            {
                StackFrame souceSF = new System.Diagnostics.StackTrace(true).GetFrame(0);

                await AddErrorLogAsync(ErrorTypeEnum.Exception, "", ex.Message, souceSF);

                throw ExceptionUtility.WrapException(MethodBase.GetCurrentMethod(), ex);
            }
            finally
            {
                msSql.Dispose();
            }
        }
        private async Task<(bool, string)> AddOrerShipmentBaseOnCentralOrderAsync(InputOrderShipmentType shipment
            , CommerceCentralOrderHeaderRow centralOrderHeader
            , List<CommerceCentralOrderLineRow> centralOrderLines)
        {
            bool ret = true;
            string errMsg = "";

            int chnAccNum = centralOrderHeader.ChannelAccountNum;
            string shipmentID = shipment.ShipmentHeader.ShipmentID;
            try
            {
                (ret, errMsg) = VerifyInputCentralOrderShipment(shipment, centralOrderLines);
                if (ret == true)
                {
                    CommerceCentralOrderShipmentType orderShipment = new CommerceCentralOrderShipmentType();
                    //Map inputShipment to orderShipment
                    orderShipment = (shipment.ObjectToString()).StringToObject<CommerceCentralOrderShipmentType>();

                    orderShipment = FillEmptyCentralOrderShipmentValue(orderShipment
                        , centralOrderHeader, centralOrderLines);

                    SetSpecialShipmentValue(ref orderShipment);

                    CentralEventErpType eventErp = new CentralEventErpType()
                    {
                        DatabaseNum = orderShipment.ShipmentHeader.DatabaseNum,
                        ErpEventType = ErpEventTypeEnum.ToCreateInvoice,
                        MasterAccountNum = orderShipment.ShipmentHeader.MasterAccountNum,
                        ProfileNum = orderShipment.ShipmentHeader.ProfileNum,
                        ChannelNum = orderShipment.ShipmentHeader.ChannelNum,
                        ChannelAccountNum = orderShipment.ShipmentHeader.ChannelAccountNum,
                        ProcessUuid = orderShipment.ShipmentHeader.OrderShipmentUuid
                    };

                    CommerceCentralOrderActivityLogType orderLog = new CommerceCentralOrderActivityLogType()
                    {
                        DatabaseNum = orderShipment.ShipmentHeader.DatabaseNum,
                        MasterAccountNum = orderShipment.ShipmentHeader.MasterAccountNum,
                        ProfileNum = orderShipment.ShipmentHeader.ProfileNum,
                        ChannelNum = orderShipment.ShipmentHeader.ChannelNum,
                        ChannelAccountNum = orderShipment.ShipmentHeader.ChannelAccountNum,
                        CentralOrderNum = orderShipment.ShipmentHeader.CentralOrderNum,
                        ActivityType = (int)OrderActivityTypeEnum.OrderShipment,
                        ActivityNote = "Created OrderShipment. " +
                            $"ShipmentID: {orderShipment.ShipmentHeader.ShipmentID}. " +
                            $"ShipmentStatus: {orderShipment.ShipmentHeader.ShipmentStatus}. " +
                            (orderShipment.ShipmentHeader.ShipmentStatus == (int)ShipmentStatusEnum.Canceled ? "" :
                                "TrackingNum: " + orderShipment.ShipmentHeader.MainTrackingNumber),
                        //ActivityNote = $"Get OrderShipment From DistributionCenter {orderShipment.ShipmentHeader.DistributionCenterNum}. " +
                        //                            $"ShipmentID: {orderShipment.ShipmentHeader.ShipmentID} ShipmentStatus: {orderShipment.ShipmentHeader.ShipmentStatus}.",
                        DigitBridgeGuid = orderShipment.ShipmentHeader.OrderShipmentUuid,
                        CentralOrderUuid = ""
                    };

                    (ret, errMsg) = await _addOrderShipmentDb.AddAsync(orderShipment, eventErp, orderLog);
                    if (ret)
                    {
                        //Add ErpEvent To Event Grid
                        AddErpEventMessage(eventErp);
                    }
                }

            }
            catch (Exception ex)
            {
                StackFrame souceSF = new System.Diagnostics.StackTrace(true).GetFrame(0);

                await AddErrorLogAsync(ErrorTypeEnum.Exception, "", ex.Message, souceSF);

                ret = false;
                errMsg = ex.Message;
            }
            if (ret == false)
            {
                errMsg = "ChannelAccountNum: " + chnAccNum + " ShipmentID: " + shipmentID + " " + errMsg;
            }
            return (ret, errMsg);
        }

        private (bool ret, string errMsg) VerifyInputCentralOrderShipment(InputOrderShipmentType shipment, List<CommerceCentralOrderLineRow> centralOrderLines)
        {
            try
            {
                string shipmentID = shipment.ShipmentHeader.ShipmentID;
                int chnAccNum = centralOrderLines.First().ChannelAccountNum;

                var existOrderShipment = _existOrderShipmentHeaders.FirstOrDefault(p => p.ChannelAccountNum == chnAccNum &&
                         p.ShipmentID == shipmentID);
                if (existOrderShipment != null)
                {
                    //ordershipment exists
                    return (false, $"OrderShipment Exists");
                }

                if (shipment.PackageItems != null && shipment.PackageItems.Count > 0)
                {
                    foreach (var pkg in shipment.PackageItems)
                    {
                        if (pkg.ShippedItems == null || pkg.ShippedItems.Count == 0)
                        {
                            return (false, "ShippedItems' cannot be empty in ShipmentPackageID " + pkg.ShipmentPackage.PackageID);
                        }
                        var noCentralOrderLineNums = pkg.ShippedItems.Where(p => p.CentralOrderLineNum == 0).ToList();
                        if (noCentralOrderLineNums.Count > 0)
                        {
                            return (false, "ShippedItems' CentralOrderLineNum cannot be 0 in ShipmentPackageID" + pkg.ShipmentPackage.PackageID);
                        }
                        if (_existOrderShipmentPkgs != null && _existOrderShipmentPkgs.Count > 0)
                        {
                            var existTrackingNumber = _existOrderShipmentPkgs.FirstOrDefault(p => p.ChannelAccountNum == chnAccNum &&
                                p.PackageTrackingNumber.Equals(pkg.ShipmentPackage.PackageTrackingNumber, StringComparison.InvariantCultureIgnoreCase));
                            if (existTrackingNumber != null)
                            {
                                return (false, "PackageTrackingNumber exists " + pkg.ShipmentPackage.PackageTrackingNumber);
                            }
                        }

                        foreach (var pkgItem in pkg.ShippedItems)
                        {
                            var orderLine = centralOrderLines.FirstOrDefault(p => p.CentralOrderLineNum == pkgItem.CentralOrderLineNum);
                            if (orderLine == null)
                            {
                                return (false, "Cannot find CentralOrderLine with CentralOrderLineNum " + pkgItem.CentralOrderLineNum);
                            }
                        }
                    }
                }
                if (shipment.CanceledItems != null && shipment.CanceledItems.Count > 0)
                {
                    var noCancelQtys = shipment.CanceledItems.Where(p => p.CanceledQty == 0).ToList();
                    if (noCancelQtys.Count > 0)
                    {
                        return (false, "CanceledItems' Qty cannot be 0");
                    }

                    var noCentralOrderLineNums = shipment.CanceledItems.Where(p => p.CentralOrderLineNum == 0).ToList();
                    if (noCentralOrderLineNums.Count > 0)
                    {
                        return (false, "CanceledItems' CentralOrderLineNum cannot be 0");
                    }
                    foreach (var canceledItem in shipment.CanceledItems)
                    {
                        var orderLine = noCentralOrderLineNums.FirstOrDefault(p => p.CentralOrderLineNum == canceledItem.CentralOrderLineNum);
                        if (orderLine == null)
                        {
                            return (false, "Cannot find CentralOrderLine with CentralOrderLineNum " + canceledItem.CentralOrderLineNum);
                        }
                    }

                    var unknowCancelCodes = shipment.CanceledItems.Where(p => !_cancelCodes.Contains(p.CancelCode)).
                                           Select(p => p.CancelCode).Distinct().ToList();
                    if (unknowCancelCodes != null && unknowCancelCodes.Count > 0)
                    {
                        return (false, "Invalide CancelCodes " + string.Join(", ", unknowCancelCodes));
                    }

                }
                return (true, "");
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }

    }

}
