using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DigitBridge.Base.Common;
using DigitBridge.Base.Utility;
using DigitBridge.CommerceCentral.ERPDb;
using DigitBridge.CommerceCentral.YoPoco;
using Microsoft.Data.SqlClient;
using InfoHelper = DigitBridge.CommerceCentral.ERPDb.OrderShipmentPackageHelper;

namespace DigitBridge.CommerceCentral.ERPMdl
{
    public class OrderShipmentList : SqlQueryBuilder<OrderShipmentQuery>
    {
        public OrderShipmentList(IDataBaseFactory dbFactory) : base(dbFactory)
        {
        }
        public OrderShipmentList(IDataBaseFactory dbFactory, OrderShipmentQuery queryObject)
            : base(dbFactory, queryObject)
        {
        }

        #region override methods

        protected override string GetSQL_select()
        {
            this.SQL_Select = $@"
SELECT 
{OrderShipmentHeaderHelper.OrderShipmentNum()}, 
{OrderShipmentHeaderHelper.OrderShipmentUuid()},  
{OrderShipmentHeaderHelper.DatabaseNum()}, 
{OrderShipmentHeaderHelper.MasterAccountNum()}, 
{OrderShipmentHeaderHelper.ProfileNum()}, 
{OrderShipmentHeaderHelper.ChannelNum()}, 
{OrderShipmentHeaderHelper.ChannelAccountNum()}, 
chanel.ChannelName,
channelAccount.ChannelAccountName,
{OrderShipmentHeaderHelper.OrderDCAssignmentNum()}, 
{OrderShipmentHeaderHelper.DistributionCenterNum()}, 
{OrderShipmentHeaderHelper.CentralOrderNum()}, 
{OrderShipmentHeaderHelper.ChannelOrderID()},
{OrderShipmentHeaderHelper.ShipmentID()},
{OrderShipmentHeaderHelper.ShipmentType()}, 
COALESCE(stt.text, '') ShipmentTypeText, 
{OrderShipmentHeaderHelper.ShipmentStatus()}, 
COALESCE(sst.text, '') ShipmentStatusText,  
{OrderShipmentHeaderHelper.ProcessStatus()}, 
COALESCE(pst.text, '') ProcessStatusText,  
{OrderShipmentHeaderHelper.ShipmentDateUtc()}, 
{OrderShipmentHeaderHelper.ShippingCarrier()}, 
{OrderShipmentHeaderHelper.ShippingClass()}, 
{OrderShipmentHeaderHelper.MainTrackingNumber()},  
{OrderShipmentHeaderHelper.MainReturnTrackingNumber()},  
{OrderShipmentHeaderHelper.TotalPackages()}, 
{OrderShipmentHeaderHelper.TotalShippedQty()}, 
{OrderShipmentHeaderHelper.TotalCanceledQty()},  
{OrderShipmentHeaderHelper.TotalWeight()},
{OrderShipmentHeaderHelper.TotalVolume()},
{OrderShipmentHeaderHelper.WeightUnit()}, 
{OrderShipmentHeaderHelper.LengthUnit()},
{OrderShipmentHeaderHelper.VolumeUnit()}

";
            return this.SQL_Select;
        }

        protected override string GetSQL_from()
        {
            var masterAccountNum = $"{OrderShipmentHeaderHelper.TableAllies}.MasterAccountNum";
            var profileNum = $"{OrderShipmentHeaderHelper.TableAllies}.ProfileNum";
            var channelNum = $"{OrderShipmentHeaderHelper.TableAllies}.ChannelNum";
            var channelAccountNum = $"{OrderShipmentHeaderHelper.TableAllies}.ChannelAccountNum";

            this.SQL_From = $@"
 FROM {OrderShipmentHeaderHelper.TableName} {OrderShipmentHeaderHelper.TableAllies}  
 LEFT JOIN @ShipmentStatusText sst ON ({OrderShipmentHeaderHelper.TableAllies}.ShipmentStatus = sst.num)
 LEFT JOIN @ShipmentTypeText stt ON ({OrderShipmentHeaderHelper.TableAllies}.ShipmentType = stt.num)
 LEFT JOIN @ProcessStatusText pst ON ({OrderShipmentHeaderHelper.TableAllies}.ProcessStatus = pst.num)
 {SqlStringHelper.Join_Setting_Channel(masterAccountNum, profileNum, channelNum)}
 {SqlStringHelper.Join_Setting_ChannelAccount(masterAccountNum, profileNum, channelNum, channelAccountNum)}
 
";
            return this.SQL_From;
        }

        public override SqlParameter[] GetSqlParameters()
        {
            var paramList = base.GetSqlParameters().ToList();
            paramList.Add("@ShipmentStatusText".ToEnumParameter<OrderShipmentStatusEnum>());
            paramList.Add("@ShipmentTypeText".ToEnumParameter<OrderShipmentTypeEnum>());
            paramList.Add("@ProcessStatusText".ToEnumParameter<OrderShipmentProcessStatusEnum>());
            return paramList.ToArray();

        }


        #endregion override methods

        public virtual void GetOrderShipmentList(OrderShipmentPayload payload)
        {
            if (payload == null)
                payload = new OrderShipmentPayload();

            this.LoadRequestParameter(payload);
            StringBuilder sb = new StringBuilder();
            try
            {
                payload.OrderShipmentListCount = Count();
                payload.Success = ExcuteJson(sb);
                if (payload.Success)
                    payload.OrderShipmentList = sb;
            }
            catch (Exception ex)
            {
                payload.OrderShipmentListCount = 0;
                payload.OrderShipmentList = null;
                AddError(ex.ObjectToString());
                payload.Messages = this.Messages;
            }
        }

        public virtual async Task GetOrderShipmentListAsync(OrderShipmentPayload payload)
        {
            if (payload == null)
                payload = new OrderShipmentPayload();

            this.LoadRequestParameter(payload);
            StringBuilder sb = new StringBuilder();
            try
            {
                payload.OrderShipmentListCount = await CountAsync();
                payload.Success = await ExcuteJsonAsync(sb);
                if (payload.Success)
                    payload.OrderShipmentList = sb;
            }
            catch (Exception ex)
            {
                payload.OrderShipmentListCount = 0;
                payload.OrderShipmentList = null;
                AddError(ex.ObjectToString());
                payload.Messages = this.Messages;
            }
        }

        public virtual async Task<IList<long>> GetRowNumListAsync(OrderShipmentPayload payload)
        {
            if (payload == null)
                payload = new OrderShipmentPayload();

            this.LoadRequestParameter(payload);
            var rowNumList = new List<long>();

            var sql = $@"
SELECT distinct {OrderShipmentHeaderHelper.TableAllies}.OrderShipmentNum 
{GetSQL_from()} 
{GetSQL_where()}
ORDER BY  {OrderShipmentHeaderHelper.TableAllies}.OrderShipmentNum
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

        public virtual IList<long> GetRowNumList(OrderShipmentPayload payload)
        {
            if (payload == null)
                payload = new OrderShipmentPayload();

            this.LoadRequestParameter(payload);
            var rowNumList = new List<long>();
            var sql = $@"
SELECT distinct {OrderShipmentHeaderHelper.TableAllies}.OrderShipmentNum 
{GetSQL_from()} 
{GetSQL_where()} 
ORDER BY  {OrderShipmentHeaderHelper.TableAllies}.OrderShipmentNum
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

    }
}
