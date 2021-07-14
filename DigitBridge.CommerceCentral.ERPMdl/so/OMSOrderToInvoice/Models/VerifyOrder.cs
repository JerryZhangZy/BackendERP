using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DuoDotNetHelper.CommonV1;
using DuoDotNetHelper.CommonV1.Data;
using GhpIntegration.OrderImportApmMdl.Role;
using GhpIntegration.IntermediateDb;
using GhpIntegration.OrderImportApmDB;
namespace GhpIntegration.OrderImportApmMdl.Models
{
    public class VerifyOrder : IVerifyOrder
    {
        private OrderDst _OrderDst;
        public OrderDst OrderDst
        {
            set { _OrderDst = value; }
            get { return _OrderDst; }
        }
        public async Task<BoolWithMessage> GetOrder(UniqueOrderParameter orderParameter, string intermediateConnString)
        {
            try
            {
                _OrderDst = await Order.GetOrderAsync(orderParameter, intermediateConnString);
                if (_OrderDst.vw_OrderHeader.Count == 0)
                {
                    return new BoolWithMessage(false, "Order doesn't exist. OrderNum: " + orderParameter.OrderNum +
                        " BranchID: " + orderParameter.BranchId + " Source: " + orderParameter.Source + " SplitNum: " + orderParameter.SplitNum);
                }
                if (_OrderDst.vw_OrderLine.Count == 0)
                {
                    return new BoolWithMessage(false, "Order Line doesn't exist. OrderNum: " + orderParameter.OrderNum +
                        " BranchID: " + orderParameter.BranchId + " Source: " + orderParameter.Source + " SplitNum: " + orderParameter.SplitNum);
                }

                return new BoolWithMessage(true, "");
            }
            catch (Exception ex)
            {
                return new BoolWithMessage(false, ex.Message);

            }
        }

