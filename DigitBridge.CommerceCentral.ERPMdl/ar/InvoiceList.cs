using System;
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
using Helper = DigitBridge.CommerceCentral.ERPDb.InvoiceHeaderHelper;
using InfoHelper = DigitBridge.CommerceCentral.ERPDb.InvoiceHeaderInfoHelper;

namespace DigitBridge.CommerceCentral.ERPMdl
{
    public class InvoiceList : SqlQueryBuilder<InvoiceQuery>
    {
        public InvoiceList(IDataBaseFactory dbFactory) : base(dbFactory)
        {
        }
        public InvoiceList(IDataBaseFactory dbFactory, InvoiceQuery queryObject)
            : base(dbFactory, queryObject)
        {
        }

        #region override methods

        protected override string GetSQL_select()
        {
            this.SQL_Select = $@"
SELECT 
{Helper.RowNum()}, 
{Helper.InvoiceUuid()}, 
{Helper.InvoiceNumber()}, 
{Helper.QboDocNumber()}, 
{Helper.SalesOrderUuid()}, 
{Helper.OrderNumber()}, 
{Helper.InvoiceType()}, 
COALESCE(itt.text, '') invoiceTypeText, 
{Helper.InvoiceStatus()}, 
COALESCE(ist.text, '') invoiceStatusText, 
{Helper.InvoiceDate()}, 
{Helper.DueDate()},   
{Helper.CustomerUuid()}, 
{Helper.CustomerCode()}, 
{Helper.CustomerName()}, 
{Helper.Terms()}, 
{Helper.TermsDays()}, 
{Helper.SubTotalAmount()},
{Helper.TotalAmount()},
{Helper.PaidAmount()},
{Helper.CreditAmount()},
{Helper.Balance()},
{Helper.InvoiceSourceCode()},

{InfoHelper.CentralFulfillmentNum()},
{InfoHelper.OrderShipmentNum()},
{InfoHelper.OrderShipmentUuid()},
{InfoHelper.ShippingCarrier()},
{InfoHelper.ShippingClass()},
{InfoHelper.DistributionCenterNum()},
{InfoHelper.CentralOrderNum()},
{InfoHelper.ChannelNum()}, 
{InfoHelper.ChannelAccountNum()},
chanel.ChannelName,
channelAccount.ChannelAccountName,
{InfoHelper.ChannelOrderID()},
{InfoHelper.WarehouseUuid()},
{InfoHelper.WarehouseCode()},
{InfoHelper.RefNum()},
{InfoHelper.CustomerPoNum()},
{InfoHelper.ShipToName()},
{InfoHelper.ShipToAttention()},
{InfoHelper.ShipToAddressLine1()},
{InfoHelper.ShipToAddressLine2()},
{InfoHelper.ShipToCity()},
{InfoHelper.ShipToState()},
{InfoHelper.ShipToPostalCode()},
{InfoHelper.ShipToCountry()},
{InfoHelper.ShipToEmail()},
{InfoHelper.BillToEmail()}
";
            return this.SQL_Select;
        }

        protected override string GetSQL_from()
        {
            var masterAccountNum = $"{Helper.TableAllies}.MasterAccountNum";
            var profileNum = $"{Helper.TableAllies}.ProfileNum";
            var channelNum = $"{InfoHelper.TableAllies}.ChannelNum";
            var channelAccountNum = $"{InfoHelper.TableAllies}.ChannelAccountNum";

            this.SQL_From = $@"
 FROM {Helper.TableName} {Helper.TableAllies} 
 LEFT JOIN {InfoHelper.TableName} {InfoHelper.TableAllies} ON ({Helper.TableAllies}.InvoiceUuid = {InfoHelper.TableAllies}.InvoiceUuid)
 {SqlStringHelper.Join_Setting_Channel(masterAccountNum, profileNum, channelNum)} 
 {SqlStringHelper.Join_Setting_ChannelAccount(masterAccountNum, profileNum, channelNum, channelAccountNum)} 
 LEFT JOIN @InvoiceStatusEnum ist ON ({Helper.TableAllies}.InvoiceStatus = ist.num)
 LEFT JOIN @InvoiceTypeEnum itt ON ({Helper.TableAllies}.InvoiceType = itt.num)
";
            return this.SQL_From;
        }

