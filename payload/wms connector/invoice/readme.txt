Call api via Integration SDK

1, Install latest package 
   Install-Package DigitBridge.CommerceCentral.ERPApiSDK -Version 1.0.0.4


2, Copy setting from local.settings.json to your config file

3, If no record in db, you can run srcipt to init unprocess invoices
   Script path: DigitBridge.CommerceCentral.ERPDatabase\sql\AddEventProcessFromInvocie.sql

4, 
   4.1 Get unprocess invoices without payload.
   
   var client = new CommerceCentralInvoiceClient(); 
   var success = await client.GetUnprocessedInvoicesAsync(MasterAccountNum, ProfileNum);
   
   //client.Messages : get error message.
   //client.ResopneData.InvoiceUnprocessList : paging data.
   //client.ResopneData.InvoiceUnprocessListCount : total data count.
    
	
	
   4.2 Get unprocess invoices with payload.
            
	var client = new CommerceCentralInvoiceClient(); 
    var client = new CommerceCentralInvoiceClient(_baseUrl, _code);

    var payload = new CommerceCentralInvoiceRequestPayload()
    {
        //LoadAll = true,
        Top=10,
        Filter=new InvoiceUnprocessPayloadFilter()
        {
            ChannelAccountNum = 10001,
        },
    }; 

    var success = await client.GetUnprocessedInvoicesAsync(MasterAccountNum, ProfileNum, payload);
			
   //client.Messages : get error message.
   //client.ResopneData.InvoiceUnprocessList : paging data.
   //client.ResopneData.InvoiceUnprocessListCount : total data count.
   

5, Ack receive invoices 

    var client = new CommerceCentralAckReceiveInvoiceClient();
    var InvoiceUuids = new List<string>() { "15082ed6-b62f-4faf-9491-dc182d7bd4a9", "61F6B440-17B2-4A33-BD4F-27CD7C5C3F5D" };
    var success = await client.AckReceiveInvoicesAsync(MasterAccountNum, ProfileNum, InvoiceUuids);
    //client.Messages : get error message.
    //client.ResopneData: ack result.
	
6, Ack process invoices


    var client = new CommerceCentralAckProcessInvoiceClient();
    var processResults = new List<ProcessResult>();
    var processResult = new ProcessResult()
    {
        ProcessStatus = Base.Common.EventProcessProcessStatusEnum.Success,
        ProcessData = JObject.Parse("{\"message\": \"This is a test error message\"}"),
        ProcessUuid = "15082ed6-b62f-4faf-9491-dc182d7bd4a9"
    };
    processResults.Add(processResult);
    var success = await client.AckProcessInvoicesAsync(MasterAccountNum, ProfileNum, processResults);
    //client.Messages : get error message.
    //client.ResopneData: ack result.
 
 
7, Test Project:DigitBridge.CommerceCentral.ERPApiSDK.Tests.Integration
   Files:   
   CommerceCentralInvoiceClientTests
   CommerceCentralAckReceiveInvoiceClientTests
   CommerceCentralAckProcessInvoiceClientTests 
   
