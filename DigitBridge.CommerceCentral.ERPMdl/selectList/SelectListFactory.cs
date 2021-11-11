using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DigitBridge.Base.Common;
using DigitBridge.Base.Utility;
using DigitBridge.CommerceCentral.ERPDb;

using DigitBridge.CommerceCentral.ERPMdl.selectList.customer;
using DigitBridge.CommerceCentral.YoPoco;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using Hepler = DigitBridge.CommerceCentral.ERPDb.SalesOrderHeaderHelper;
using InfoHepler = DigitBridge.CommerceCentral.ERPDb.SalesOrderHeaderInfoHelper;
using DigitBridge.CommerceCentral.ERPMdl.selectList.po;
using DigitBridge.CommerceCentral.ERPMdl.selectList.vender;
using DigitBridge.CommerceCentral.ERPMdl.selectList.poReceive;

namespace DigitBridge.CommerceCentral.ERPMdl
{
    public class SelectListFactory
    {
        #region DataBase
        [JsonIgnore]
        protected IDataBaseFactory _dbFactory;

        [JsonIgnore]
        public IDataBaseFactory dbFactory
        {
            get
            {
                if (_dbFactory is null)
                    _dbFactory = DataBaseFactory.CreateDefault();
                return _dbFactory;
            }
        }

        public void SetDataBaseFactory(IDataBaseFactory dbFactory)
        {
            _dbFactory = dbFactory;
            return;
        }
        #endregion DataBase


        public SelectListFactory(IDataBaseFactory dbFactory)
        {
            SetDataBaseFactory(dbFactory);
        }

        public virtual async Task<bool> GetSelectListAsync(SelectListPayload payload)
        {
            if (payload == null) return false;

            var srv = GetSelectListService(payload);
            if (srv == null)
                return false;
            return await srv.GetSelectListAsync(payload);
        }