        public override SqlParameter[] GetSqlParameters()
        {
            var paramList = base.GetSqlParameters().ToList();
            paramList.Add("@InvoiceStatusEnum".ToEnumParameter<InvoiceStatusEnum>());
            paramList.Add("@InvoiceTypeEnum".ToEnumParameter<InvoiceType>());

            return paramList.ToArray();

        }

        #endregion override methods

        public virtual void GetInvoiceList(InvoicePayload payload)
        {
            if (payload == null)
                payload = new InvoicePayload();

            this.LoadRequestParameter(payload);
            StringBuilder sb = new StringBuilder();
            try
            {
                payload.InvoiceListCount = Count();
                payload.Success = ExcuteJson(sb);
                if (payload.Success)
                    payload.InvoiceList = sb;
            }
            catch (Exception ex)
            {
                payload.Success = false;
                payload.InvoiceListCount = 0;
                AddError(ex.ObjectToString());
                payload.Messages = this.Messages;
            }
        }

        public virtual async Task GetInvoiceListAsync(InvoicePayload payload)
        {
            if (payload == null)
                payload = new InvoicePayload();

            this.LoadRequestParameter(payload);
            StringBuilder sb = new StringBuilder();
            try
            {
                payload.InvoiceListCount = await CountAsync();
                payload.Success = await ExcuteJsonAsync(sb);
                if (payload.Success)
                    payload.InvoiceList = sb;
            }
            catch (Exception ex)
            {
                payload.Success = false;
                payload.InvoiceListCount = 0;
                AddError(ex.ObjectToString());
                payload.Messages = this.Messages;
            }
        }
        public virtual async Task<IList<long>> GetRowNumListAsync(InvoicePayload payload)
        {
            if (payload == null)
                payload = new InvoicePayload();

            this.LoadRequestParameter(payload);
            var rowNumList = new List<long>();

            var sql = $@"
SELECT distinct {Helper.TableAllies}.RowNum 
{GetSQL_from()} 
{GetSQL_where()} 
ORDER BY  {Helper.TableAllies}.RowNum
OFFSET {payload.FixedSkip} ROWS FETCH NEXT {payload.FixedTop} ROWS ONLY
";
            try
            {
                using (var trs = new ScopedTransaction(dbFactory))
                {
                    rowNumList = await SqlQuery.ExecuteAsync(
                    sql,
                    (long rowNum) => rowNum,
                    GetSqlParameters().ToArray()
                );
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return rowNumList;
        }

        public virtual IList<long> GetRowNumList(InvoicePayload payload)
        {
            if (payload == null)
                payload = new InvoicePayload();

            this.LoadRequestParameter(payload);
            var rowNumList = new List<long>();
            var sql = $@"
SELECT distinct {Helper.TableAllies}.RowNum 
{GetSQL_from()} 
{GetSQL_where()}
ORDER BY  {Helper.TableAllies}.RowNum
OFFSET {payload.FixedSkip} ROWS FETCH NEXT {payload.FixedTop} ROWS ONLY
";
            try
            {
                using (var trs = new ScopedTransaction(dbFactory))
                {
                    rowNumList = SqlQuery.Execute(
                    sql,
                    (long rowNum) => rowNum,
                    GetSqlParameters().ToArray()
                );
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return rowNumList;
        }

        /// <summary>
        /// Get row num list by  numbers
        /// </summary>
        /// <param name="invoiceNumbers"></param>
        /// <returns></returns>
        public virtual async Task<List<long>> GetRowNumListAsync(IList<string> invoiceNumbers, int masterAccountNum, int profileNum)
        {
            var sql = @"
SELECT RowNum FROM InvoiceHeader ins
WHERE MasterAccountNum = @masterAccountNum
AND ProfileNum = @profileNum
AND @invoiceNumbers.exist('/parameters/value[text()=sql:column(''ins.InvoiceNumber'')]')=1";
            var paras = new SqlParameter[]
            {
                new SqlParameter("@masterAccountNum",masterAccountNum),
                new SqlParameter("@profileNum",profileNum),
                new SqlParameter("@invoiceNumbers",invoiceNumbers.ListToXml()){ DbType=DbType.Xml}
        };
            using (var trs = new ScopedTransaction(dbFactory))
            {
                return await SqlQuery.ExecuteAsync(
                sql,
                (long rowNum) => rowNum,
                paras
            );
            }
        }

    }
}
