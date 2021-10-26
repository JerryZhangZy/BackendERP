
#region License

/*
 * Copyright 2010-2011 Master System Inc.
 */

#endregion

#region Imports

using System;
using NHibernate;

using Mastersystem.Emaster.Data;
using Mastersystem.Emaster.Dao;
using Mastersystem.Emaster.Dao.Ado;

#endregion

namespace Mastersystem.Emaster.Service
{
    #region Interface
    public interface IDemListService : IServiceBase
    {
        SelectListPayload GetDemList(SelectListPayload json);
        DemListJson GetDemList(DemListJson json);
        IDemListAdo GetDemListAdoClass(SelectListPayload json);
    }
    #endregion Interface

    #region Class

    public class SelectListService : ServiceBase<SelectListService>, IDemListService
    {
        #region Fields
        public static String SERVICE_TAG = "Data Entry List Service";
        //private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        //private static readonly log4net.ILog log = log4net.LogManager.GetLogger(typeof(DemListService));
        //protected IOmsSecurityBase _security = new SoSecurity();

        protected ICtrfile2Dao _Ctrfile2Dao;

        #endregion

        #region Constructor

        // for constructor DI
        public SelectListService(ICtrfile2Dao Ctrfile2Dao)
        {
            _Ctrfile2Dao = Ctrfile2Dao;
        }

        #endregion

        #region Properties
        public DemListJson JsonData { get; set; } = new DemListJson();
        #endregion

        #region Override Methods

        public override ISession GetCurrentSession()
        {
            return _Ctrfile2Dao.CurrentSession;
        }

        #endregion

