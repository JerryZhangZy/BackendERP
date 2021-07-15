using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace GhpIntegration.OrderImportApmMdl.Mappings
{
    public class MapIntermediateOrderToOrder
    {
        /*
            private static string[] _AmazonFBAInvoiceTypes = { "MF", "MP", "MV" };
            public static IntermediateInstockDst.vw_WaitingOrderSkuWhsInstockDataTable SkuInstockDt;

            public static OrderDst ConvertIntermediateOrderToOrder(IntermediateOrderDst imOrderDst)
            {
                OrderDst ordDst = new OrderDst();

                try
                {
                    OrderDst.OrderHeaderDataTable ordHd = ConvertIntermediateOrderHeaderToOrderHeader(imOrderDst.IntermediateOrderHeader[0]);

                    ordDst.OrderHeader.Merge(ordHd);

                    OrderDst.OrderLineDataTable ordLn = ConvertIntermediateOrderLineToOrderLine(imOrderDst.IntermediateOrderLine);

                    ordDst.OrderLine.Merge(ordLn);

                    List<int> lineSplitNums = ordLn.Select(p => p.SplitNum).Distinct().ToList();
                    if (lineSplitNums.Count > 1)
                    {
                        //split Header 3/8/2019                    
                        OrderDst.OrderHeaderDataTable splitHd = new OrderDst.OrderHeaderDataTable();
                        int totQty = ordLn.Sum(p => p.Quantity);
                        decimal taxRate = imOrderDst.IntermediateOrderHeader[0].TaxRate;
                        decimal totFreight = imOrderDst.IntermediateOrderHeader[0].Freight;
                        decimal totHandleFee = imOrderDst.IntermediateOrderHeader[0].HandleFee;
                        decimal splitFreight = 0M, splitHandleFee = 0M;
                        for (int idx = 0; idx < lineSplitNums.Count; idx++)
                        {
                            int spNum = lineSplitNums[idx];
                            var splitLns = ordLn.Where(p => p.SplitNum == spNum);
                            List<int> splitLnLineNums = splitLns.Select(p => p.LineNum).ToList();
                            var splitImLns = imOrderDst.IntermediateOrderLine.Where(p => splitLnLineNums.Contains(p.LineNum));

                            int splitQty = splitLns.Sum(p => p.Quantity);

                            OrderDst.OrderHeaderRow splitHdDr = ordHd[0];
                            splitHdDr.SplitNum = spNum;
                            splitHdDr.OrderAmt = splitLns.Sum(p => p.Quantity * p.UnitPrice);
                            splitHdDr.DiscountAmt = splitImLns.Sum(p => p.ProportionedDicountAmt + p.ProportionedFreightAmt);
                            splitHdDr.TaxableAmt = taxRate != 0 ? (splitHdDr.OrderAmt - splitHdDr.DiscountAmt) : 0M;
                            splitHdDr.TaxAmt = splitHdDr.TaxableAmt * taxRate;
                            if (idx != lineSplitNums.Count - 1)
                            {
                                splitHdDr.Freight = Math.Round((splitQty / ConvertUtility.ToDecimal(totQty)) * totFreight + 0.005M, 2);
                                splitHdDr.HandleFee = Math.Round((splitQty / ConvertUtility.ToDecimal(totQty)) * totHandleFee + 0.005M, 2);

                                splitFreight += splitHdDr.Freight;
                                splitHandleFee += splitHdDr.HandleFee;
                            }
                            else
                            {
                                splitHdDr.Freight = totFreight - splitFreight;
                                splitHdDr.HandleFee = totHandleFee - splitHandleFee;
                            }

                            splitHdDr.TotalAmt = splitHdDr.OrderAmt + splitHdDr.TaxAmt + splitHdDr.Freight + splitHdDr.HandleFee - splitHdDr.DiscountAmt;
                            if (splitHdDr.PaidAmt > 0)
                            {
                                splitHdDr.PaidAmt = splitHdDr.TotalAmt;
                            }
                            splitHd.ImportRow(splitHdDr);
                        }

                        ordDst.OrderHeader.Clear();
                        ordDst.OrderHeader.Merge(splitHd);
                    }


                    return ordDst;
                }
                catch (Exception ex)
                {

                    throw ExceptionUtility.ReformatException(ex);
                }
            }

            private static OrderDst.OrderHeaderDataTable ConvertIntermediateOrderHeaderToOrderHeader(IntermediateOrderDst.IntermediateOrderHeaderRow imOrdHR)
            {
                try
                {
                    string[] shipExpByFreightInvsTypes = { "PJ", "SP" };

                    OrderDst.OrderHeaderDataTable ordHd = new OrderDst.OrderHeaderDataTable();
                    OrderDst.OrderHeaderRow ordHR = ordHd.NewOrderHeaderRow();

                    ordHR.OrderNum = imOrdHR.OrderNum;
                    ordHR.BranchID = imOrdHR.BranchID;
                    ordHR.Source = imOrdHR.Source;
                    ordHR.SplitNum = (int)IntermediateDbConst.SplitNum.NotSplit;
                    ordHR.TotalAmt = imOrdHR.TotalAmt;
                    ordHR.OrderAmt = imOrdHR.OrderAmt;
                    ordHR.Freight = imOrdHR.Freight;
                    ordHR.HandleFee = imOrdHR.HandleFee;
                    ordHR.OtherFee = imOrdHR.OtherFee;
                    ordHR.TaxAmt = imOrdHR.TaxAmt;
                    ordHR.DiscountAmt = imOrdHR.DiscountAmt;
                    ordHR.TaxableAmt = imOrdHR.TaxableAmt;
                    ordHR.TaxRate = imOrdHR.TaxRate;
                    ordHR.DestinationShipDesc = ImportApmUtility.GetDestinationShipDesc(imOrdHR.BranchID, imOrdHR.Source, imOrdHR.ShipDesc);

                    //if freight >= 20 , correct shipmethod
                    string shipDesc = ordHR.DestinationShipDesc.ToLower();
                    if (ordHR.Freight >= 20M && (shipDesc.Contains("stand") || shipDesc.Contains("ground") || string.IsNullOrEmpty(shipDesc))
                        && shipExpByFreightInvsTypes.Contains(ordHR.BranchID))
                    {
                        if (imOrdHR.ShipCountry == "" || imOrdHR.ShipCountry == "US")
                        {
                            if (ordHR.Freight >= 29M)
                            {
                                ordHR.DestinationShipDesc = "Exp OverNight";
                            }
                            else
                            {
                                ordHR.DestinationShipDesc = "Exp 3 Days";
                            }
                        }
                        else
                        {
                            //if(ordHR.Freight >= 29M)
                            //{
                            //    ordHR.DestinationShipDesc = "Exp 3 Days";
                            //}
                            //else
                            {
                                ordHR.DestinationShipDesc = "Standard Ground";
                            }
                        }
                    }
                    ordHR.DestinationPaymentType = ImportApmUtility.GetDestinationPaymentType(imOrdHR.BranchID, imOrdHR.Source, imOrdHR.PaymentType);
                    ordHR.TransferStatus = (byte)IntermediateDbConst.OrderTransfer.NotTransferred;
                    ordHR.TransferDate = VibesDateTimeUtility.BaseDate;
                    ordHR.VerifyStatus = (byte)IntermediateDbConst.IntermediateOrderVerifyStatusEnum.NotVerified;
                    ordHR.DatabaseSourceID = imOrdHR.DatabaseSourceID;
                    ordHR.AccountID = imOrdHR.AccountID;
                    ordHR.PaidAmt = imOrdHR.PaidAmt;
                    ordHR.SplitReferenceNum1 = imOrdHR.ReferenceNum1;
                    ordHR.SplitReferenceNum2 = imOrdHR.ReferenceNum2;
                    ordHR.EnterDate = DateTime.Now;
                    ordHR.LastUpdate = DateTime.Now;

                    ordHd.Rows.Add(ordHR);

                    return ordHd;

                }
                catch (Exception ex)
                {

                    throw ExceptionUtility.ReformatException(ex);
                }
            }

            private static OrderDst.OrderLineDataTable ConvertIntermediateOrderLineToOrderLine(IntermediateOrderDst.IntermediateOrderLineDataTable imOrdLn)
            {
                try
                {
                    OrderDst.OrderLineDataTable ordLn = new OrderDst.OrderLineDataTable();
                    foreach (var imOrdLR in imOrdLn)
                    {
                        OrderDst.OrderLineRow ordLR = ordLn.NewOrderLineRow();

                        ordLR.OrderNum = imOrdLR.OrderNum;
                        ordLR.BranchID = imOrdLR.BranchID;
                        ordLR.LineNum = imOrdLR.LineNum;
                        ordLR.SplitNum = (int)IntermediateDbConst.SplitNum.NotSplit;
                        ordLR.Source = imOrdLR.Source;
                        ordLR.ItemNum = imOrdLR.ItemNum;
                        ordLR.StyleColor = imOrdLR.StyleColor;
                        ordLR.Size = imOrdLR.Size;
                        ordLR.Length = imOrdLR.Length;
                        ordLR.Quantity = imOrdLR.Quantity;
                        ordLR.UnitPrice = imOrdLR.UnitPrice;
                        ordLR.ItemDesc = imOrdLR.ItemDesc;
                        ordLR.OrderItemType = imOrdLR.OrderItemType;
                        ordLR.VerifyStatus = (byte)IntermediateDbConst.IntermediateOrderVerifyStatusEnum.NotVerified;
                        ordLR.Taxable = imOrdLR.Taxable;
                        ordLR.ApplyLineNum = 0;
                        ordLR.CouponStatus = 0;
                        ordLR.ParentLineNum = 0;
                        ordLR.IntermediateLineNum = (int)imOrdLR.SourceLineNum;
                        ordLR.ProportionedFreightAmt = imOrdLR.ProportionedFreightAmt;
                        ordLR.ProportionedHandleFee = imOrdLR.ProportionedHandleFee;
                        ordLR.ProportionedOtherFee = imOrdLR.ProportionedOtherFee;
                        ordLR.ProportionedUnitPrice = imOrdLR.ProportionedUnitPrice;
                        ordLR.SplitLineRefNum1 = imOrdLR.LineRefNum1;
                        ordLR.SplitLineRefNum2 = imOrdLR.LineRefNum2;
                        //Add for different warehouse 3/8/2019
                        //Update by Yunman 3/2/2020, Amazon Fulfillment order warehouse = "A01"
                        if (imOrdLR.SourceItemNum.EndsWith("-FBA") &&
                            _AmazonFBAInvoiceTypes.Contains(ordLR.BranchID))
                        {
                            ordLR.DestinateWhsNum = "A01";
                        }
                        else //use SplitOrderLines() function 8 / 21 / 2020
                        {
                            ordLR.DestinateWhsNum = ""; //GetDestinateWhsNum(imOrdLR.ItemNum, imOrdLR.StyleColor, imOrdLR.Size, imOrdLR.Length, imOrdLR.Quantity);
                        }
                        ordLR.EnterDate = DateTime.Now;
                        ordLR.LastUpdate = DateTime.Now;

                        ordLn.Rows.Add(ordLR);
                    }

                    //CombineSameWarehouse(ordLn);
                    SplitOrderLines(ordLn);

                    #region split whs into different invoice
                    List<string> desWhsNums = ordLn.Select(p => p.DestinateWhsNum).Distinct().ToList();

                    if (desWhsNums.Count > 1)
                    {
                        int splitNum = 0;

                        Dictionary<int, string[]> sameInvoiceWhsDic = ImportApmUtility.PrefinedSameInvoiceWhsDic.
                            ToDictionary(p => p.Key, p => p.Value);

                        List<string> otherWhs = ordLn.Where(p => !ImportApmUtility.PrefinedSameInvoiceAllWhs.Contains(p.DestinateWhsNum)).
                            Select(p => p.DestinateWhsNum).Distinct().ToList();
                        if (otherWhs != null && otherWhs.Count > 0)
                        {
                            splitNum = 1; //sameInvoiceWhsDic.Count;

                            foreach (string whs in otherWhs)
                            {
                                string[] whsArray = new string[] { whs };
                                sameInvoiceWhsDic.Add(splitNum++, whsArray);
                            }
                        }

                        if (splitNum > 0)
                        {
                            foreach (KeyValuePair<int, string[]> invsWhs in sameInvoiceWhsDic)
                            {
                                var ordLRs = ordLn.Where(p => invsWhs.Value.Contains(p.DestinateWhsNum));
                                List<int> invsLns = ordLRs.Select(p => p.LineNum).ToList();
                                foreach (var ordLR in ordLRs)
                                {
                                    ordLR.SplitNum = invsWhs.Key + 1;
                                }
                            }
                        }
                    }
                    #endregion split whs into different invoice

                    return ordLn;
                }
                catch (Exception ex)
                {

                    throw ExceptionUtility.ReformatException(ex);
                }
            }

            private static void CombineSameWarehouse(OrderDst.OrderLineDataTable ordLn)
            {
                List<string> desWhsNums = ordLn.Where(p => //p.DestinateWhsNum != "04" && 7/30/2019
                    p.DestinateWhsNum != "A01" &&
                    p.DestinateWhsNum != "")
                    .Select(p => p.DestinateWhsNum).Distinct().ToList();
                //no split warehouse
                if (desWhsNums.Count == 1 || desWhsNums.Count == 0)
                {
                    return;
                }
                //split warehouse, check if the skus in same warehouse
                bool toCombine = true;
                string combineToWhs = "";
                List<int> combineLineNums = new List<int>();
                foreach (string whs in desWhsNums)
                {

                    toCombine = true;
                    combineToWhs = whs;

                    var difWhsOrds = ordLn.Where(p => p.DestinateWhsNum != whs);
                    //check difwhsSkus if has inventory in whs
                    foreach (var ord in difWhsOrds)
                    {
                        //04 combine to others 7/30/2019

                        string itemNo = ConvertUtility.Trim(ord["ItemNum"]);
                        string color = ConvertUtility.Trim(ord["StyleColor"]);
                        string size = ConvertUtility.Trim(ord["Size"]);
                        string length = ConvertUtility.Trim(ord["Length"]);
                        int qty = ConvertUtility.ToInt(ord["Quantity"]);
                        string itemWhs = ConvertUtility.Trim(ord["DestinateWhsNum"]);

                        if (itemWhs == "04")
                        {
                            combineLineNums.Add(ConvertUtility.ToInt(ord["LineNum"]));
                            continue;
                        }

                        var avaWhsQtys = SkuInstockDt.FirstOrDefault(p => p.ItemNo == itemNo && p.Color == color && p.Size == size && p.Length == length &&
                            p.WhsNum == whs);
                        int whsInstock = avaWhsQtys == null ? 0 : ConvertUtility.ToInt(avaWhsQtys["Instock"]);
                        if (whsInstock >= qty)
                        {
                            combineLineNums.Add(ConvertUtility.ToInt(ord["LineNum"]));
                        }
                        else
                        {
                            //whs No instock for sku, check next whs
                            toCombine = false;
                            break;
                        }
                    }
                    if (toCombine)
                    {
                        //Combine split skus to one
                        break;
                    }
                }
                if (toCombine)
                {
                    var comSkus = ordLn.AsEnumerable().Where(p => combineLineNums.Contains(ConvertUtility.ToInt(p["LineNum"])));
                    foreach (var comSku in comSkus)
                    {
                        comSku["DestinateWhsNum"] = combineToWhs;
                    }
                }

                //Check if have other whs have all items (when whs more than 2) 3/20/2020
                desWhsNums = ordLn.Select(p => p.DestinateWhsNum).Distinct().ToList();
                if (desWhsNums.Count > 1)
                {
                    var skuQtys = ordLn.GroupBy(p => new
                    {
                        Sku = p.ItemNum + "-" + ImportApmUtility.GetASAColor(p.StyleColor) +
                    "-" + p.Size + "-" + p.Length
                    }).Select(g => new
                    {
                        Sku = g.Key.Sku,
                        Qty = g.Sum(s => s.Quantity)
                    });
                    List<string> skus = skuQtys.Select(p => p.Sku).ToList();
                    var otherWhsAvailbleQty = SkuInstockDt.AsEnumerable().
                      Where(p => p.CheckWhsInstock == true &&
                        skus.Contains(p.Sku) &&
                        desWhsNums.Contains(p.WhsNum) == false).
                      OrderBy(p => p.WhsSeq).
                      Select(p => new
                      {
                          Sku = p.Sku,
                          WhsNum = p.WhsNum,
                          Instock = p.Instock
                      });
                    //otherWhs has all items, change destinatewhsnum
                    if (otherWhsAvailbleQty.Count() > 0 &&
                        skus.Count == otherWhsAvailbleQty.Count())
                    {
                        string desWhsNum = otherWhsAvailbleQty.First().WhsNum;
                        for (int idx = 0; idx < ordLn.Rows.Count; idx++)
                        {
                            ordLn[idx]["DestinateWhsNum"] = desWhsNum;
                        }

                    }
                    else
                    {
                        //Check if combine to other whs have more items (when whs more than 2) 3/23/2020
                        var whsSkuQtys = ordLn.GroupBy(p => new
                        {
                            WhsNum = p.DestinateWhsNum,
                            Sku = p.ItemNum + "-" + ImportApmUtility.GetASAColor(p.StyleColor) +
                    "-" + p.Size + "-" + p.Length
                        }).Select(g => new
                        {
                            WhsNum = g.Key.WhsNum,
                            Sku = g.Key.Sku,
                            Qty = g.Sum(s => s.Quantity)
                        });
                        var whss = whsSkuQtys.GroupBy(p => p.WhsNum).
                            Select(p => new
                            {
                                WhsNum = p.Key,
                                SkuCount = p.Count()
                            }).OrderByDescending(p => p.SkuCount);
                        //<sku, newWhs>
                        Dictionary<string, string> changeSkuDesWhsNums = new Dictionary<string, string>();
                        List<string> checkedWhss = new List<string>();
                        foreach (var whs in whss)
                        {
                            var otherWhsSkus = whsSkuQtys.Where(p => p.WhsNum.Equals(whs.WhsNum) == false &&
                                checkedWhss.Contains(p.WhsNum) == false &&
                                changeSkuDesWhsNums.Keys.Contains(p.Sku) == false);
                            foreach (var otherSku in otherWhsSkus)
                            {
                                var otherWhsAvailbleQty2 = SkuInstockDt.AsEnumerable().
                                    FirstOrDefault(p => p.CheckWhsInstock == true &&
                                    p.WhsNum == whs.WhsNum &&
                                    p.Sku == otherSku.Sku &&
                                    p.Instock >= otherSku.Qty);

                                if (otherWhsAvailbleQty2 != null)
                                {
                                    changeSkuDesWhsNums.Add(otherSku.Sku, otherWhsAvailbleQty2.WhsNum);
                                }
                            }

                            checkedWhss.Add(whs.WhsNum);
                        }

                        if (changeSkuDesWhsNums.Count > 0)
                        {
                            foreach (KeyValuePair<string, string> skuWhs in changeSkuDesWhsNums)
                            {
                                var comSkus = ordLn.
                                     Where(p => p.ItemNum + "-" + ImportApmUtility.GetASAColor(p.StyleColor) +
                     "-" + p.Size + "-" + p.Length == skuWhs.Key);
                                foreach (var comSku in comSkus)
                                {
                                    comSku["DestinateWhsNum"] = skuWhs.Value;
                                }
                            }
                        }
                    }
                }
            }
            private static string GetDestinateWhsNum(string itemNo, string color, string size, string length, int qty)
            {
                string whsNum = "";
                try
                {
                    //string sku = itemNo + "-" + ImportApmUtility.GetASAColor(color) + "-" + size + "-" + length;
                    //var avaWhsQtys = SkuInstockDt.Where(p=> p.Sku.Equals(sku, StringComparison.InvariantCultureIgnoreCase)).
                    //    
                    if (SkuInstockDt == null || SkuInstockDt.Rows.Count == 0)
                    {
                        return "";
                    }
                    var avaWhsQtys = SkuInstockDt.Where(p => p.ItemNo == itemNo && p.Color == color && p.Size == size && p.Length == length).
                        OrderBy(p => p.WhsSeq);
                    foreach (var avaWhsQty in avaWhsQtys)
                    {
                        if (avaWhsQty.CheckWhsInstock)
                        {
                            if (qty <= avaWhsQty.Instock)
                            {
                                whsNum = avaWhsQty.WhsNum;
                                break;
                            }
                            else
                            {
                                whsNum = "04"; //cannot find instock
                            }
                        }
                        else
                        {
                            whsNum = avaWhsQty.WhsNum;
                        }
                    }
                    if (whsNum == "") whsNum = "04"; //cannot find itemsku
                    return whsNum;
                }
                catch (Exception ex)
                {
                    throw ExceptionUtility.ReformatException(ex);
                }
            }

            private static string _DefaultSplitWhsNum = "01";
            private static void SplitOrderLines(
                OrderDst.OrderLineDataTable ordLn)
            {
                var fbaOrders = ordLn.Where(p => p.DestinateWhsNum == "A01");
                if (fbaOrders != null && fbaOrders.Count() > 0)
                {
                    return;
                }

                if (SkuInstockDt == null || SkuInstockDt.Rows.Count == 0)
                {
                    ordLn.ToList().ForEach(p => p.DestinateWhsNum = _DefaultSplitWhsNum);
                    return;
                }

                var ordSkuGrp = ordLn.GroupBy(p => new
                {
                    p.ItemNum,
                    p.StyleColor,
                    p.Size,
                    p.Length
                }).Select(p => new
                {
                    //ItemNo = p.Key.ItemNum,
                    //Color = p.Key.StyleColor,
                    //Size = p.Key.Size,
                    //Length = p.Key.Length,
                    Sku = p.Key.ItemNum + "-" + ImportApmUtility.GetASAColor(p.Key.StyleColor) + "-" + p.Key.Size + "-" + p.Key.Length,
                    OrderQty = p.Sum(s => s.Quantity)
                });
                //Get Sku Available Warehouse (has instock in warehouse)
                List<string> skus = ordSkuGrp.Select(p => p.Sku).ToList();
                var ordSkuInstock = SkuInstockDt.Where(p => skus.Contains(p.Sku) && p.Instock > 0);

                var validWhsSkus = ordSkuInstock.GroupBy(p => new { p.WhsNum, p.WhsSeq }).Select(p => new
                {
                    WhsNum = p.Key.WhsNum,
                    WhsSeq = p.Key.WhsSeq,
                    SkuCount = p.Count(),
                    Skus = string.Join(",", p.Select(s => s.Sku))
                }).OrderByDescending(p => p.SkuCount).ThenBy(p => p.WhsSeq);

                //(orderline#, whs#)
                //  Dictionary<int, string> skuWhs = new Dictionary<int, string>();
                foreach (var ln in ordLn)
                {
                    string destWhsNum = "";
                    string sku = ln.ItemNum + "-" + ImportApmUtility.GetASAColor(ln.StyleColor) + "-" + ln.Size + "-" + ln.Length;
                    var whsSkus = validWhsSkus.Where(p => p.Skus.Contains(sku));
                    foreach (var whsSku in whsSkus)
                    {
                        int orderQty = ln.Quantity;
                        var skuInstock = SkuInstockDt.FirstOrDefault(p =>
                            p.Sku == sku &&
                            p.WhsNum == whsSku.WhsNum);
                        int whsInstock = skuInstock.Instock;
                        if (orderQty <= whsInstock)
                        {
                            //skuWhs.Add(ln.LineNum, whsSku.WhsNum);
                            destWhsNum = whsSku.WhsNum;
                            skuInstock["Instock"] = whsInstock - orderQty;
                            break;
                        }
                    }

                    ln["DestinateWhsNum"] = destWhsNum;
                }

                var noWhsSkus = ordLn.Where(p => p.DestinateWhsNum == "").ToList();
                if (noWhsSkus.Count > 0)
                {
                    string defaultWhs = _DefaultSplitWhsNum;
                    if (noWhsSkus.Count() < ordLn.Count())
                    {
                        defaultWhs = ordLn.FirstOrDefault(p => p.DestinateWhsNum != "").DestinateWhsNum;
                    }
                    noWhsSkus.ForEach(p => p.DestinateWhsNum = defaultWhs);
                }
                return;
            }
        */

    }
}
