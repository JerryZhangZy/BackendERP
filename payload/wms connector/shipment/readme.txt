
Call api via Integration SDK

1, Add reference to project DigitBridge.CommerceCentral.ERPApiSDK 
   Or install package 

2, Copy setting from local.settings.json to your config file
 
3, Simple invoke Api
   var client = new WMSSalesOrderClient();
   var success = await client.GetSalesOrdersOpenListAsync(MasterAccountNum, ProfileNum);
