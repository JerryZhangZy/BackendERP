1, Add reference to project DigitBridge.CommerceCentral.ERPApiSDK 
   Or install package 

2, Copy setting to your config file

3 Simple invoke api by sdk
    var client = new WMSSalesOrderClient();
    var success = await client.GetSalesOrdersOpenListAsync(MasterAccountNum, ProfileNum);
    var openOrderList= JsonConvert.DeserializeObject<List<AddOrderHeaderModel>>(client.ResopneData.SalesOrderOpenList.ToString());