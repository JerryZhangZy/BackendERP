using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GhpIntegration.OrderImportApmMdl.Mappings
{
    /*
    public class ParaValue
    {
        public string CusId;
        public string UserId;
        public string SZRun;
        public string WhsNum;
        public int InvsNum;
        public int TodayInt;
        public int NowInt;
        public int InvsDateInt;

        public AccountSalesNumRate Sales1 = new AccountSalesNumRate();
        public AccountSalesNumRate Sales2 = new AccountSalesNumRate();

        public bool IsOrderForASA;
    }

    public class MapOrderToApmInvoice
    {
        private static ParaValue _ParaValue = new ParaValue();

        private static InvoiceTempAddDst _InvsDst;

        private static SortedList<int, int> _LineShipQty;

        private static bool _IsInvoiceForASA;

        public static InvoiceTempAddDst ConvertOrderToApmTempAddInvoice(OrderDst orderDst, string cusId, int invsNum, string userId
            , string szRun, string whs, AccountSalesNumRate sales1, AccountSalesNumRate sales2, bool isInvoiceForASA)
        {
            _IsInvoiceForASA = isInvoiceForASA;

            _InvsDst = new InvoiceTempAddDst();

            _ParaValue.CusId = cusId;
            _ParaValue.InvsNum = invsNum;
            _ParaValue.UserId = userId;
            _ParaValue.SZRun = GetSZRun(szRun);
            _ParaValue.WhsNum = GetWhsNum(whs, "");
            _ParaValue.TodayInt = VibesDateTimeUtility.ToDateInt(ImportApmMdl.ImportInvoiceDate);
            _ParaValue.NowInt = VibesDateTimeUtility.ToNowTimeInt;
            _ParaValue.Sales1 = sales1;
            _ParaValue.Sales2 = sales2;
            _ParaValue.IsOrderForASA = isInvoiceForASA;

            _LineShipQty = new SortedList<int, int>();

            try
            {
                //Invoice
                var ordHR = (OrderDst.vw_OrderHeaderRow)orderDst.vw_OrderHeader[0];
                var ordLn = orderDst.vw_OrderLine;

                InvoiceTempAddDst.Temp_InvoiceAddDataTable invoice = ConvertOrderHeaderToTempAddInvoice(ordHR);

                _InvsDst.Temp_InvoiceAdd.Merge(invoice);

                //Ins_Data
                InvoiceTempAddDst.Temp_Ins_DataAddDataTable insData = ConvertOrderHeaderToTempAddInsData(ordHR);

                _InvsDst.Temp_Ins_DataAdd.Merge(insData);

                //Invt_Log
                InvoiceTempAddDst.Temp_Invt_LogAddDataTable invtLog = ConvertOrderLineToTempAddInvtLog(ordHR, ordLn);
                _InvsDst.Temp_Invt_LogAdd.Merge(invtLog);

                //Insrun
                InvoiceTempAddDst.Temp_InszrunAddDataTable inszrun = ConvertOrderLineToTempAddInszrun(ordHR, ordLn);
                _InvsDst.Temp_InszrunAdd.Merge(inszrun);

                InvoiceTempAddDst.Temp_VibesInvoiceSizeExtAddDataTable insSizeExt = ConvertOrderLineToTempAddInvsSizeExt(ordHR, ordLn);
                _InvsDst.Temp_VibesInvoiceSizeExtAdd.Merge(insSizeExt);

                //ShipQty
                for (int idx = 0; idx < invtLog.Rows.Count; idx++)
                {
                    short lineNum = invtLog[idx].NT_NUM;
                    _InvsDst.Temp_Invt_LogAdd[idx].PROD_QTY = _LineShipQty[lineNum];
                }
                //Get Summary
                //discount = line discount + shipdiscount 2019.4.8
                //decimal invsAmt = _InvsDst.Temp_Invt_LogAdd.Sum(p => p.PROD_QTY * p.UNIT_PRS - p.DISC_LINE);
                decimal invsAmt = _InvsDst.Temp_Invt_LogAdd.Sum(p => p.PROD_QTY * p.UNIT_PRS) * (100 - _InvsDst.Temp_InvoiceAdd[0].DISCOUNT) / 100;
                _InvsDst.Temp_InvoiceAdd[0].INVS_AMT = invsAmt;
                _InvsDst.Temp_InvoiceAdd[0].INVS_COST = _InvsDst.Temp_Invt_LogAdd.Sum(p => p.PROD_QTY * p.LOG_COST);
                //_InvsDst.Temp_InvoiceAdd[0].COMM_AMT = _InvsDst.Temp_Invt_LogAdd.Sum(p => p.PROD_QTY * p.UNIT_PRS * p.COMM_RT) / 100;
                _InvsDst.Temp_InvoiceAdd[0].COMM_AMT = invsAmt * _InvsDst.Temp_InvoiceAdd[0].COMM_RT / 100;
                _InvsDst.Temp_InvoiceAdd[0].COMM_AMT2 = invsAmt * _InvsDst.Temp_InvoiceAdd[0].COMM_RT2 / 100;
                _InvsDst.Temp_Ins_DataAdd[0].INVS_CASE = (short)_InvsDst.Temp_Invt_LogAdd.Sum(p => p.TERM_LN);

                _InvsDst.Temp_Ins_DataAdd[0].INVS_TTL = invsAmt + ordHR.Freight + ordHR.HandleFee + ordHR.OtherFee + ordHR.TaxAmt; //Total amount 
                _InvsDst.Temp_Ins_DataAdd[0].INVS_BAL = _InvsDst.Temp_Ins_DataAdd[0].INVS_TTL - ordHR.PaidAmt; //> 0 Customer own Company, < 0 Company own Customer

                return _InvsDst;
            }
            catch (Exception ex)
            {

                throw ExceptionUtility.ReformatException(ex);
            }
        }

        private static InvoiceTempAddDst.Temp_InvoiceAddDataTable ConvertOrderHeaderToTempAddInvoice(OrderDst.vw_OrderHeaderRow ordHR)
        {
            try
            {
                InvoiceTempAddDst.Temp_InvoiceAddDataTable invoiceDt = new InvoiceTempAddDst.Temp_InvoiceAddDataTable();
                InvoiceTempAddDst.Temp_InvoiceAddRow tmpInvs = invoiceDt.NewTemp_InvoiceAddRow();

                VibesPaymentMapParameter vibesPayment = ImportApmUtility.GetVibesPaymentMap(ordHR.DestinationPaymentType);
                tmpInvs.INVS_NUM = _ParaValue.InvsNum;
                tmpInvs.INVS_CD = 1;
                tmpInvs.CUS_ID = _ParaValue.CusId;
                //tmpInvs.INVS_DT = _ParaValue.TodayInt; Update by Yunman 2015.8.18 Requested by Helen
                //tmpInvs.INVS_DT = VibesDateTimeUtility.ToDateInt(ordHR.OrderDate); 2015.10.30 By Helen ASA: invoicedate = orderdate; Vibes invoicedate = importdate
                int ordDt = VibesDateTimeUtility.ToDateInt(ordHR.OrderDate);
                if (_ParaValue.IsOrderForASA) //ASA Order
                {
                    tmpInvs.INVS_DT = ordDt;
                }
                else //Vibes Order
                {
                    tmpInvs.INVS_DT = _ParaValue.TodayInt > ordDt ? _ParaValue.TodayInt : ordDt;
                }
                tmpInvs.PROC_DT = _ParaValue.TodayInt;
                tmpInvs.BAT_NUM = 0;
                tmpInvs.INVS_AMT = 0; // ordHR.TotalAmt; sum from line (shipQty * price) - discount
                tmpInvs.PAID_AMT = ordHR.PaidAmt;
                tmpInvs.INVS_TAX = ordHR.TaxAmt;
                tmpInvs.TAX_RATE = ordHR.TaxRate;
                tmpInvs.MISC_CHG = ordHR.Freight + ordHR.OtherFee;
                tmpInvs.HANDL_FEE = ordHR.HandleFee;
                tmpInvs.DISCOUNT = ordHR.DiscountRate * 100; // discount rate in invoice ==> %
                tmpInvs.PAID_BY = (byte)(vibesPayment == null ? 0 : vibesPayment.PaymentByID);
                tmpInvs.TERMS_COD = "N";

                if (ordHR.DatabaseSourceID.Equals(ImportApmConst.ImportOrderDatasource.Magento))
                {
                    string sale1 = ImportApmUtility.GetOrderCouponSalesNM(ordHR.Source, ordHR.CouponCode, 1);
                    if (!string.IsNullOrEmpty(sale1))
                    {
                        _ParaValue.Sales1.SalesNum = sale1;
                        _ParaValue.Sales1.SalesRate = ImportApmUtility.GetSalesCommRT(sale1);
                    }
                    string sale2 = ImportApmUtility.GetOrderCouponSalesNM(ordHR.Source, ordHR.CouponCode, 2);
                    if (!string.IsNullOrEmpty(sale1))
                    {
                        _ParaValue.Sales2.SalesNum = sale2;
                        _ParaValue.Sales2.SalesRate = ImportApmUtility.GetSalesCommRT(sale2);
                    }
                }
                tmpInvs.SALES_NUM = _ParaValue.Sales1.SalesNum;
                tmpInvs.SALES_NUM2 = _ParaValue.Sales2.SalesNum;
                tmpInvs.TERM_DESC = ordHR.DestinationPaymentType;
                tmpInvs.TERMS_DAY = (short)ImportApmUtility.GetDestinationPaymentTermsDay(ordHR.BranchID, ordHR.Source, ordHR.DestinationPaymentType);
                tmpInvs.INVS_TM = _ParaValue.NowInt;
                tmpInvs.INVS_COST = 0; // Sum line cost
                tmpInvs.SALES_COST = 0;
                tmpInvs.HOLD_DAYS = 0;
                tmpInvs.ACT_NUM = (byte)(vibesPayment == null ? 0 : vibesPayment.BankID);
                tmpInvs.PROCESSOR = _ParaValue.UserId;
                int chkNum = 0;
                if (tmpInvs.PAID_BY == (byte)ImportApmConst.PaymentByID.CreditCard)
                {
                    if (ordHR.BranchID == "L7")
                    {
                        chkNum = ConvertUtility.ToInt(ordHR.OrderNum.Replace("#LV", ""));
                    }
                    else
                    {
                        chkNum = ConvertUtility.ToInt(ordHR.OrderNum);
                    }
                }
                tmpInvs.CHK_NUM = chkNum;
                tmpInvs.INVS_TYPE = ordHR.BranchID;
                tmpInvs.COMM_AMT = 0; //Sum Comm / OrderAmt
                tmpInvs.COMM_AMT2 = 0;
                tmpInvs.COMM_RT = _ParaValue.Sales1.SalesRate;
                tmpInvs.COMM_RT2 = _ParaValue.Sales2.SalesRate;
                tmpInvs.LAST_UPDT = _ParaValue.UserId;
                tmpInvs.TAXABLE_AMT = ordHR.TaxableAmt;
                tmpInvs.WHS_NUM = TextUtility.SubStringIf(ordHR.CouponCode, ImportApmConst.ColumnMaxLen.WhsNum);
                tmpInvs.CHT_NUM = 0;
                tmpInvs.CHT_NUM_2 = 0;
                tmpInvs.PROC_NUM = 0;
                tmpInvs.SALES_NUM3 = "";
                tmpInvs.SALES_NUM4 = "";
                tmpInvs.COMM_AMT3 = 0;
                tmpInvs.COMM_RT3 = 0;
                tmpInvs.COMM_AMT4 = 0;
                tmpInvs.COMM_RT4 = 0;
                tmpInvs.CURRENCY = "";
                tmpInvs.EXC_RT = 0;

                invoiceDt.Rows.Add(tmpInvs);

                return invoiceDt;
            }
            catch (Exception ex)
            {

                throw ExceptionUtility.ReformatException(ex);
            }
        }

        private static InvoiceTempAddDst.Temp_Ins_DataAddDataTable ConvertOrderHeaderToTempAddInsData(OrderDst.vw_OrderHeaderRow ordHR)
        {
            try
            {
                InvoiceTempAddDst.Temp_Ins_DataAddDataTable insDataDt = new InvoiceTempAddDst.Temp_Ins_DataAddDataTable();
                InvoiceTempAddDst.Temp_Ins_DataAddRow tmpInsData = insDataDt.NewTemp_Ins_DataAddRow();

                string country = string.IsNullOrEmpty(ordHR.ShipCountry) ? "US" : ordHR.ShipCountry;
                string state = ImportApmUtility.GetProvinceAbbreivation(country, ordHR.ShipState);

                tmpInsData.INVS_NUM = _ParaValue.InvsNum;
                tmpInsData.CUS_ID = _ParaValue.CusId;
                // tmpInsData.INVS_DT = _ParaValue.TodayInt;Update by Yunman 2015.8.18 Requested by Helen
                //tmpInsData.INVS_DT = VibesDateTimeUtility.ToDateInt(ordHR.OrderDate);2015.10.30 By Helen ASA: invoicedate = orderdate; Vibes invoicedate = importdate
                if (_ParaValue.IsOrderForASA)
                {
                    tmpInsData.INVS_DT = VibesDateTimeUtility.ToDateInt(ordHR.OrderDate);
                }
                else
                {
                    tmpInsData.INVS_DT = _ParaValue.TodayInt;
                }
                tmpInsData.ORDER_NO = ordHR.SplitNum;
                tmpInsData.ORDER_DT = VibesDateTimeUtility.ToDateInt(ordHR.OrderDate);
                tmpInsData.SP_SM_ADR = "N";
                tmpInsData.PO_NUM = ordHR.OrderNum;
                tmpInsData.SHIP_DT = 0;
                tmpInsData.SP_ADR = ordHR.ShipName;
                tmpInsData.SP_ADR_2 = ordHR.ShipAddressLine1;
                tmpInsData.SP_ADR_22 = ordHR.ShipAddressLine2;
                tmpInsData.SP_ADR_3 = TextUtility.SubStringIf(ordHR.ShipCity + "," + state + " " + ordHR.ShipZip, ImportApmConst.ColumnMaxLen.ShipAddress);
                tmpInsData.SP_CITY = ordHR.ShipCity;


                tmpInsData.SP_ST = state;
                tmpInsData.SP_ZIP = ordHR.ShipZip;
                tmpInsData.SP_COUNTRY = country;
                tmpInsData.IN_ADR = ordHR.BillAddressLine1;
                tmpInsData.IN_ADR_2 = ordHR.BillAddressLine2;
                tmpInsData.IN_ADR_22 = "";
                tmpInsData.IN_ADR_3 = TextUtility.SubStringIf(ordHR.BillAddressLine3, ImportApmConst.ColumnMaxLen.ShipAddress);
                tmpInsData.IN_CITY = ordHR.BillCity;

                country = string.IsNullOrEmpty(ordHR.BillCountry) ? "US" : ordHR.BillCountry;
                tmpInsData.IN_ST = ImportApmUtility.GetProvinceAbbreivation(country, ordHR.BillState);
                tmpInsData.IN_ZIP = ordHR.BillZip;
                tmpInsData.IN_COUNTRY = country;
                tmpInsData.NT_SEL = string.IsNullOrEmpty(ordHR.Notes) ? "N" : "Y";

                if (tmpInsData.NT_SEL == "Y")
                {
                    InvoiceTempAddDst.Temp_NtfileAddDataTable ntFile = ConvertOrderHeaderToTempAddNtFile(ordHR);
                    _InvsDst.Temp_NtfileAdd.Merge(ntFile);
                }

                tmpInsData.ATTN = "";
                tmpInsData.REF_NUM = TextUtility.SubStringIf(ordHR.ReferenceNum1, ImportApmConst.ColumnMaxLen.RefNum);
                if (ordHR.DatabaseSourceID.Equals(ImportApmConst.ImportOrderDatasource.DiCenDS) &&
                    ordHR.AccountID.Equals(ImportApmConst.ImportOrderAccout.Bluestem))
                {
                    tmpInsData.ATTN = ordHR.ReferenceNum1;
                    tmpInsData.REF_NUM = "";
                }
                tmpInsData.INT_SP_DT = 0;
                tmpInsData.INVS_CASE = 0;
                tmpInsData.INVS_WT = 0;
                tmpInsData.INVS_FT = 0;

                tmpInsData.SHIP_DESC = TextUtility.SubStringIf(ordHR.DestinationShipDesc, ImportApmConst.ColumnMaxLen.ShipDesc);
                tmpInsData.FOB_DESC = "EL MONTE, CA";
                tmpInsData.BK_INVS = 0;
                tmpInsData.CHN_STR = "";
                tmpInsData.SHIP_TERM = ordHR.DestinationShipDesc.Contains("-Prime") ? "P" : "";
                tmpInsData.NUM_CTL = 0;
                tmpInsData.PK_PDT = 0;
                tmpInsData.INS_CNT = 0;
                tmpInsData.INS_PDT = 0;
                tmpInsData.DISCOUNT = ordHR.DiscountAmt; // discount amount in ins_data
                tmpInsData.MISC_CHG = ordHR.OtherFee;
                tmpInsData.FRT_CHG = ordHR.Freight;
                tmpInsData.COD_CHG = 0;
                tmpInsData.HANDL_FEE = ordHR.HandleFee;
                tmpInsData.INVS_TAX = ordHR.TaxAmt;
                tmpInsData.TAX_RATE = ordHR.TaxRate;
                tmpInsData.INVS_TTL = 0;// ordHR.TotalAmt + ordHR.Freight + ordHR.HandleFee + ordHR.OtherFee + ordHR.TaxAmt; //Total amount 
                tmpInsData.INVS_BAL = 0; // tmpInsData.INVS_TTL - ordHR.PaidAmt; //> 0 Customer own Company, < 0 Company own Customer
                tmpInsData.INVS_TYPE = "";
                tmpInsData.PRT_SP = 0;
                tmpInsData.PACK_PRT = 0;
                tmpInsData.INVS_PRT = 0;
                tmpInsData.SOUR_DESC = TextUtility.SubStringIf(ordHR.Source, ImportApmConst.ColumnMaxLen.Source);


                insDataDt.Rows.Add(tmpInsData);

                return insDataDt;
            }
            catch (Exception ex)
            {

                throw ExceptionUtility.ReformatException(ex);
            }
        }

        private static InvoiceTempAddDst.Temp_NtfileAddDataTable ConvertOrderHeaderToTempAddNtFile(OrderDst.vw_OrderHeaderRow ordHR)
        {
            try
            {
                InvoiceTempAddDst.Temp_NtfileAddDataTable ntFileDt = new InvoiceTempAddDst.Temp_NtfileAddDataTable();
                InvoiceTempAddDst.Temp_NtfileAddRow tmpNtFile = ntFileDt.NewTemp_NtfileAddRow();

                tmpNtFile.NT_NUM = _ParaValue.InvsNum;
                tmpNtFile.NT_CD = 1;
                string note1 = TextUtility.SubStringIf(ordHR.Notes, ImportApmConst.ColumnMaxLen.InvsHeaderNote), note2 = "";
                if (ordHR.Notes.Length > ImportApmConst.ColumnMaxLen.InvsHeaderNote)
                {
                    note2 = ordHR.Notes.Substring(ImportApmConst.ColumnMaxLen.InvsHeaderNote + 1);
                }
                tmpNtFile.NOTES1 = note1;
                tmpNtFile.NOTES2 = note2;

                ntFileDt.Rows.Add(tmpNtFile);

                return ntFileDt;
            }
            catch (Exception ex)
            {

                throw ExceptionUtility.ReformatException(ex);
            }
        }

        private static InvoiceTempAddDst.Temp_Invt_LogAddDataTable ConvertOrderLineToTempAddInvtLog(OrderDst.vw_OrderHeaderRow ordHR
            , OrderDst.vw_OrderLineDataTable ordLn)
        {
            try
            {
                InvoiceTempAddDst.Temp_Invt_LogAddDataTable invtLogDt = new InvoiceTempAddDst.Temp_Invt_LogAddDataTable();

                var ordItems = ordLn.OrderBy(p => p.LineNum).ToList();

                int lineNum = 1;
                foreach (var ordItem in ordItems)
                {
                    string item = ordItem.ItemNum;
                    string color = ordItem.StyleColor;

                    lineNum = ordItem.LineNum;

                    _ParaValue.WhsNum = GetWhsNum(_ParaValue.WhsNum, item);
                    //split whs by destinatewhs
                    if (_ParaValue.WhsNum != "A01")
                    {
                        _ParaValue.WhsNum = ordLn[0].DestinateWhsNum;
                    }
                    InventoryDst.vw_InStockInDataRow invData = ImportApmUtility.GetItemWithColor(item, color, _ParaValue.WhsNum);
                    if (invData == null)
                    {
                        string errMsg = "Inventory Error! No Color in Inv_Data. Item: " + item + ", Color: " + color + ", Whs: " + _ParaValue.WhsNum + ". ";
                        throw new Exception(errMsg);
                    }

                    #region Invt_Log
                    InvoiceTempAddDst.Temp_Invt_LogAddRow tmpInvtLog = invtLogDt.NewTemp_Invt_LogAddRow();

                    tmpInvtLog.LOG_DT = _ParaValue.TodayInt;
                    tmpInvtLog.LOG_TIME = _ParaValue.NowInt;
                    tmpInvtLog.INVS_NUM = _ParaValue.InvsNum;
                    tmpInvtLog.INVS_CD = 1;
                    tmpInvtLog.CUS_ID = _ParaValue.CusId;
                    tmpInvtLog.PROD_CD = item;
                    tmpInvtLog.PROD_CLR = color;
                    tmpInvtLog.RUN_CD = _ParaValue.SZRun;
                    tmpInvtLog.WHS_NUM = _ParaValue.WhsNum;
                    tmpInvtLog.PROD_QTY = 0;
                    tmpInvtLog.ORDER_QTY = ordItem.Quantity;
                    tmpInvtLog.UNIT_PRS = ordItem.UnitPrice;

                    tmpInvtLog.DISCOUNT = ordItem.DiscountRate * 100;
                    tmpInvtLog.REAL_QTY = 0;
                    tmpInvtLog.LOG_COST = invData.BaseCost; //Item Cost;
                    tmpInvtLog.PROD_DUTY = 0;
                    tmpInvtLog.ORD_TYPE = 0;
                    tmpInvtLog.UT_LINE = 0;
                    tmpInvtLog.UNIT_WT = 0;
                    tmpInvtLog.UNIT_NM = "";
                    tmpInvtLog.PO_NUM = ordItem.IntermediateLineNum.ToString();

                    tmpInvtLog.SALES_NUM = _ParaValue.Sales1.SalesNum;
                    tmpInvtLog.SALES_NUM2 = _ParaValue.Sales2.SalesNum;
                    tmpInvtLog.COMM_RT = _ParaValue.Sales1.SalesRate;
                    tmpInvtLog.INVS_TYPE = "";
                    tmpInvtLog.TAX_IND = ordHR.TaxableAmt > 0 ? "Y" : "N";
                    string itemDesc = TextUtility.SubStringIf(ordItem.ItemDesc, ImportApmConst.ColumnMaxLen.ItemDescription);
                    if (string.IsNullOrEmpty(itemDesc))
                    {
                        itemDesc = TextUtility.SubStringIf(invData.ItemDescription, ImportApmConst.ColumnMaxLen.ItemDescription);
                    }
                    tmpInvtLog.UT_DESC = itemDesc;
                    tmpInvtLog.COMM_LN = (short)lineNum;
                    tmpInvtLog.NT_NUM = (short)lineNum;
                    string notes = ordItem.LineNotes;
                    tmpInvtLog.UT_NT = string.IsNullOrEmpty(notes) ? "N" : "Y";
                    if (tmpInvtLog.UT_NT == "Y")
                    {
                        //Invs_lnt(line note)
                        InvoiceTempAddDst.Temp_Invs_LntAddDataTable invsLnt = ConvertOrderLineToTempAddInvsLnt(lineNum, notes);
                        _InvsDst.Temp_Invs_LntAdd.Merge(invsLnt);
                    }
                    tmpInvtLog.TERM_LN = (short)ordItem.Quantity; //Case
                    tmpInvtLog.CURRENCY = "";
                    tmpInvtLog.EXC_RT = 0;
                    tmpInvtLog.DISC_LINE = ordItem.DiscountAmt;

                    invtLogDt.Rows.Add(tmpInvtLog);
                    #endregion InvtLog
                }
                return invtLogDt;
            }
            catch (Exception ex)
            {

                throw ExceptionUtility.ReformatException(ex);
            }
        }

        private static InvoiceTempAddDst.Temp_InszrunAddDataTable ConvertOrderLineToTempAddInszrun(OrderDst.vw_OrderHeaderRow ordHR
   , OrderDst.vw_OrderLineDataTable ordLn)
        {
            try
            {
                InvoiceTempAddDst.Temp_InszrunAddDataTable inszrunDt = new InvoiceTempAddDst.Temp_InszrunAddDataTable();

                var ordItems = ordLn.OrderBy(p => p.LineNum).ToList();

                foreach (var ordItem in ordItems)
                {

                    string item = ordItem.ItemNum;
                    string color = ordItem.StyleColor;
                    int lineNum = ordItem.LineNum;

                    _ParaValue.WhsNum = GetWhsNum(_ParaValue.WhsNum, item);

                    string length = ordItem.Length;
                    List<InventoryDst.vw_InStockInSZMTRow> invSzmts = new List<InventoryDst.vw_InStockInSZMTRow>();
                    if (!string.IsNullOrEmpty(length))
                    {

                        invSzmts = ImportApmUtility.GetItemSizes(item, color, length, _ParaValue.SZRun, _ParaValue.WhsNum).ToList();

                        if (invSzmts == null || invSzmts.Count == 0)
                        {
                            string errMsg = "Inventory Error! No Color or Length in Inv_SZMT. Item: " + item + ", Color: " + color + ", Length: " + length + ".";
                            throw new Exception(errMsg);
                        }
                    }
                    else
                    {
                        bool notFoundLength = true;
                        if (ImportApmMdl.OmsStandardInseams.Count == 0)
                        {
                            throw new Exception("No Default Standard Inseam found.");
                        }
                        foreach (string defaultLength in ImportApmMdl.OmsStandardInseams)
                        {
                            length = defaultLength;
                            invSzmts = ImportApmUtility.GetItemSizes(item, color, length, _ParaValue.SZRun, _ParaValue.WhsNum).ToList();
                            if (invSzmts != null && invSzmts.Count > 0)
                            {
                                notFoundLength = false;
                                break;
                            }
                        }

                        if (notFoundLength)
                        {
                            string errMsg = "Inventory Error! No Color or Length in Inv_SZMT. Item: " + item + ", Color: " + color + ", Length: .";
                            throw new Exception(errMsg);
                        }
                    }

                    InvoiceTempAddDst.Temp_InszrunAddRow tmpInszrun = inszrunDt.NewTemp_InszrunAddRow();

                    tmpInszrun.INVS_TYPE = 1;
                    tmpInszrun.INVS_NUM = _ParaValue.InvsNum * 100 + 1;
                    tmpInszrun.RUN_CD = ordItem.LineNum;
                    tmpInszrun.PROD_LEN = length;
                    tmpInszrun.RUN_TBL1 = 0;
                    tmpInszrun.RUN_TBL2 = 0;
                    tmpInszrun.RUN_TBL3 = 0;
                    tmpInszrun.RUN_TBL4 = 0;
                    tmpInszrun.RUN_TBL5 = 0;
                    tmpInszrun.RUN_TBL6 = 0;
                    tmpInszrun.RUN_TBL7 = 0;
                    tmpInszrun.RUN_TBL8 = 0;
                    tmpInszrun.RUN_TBL9 = 0;
                    tmpInszrun.RUN_TBL10 = 0;
                    tmpInszrun.RUN_TBL11 = 0;
                    tmpInszrun.RUN_TBL12 = 0;
                    tmpInszrun.RUN_TBL13 = 0;
                    tmpInszrun.RUN_TBL14 = 0;
                    tmpInszrun.RUN_TBL15 = 0;
                    tmpInszrun.RUN_TBL16 = 0;
                    tmpInszrun.RUN_TBL17 = 0;
                    tmpInszrun.RUN_TBL18 = 0;
                    tmpInszrun.RUN_TBL19 = 0;
                    tmpInszrun.RUN_TBL20 = 0;

                    int sizePosition = 1;
                    int itemInstock = 0;
                    if (!string.IsNullOrEmpty(ordItem.Size))
                    {
                        var orderItemSize = invSzmts.OrderBy(p => p.SizePosition).FirstOrDefault(p => p.Size == ordItem.Size && p.Length == length);

                        if (orderItemSize == null)
                        {
                            string errMsg = "Inventory Error! No Size in Inv_SZMT. Item: " + ordItem.ItemNum +
                                ", Color: " + ordItem.StyleColor +
                                ", Size: " + ordItem.Size +
                                ", Length: " + length +
                                ". ";
                            throw new Exception(errMsg);
                        }

                        sizePosition = orderItemSize.SizePosition;

                        itemInstock = orderItemSize.InStock;
                    }
                    int shipQty = 0;
                    if (ImportApmMdl.CheckInstock)
                    {
                        shipQty = ordItem.Quantity < itemInstock ? ordItem.Quantity : 0;
                    }
                    else
                    {
                        shipQty = ordItem.Quantity;
                    }

                    _LineShipQty.Add(lineNum, shipQty);
                    switch (sizePosition)
                    {
                        case 1:
                            tmpInszrun.RUN_TBL1 = shipQty;
                            break;
                        case 2:
                            tmpInszrun.RUN_TBL2 = shipQty;
                            break;
                        case 3:
                            tmpInszrun.RUN_TBL3 = shipQty;
                            break;
                        case 4:
                            tmpInszrun.RUN_TBL4 = shipQty;
                            break;
                        case 5:
                            tmpInszrun.RUN_TBL5 = shipQty;
                            break;
                        case 6:
                            tmpInszrun.RUN_TBL6 = shipQty;
                            break;
                        case 7:
                            tmpInszrun.RUN_TBL7 = shipQty;
                            break;
                        case 8:
                            tmpInszrun.RUN_TBL8 = shipQty;
                            break;
                        case 9:
                            tmpInszrun.RUN_TBL9 = shipQty;
                            break;
                        case 10:
                            tmpInszrun.RUN_TBL10 = shipQty;
                            break;
                        case 11:
                            tmpInszrun.RUN_TBL11 = shipQty;
                            break;
                        case 12:
                            tmpInszrun.RUN_TBL12 = shipQty;
                            break;
                        case 13:
                            tmpInszrun.RUN_TBL13 = shipQty;
                            break;
                        case 14:
                            tmpInszrun.RUN_TBL14 = shipQty;
                            break;
                        case 15:
                            tmpInszrun.RUN_TBL15 = shipQty;
                            break;
                        case 16:
                            tmpInszrun.RUN_TBL16 = shipQty;
                            break;
                        case 17:
                            tmpInszrun.RUN_TBL17 = shipQty;
                            break;
                        case 18:
                            tmpInszrun.RUN_TBL18 = shipQty;
                            break;
                        case 19:
                            tmpInszrun.RUN_TBL19 = shipQty;
                            break;
                        case 20:
                            tmpInszrun.RUN_TBL20 = shipQty;
                            break;
                        default:
                            throw new Exception("Unknow Sizeposition. " + sizePosition);
                    }
                    inszrunDt.Rows.Add(tmpInszrun);

                }
                return inszrunDt;
            }
            catch (Exception ex)
            {

                throw ExceptionUtility.ReformatException(ex);
            }
        }

        private static InvoiceTempAddDst.Temp_Invs_LntAddDataTable ConvertOrderLineToTempAddInvsLnt(int lineNum, string notes)
        {
            try
            {
                InvoiceTempAddDst.Temp_Invs_LntAddDataTable invsLnt = new InvoiceTempAddDst.Temp_Invs_LntAddDataTable();

                InvoiceTempAddDst.Temp_Invs_LntAddRow tmpInvsLnt = invsLnt.NewTemp_Invs_LntAddRow();

                tmpInvsLnt.INVS_NUM = _ParaValue.InvsNum;
                tmpInvsLnt.INVS_CD = 1;
                tmpInvsLnt.NT_NUM = (short)lineNum;
                tmpInvsLnt.INVS_NT = notes;

                invsLnt.Rows.Add(tmpInvsLnt);

                return invsLnt;
            }
            catch (Exception ex)
            {

                throw ExceptionUtility.ReformatException(ex);
            }

        }

        private static InvoiceTempAddDst.Temp_VibesInvoiceSizeExtAddDataTable ConvertOrderLineToTempAddInvsSizeExt(OrderDst.vw_OrderHeaderRow ordHR
  , OrderDst.vw_OrderLineDataTable ordLn)
        {
            try
            {
                InvoiceTempAddDst.Temp_VibesInvoiceSizeExtAddDataTable insSizeExtDt = new InvoiceTempAddDst.Temp_VibesInvoiceSizeExtAddDataTable();

                var ordItems = ordLn.OrderBy(p => p.LineNum).ToList();

                foreach (var ordItem in ordItems)
                {

                    string item = ordItem.ItemNum;
                    string color = ordItem.StyleColor;
                    int lineNum = ordItem.LineNum;
                    string length = !string.IsNullOrEmpty(ordItem.Length) ? ordItem.Length : "STD";

                    _ParaValue.WhsNum = GetWhsNum(_ParaValue.WhsNum, item);

                    List<InventoryDst.vw_InStockInSZMTRow> invSzmts = ImportApmUtility.GetItemSizes(item, color, length, _ParaValue.SZRun, _ParaValue.WhsNum).ToList();
                    if (invSzmts == null)
                    {
                        string errMsg = "Inventory Error! No Color or Length in Inv_SZMT. Item: " + item + ", Color: " + color + ", Length: " + length + ".";
                        throw new Exception(errMsg);
                    }

                    InvoiceTempAddDst.Temp_VibesInvoiceSizeExtAddRow tmpInsSizeExt = insSizeExtDt.NewTemp_VibesInvoiceSizeExtAddRow();

                    tmpInsSizeExt.InvoiceNum = _ParaValue.InvsNum;
                    tmpInsSizeExt.LineNum = ordItem.LineNum;
                    tmpInsSizeExt.Length = length;
                    tmpInsSizeExt.Size = ordItem.Size;
                    tmpInsSizeExt.OrderQty = ordItem.Quantity;

                    insSizeExtDt.Rows.Add(tmpInsSizeExt);

                }
                return insSizeExtDt;
            }
            catch (Exception ex)
            {

                throw ExceptionUtility.ReformatException(ex);
            }
        }

        private static string GetSZRun(string szRun)
        {
            return string.IsNullOrEmpty(szRun) ? "X" : szRun;
        }

        private static string GetWhsNum(string whs, string itemNo)
        {
            //Update 2019.9.9
            //if (itemNo.ToUpper().StartsWith("LV") && whs != "A01")
            //{
            //    if (_IsInvoiceForASA == true)
            //    {
            //        return "01";
            //    }
            //    else
            //    {
            //        return "11";
            //    }
            //}
            //else
                return string.IsNullOrEmpty(whs) ? "01" : whs;
        }
    }
    */
}
