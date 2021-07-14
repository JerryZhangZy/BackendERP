using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

using DuoDotNetHelper.CommonV1;
using GhpIntegration.OrderImportApmMdl.Role;
using GhpIntegration.OrderImportApmMdl.Mappings;
using GhpIntegration.IntermediateDb;

using VibesCommon45.OmsDB;
namespace GhpIntegration.OrderImportApmMdl.Models
{
    public class TransferOrderToApm : ITransferOrderToApm
    {
        private OrderDst _OrderDst;
        private InvoiceTempAddDst _InvoiceTempAddDst;
        private string _CustomerID;
        private int _InvoiceNum;
        public int InvoiceNum
        {
            get { return _InvoiceNum; }
        }

        private string _UserId;
        public string UserId
        {
            set { _UserId = value; }
        }

        private string _SZRun;
        public string SZRun
        {
            set { _SZRun = value; }
        }

        private string _WhsNum;
        public string WhsNum
        {
            set { _WhsNum = value; }
        }
        public AccountSalesNumRate AccountSales1, AccountSales2;
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

        public async Task<BoolWithMessage> GetCustomerID(string apmConnString)
        {
            try
            {
                var vwOrderHeader = _OrderDst.vw_OrderHeader[0];

                _CustomerID = await CustomerDb.GetCustomerIDAsync(vwOrderHeader.BillName, vwOrderHeader.BillEmail, apmConnString);
                if (string.IsNullOrEmpty(_CustomerID))
                {
                    //Add new customer

                    _CustomerID = CustomerDb.GetNextCustomerID(apmConnString);

                    CustomerDst cusDst = MapOrderBillingToApmCustomer.ConvertOrderCustomerToApmCustomer(_CustomerID, vwOrderHeader, AccountSales1, AccountSales2);

                    CustomerHost cusHost = new CustomerHost(cusDst);
                    await CustomerDb.AddCustomerAsync(_UserId, cusHost, apmConnString);
                }
                else
                {
                    CustomerDst cusDst = MapOrderBillingToApmCustomer.ConvertOrderCustomerToApmCustomer(_CustomerID, vwOrderHeader, AccountSales1, AccountSales2);

                    await CustomerDb.UpdateCustomerAddressAsync(_CustomerID, cusDst.customer, apmConnString);

                    //GetCustomer SalesNum Yunman 6/4/2014
                    //CustomerDst.customerDataTable cusDt = await CustomerDb.GetCustomerAsync(_CustomerID, apmConnString);
                    //ImportApmMdl.CustomerSalesNM = new SortedList<int, string>();
                    //ImportApmMdl.CustomerSalesNM.Add(1, ConvertUtility.Trim(cusDt[0].SALES_NUM));
                    //ImportApmMdl.CustomerSalesNM.Add(2, ConvertUtility.Trim(cusDt[0].SALES_NUM2));

                    if (AccountSales1 == null || string.IsNullOrEmpty(AccountSales1.SalesNum))
                    {
                        CustomerDst.customerDataTable cusDt = await CustomerDb.GetCustomerAsync(_CustomerID, apmConnString);
                        AccountSales1.SalesNum = ConvertUtility.Trim(cusDt[0].SALES_NUM);
                        AccountSales1.SalesRate = ImportApmUtility.GetSalesCommRT(AccountSales1.SalesNum);

                        AccountSales2.SalesNum = ConvertUtility.Trim(cusDt[0].SALES_NUM2);
                        AccountSales2.SalesRate = ImportApmUtility.GetSalesCommRT(AccountSales2.SalesNum);
                    }
                }
                return new BoolWithMessage(true, "");
            }
            catch (Exception ex)
            {
                return new BoolWithMessage(false, ex.Message);
            }

        }

        public BoolWithMessage ConvertOrderToApmInvoice(bool isOrderForASA)
        {
            try
            {
                _InvoiceTempAddDst = MapOrderToApmInvoice.ConvertOrderToApmTempAddInvoice(_OrderDst, _CustomerID, _InvoiceNum, _UserId
                    , _SZRun, _WhsNum, AccountSales1, AccountSales2, isOrderForASA);

                return new BoolWithMessage(true, "");
            }
            catch (Exception ex)
            {
                return new BoolWithMessage(false, ex.Message);
            }
        }

        public async Task<BoolWithMessage> GetNextInvoiceNum(string apmConnString)
        {
            _InvoiceNum = await InvoiceDb.GetNextInvoiceNumAsync(apmConnString);
            if (_InvoiceNum == -1)
            {
                return new BoolWithMessage(false, "Cannot get next InvoiceNum.");
            }
            else
            {//Set InvoiceNum
                _InvoiceTempAddDst.Temp_InvoiceAdd[0].INVS_NUM = _InvoiceNum;
                _InvoiceTempAddDst.Temp_Ins_DataAdd[0].INVS_NUM = _InvoiceNum;
                if (_InvoiceTempAddDst.Temp_NtfileAdd.Count > 0)
                {
                    _InvoiceTempAddDst.Temp_NtfileAdd[0].NT_NUM = _InvoiceNum;
                }
                for (int idx = 0; idx < _InvoiceTempAddDst.Temp_Invt_LogAdd.Count; idx++)
                {
                    _InvoiceTempAddDst.Temp_Invt_LogAdd[idx].INVS_NUM = _InvoiceNum;
                }
                for (int idx = 0; idx < _InvoiceTempAddDst.Temp_InszrunAdd.Count; idx++)
                {
                    _InvoiceTempAddDst.Temp_InszrunAdd[idx].INVS_NUM = _InvoiceNum * 100 + 1; ;
                }
                for (int idx = 0; idx < _InvoiceTempAddDst.Temp_Invs_LntAdd.Count; idx++)
                {
                    _InvoiceTempAddDst.Temp_Invs_LntAdd[idx].INVS_NUM = _InvoiceNum;
                }
                for (int idx = 0; idx < _InvoiceTempAddDst.Temp_VibesInvoiceSizeExtAdd.Count; idx++)
                {
                    _InvoiceTempAddDst.Temp_VibesInvoiceSizeExtAdd[idx].InvoiceNum = _InvoiceNum;
                }
                return new BoolWithMessage(true, "");
            }
        }

        public async Task<BoolWithMessage> AddInvoiceToApm(string apmConnString)
        {
            try
            {
                InvoiceTempAddHost invsHost = new InvoiceTempAddHost(_InvoiceTempAddDst);

                BoolWithMessage ret = await InvoiceDb.AddAsync(_UserId, invsHost, apmConnString);

                return ret;
            }
            catch (Exception ex)
            {
                return new BoolWithMessage(false, ex.Message);
            }
        }


        public async Task<BoolWithMessage> MarkTranferResult(UniqueOrderParameter orderParameter, bool result, string intermediateConnString)
        {
            try
            {
                if (result == true)
                {
                    return await Order.MarkTransferSuccessAsync(orderParameter, intermediateConnString);
                }
                else
                {
                    return await Order.MarkTransferWithErrorAsync(orderParameter, intermediateConnString);
                }
            }
            catch (Exception ex)
            {
                return new BoolWithMessage(false, ex.Message);
            }
        }

    }
}
