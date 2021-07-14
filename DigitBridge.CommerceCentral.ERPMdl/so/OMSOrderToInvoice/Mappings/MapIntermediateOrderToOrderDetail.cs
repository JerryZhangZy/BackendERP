using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DuoDotNetHelper.CommonV1;
using GhpIntegration.IntermediateDb;
using GhpIntegration.OrderImportApmMdl;
namespace GhpIntegration.OrderImportApmMdl.Mappings
{
    public class MapIntermediateOrderToOrderDetail
    {
        public static OrderDetail ConvertIntermediateOrderToOrderDetail(IntermediateOrderDst imOrdDst)
        {
            OrderDetail ordDetail = new OrderDetail();
            try
            {
                if (imOrdDst.IntermediateOrderHeader.Count > 0)
                {
                    var imOrdHR = imOrdDst.IntermediateOrderHeader[0];

                    ordDetail.OrderNum = imOrdHR.OrderNum;
                    ordDetail.OrderDate = imOrdHR.OrderDate;
                    ordDetail.DatabaseSource = imOrdHR.DatabaseSourceID;
                    ordDetail.AccountID = imOrdHR.AccountID;
                    ordDetail.BranchID = imOrdHR.BranchID;
                    ordDetail.SplitNum = 0;
                    ordDetail.Customer.BillName = imOrdHR.BillName;
                    ordDetail.Customer.BillAddressLine1 = imOrdHR.BillAddressLine1;
                    ordDetail.Customer.BillAddressLine2 = imOrdHR.BillAddressLine2;
                    ordDetail.Customer.BillAddressLine3 = imOrdHR.BillAddressLine3;
                    ordDetail.Customer.BillCity = imOrdHR.BillCity;
                    ordDetail.Customer.BillState = imOrdHR.BillState;
                    ordDetail.Customer.BillZip = imOrdHR.BillZip;
                    ordDetail.Customer.BillCountry = imOrdHR.BillCountry;
                    ordDetail.Customer.BillPhone = imOrdHR.BillPhone;
                    ordDetail.Customer.BillEmail = imOrdHR.BillEmail;
                    ordDetail.Customer.ShipName = imOrdHR.ShipName;
                    ordDetail.Customer.ShipAddressLine1 = imOrdHR.ShipAddressLine1;
                    ordDetail.Customer.ShipAddressLine2 = imOrdHR.ShipAddressLine2;
                    ordDetail.Customer.ShipAddressLine3 = imOrdHR.ShipAddressLine3;
                    ordDetail.Customer.ShipCity = imOrdHR.ShipCity;
                    ordDetail.Customer.ShipState = imOrdHR.ShipState;
                    ordDetail.Customer.ShipZip = imOrdHR.ShipZip;
                    ordDetail.Customer.ShipCountry = imOrdHR.ShipCountry;
                    ordDetail.ShipDesc = imOrdHR.ShipDesc;
                    ordDetail.TotalAmt = imOrdHR.TotalAmt;
                    ordDetail.OrderAmt = imOrdHR.OrderAmt;
                    ordDetail.DiscountAmt = imOrdHR.DiscountAmt;
                    ordDetail.DiscountRate = imOrdHR.DiscountRate;
                    ordDetail.TaxAmt = imOrdHR.TaxAmt;
                    ordDetail.TaxRate = imOrdHR.TaxRate;
                    ordDetail.TaxableAmt = imOrdHR.TaxableAmt;
                    ordDetail.Freight = imOrdHR.Freight;
                    ordDetail.HandleFee = imOrdHR.HandleFee;
                    ordDetail.OtherFee = imOrdHR.OtherFee;
                    ordDetail.PaidAmt = imOrdHR.PaidAmt;
                    ordDetail.PaymentType = imOrdHR.PaymentType;
                    ordDetail.RefNum1 = imOrdHR.ReferenceNum1;
                    ordDetail.RefNum2 = imOrdHR.ReferenceNum2;
                    
                    IntermediateDbConst.IntermediateOrderSplitStatusEnum splitsSts = (IntermediateDbConst.IntermediateOrderSplitStatusEnum)Enum.ToObject(typeof(IntermediateDbConst.IntermediateOrderSplitStatusEnum), imOrdHR.SplitStatus);
                    ordDetail.SplitStatus.Status = (int)splitsSts;
                    ordDetail.SplitStatus.StatusString = splitsSts.ToString(); ;
                    ordDetail.SplitStatus.StatusDate = imOrdHR.LastUpdate.ToString();
                    
                    ordDetail.OrderLines = imOrdDst.IntermediateOrderLine.Copy();
                }

                return ordDetail;
            }
            catch (Exception ex)
            {
                throw ExceptionUtility.ReformatException(ex);
            }
        }
        public static OrderDetail ConvertOrderToOrderDetail(OrderDst ordDst)
        {
            OrderDetail ordDetail = new OrderDetail();
            try
            {
                if (ordDst.vw_OrderHeader.Count > 0)
                {
                    var imOrdHR = ordDst.vw_OrderHeader[0];

                    ordDetail.OrderNum = imOrdHR.OrderNum;
                    ordDetail.OrderDate = imOrdHR.OrderDate;
                    ordDetail.DatabaseSource = imOrdHR.DatabaseSourceID;
                    ordDetail.AccountID = imOrdHR.AccountID;
                    ordDetail.BranchID = imOrdHR.BranchID;
                    ordDetail.SplitNum = 0;
                    ordDetail.Customer.BillName = imOrdHR.BillName;
                    ordDetail.Customer.BillAddressLine1 = imOrdHR.BillAddressLine1;
                    ordDetail.Customer.BillAddressLine2 = imOrdHR.BillAddressLine2;
                    ordDetail.Customer.BillAddressLine3 = imOrdHR.BillAddressLine3;
                    ordDetail.Customer.BillCity = imOrdHR.BillCity;
                    ordDetail.Customer.BillState = imOrdHR.BillState;
                    ordDetail.Customer.BillZip = imOrdHR.BillZip;
                    ordDetail.Customer.BillCountry = imOrdHR.BillCountry;
                    ordDetail.Customer.BillPhone = imOrdHR.BillPhone;
                    ordDetail.Customer.BillEmail = imOrdHR.BillEmail;
                    ordDetail.Customer.ShipName = imOrdHR.ShipName;
                    ordDetail.Customer.ShipAddressLine1 = imOrdHR.ShipAddressLine1;
                    ordDetail.Customer.ShipAddressLine2 = imOrdHR.ShipAddressLine2;
                    ordDetail.Customer.ShipAddressLine3 = imOrdHR.ShipAddressLine3;
                    ordDetail.Customer.ShipCity = imOrdHR.ShipCity;
                    ordDetail.Customer.ShipState = imOrdHR.ShipState;
                    ordDetail.Customer.ShipZip = imOrdHR.ShipZip;
                    ordDetail.Customer.ShipCountry = imOrdHR.ShipCountry;
                    ordDetail.ShipDesc = imOrdHR.ShipDesc;
                    ordDetail.TotalAmt = imOrdHR.TotalAmt;
                    ordDetail.OrderAmt = imOrdHR.OrderAmt;
                    ordDetail.DiscountAmt = imOrdHR.DiscountAmt;
                    ordDetail.DiscountRate = imOrdHR.DiscountRate;
                    ordDetail.TaxAmt = imOrdHR.TaxAmt;
                    ordDetail.TaxRate = imOrdHR.TaxRate;
                    ordDetail.TaxableAmt = imOrdHR.TaxableAmt;
                    ordDetail.Freight = imOrdHR.Freight;
                    ordDetail.HandleFee = imOrdHR.HandleFee;
                    ordDetail.OtherFee = imOrdHR.OtherFee;
                    ordDetail.PaidAmt = imOrdHR.PaidAmt;
                    ordDetail.PaymentType = imOrdHR.PaymentType;
                    ordDetail.RefNum1 = imOrdHR.ReferenceNum1;
                    ordDetail.RefNum2 = imOrdHR.ReferenceNum2;

                    IntermediateDbConst.IntermediateOrderVerifyStatusEnum verifySts = (IntermediateDbConst.IntermediateOrderVerifyStatusEnum)Enum.ToObject(typeof(IntermediateDbConst.IntermediateOrderVerifyStatusEnum), imOrdHR.VerifyStatus);
                    ordDetail.VerifyStatus.Status = (int)verifySts;
                    ordDetail.VerifyStatus.StatusString = verifySts.ToString(); ;
                    ordDetail.VerifyStatus.StatusDate = imOrdHR.LastUpdate.ToString();

                    IntermediateDbConst.OrderTransfer transSts = (IntermediateDbConst.OrderTransfer)Enum.ToObject(typeof(IntermediateDbConst.OrderTransfer), imOrdHR.TransferStatus);
                    ordDetail.TransferStatus.Status = (int)transSts;
                    ordDetail.TransferStatus.StatusString = transSts.ToString(); ;
                    ordDetail.TransferStatus.StatusDate = imOrdHR.TransferDate.ToString();

                    ordDetail.OrderLines = ordDst.vw_OrderLine.Copy();
                }

                return ordDetail;
            }
            catch (Exception ex)
            {
                throw ExceptionUtility.ReformatException(ex);
            }
        }
    }
}
