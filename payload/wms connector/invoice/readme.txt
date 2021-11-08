Call api via Integration SDK

1, Add reference to project DigitBridge.CommerceCentral.ERPApiSDK 
   Or install package 

2, Copy setting from local.settings.json to your config file

3, If no record in db, you can run srcipt to init unprocess invoices
   Script path: DigitBridge.CommerceCentral.ERPDatabase\sql\AddEventProcessFromInvocie.sql

4, Simple invoke Api
   var client = new CommerceCentralInvoiceClient(); 
   var success = await client.GetUnprocessedInvoicesAsync(MasterAccountNum, ProfileNum);

   