        public BoolWithMessage VerifyOrderInformation(string apmConnString)
        {
            try
            {
                var ordHd = _OrderDst.vw_OrderHeader[0];

                //check OrderNum exist in OMS or not
                string orderNum = ordHd.OrderNum;
                string branchId = ordHd.BranchID;
                string source = ordHd.Source;
                bool ret = ApmInvoiceDb.HasOrderInvoice(orderNum, branchId, source, apmConnString);
                if (ret == true)
                {
                    return new BoolWithMessage(false, "OrderNum " + orderNum + " exists in OMS. InvsType: " + branchId + ", Source: " + source + ".");
                }

                //List<string> itemOrderedList = _OrderDst.vw_OrderLine.GroupBy(p => new
                //{
                //    Sku = p.ItemNum + "-" + p.StyleColor + "-" + p.Size + "-" + p.Length                    
                //}).Select(g => g.Key.Sku + "-" + g.Sum(p => p.Quantity)).ToList();

                List<string> itemOrderedList = _OrderDst.vw_OrderLine.Select(p => p.ItemNum + "-" + p.StyleColor + "-" + p.Size + "-" + p.Length).Distinct().ToList();
                ret = ApmInvoiceDb.HasOrderInvoiceItems(orderNum, itemOrderedList, apmConnString);
                if (ret == true)
                {
                    return new BoolWithMessage(false, "OrderNum " + orderNum + " items exist in OMS." + TextUtility.NewLine +
                        string.Join(TextUtility.NewLine, itemOrderedList));
                }
                
                //Verify Length
                string errMsg = "Field Length Error. ";

                string shipCountry = string.IsNullOrEmpty(ordHd.ShipCountry) ? "US" : ordHd.ShipCountry;
                string shipState = ImportApmUtility.GetProvinceAbbreivation(shipCountry, ordHd.ShipState);
                string billCountry = string.IsNullOrEmpty(ordHd.BillCountry) ? "US" : ordHd.BillCountry;
                string billState = ImportApmUtility.GetProvinceAbbreivation(billCountry, ordHd.BillState);

                bool noErr = true;
                #region Verify Bill
                //Verify Bill information (Customer)
                Dictionary<string, DBTblColumn> customerCols = SqlDbUtility.SqlGetDbTblColumns("customer", apmConnString);
                foreach (string colCus in ImportApmConst.ColumnNames.Customer.VerifyColumns)
                {
                    if (customerCols.Keys.Contains(colCus) == false)
                    {
                        return new BoolWithMessage(false, errMsg + " Cannot find column " + colCus + " in table customer");
                    }

                    var dtColCus = customerCols[colCus];

                    string val = "";
                    string colDesc = "";
                    int omsUIMaxLeng = 0;
                    if (colCus == ImportApmConst.ColumnNames.Customer.BillName[0])
                    {
                        val = ImportApmUtility.ConvertUnicodeToAsciiCode(ordHd.BillName);
                        colDesc = ImportApmConst.ColumnNames.Customer.BillName[1];
                        omsUIMaxLeng = ImportApmConst.OmsUIMaxLen.Name;
                    }
                    else if (colCus == ImportApmConst.ColumnNames.Customer.BillAddr1[0])
                    {
                        val = ImportApmUtility.ConvertUnicodeToAsciiCode(ordHd.BillAddressLine1);
                        colDesc = ImportApmConst.ColumnNames.Customer.BillAddr1[1];
                        omsUIMaxLeng = ImportApmConst.OmsUIMaxLen.Address;
                    }
                    else if (colCus == ImportApmConst.ColumnNames.Customer.BillAddr2[0])
                    {
                        val = ImportApmUtility.ConvertUnicodeToAsciiCode(ordHd.BillAddressLine2);
                        colDesc = ImportApmConst.ColumnNames.Customer.BillAddr2[1];
                        omsUIMaxLeng = ImportApmConst.OmsUIMaxLen.Address;
                    }
                    else if (colCus == ImportApmConst.ColumnNames.Customer.BillCity[0])
                    {
                        val = ImportApmUtility.ConvertUnicodeToAsciiCode(ordHd.BillCity);
                        colDesc = ImportApmConst.ColumnNames.Customer.BillCity[1];
                        omsUIMaxLeng = ImportApmConst.OmsUIMaxLen.City;
                    }
                    else if (colCus == ImportApmConst.ColumnNames.Customer.BillState[0])
                    {
                        val = ImportApmUtility.ConvertUnicodeToAsciiCode(billState);
                        colDesc = ImportApmConst.ColumnNames.Customer.BillState[1];
                    }
                    else if (colCus == ImportApmConst.ColumnNames.Customer.BillZip[0])
                    {
                        val = ImportApmUtility.ConvertUnicodeToAsciiCode(ordHd.BillZip);
                        colDesc = ImportApmConst.ColumnNames.Customer.BillZip[1];
                    }
                    else if (colCus == ImportApmConst.ColumnNames.Customer.BillCountry[0])
                    {
                        val = ImportApmUtility.ConvertUnicodeToAsciiCode(ordHd.BillCountry);
                        colDesc = ImportApmConst.ColumnNames.Customer.BillCountry[1];
                    }
                    else if (colCus == ImportApmConst.ColumnNames.Customer.ShipName[0])
                    {
                        val = ImportApmUtility.ConvertUnicodeToAsciiCode(ordHd.ShipName);
                        colDesc = ImportApmConst.ColumnNames.Customer.ShipName[1];
                        omsUIMaxLeng = ImportApmConst.OmsUIMaxLen.Name;
                    }
                    else if (colCus == ImportApmConst.ColumnNames.Customer.ShipAddr1[0])
                    {
                        val = ImportApmUtility.ConvertUnicodeToAsciiCode(ordHd.ShipAddressLine1);
                        colDesc = ImportApmConst.ColumnNames.Customer.ShipAddr1[1];
                        omsUIMaxLeng = ImportApmConst.OmsUIMaxLen.Address;
                    }
                    else if (colCus == ImportApmConst.ColumnNames.Customer.ShipAddr2[0])
                    {
                        val = ImportApmUtility.ConvertUnicodeToAsciiCode(ordHd.ShipAddressLine2);
                        colDesc = ImportApmConst.ColumnNames.Customer.ShipAddr2[1];
                        omsUIMaxLeng = ImportApmConst.OmsUIMaxLen.Address;
                    }
                    else if (colCus == ImportApmConst.ColumnNames.Customer.ShipCity[0])
                    {
                        val = ImportApmUtility.ConvertUnicodeToAsciiCode(ordHd.ShipCity);
                        colDesc = ImportApmConst.ColumnNames.Customer.ShipCity[1];
                        omsUIMaxLeng = ImportApmConst.OmsUIMaxLen.City;
                    }
                    else if (colCus == ImportApmConst.ColumnNames.Customer.ShipState[0])
                    {
                        val = ImportApmUtility.ConvertUnicodeToAsciiCode(shipState);
                        colDesc = ImportApmConst.ColumnNames.Customer.ShipState[1];
                    }
                    else if (colCus == ImportApmConst.ColumnNames.Customer.ShipZip[0])
                    {
                        val = ImportApmUtility.ConvertUnicodeToAsciiCode(ordHd.ShipZip);
                        colDesc = ImportApmConst.ColumnNames.Customer.ShipZip[1];
                    }
                    else if (colCus == ImportApmConst.ColumnNames.Customer.ShipCountry[0])
                    {
                        val = ImportApmUtility.ConvertUnicodeToAsciiCode(ordHd.ShipCountry);
                        colDesc = ImportApmConst.ColumnNames.Customer.ShipCountry[1];
                    }
                    else if (colCus == ImportApmConst.ColumnNames.Customer.PHONE[0])
                    {
                        val = ordHd.BillPhone;
                        colDesc = ImportApmConst.ColumnNames.Customer.PHONE[1];
                    }
                    else if (colCus == ImportApmConst.ColumnNames.Customer.EmailAddr[0])
                    {
                        val = ordHd.BillEmail;
                        colDesc = ImportApmConst.ColumnNames.Customer.EmailAddr[1];
                    }

                    int maxLen = omsUIMaxLeng > 0 ? omsUIMaxLeng : dtColCus.MaxLength;
                    if (val.Length > maxLen)
                    {
                        errMsg += " Column " + colDesc + " MaxLength: " + maxLen + ", Value: " + val + ", Length: " + val.Length + ". ";
                        noErr = false;
                    }
                }
                #endregion Verify Bill

                #region Verify Ship
                //Verify Ship information (InsData)
                Dictionary<string, DBTblColumn> insDataCols = SqlDbUtility.SqlGetDbTblColumns("Temp_Ins_DataAdd", apmConnString);
                foreach (string colInsData in ImportApmConst.ColumnNames.InsData.VerifyColumns)
                {
                    if (insDataCols.Keys.Contains(colInsData) == false)
                    {
                        return new BoolWithMessage(false, errMsg + " Cannot find column " + colInsData + " in table Temp_Ins_DataAdd");
                    }

                    var insDataCol = insDataCols[colInsData];

                    string val = "";
                    string colDesc = "";
                    int omsUIMaxLeng = 0;
                    if (colInsData == ImportApmConst.ColumnNames.InsData.ShipName[0])
                    {
                        val = ordHd.ShipName;
                        colDesc = ImportApmConst.ColumnNames.InsData.ShipName[1];
                        omsUIMaxLeng = ImportApmConst.OmsUIMaxLen.Name;
                    }
                    else if (colInsData == ImportApmConst.ColumnNames.InsData.ShipAddr1[0])
                    {
                        val = ordHd.ShipAddressLine1;
                        colDesc = ImportApmConst.ColumnNames.InsData.ShipAddr1[1];
                        omsUIMaxLeng = ImportApmConst.OmsUIMaxLen.Address;
                    }
                    else if (colInsData == ImportApmConst.ColumnNames.InsData.ShipAddr2[0])
                    {
                        val = ordHd.ShipAddressLine2;
                        colDesc = ImportApmConst.ColumnNames.InsData.ShipAddr2[1];
                        omsUIMaxLeng = ImportApmConst.OmsUIMaxLen.Address;
                    }
                    else if (colInsData == ImportApmConst.ColumnNames.InsData.ShipAddr3[0])
                    {
                        //val = ordHd.ShipCity + "," + shipState + "," + ordHd.ShipZip;
                        val = ordHd.ShipAddressLine3;
                        colDesc = ImportApmConst.ColumnNames.InsData.ShipAddr3[1];
                        omsUIMaxLeng = ImportApmConst.OmsUIMaxLen.Address;
                    }
                    else if (colInsData == ImportApmConst.ColumnNames.InsData.ShipCity[0])
                    {
                        val = ordHd.ShipCity;
                        colDesc = ImportApmConst.ColumnNames.InsData.ShipCity[1];
                        omsUIMaxLeng = ImportApmConst.OmsUIMaxLen.City;
                    }
                    else if (colInsData == ImportApmConst.ColumnNames.InsData.ShipState[0])
                    {
                        val = shipState;
                        colDesc = ImportApmConst.ColumnNames.InsData.ShipState[1];
                    }
                    else if (colInsData == ImportApmConst.ColumnNames.InsData.ShipZip[0])
                    {
                        val = ordHd.ShipZip;
                        colDesc = ImportApmConst.ColumnNames.InsData.ShipZip[1];
                    }
                    else if (colInsData == ImportApmConst.ColumnNames.InsData.ShipCountry[0])
                    {
                        val = shipCountry;
                        colDesc = ImportApmConst.ColumnNames.InsData.ShipCountry[1];
                    }
                    else if (colInsData == ImportApmConst.ColumnNames.InsData.BillAddr1[0])
                    {
                        val = ordHd.BillAddressLine1;
                        colDesc = ImportApmConst.ColumnNames.InsData.BillAddr1[1];
                        omsUIMaxLeng = ImportApmConst.OmsUIMaxLen.Address;
                    }
                    else if (colInsData == ImportApmConst.ColumnNames.InsData.BillAddr2[0])
                    {
                        val = ordHd.BillAddressLine2;
                        colDesc = ImportApmConst.ColumnNames.InsData.BillAddr2[1];
                        omsUIMaxLeng = ImportApmConst.OmsUIMaxLen.Address;
                    }
                    //else if (colInsData == ImportApmConst.ColumnNames.InsData.BillAddr3[0])
                    //{
                    //    val = ordHd.BillAddressLine3;
                    //    colDesc = ImportApmConst.ColumnNames.InsData.BillAddr3[1];
                    //}
                    else if (colInsData == ImportApmConst.ColumnNames.InsData.BillCity[0])
                    {
                        val = ordHd.BillCity;
                        colDesc = ImportApmConst.ColumnNames.InsData.BillCity[1];
                        omsUIMaxLeng = ImportApmConst.OmsUIMaxLen.City;
                    }
                    else if (colInsData == ImportApmConst.ColumnNames.InsData.BillState[0])
                    {
                        val = billState;
                        colDesc = ImportApmConst.ColumnNames.InsData.BillState[1];
                    }
                    else if (colInsData == ImportApmConst.ColumnNames.InsData.BillZip[0])
                    {
                        val = ordHd.BillZip;
                        colDesc = ImportApmConst.ColumnNames.InsData.BillZip[1];
                    }
                    else if (colInsData == ImportApmConst.ColumnNames.InsData.BillCountry[0])
                    {
                        val = billCountry;
                        colDesc = ImportApmConst.ColumnNames.InsData.BillCountry[1];
                    }

                    int maxLen = (insDataCol.ColumnType.ToLower()).StartsWith("n") ? insDataCol.MaxLength / 2 : insDataCol.MaxLength;
                    if(omsUIMaxLeng > 0)
                    {
                        maxLen = omsUIMaxLeng;
                    }
                    if (val.Length > maxLen)
                    {
                        errMsg += " Column " + colDesc + " MaxLength: " + insDataCol.MaxLength + ", Value: " + val + ", Length: " + val.Length + ". ";
                        noErr = false;
                    }
                }
                #endregion Verify ship

                return new BoolWithMessage(noErr, errMsg);

            }
            catch (Exception ex)
            {
                return new BoolWithMessage(false, ex.Message);
            }
        }

        public async Task<BoolWithMessage> MarkVerifyResult(UniqueOrderParameter orderParameter, bool result, string intermediateConnString)
        {
            try
            {
                if (result == true)
                {
                    return await Order.MarkVerifiedWithoutErrorAsync(orderParameter, intermediateConnString);
                }
                else
                {
                    return await Order.MarkVerifiedWithErrorAsync(orderParameter, intermediateConnString);
                }
            }
            catch (Exception ex)
            {
                return new BoolWithMessage(false, ex.Message);
            }
        }
    }
}