        #region Methods
        public virtual DemListJson GetDemList(DemListJson json)
        {
            if (json == null) 
                json = new DemListJson();
            JsonData = new DemListJson();
            foreach (SelectListPayload item in json.DemList)
            {
                if (!item.With_listFor) continue;
                var ado = GetDemListAdoClass(item);
                JsonData.AddDemList(ado?.SetListFilter(item).GetDemListClass(item));
            }
            return JsonData;
        }
        public virtual SelectListPayload GetDemList(SelectListPayload obj)
        {
            if (obj == null)
                obj = new SelectListPayload();
            var ado = GetDemListAdoClass(obj);
            return ado?.SetListFilter(obj).GetDemListClass(obj);
        }
        public virtual IDemListAdo GetDemListAdoClass(SelectListPayload obj)
        {
            if (obj == null || string.IsNullOrWhiteSpace(obj.listFor))
                return null;

            #region oms global 
            if (obj.listFor.EqualsIgnoreSpace("oms_ar_term"))
                return new DemList_oms_ar_term(obj);
            if (obj.listFor.EqualsIgnoreSpace("oms_src_file"))
                return new DemList_oms_src_file(obj);
            if (obj.listFor.EqualsIgnoreSpace("oms_div_file"))
                return new DemList_oms_div_file(obj);
            if (obj.listFor.EqualsIgnoreSpace("oms_det_file"))
                return new DemList_oms_det_file(obj);
            if (obj.listFor.EqualsIgnoreSpace("oms_ssc_cdf"))
                return new DemList_oms_ssc_cdf(obj);
            if (obj.listFor.EqualsIgnoreSpace("oms_bnk_file"))
                return new DemList_oms_bnk_file(obj).SetWithZero(true);
            if (obj.listFor.EqualsIgnoreSpace("oms_ship_desc"))
                return new DemList_oms_ship_desc(obj);
            if (obj.listFor.EqualsIgnoreSpace("oms_sls_pro"))
                return new DemList_oms_sls_pro(obj);
            if (obj.listFor.EqualsIgnoreSpace("oms_ar_paidby"))
                return new DemList_oms_ar_paidby(obj);
            if (obj.listFor.EqualsIgnoreSpace("oms_cos_paidby"))
                return new DemList_oms_cos_paidby(obj);
            if (obj.listFor.EqualsIgnoreSpace("oms_prs_file"))
                return new DemList_oms_prs_file(obj);
            if (obj.listFor.EqualsIgnoreSpace("oms_typefile"))
                return new DemList_oms_typefile(obj);
            if (obj.listFor.EqualsIgnoreSpace("oms_deptfile"))
                return new DemList_oms_deptfile(obj);
            if (obj.listFor.EqualsIgnoreSpace("oms_cus_grp"))
                return new DemList_oms_cus_grp(obj);
            if (obj.listFor.EqualsIgnoreSpace("oms_cus_subgrp"))
                return new DemList_oms_cus_subgrp(obj);
            if (obj.listFor.EqualsIgnoreSpace("oms_clssfile"))
                return new DemList_oms_clssfile(obj);
            if (obj.listFor.EqualsIgnoreSpace("oms_clrfile"))
                return new DemList_oms_clrfile(obj);
            if (obj.listFor.EqualsIgnoreSpace("oms_size_cdf"))
                return new DemList_oms_size_cdf(obj);
            if (obj.listFor.EqualsIgnoreSpace("oms_brand_nm"))
                return new DemList_oms_brand_nm(obj);
            if (obj.listFor.EqualsIgnoreSpace("oms_oem_cdf"))
                return new DemList_oms_oem_cdf(obj);
            if (obj.listFor.EqualsIgnoreSpace("oms_measfile"))
                return new DemList_oms_measfile(obj);
            if (obj.listFor.EqualsIgnoreSpace("oms_modfile_maker"))
                return new DemList_oms_modfile_maker(obj);
            if (obj.listFor.EqualsIgnoreSpace("oms_modfile_model"))
                return new DemList_oms_modfile_model(obj);
            if (obj.listFor.EqualsIgnoreSpace("oms_clsfile_group"))
                return new DemList_oms_clsfile_group(obj);
            if (obj.listFor.EqualsIgnoreSpace("oms_clsfile_subgroup"))
                return new DemList_oms_clsfile_subgroup(obj);


            #endregion

            #region inventory
            if (obj.listFor.EqualsIgnoreSpace("inv_prod_cd"))
                return new DemList_inv_prod_cd(obj);
            if (obj.listFor.EqualsIgnoreSpace("inv_descrip"))
                return new DemList_inv_descrip(obj);
            if (obj.listFor.EqualsIgnoreSpace("inv_dept_num"))
                return new DemList_inv_dept_num(obj);
            if (obj.listFor.EqualsIgnoreSpace("inv_cat_cd"))
                return new DemList_inv_cat_cd(obj);
            if (obj.listFor.EqualsIgnoreSpace("inv_class_cd"))
                return new DemList_inv_class_cd(obj);
            if (obj.listFor.EqualsIgnoreSpace("inv_unit_color"))
                return new DemList_inv_unit_color(obj);
            if (obj.listFor.EqualsIgnoreSpace("inv_unit_size"))
                return new DemList_inv_unit_size(obj);
            if (obj.listFor.EqualsIgnoreSpace("inv_brd_nm"))
                return new DemList_inv_brd_nm(obj);
            if (obj.listFor.EqualsIgnoreSpace("inv_maker"))
                return new DemList_inv_maker(obj);
            if (obj.listFor.EqualsIgnoreSpace("inv_model"))
                return new DemList_inv_model(obj);
            if (obj.listFor.EqualsIgnoreSpace("inv_prod_yr"))
                return new DemList_inv_prod_yr(obj);
            if (obj.listFor.EqualsIgnoreSpace("inv_group_cd"))
                return new DemList_inv_group_cd(obj);
            if (obj.listFor.EqualsIgnoreSpace("inv_subgroup_cd"))
                return new DemList_inv_subgroup_cd(obj);

            if (obj.listFor.EqualsIgnoreSpace("inv_oem"))
                return new DemList_inv_oem(obj);
            if (obj.listFor.EqualsIgnoreSpace("inv_alt_cd"))
                return new DemList_inv_alt_cd(obj);
            if (obj.listFor.EqualsIgnoreSpace("inv_unit_nm"))
                return new DemList_inv_unit_nm(obj);
            if (obj.listFor.EqualsIgnoreSpace("inv_category"))
                return new DemList_inv_category(obj);

            if (obj.listFor.EqualsIgnoreSpace("inv_prod_clr"))
                return new DemList_inv_prod_clr(obj);
            if (obj.listFor.EqualsIgnoreSpace("inv_run_cd"))
                return new DemList_inv_run_cd(obj);
            if (obj.listFor.EqualsIgnoreSpace("inv_size_name"))
                return new DemList_inv_size_name(obj);

            if (obj.listFor.EqualsIgnoreSpace("inv_whs_num"))
                return new DemList_inv_whs_num(obj);

            #endregion

            #region Warehouse Transfer
            if (obj.listFor.EqualsIgnoreSpace("wtrs_bat_num"))
                return new DemList_wtrs_bat_num(obj);
            if (obj.listFor.EqualsIgnoreSpace("wtrs_processor"))
                return new DemList_wtrs_processor(obj);
            if (obj.listFor.EqualsIgnoreSpace("wtrs_prod_cd"))
                return new DemList_wtrs_prod_cd(obj);
            #endregion

            #region Inventory Update
            if (obj.listFor.EqualsIgnoreSpace("invadj_bat_num"))
                return new DemList_invadj_bat_num(obj);
            if (obj.listFor.EqualsIgnoreSpace("invadj_processor"))
                return new DemList_invadj_processor(obj);
            if (obj.listFor.EqualsIgnoreSpace("invadj_prod_cd"))
                return new DemList_invadj_prod_cd(obj);
            if (obj.listFor.EqualsIgnoreSpace("invadj_whs_num"))
                return new DemList_invadj_whs_num(obj);
            #endregion

            #region customer
            if (obj.listFor.EqualsIgnoreSpace("cus_cus_id"))
                return new DemList_cus_cus_id(obj);
            if (obj.listFor.EqualsIgnoreSpace("cus_cus_nm"))
                return new DemList_cus_cus_nm(obj);
            if (obj.listFor.EqualsIgnoreSpace("cus_phone"))
                return new DemList_cus_phone(obj);
            if (obj.listFor.EqualsIgnoreSpace("cus_email_adr"))
                return new DemList_cus_email_adr(obj);
            if (obj.listFor.EqualsIgnoreSpace("cus_city"))
                return new DemList_cus_city(obj);
            if (obj.listFor.EqualsIgnoreSpace("cus_state"))
                return new DemList_cus_state(obj);
            if (obj.listFor.EqualsIgnoreSpace("cus_sales_num"))
                return new DemList_cus_sales_num(obj);
            if (obj.listFor.EqualsIgnoreSpace("cus_dept_num"))
                return new DemList_cus_dept_num(obj);
            if (obj.listFor.EqualsIgnoreSpace("cus_terr_cd"))
                return new DemList_cus_terr_cd(obj);
            if (obj.listFor.EqualsIgnoreSpace("cus_cus_sur"))
                return new DemList_cus_cus_sur(obj);
            if (obj.listFor.EqualsIgnoreSpace("cus_area_cd"))
                return new DemList_cus_area_cd(obj);
            


            #endregion

            #region web customer
            if (obj.listFor.EqualsIgnoreSpace("wcs_email"))
                return new DemList_wcs_email(obj);
            if (obj.listFor.EqualsIgnoreSpace("wcs_cus_id"))
                return new DemList_wcs_cus_id(obj);
            if (obj.listFor.EqualsIgnoreSpace("wcs_cus_nm"))
                return new DemList_wcs_cus_nm(obj);
            if (obj.listFor.EqualsIgnoreSpace("wcs_cus_phone"))
                return new DemList_wcs_cus_phone(obj);
            if (obj.listFor.EqualsIgnoreSpace("wcs_cus_city"))
                return new DemList_wcs_cus_city(obj);
            if (obj.listFor.EqualsIgnoreSpace("wcs_cus_state"))
                return new DemList_wcs_cus_state(obj);
            if (obj.listFor.EqualsIgnoreSpace("wcs_cus_sales"))
                return new DemList_wcs_cus_sales(obj);
            #endregion

            #region S/O
            if (obj.listFor.EqualsIgnoreSpace("so_ord_num"))
                return new DemList_so_ord_num(obj);
            if (obj.listFor.EqualsIgnoreSpace("so_cus_id"))
                return new so_customerCode(obj);
            if (obj.listFor.EqualsIgnoreSpace("so_cus_nm"))
                return new DemList_so_cus_nm(obj);
            if (obj.listFor.EqualsIgnoreSpace("so_cust_po"))
                return new DemList_so_cust_po(obj);
            if (obj.listFor.EqualsIgnoreSpace("so_ref_num"))
                return new DemList_so_ref_num(obj);
            if (obj.listFor.EqualsIgnoreSpace("so_prod_cd"))
                return new DemList_so_prod_cd(obj);
            if (obj.listFor.EqualsIgnoreSpace("so_whs_num"))
                return new DemList_so_whs_num(obj);
            if (obj.listFor.EqualsIgnoreSpace("so_ship_desc"))
                return new DemList_so_ship_desc(obj);
            if (obj.listFor.EqualsIgnoreSpace("so_ship_carriercode"))
                return new DemList_so_ship_carriercode(obj);
            if (obj.listFor.EqualsIgnoreSpace("so_ord_type"))
                return new DemList_so_ord_type(obj);
            if (obj.listFor.EqualsIgnoreSpace("so_sales_num"))
                return new DemList_so_sales_num(obj);
            if (obj.listFor.EqualsIgnoreSpace("so_str_num"))
                return new DemList_so_str_num(obj);

            #endregion

            #region invoice
            if (obj.listFor.EqualsIgnoreSpace("ins_invs_num"))
                return new DemList_ins_invs_num(obj);
            if (obj.listFor.EqualsIgnoreSpace("ins_cus_id"))
                return new DemList_ins_cus_id(obj);
            if (obj.listFor.EqualsIgnoreSpace("ins_cus_nm"))
                return new DemList_ins_cus_nm(obj);
            if (obj.listFor.EqualsIgnoreSpace("ins_cust_po"))
                return new DemList_ins_cust_po(obj);
            if (obj.listFor.EqualsIgnoreSpace("ins_ref_num"))
                return new DemList_ins_ref_num(obj);
            if (obj.listFor.EqualsIgnoreSpace("ins_prod_cd"))
                return new DemList_ins_prod_cd(obj);
            if (obj.listFor.EqualsIgnoreSpace("ins_ord_num"))
                return new DemList_ins_ord_num(obj);
            if (obj.listFor.EqualsIgnoreSpace("ins_sales_num"))
                return new DemList_ins_sales_num(obj);
            if (obj.listFor.EqualsIgnoreSpace("ins_whs_num"))
                return new DemList_ins_whs_num(obj);
            if (obj.listFor.EqualsIgnoreSpace("ins_invs_type"))
                return new DemList_ins_invs_type(obj);
            if (obj.listFor.EqualsIgnoreSpace("ins_str_num"))
                return new DemList_ins_str_num(obj);
            if (obj.listFor.EqualsIgnoreSpace("ins_processor"))
                return new DemList_ins_processor(obj);
            #endregion

            #region shipping confirmation
            if (obj.listFor.EqualsIgnoreSpace("cnfm_pack_num"))
                return new DemList_cnfm_pack_num(obj);
            if (obj.listFor.EqualsIgnoreSpace("cnfm_cus_id"))
                return new DemList_cnfm_cus_id(obj);
            if (obj.listFor.EqualsIgnoreSpace("cnfm_invs_num"))
                return new DemList_cnfm_invs_num(obj);
            if (obj.listFor.EqualsIgnoreSpace("cnfm_processor"))
                return new DemList_cnfm_processor(obj);
            #endregion

            #region user
            if (obj.listFor.EqualsIgnoreSpace("user"))
                return new DemList_user(obj);
            #endregion

            #region OMS Api
            if (obj.listFor.EqualsIgnoreSpace("api_store_cd"))
                return new DemList_api_store_cd(obj);
            if (obj.listFor.EqualsIgnoreSpace("apilog_api_method"))
                return new DemList_apilog_api_method(obj);
            if (obj.listFor.EqualsIgnoreSpace("apilog_ref_num"))
                return new DemList_apilog_ref_num(obj);
            #endregion

            #region webevent
            if (obj.listFor.EqualsIgnoreSpace("webevent"))
                return new DemList_webevent(obj);
            #endregion

            #region WMS
            if (obj.listFor.EqualsIgnoreSpace("wms_whs_num"))
                return new DemList_wms_whs_num(obj);
            if (obj.listFor.EqualsIgnoreSpace("wms_bin_cd"))
                return new DemList_wms_bin_cd(obj);
            if (obj.listFor.EqualsIgnoreSpace("wms_wave_num"))
                return new DemList_wms_wave_num(obj);
            if (obj.listFor.EqualsIgnoreSpace("wms_wave_whs_num"))
                return new DemList_wms_wave_whs_num(obj);
            if (obj.listFor.EqualsIgnoreSpace("wms_wave_by"))
                return new DemList_wms_wave_by(obj);
            if (obj.listFor.EqualsIgnoreSpace("wms_wave_sku_cd"))
                return new DemList_wms_wave_sku_cd(obj);
            if (obj.listFor.EqualsIgnoreSpace("wms_wave_log_num"))
                return new DemList_wms_wave_log_num(obj);

            if (obj.listFor.EqualsIgnoreSpace("wms_binfile_whs_num"))
                return new DemList_wms_binfile_whs_num(obj);
            if (obj.listFor.EqualsIgnoreSpace("wms_binfile_bin_cd"))
                return new DemList_wms_binfile_bin_cd(obj);
            if (obj.listFor.EqualsIgnoreSpace("wms_binfile_sku_cd"))
                return new DemList_wms_binfile_sku_cd(obj);
            if (obj.listFor.EqualsIgnoreSpace("wms_binfile_plt_cd"))
                return new DemList_wms_binfile_plt_cd(obj);
            if (obj.listFor.EqualsIgnoreSpace("wms_binfile_bin_area"))
                return new DemList_wms_binfile_bin_area(obj);
            if (obj.listFor.EqualsIgnoreSpace("wms_binfile_bin_aisle"))
                return new DemList_wms_binfile_bin_aisle(obj);
            if (obj.listFor.EqualsIgnoreSpace("wms_binfile_bin_row"))
                return new DemList_wms_binfile_bin_row(obj);
            if (obj.listFor.EqualsIgnoreSpace("wms_binfile_bin_levle"))
                return new DemList_wms_binfile_bin_levle(obj);

            if (obj.listFor.EqualsIgnoreSpace("wms_binpro_whs_num"))
                return new DemList_wms_binpro_whs_num(obj);
            if (obj.listFor.EqualsIgnoreSpace("wms_binpro_bin_cd"))
                return new DemList_wms_binpro_bin_cd(obj);
            if (obj.listFor.EqualsIgnoreSpace("wms_binpro_type_cd"))
                return new DemList_wms_binpro_type_cd(obj);

            if (obj.listFor.EqualsIgnoreSpace("wms_bin_area"))
                return new DemList_wms_bin_area(obj);
            if (obj.listFor.EqualsIgnoreSpace("wms_bin_aisle"))
                return new DemList_wms_bin_aisle(obj);
            if (obj.listFor.EqualsIgnoreSpace("wms_bin_row"))
                return new DemList_wms_bin_row(obj);
            if (obj.listFor.EqualsIgnoreSpace("wms_bin_levle"))
                return new DemList_wms_bin_level(obj);

            #endregion

            return null;
        }
        #endregion
    }
    #endregion Class

}

