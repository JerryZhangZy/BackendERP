﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DigitBridge.Base.Common;
using DigitBridge.Base.Utility;
using DigitBridge.CommerceCentral.ERPDb;
using DigitBridge.CommerceCentral.YoPoco;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using Hepler = DigitBridge.CommerceCentral.ERPDb.SalesOrderHeaderHelper;
using InfoHepler = DigitBridge.CommerceCentral.ERPDb.SalesOrderHeaderInfoHelper;

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
            //if (obj.listFor.EqualsIgnoreSpace("cus_cus_id"))
            //    return new DemList_cus_cus_id(obj);
            //if (obj.listFor.EqualsIgnoreSpace("cus_cus_nm"))
            //    return new DemList_cus_cus_nm(obj);
            //if (obj.listFor.EqualsIgnoreSpace("cus_phone"))
            //    return new DemList_cus_phone(obj);
            //if (obj.listFor.EqualsIgnoreSpace("cus_email_adr"))
            //    return new DemList_cus_email_adr(obj);
            //if (obj.listFor.EqualsIgnoreSpace("cus_city"))
            //    return new DemList_cus_city(obj);
            //if (obj.listFor.EqualsIgnoreSpace("cus_state"))
            //    return new DemList_cus_state(obj);
            //if (obj.listFor.EqualsIgnoreSpace("cus_sales_num"))
            //    return new DemList_cus_sales_num(obj);
            //if (obj.listFor.EqualsIgnoreSpace("cus_dept_num"))
            //    return new DemList_cus_dept_num(obj);
            //if (obj.listFor.EqualsIgnoreSpace("cus_terr_cd"))
            //    return new DemList_cus_terr_cd(obj);
            //if (obj.listFor.EqualsIgnoreSpace("cus_cus_sur"))
            //    return new DemList_cus_cus_sur(obj);
            //if (obj.listFor.EqualsIgnoreSpace("cus_area_cd"))
            //    return new DemList_cus_area_cd(obj);



            #endregion

            #region S/O
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
            if (payload.Name.EqualsIgnoreSpace("invoice_customerCode"))
                return new invoice_customerCode(dbFactory);

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


            return null;
        }
    }
}