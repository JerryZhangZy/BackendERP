using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DuoDotNetHelper.CommonV1;
using GhpIntegration.OrderImportApmMdl.Role;
using GhpIntegration.OrderImportApmMdl.Mappings;
using GhpIntegration.IntermediateDb;
namespace GhpIntegration.OrderImportApmMdl.Models
{
    public class SplitTransferOrder : ISplitTransferOrder
    {
        private IntermediateOrderDst _IntermediateOrderDst;
       
        private OrderDst _OrderDst;
        public OrderDst OrderDst
        {
            get { return _OrderDst; }
        }

        public async Task<BoolWithMessage> GetIntermediateOrder(string orderNum, string branchId, string source, string intermediateConnString)
        {
            try
            {

                _IntermediateOrderDst = await IntermediateOrder.GetOrderAsync(orderNum, branchId, source, intermediateConnString);
                if (_IntermediateOrderDst.IntermediateOrderHeader.Count == 0)
                {
                    return new BoolWithMessage(false, "Order doesn't exist. OrderNum: " + orderNum + " BranchID: " + branchId + " Source: " + source);
                }
                if (_IntermediateOrderDst.IntermediateOrderLine.Count == 0)
                {
                    return new BoolWithMessage(false, "Order Line doesn't exist. OrderNum: " + orderNum + " BranchID: " + branchId + " Source: " + source);
                }

                return new BoolWithMessage(true, "");
            }
            catch (Exception ex)
            {
                return new BoolWithMessage(false, ex.Message);
            }
        }

        public BoolWithMessage VerifyOrder()
        {
            try
            {

                #region Verify Amount
                var orderHeader = _IntermediateOrderDst.IntermediateOrderHeader[0];
                var orderLines = _IntermediateOrderDst.IntermediateOrderLine;

                string msg = "Order: " + orderHeader.OrderNum + " BranchId: " + orderHeader.BranchID + " Source: " + orderHeader.Source;

                decimal totalLineAmt = orderLines.Sum(p => p.Quantity * p.UnitPrice);
                decimal orderAmt = orderHeader.OrderAmt;
                if (totalLineAmt != orderAmt)
                {
                    return new BoolWithMessage(false, "Error of " + msg + ". Total Line Amount(" + totalLineAmt + ") is not equal to the OrderAmount(" + orderAmt + ")");
                }

                #endregion Verify Amount

                return new BoolWithMessage(true, "");
            }
            catch (Exception ex)
            {
                return new BoolWithMessage(false, ex.Message);
            }
        }

        public BoolWithMessage ConvertIntermediateOrderToOrder()
        {
            try
            {
                _OrderDst = MapIntermediateOrderToOrder.ConvertIntermediateOrderToOrder(_IntermediateOrderDst);

                return new BoolWithMessage(true, "");
            }
            catch (Exception ex)
            {
                return new BoolWithMessage(false, ex.Message);
            }
        }

        public async Task<BoolWithMessage> AddOrder(string intermediateConnString)
        {
            try
            {
                OrderHost orderSHost = new OrderHost(_OrderDst);
                BoolWithMessage ret = await Order.AddAsync(orderSHost, intermediateConnString);
                return ret;
            }
            catch (Exception ex)
            {
                return new BoolWithMessage(false, ex.Message);
            }
        }


        public async Task MarkSplitResult(string orderNum, string branchId, string source, bool result, string intermediateConnString)
        {
            try
            {
                if (result == true)
                {
                    await IntermediateOrder.MarkSplitWithoutErrorAsync(orderNum, branchId, source, intermediateConnString);
                }
                else
                {
                    await IntermediateOrder.MarkSplitWithErrorAsync(orderNum, branchId, source, intermediateConnString);
                }
            }
            catch (Exception ex)
            {
                throw ExceptionUtility.ReformatException(ex);
            }
        }
    }
}