        protected virtual ISelectList GetSelectListService(SelectListPayload payload)
        {
            if (payload == null || string.IsNullOrWhiteSpace(payload.Name))
                return null;

            #region system global
            if (payload.Name.EqualsIgnoreSpace("inventory_alternateCode")) return new inventory_alternateCode(dbFactory);
            if (payload.Name.EqualsIgnoreSpace("inventory_oemCode")) return new inventory_oemCode(dbFactory);
            if (payload.Name.EqualsIgnoreSpace("inventory_catalogPage")) return new inventory_catalogPage(dbFactory);
            if (payload.Name.EqualsIgnoreSpace("inventory_productYear")) return new inventory_productYear(dbFactory);
            if (payload.Name.EqualsIgnoreSpace("inventory_categoryCode")) return new inventory_categoryCode(dbFactory);
            if (payload.Name.EqualsIgnoreSpace("inventory_remark")) return new inventory_remark(dbFactory);
            if (payload.Name.EqualsIgnoreSpace("inventory_classCode")) return new inventory_classCode(dbFactory);
            if (payload.Name.EqualsIgnoreSpace("inventory_sizeCode")) return new inventory_sizeCode(dbFactory);
            if (payload.Name.EqualsIgnoreSpace("inventory_colorPatternCode")) return new inventory_colorPatternCode(dbFactory);
            if (payload.Name.EqualsIgnoreSpace("inventory_sizeType")) return new inventory_sizeType(dbFactory);
            if (payload.Name.EqualsIgnoreSpace("inventory_departmentCode")) return new inventory_departmentCode(dbFactory);
            if (payload.Name.EqualsIgnoreSpace("inventory_sku")) return new inventory_sku(dbFactory);
            if (payload.Name.EqualsIgnoreSpace("inventory_divisionCode")) return new inventory_divisionCode(dbFactory);
            if (payload.Name.EqualsIgnoreSpace("inventory_styleCode")) return new inventory_styleCode(dbFactory);
            if (payload.Name.EqualsIgnoreSpace("inventory_groupCode")) return new inventory_groupCode(dbFactory);
            if (payload.Name.EqualsIgnoreSpace("inventory_subClassCode")) return new inventory_subClassCode(dbFactory);
            if (payload.Name.EqualsIgnoreSpace("inventory_lengthCode")) return new inventory_lengthCode(dbFactory);
            if (payload.Name.EqualsIgnoreSpace("inventory_subGroupCode")) return new inventory_subGroupCode(dbFactory);
            if (payload.Name.EqualsIgnoreSpace("inventory_lotNum")) return new inventory_lotNum(dbFactory);
            if (payload.Name.EqualsIgnoreSpace("inventory_warehouseCode")) return new inventory_warehouseCode(dbFactory);
            if (payload.Name.EqualsIgnoreSpace("inventory_lpnNum")) return new inventory_lpnNum(dbFactory);
            if (payload.Name.EqualsIgnoreSpace("inventory_widthCode")) return new inventory_widthCode(dbFactory);
            if (payload.Name.EqualsIgnoreSpace("inventory_model")) return new inventory_model(dbFactory);
            //if (obj.listFor.EqualsIgnoreSpace("oms_ar_term"))
            //    return new DemList_oms_ar_term(obj);
            //if (obj.listFor.EqualsIgnoreSpace("oms_src_file"))
            //    return new DemList_oms_src_file(obj);
            //if (obj.listFor.EqualsIgnoreSpace("oms_div_file"))
            //    return new DemList_oms_div_file(obj);
            //if (obj.listFor.EqualsIgnoreSpace("oms_det_file"))
            //    return new DemList_oms_det_file(obj);
            //if (obj.listFor.EqualsIgnoreSpace("oms_ssc_cdf"))
            //    return new DemList_oms_ssc_cdf(obj);
            //if (obj.listFor.EqualsIgnoreSpace("oms_bnk_file"))
            //    return new DemList_oms_bnk_file(obj).SetWithZero(true);
            //if (obj.listFor.EqualsIgnoreSpace("oms_ship_desc"))
            //    return new DemList_oms_ship_desc(obj);
            //if (obj.listFor.EqualsIgnoreSpace("oms_sls_pro"))
            //    return new DemList_oms_sls_pro(obj);
            //if (obj.listFor.EqualsIgnoreSpace("oms_ar_paidby"))
            //    return new DemList_oms_ar_paidby(obj);
            //if (obj.listFor.EqualsIgnoreSpace("oms_cos_paidby"))
            //    return new DemList_oms_cos_paidby(obj);
            //if (obj.listFor.EqualsIgnoreSpace("oms_prs_file"))
            //    return new DemList_oms_prs_file(obj);
            //if (obj.listFor.EqualsIgnoreSpace("oms_typefile"))
            //    return new DemList_oms_typefile(obj);
            //if (obj.listFor.EqualsIgnoreSpace("oms_deptfile"))
            //    return new DemList_oms_deptfile(obj);
            //if (obj.listFor.EqualsIgnoreSpace("oms_cus_grp"))
            //    return new DemList_oms_cus_grp(obj);
            //if (obj.listFor.EqualsIgnoreSpace("oms_cus_subgrp"))
            //    return new DemList_oms_cus_subgrp(obj);
            //if (obj.listFor.EqualsIgnoreSpace("oms_clssfile"))
            //    return new DemList_oms_clssfile(obj);
            //if (obj.listFor.EqualsIgnoreSpace("oms_clrfile"))
            //    return new DemList_oms_clrfile(obj);
            //if (obj.listFor.EqualsIgnoreSpace("oms_size_cdf"))
            //    return new DemList_oms_size_cdf(obj);
            //if (obj.listFor.EqualsIgnoreSpace("oms_brand_nm"))
            //    return new DemList_oms_brand_nm(obj);
            //if (obj.listFor.EqualsIgnoreSpace("oms_oem_cdf"))
            //    return new DemList_oms_oem_cdf(obj);
            //if (obj.listFor.EqualsIgnoreSpace("oms_measfile"))
            //    return new DemList_oms_measfile(obj);
            //if (obj.listFor.EqualsIgnoreSpace("oms_modfile_maker"))
            //    return new DemList_oms_modfile_maker(obj);
            //if (obj.listFor.EqualsIgnoreSpace("oms_modfile_model"))
            //    return new DemList_oms_modfile_model(obj);
            //if (obj.listFor.EqualsIgnoreSpace("oms_clsfile_group"))
            //    return new DemList_oms_clsfile_group(obj);
            //if (obj.listFor.EqualsIgnoreSpace("oms_clsfile_subgroup"))
            //    return new DemList_oms_clsfile_subgroup(obj);


            #endregion

            #region inventory
            //if (obj.listFor.EqualsIgnoreSpace("inv_prod_cd"))
            //    return new DemList_inv_prod_cd(obj);
            //if (obj.listFor.EqualsIgnoreSpace("inv_descrip"))
            //    return new DemList_inv_descrip(obj);
            //if (obj.listFor.EqualsIgnoreSpace("inv_dept_num"))
            //    return new DemList_inv_dept_num(obj);
            //if (obj.listFor.EqualsIgnoreSpace("inv_cat_cd"))
            //    return new DemList_inv_cat_cd(obj);
            //if (obj.listFor.EqualsIgnoreSpace("inv_class_cd"))
            //    return new DemList_inv_class_cd(obj);
            //if (obj.listFor.EqualsIgnoreSpace("inv_unit_color"))
            //    return new DemList_inv_unit_color(obj);
            //if (obj.listFor.EqualsIgnoreSpace("inv_unit_size"))
            //    return new DemList_inv_unit_size(obj);
            //if (obj.listFor.EqualsIgnoreSpace("inv_brd_nm"))
            //    return new DemList_inv_brd_nm(obj);
            //if (obj.listFor.EqualsIgnoreSpace("inv_maker"))
            //    return new DemList_inv_maker(obj);
            //if (obj.listFor.EqualsIgnoreSpace("inv_model"))
            //    return new DemList_inv_model(obj);
            //if (obj.listFor.EqualsIgnoreSpace("inv_prod_yr"))
            //    return new DemList_inv_prod_yr(obj);
            //if (obj.listFor.EqualsIgnoreSpace("inv_group_cd"))
            //    return new DemList_inv_group_cd(obj);
            //if (obj.listFor.EqualsIgnoreSpace("inv_subgroup_cd"))
            //    return new DemList_inv_subgroup_cd(obj);

            //if (obj.listFor.EqualsIgnoreSpace("inv_oem"))
            //    return new DemList_inv_oem(obj);
            //if (obj.listFor.EqualsIgnoreSpace("inv_alt_cd"))
            //    return new DemList_inv_alt_cd(obj);
            //if (obj.listFor.EqualsIgnoreSpace("inv_unit_nm"))
            //    return new DemList_inv_unit_nm(obj);
            //if (obj.listFor.EqualsIgnoreSpace("inv_category"))
            //    return new DemList_inv_category(obj);

            //if (obj.listFor.EqualsIgnoreSpace("inv_prod_clr"))
            //    return new DemList_inv_prod_clr(obj);
            //if (obj.listFor.EqualsIgnoreSpace("inv_run_cd"))
            //    return new DemList_inv_run_cd(obj);
            //if (obj.listFor.EqualsIgnoreSpace("inv_size_name"))
            //    return new DemList_inv_size_name(obj);

            //if (obj.listFor.EqualsIgnoreSpace("inv_whs_num"))
            //    return new DemList_inv_whs_num(obj);

            #endregion

            #region Warehouse Transfer
            //if (obj.listFor.EqualsIgnoreSpace("wtrs_bat_num"))
            //    return new DemList_wtrs_bat_num(obj);
            //if (obj.listFor.EqualsIgnoreSpace("wtrs_processor"))
            //    return new DemList_wtrs_processor(obj);
            //if (obj.listFor.EqualsIgnoreSpace("wtrs_prod_cd"))
            //    return new DemList_wtrs_prod_cd(obj);
            //#endregion

            //#region Inventory Update
            //if (obj.listFor.EqualsIgnoreSpace("invadj_bat_num"))
            //    return new DemList_invadj_bat_num(obj);
            //if (obj.listFor.EqualsIgnoreSpace("invadj_processor"))
            //    return new DemList_invadj_processor(obj);
            //if (obj.listFor.EqualsIgnoreSpace("invadj_prod_cd"))
            //    return new DemList_invadj_prod_cd(obj);
            //if (obj.listFor.EqualsIgnoreSpace("invadj_whs_num"))
            //    return new DemList_invadj_whs_num(obj);
            #endregion

            #region customer
            if (payload.Name.EqualsIgnoreSpace("customer_area"))
                return new customer_area(dbFactory);

            if (payload.Name.EqualsIgnoreSpace("customer_customerCode"))
                return new customer_customerCode(dbFactory);

            if (payload.Name.EqualsIgnoreSpace("customer_classCode"))
                return new customer_classCode(dbFactory);

            if (payload.Name.EqualsIgnoreSpace("customer_customerCode"))
                return new customer_customerCode(dbFactory);

            if (payload.Name.EqualsIgnoreSpace("customer_customerName"))
                return new customer_customerName(dbFactory);

            if (payload.Name.EqualsIgnoreSpace("customer_departmentCode"))
                return new customer_departmentCode(dbFactory);

            if (payload.Name.EqualsIgnoreSpace("customer_districtn"))
                return new customer_districtn(dbFactory);

            if (payload.Name.EqualsIgnoreSpace("customer_divisionCode"))
                return new customer_divisionCode(dbFactory);

            if (payload.Name.EqualsIgnoreSpace("customer_email"))
                return new customer_email(dbFactory);

            if (payload.Name.EqualsIgnoreSpace("customer_phone1"))
                return new customer_phone1(dbFactory);

            if (payload.Name.EqualsIgnoreSpace("customer_region"))
                return new customer_region(dbFactory);

            if (payload.Name.EqualsIgnoreSpace("customer_sourceCode"))
                return new customer_sourceCode(dbFactory);

            if (payload.Name.EqualsIgnoreSpace("customer_zone"))
                return new customer_zone(dbFactory);
   
 
            #endregion

            #region S/O
            if (payload.Name.EqualsIgnoreSpace("so_orderNumber"))
                return new so_orderNumber(dbFactory);
            if (payload.Name.EqualsIgnoreSpace("so_customerCode"))
                return new so_customerCode(dbFactory);
            if (payload.Name.EqualsIgnoreSpace("so_customerName"))
                return new so_customerName(dbFactory);
            if (payload.Name.EqualsIgnoreSpace("so_terms"))
                return new so_terms(dbFactory);

            if (payload.Name.EqualsIgnoreSpace("so_shippingCarrier"))
                return new so_shippingCarrier(dbFactory);
            if (payload.Name.EqualsIgnoreSpace("so_shippingClass"))
                return new so_shippingClass(dbFactory);
            if (payload.Name.EqualsIgnoreSpace("so_centralOrderNum"))
                return new so_centralOrderNum(dbFactory);
            if (payload.Name.EqualsIgnoreSpace("so_channelOrderID"))
                return new so_channelOrderID(dbFactory);
            if (payload.Name.EqualsIgnoreSpace("so_warehouseCode"))
                return new so_warehouseCode(dbFactory);
            if (payload.Name.EqualsIgnoreSpace("so_refNum"))
                return new so_refNum(dbFactory);
            if (payload.Name.EqualsIgnoreSpace("so_customerPoNum"))
                return new so_customerPoNum(dbFactory);
            //if (obj.listFor.EqualsIgnoreSpace("so_ord_num"))
            //    return new DemList_so_ord_num(obj);
            //if (obj.listFor.EqualsIgnoreSpace("so_cus_id"))
            //    return new DemList_so_cus_id(obj);
            //if (obj.listFor.EqualsIgnoreSpace("so_cus_nm"))
            //    return new DemList_so_cus_nm(obj);
            //if (obj.listFor.EqualsIgnoreSpace("so_cust_po"))
            //    return new DemList_so_cust_po(obj);
            //if (obj.listFor.EqualsIgnoreSpace("so_ref_num"))
            //    return new DemList_so_ref_num(obj);
            //if (obj.listFor.EqualsIgnoreSpace("so_prod_cd"))
            //    return new DemList_so_prod_cd(obj);
            //if (obj.listFor.EqualsIgnoreSpace("so_whs_num"))
            //    return new DemList_so_whs_num(obj);
            //if (obj.listFor.EqualsIgnoreSpace("so_ship_desc"))
            //    return new DemList_so_ship_desc(obj);
            //if (obj.listFor.EqualsIgnoreSpace("so_ship_carriercode"))
            //    return new DemList_so_ship_carriercode(obj);
            //if (obj.listFor.EqualsIgnoreSpace("so_ord_type"))
            //    return new DemList_so_ord_type(obj);
            //if (obj.listFor.EqualsIgnoreSpace("so_sales_num"))
            //    return new DemList_so_sales_num(obj);
            //if (obj.listFor.EqualsIgnoreSpace("so_str_num"))
            //    return new DemList_so_str_num(obj);

            #endregion
            #region invoice
            if (payload.Name.EqualsIgnoreSpace("invoice_invoiceNumber"))
                return new invoice_invoiceNumber(dbFactory);
            if (payload.Name.EqualsIgnoreSpace("invoice_qboDocNumber"))
                return new invoice_qboDocNumber(dbFactory);
            if (payload.Name.EqualsIgnoreSpace("invoice_orderNumber"))
                return new invoice_orderNumber(dbFactory);
            if (payload.Name.EqualsIgnoreSpace("invoice_customerCode"))
                return new invoice_customerCode(dbFactory);
            if (payload.Name.EqualsIgnoreSpace("invoice_customerName"))
                return new invoice_customerName(dbFactory);
            if (payload.Name.EqualsIgnoreSpace("invoice_terms"))
                return new invoice_terms(dbFactory);

            if (payload.Name.EqualsIgnoreSpace("invoice_shippingCarrier"))
                return new invoice_shippingCarrier(dbFactory);
            if (payload.Name.EqualsIgnoreSpace("invoice_shippingClass"))
                return new invoice_shippingClass(dbFactory);
            if (payload.Name.EqualsIgnoreSpace("invoice_centralOrderNum"))
                return new invoice_centralOrderNum(dbFactory);
            if (payload.Name.EqualsIgnoreSpace("invoice_channelOrderID"))
                return new invoice_channelOrderID(dbFactory);
            if (payload.Name.EqualsIgnoreSpace("invoice_warehouseCode"))
                return new invoice_warehouseCode(dbFactory);
            if (payload.Name.EqualsIgnoreSpace("invoice_refNum"))
                return new invoice_refNum(dbFactory);
            if (payload.Name.EqualsIgnoreSpace("invoice_customerPoNum"))
                return new invoice_customerPoNum(dbFactory);

            //if (obj.listFor.EqualsIgnoreSpace("ins_cus_id"))
            //    return new DemList_ins_cus_id(obj);
            //if (obj.listFor.EqualsIgnoreSpace("ins_cus_nm"))
            //    return new DemList_ins_cus_nm(obj);
            //if (obj.listFor.EqualsIgnoreSpace("ins_cust_po"))
            //    return new DemList_ins_cust_po(obj);
            //if (obj.listFor.EqualsIgnoreSpace("ins_ref_num"))
            //    return new DemList_ins_ref_num(obj);
            //if (obj.listFor.EqualsIgnoreSpace("ins_prod_cd"))
            //    return new DemList_ins_prod_cd(obj);
            //if (obj.listFor.EqualsIgnoreSpace("ins_ord_num"))
            //    return new DemList_ins_ord_num(obj);
            //if (obj.listFor.EqualsIgnoreSpace("ins_sales_num"))
            //    return new DemList_ins_sales_num(obj);
            //if (obj.listFor.EqualsIgnoreSpace("ins_whs_num"))
            //    return new DemList_ins_whs_num(obj);
            //if (obj.listFor.EqualsIgnoreSpace("ins_invs_type"))
            //    return new DemList_ins_invs_type(obj);
            //if (obj.listFor.EqualsIgnoreSpace("ins_str_num"))
            //    return new DemList_ins_str_num(obj);
            //if (obj.listFor.EqualsIgnoreSpace("ins_processor"))
            //    return new DemList_ins_processor(obj);
            #endregion


            #region PO
            if (payload.Name.EqualsIgnoreSpace("po_poNum"))
                return new po_poNum(dbFactory);

            if (payload.Name.EqualsIgnoreSpace("po_vendorName"))
                return new po_vendorName(dbFactory);

            if (payload.Name.EqualsIgnoreSpace("po_vendorCode"))
                return new po_vendorCode(dbFactory);

            if (payload.Name.EqualsIgnoreSpace("po_centralOrderNum"))
                return new po_centralOrderNum(dbFactory);

            if (payload.Name.EqualsIgnoreSpace("po_channelAccountNum"))
                return new po_channelAccountNum(dbFactory);

            if (payload.Name.EqualsIgnoreSpace("po_channelNum"))
                return new po_channelNum(dbFactory);

            if (payload.Name.EqualsIgnoreSpace("po_channelOrderID"))
                return new po_channelOrderID(dbFactory);

            if (payload.Name.EqualsIgnoreSpace("po_customerPoNum"))
                return new po_customerPoNum(dbFactory);

            if (payload.Name.EqualsIgnoreSpace("po_refNum"))
                return new po_refNum(dbFactory);


            #endregion



            #region  vender

            if (payload.Name.EqualsIgnoreSpace("vendor_area"))
                return new vendor_area(dbFactory);

            if (payload.Name.EqualsIgnoreSpace("vendor_businessType"))
                return new vendor_businessType(dbFactory);

            if (payload.Name.EqualsIgnoreSpace("vendor_classCode"))
                return new vendor_classCode(dbFactory);

            if (payload.Name.EqualsIgnoreSpace("vendor_departmentCode"))
                return new vendor_departmentCode(dbFactory);

            if (payload.Name.EqualsIgnoreSpace("vendor_email"))
                return new vendor_email(dbFactory);

            if (payload.Name.EqualsIgnoreSpace("vendor_phone1"))
                return new vendor_phone1(dbFactory);

            if (payload.Name.EqualsIgnoreSpace("vendor_vendorCode"))
                return new vendor_vendorCode(dbFactory);

            if (payload.Name.EqualsIgnoreSpace("vendor_vendorName"))
                return new vendor_vendorName(dbFactory);

            if (payload.Name.EqualsIgnoreSpace("vendor_vendorStatus"))
                return new vendor_vendorStatus(dbFactory);

            if (payload.Name.EqualsIgnoreSpace("vendor_vendorType"))
                return new vendor_vendorType(dbFactory);


            #endregion

            #region poreceive

            if (payload.Name.EqualsIgnoreSpace("poreceive_poNum"))
                return new poreceive_poNum(dbFactory);

            if (payload.Name.EqualsIgnoreSpace("poreceive_transNum"))
                return new poreceive_transNum(dbFactory);

            if (payload.Name.EqualsIgnoreSpace("poreceive_transStatus"))
                return new poreceive_transStatus(dbFactory);

            if (payload.Name.EqualsIgnoreSpace("poreceive_transType"))
                return new poreceive_transType(dbFactory);

            if (payload.Name.EqualsIgnoreSpace("poreceive_vendorCode"))
                return new poreceive_vendorCode(dbFactory);

            if (payload.Name.EqualsIgnoreSpace("poreceive_vendorInvoiceNum"))
                return new poreceive_vendorInvoiceNum(dbFactory);

            if (payload.Name.EqualsIgnoreSpace("poreceive_vendorName"))
                return new poreceive_vendorName(dbFactory);

          
 

            #endregion


            return null;
        }
    }
}
