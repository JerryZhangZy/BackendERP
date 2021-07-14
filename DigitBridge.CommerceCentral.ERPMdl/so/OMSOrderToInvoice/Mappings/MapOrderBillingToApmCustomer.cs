using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DuoDotNetHelper.CommonV1;
using GhpIntegration.IntermediateDb;
using VibesCommon.Common;
using VibesCommon45.OmsDB;
using GhpIntegration.IntermediateMdl;
namespace GhpIntegration.OrderImportApmMdl.Mappings
{
    public class MapOrderBillingToApmCustomer
    {

        public static CustomerDst ConvertOrderCustomerToApmCustomer(string cusId, OrderDst.vw_OrderHeaderRow ordHR
            ,AccountSalesNumRate sales1, AccountSalesNumRate sales2)
        {
            CustomerDst cusDst = new CustomerDst();
            try
            {
                CustomerDst.customerDataTable cusDt = ConvertOrderCustomerToApmCustomerDt(cusId, ordHR, sales1, sales2);
                cusDst.customer.Merge(cusDt);

                return cusDst;
            }
            catch (Exception ex)
            {
                throw ExceptionUtility.ReformatException(ex);
            }
        }

        public static CustomerDst.customerDataTable ConvertOrderCustomerToApmCustomerDt(string cusId, OrderDst.vw_OrderHeaderRow ordHR
            , AccountSalesNumRate sales1, AccountSalesNumRate sales2)
        {
            CustomerDst.customerDataTable cusDt = new CustomerDst.customerDataTable();
            CustomerDst.customerRow cusRow = cusDt.NewcustomerRow();
            try
            {
                cusRow.CUS_NM = ImportApmUtility.ConvertUnicodeToAsciiCode(ordHR.BillName);
                cusRow.CUS_TYPE = 0;
                cusRow.CUS_ID = cusId;
                cusRow.ADDRESS = ImportApmUtility.ConvertUnicodeToAsciiCode(ordHR.BillAddressLine1);
                cusRow.ADDRESS2 = ImportApmUtility.ConvertUnicodeToAsciiCode(ordHR.BillAddressLine2);
                cusRow.CITY = ImportApmUtility.ConvertUnicodeToAsciiCode(ordHR.BillCity);

                string country = string.IsNullOrEmpty(ordHR.BillCountry) ? "US" : ordHR.BillCountry;
                cusRow.STATE = ImportApmUtility.ConvertUnicodeToAsciiCode(ImportApmUtility.GetProvinceAbbreivation(country, ordHR.BillState));
                cusRow.ZIP = ImportApmUtility.ConvertUnicodeToAsciiCode(ordHR.BillZip);
                cusRow.COUNTRY = ImportApmUtility.ConvertUnicodeToAsciiCode(country);
                cusRow.PHONE = ImportApmUtility.ConvertUnicodeToAsciiCode(ordHR.BillPhone);
                cusRow.PHONE_2 = ImportApmUtility.ConvertUnicodeToAsciiCode(ordHR.ShipPhone);
                cusRow.PHONE_3 = ""; // FAX
                cusRow.ATTN = "";
                cusRow.TITLE = "";
                cusRow.ATTN_2 = "";
                cusRow.CRD_LMT = 0;
                //cusRow.SALES_NUM = ImportApmUtility.GetNewCustomerSalesNM(ordHR.DatabaseSourceID, 1);//sales1
                //cusRow.SALES_NUM2 = ImportApmUtility.GetNewCustomerSalesNM(ordHR.DatabaseSourceID, 2);//sales2;
                cusRow.SALES_NUM = sales1.SalesNum;
                cusRow.SALES_NUM2 = sales2.SalesNum;
                cusRow.TERR_CD = "";
                cusRow.FIRST_DT = VibesDateTimeUtility.TodayInt;
                cusRow.TERM = 0;
                cusRow.APPX_AMT = 0;
                cusRow.TAX_RATE = ordHR.TaxRate * 100; //In Customer /5/4/2017
                cusRow.CUS_NT1 = "";
                cusRow.CUS_NT2 = "";
                cusRow.SALES_LS = "";
                cusRow.SLS_TYPE = 1; //Status: 1: Active 0: Inactive
                //cusRow.COMM_RATE = ImportApmUtility.GetSalesCommRT(cusRow.SALES_NUM);
                //cusRow.COMM_RATE2 = ImportApmUtility.GetSalesCommRT(cusRow.SALES_NUM2); ;
                cusRow.COMM_RATE = sales1.SalesRate;
                cusRow.COMM_RATE2 = sales2.SalesRate;
                cusRow.BAD_CHK_NUM = 0;
                cusRow.SHP_NM = ImportApmUtility.ConvertUnicodeToAsciiCode(ordHR.ShipName);
                cusRow.SHP_ADDRESS = ImportApmUtility.ConvertUnicodeToAsciiCode(ordHR.ShipAddressLine1);
                cusRow.SHP_ADDRESS2 = ImportApmUtility.ConvertUnicodeToAsciiCode(ordHR.ShipAddressLine2);
                cusRow.SHP_CITY = ImportApmUtility.ConvertUnicodeToAsciiCode(ordHR.ShipCity);

                country = string.IsNullOrEmpty(ordHR.ShipCountry) ? "US" : ordHR.ShipCountry;
                cusRow.SHP_STATE = ImportApmUtility.ConvertUnicodeToAsciiCode(ImportApmUtility.GetProvinceAbbreivation(country, ordHR.ShipState));
                cusRow.SHP_ZIP = ImportApmUtility.ConvertUnicodeToAsciiCode(ordHR.ShipZip);
                cusRow.SHP_COUNTRY = ImportApmUtility.ConvertUnicodeToAsciiCode(country);
                cusRow.PHN_EXT = 0;
                cusRow.CRD_RATE = "";
                cusRow.DEPT_NUM = "";
                cusRow.CUS_SUR = ordHR.BranchID; //BranchId;
                cusRow.EMAIL_ADR = ImportApmUtility.ConvertUnicodeToAsciiCode(ordHR.BillEmail);
                cusRow.WEB_ADR = "";
                cusRow.CC_NUM = "";
                cusRow.CC_EXP = 0;
                cusRow.CC_NAME = "";
                cusRow.DISCOUNT = 0;
                cusRow.TERMS_DAY = 0;
                cusRow.TERMS_COD = "N";
                cusRow.TERM_DESC = ordHR.DestinationPaymentType;
                cusRow.IMAGE_NM = "";
                cusRow.SOURCE_DESC = TextUtility.SubStringIf(ImportApmUtility.ConvertUnicodeToAsciiCode(ordHR.Source), ImportApmConst.ColumnMaxLen.Source);
                cusRow.SHIP_DESC = "";// TextUtility.SubStringIf(ordHR.DestinationShipDesc, ImportApmConst.ColumnMaxLen.ShipDesc);
                cusRow.PRIORITY = "";
                cusRow.DUN_NUM = "";
                cusRow.PHONE_4 = "";
                cusRow.SHIP_SPE = 0;
                cusRow.GL_ACT = "";
                cusRow.DB_NUM = "";
                cusRow.REF_NUM = "";
                cusRow.SHIP_OPT = 0;
                cusRow.RETAIL_STR = 0;
                cusRow.GRP_ID = "";
                cusRow.FAC_NM = "";
                cusRow.FAC_ACT = "";
                cusRow.FAC_LMT = 0;
                cusRow.SALES_NUM3 = "";
                cusRow.SALES_NUM4 = "";
                cusRow.COMM_RT3 = 0;
                cusRow.COMM_RT4 = 0;
                cusRow.STATUS_CD = "";
                cusDt.Rows.Add(cusRow);
                return cusDt;
            }
            catch (Exception ex)
            {

                throw ExceptionUtility.ReformatException(ex);
            }
        }

    }
}
