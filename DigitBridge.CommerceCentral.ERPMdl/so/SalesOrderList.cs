using System;
using System.Collections.Generic;
using System.Text;
using DigitBridge.Base.Utility;
using DigitBridge.CommerceCentral.ERPDb;
using DigitBridge.CommerceCentral.YoPoco;

namespace DigitBridge.CommerceCentral.ERPMdl
{
    public class SalesOrderList : SqlQueryBuilder<SalesOrderQuery>
    {
        public SalesOrderList() : base()
        {
        }
        public SalesOrderList(SalesOrderQuery queryObject)
            : base(queryObject)
        {
        }

        protected override string GetSQL_select()
        {
            this.SQL_Select = $@"
SELECT 
{SalesOrderHeaderHelper.RowNum()}, 
{SalesOrderHeaderHelper.DatabaseNum()}, 
{SalesOrderHeaderHelper.MasterAccountNum()}, 
{SalesOrderHeaderHelper.ProfileNum()}, 
{SalesOrderHeaderHelper.SalesOrderUuid()}, 
{SalesOrderHeaderHelper.OrderNumber()}, 
{SalesOrderHeaderHelper.OrderType()}, 
{SalesOrderHeaderHelper.OrderStatus()}, 
{SalesOrderHeaderHelper.OrderDate()}, 
{SalesOrderHeaderHelper.OrderTime()}, 
{SalesOrderHeaderHelper.CustomerUuid()}, 
{SalesOrderHeaderHelper.CustomerCode()}, 
{SalesOrderHeaderHelper.CustomerName()}, 
{SalesOrderHeaderHelper.Terms()}, 
{SalesOrderHeaderHelper.TermsDays()}, 
{SalesOrderHeaderHelper.TotalAmount()} 
";
            return this.SQL_Select;
        }

        protected override string GetSQL_from()
        {
            this.SQL_From = $@"
 FROM {SalesOrderHeaderHelper.TableName} {SalesOrderHeaderHelper.TableAllies} 
 LEFT JOIN {CustomerHelper.TableName} {CustomerHelper.TableAllies} ON ({CustomerHelper.TableAllies}.CustomerUuid = {SalesOrderHeaderHelper.TableAllies}.CustomerUuid)
";
            return this.SQL_From;
        }

    }
}
