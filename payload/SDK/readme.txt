
1, Get latest SDK from nuget:DigitBridge.CommerceCentral.ERPApiSDK

2, Put following setting to your config file as required.

2.1 Event API
    "EventApi_BaseUrl": "https://digitbridge-erp-event-api-dev.azurewebsites.net",
    "EventApi_AuthCode": "drZEGmRUVmGcitmCqyp3VZe5b4H8fSoy8rDUsEMkfG9U7UURXMtnrw==",

2.2 Integration API
    "ERP_Integration_Api_BaseUrl": "https://digitbridge-erp-integration-api-dev.azurewebsites.net",
    "ERP_Integration_Api_AuthCode": "aa4QcFoSH4ADcXEROimDtbPa4h0mY/dsNFuK1GfHPAhqx5xMJRAaHw==",  
 

3, API instruction

3.1 Central order to ERP sales order (Use 2.1 configuration)
   3.1.1 CommerceCentralOrderClient: Create ERP sales order by centralOrderUuid

3.2 ERP sales order to WSM (Use 2.2 configuration)
3.2.1 WMSSalesOrderClient:ERP providing open sales order for WMS to download.
3.2.2 WMSAckReceiveSalesOrderClient: WMS download sales order from erp, then send succeed downloaded salesOrderuuids back to erp.
3.2.3 WMSAckProcessSalesOrderClient: WMS process downloaded salesorder, then send the process result back to erp

3.3 WMS upload shipment to ERP (Use 2.2 configuration)
3.3.1 WMSShipmentClient：WMS upload shipment to erp.
3.3.2 WMSShipmentListClient： ERP providing shipment (which uploaded by WMS) transferred result for WMS to download.
3.3.3 WMSShipmentResendClient： Resend existing WMS shipmentID to queue

3.4 ERP invoice to central  (Use 2.2 configuration)
3.4.1 CommerceCentralInvoiceClient: ERP providing unprocess invoices for Commerce central to download.
3.4.2 CommerceCentralAckReceiveInvoiceClient: Commerce central download unprocess invoice from erp, then send succeed downloaded invoiceuuids back to erp.
3.4.3 CommerceCentralAckProcessInvoiceClient: Commerce central process downloaded invoice, then send the process result back to erp.

3.5 ERP purchase order to WMS  (Use 2.2 configuration)
3.5.1 WMSPurchaseOrderClient: ERP providing purchase order list for WMS to download. 
3.5.2 WMSAckReceivePurchaseOrderClient: WMS download purchase order from erp, then send succeed downloaded purchaseOrderUuids back to erp. 
3.5.3 WMSAckProcessPurchaseOrderClient: WMS process downloaded purchase order, then send the process result back to erp. 

3.6 WMS upload PoReceive to ERP (Use 2.2 configuration)
3.6.1 WMSPoReceiveClient: WSM upload purchase order received items to erp.

3.7 WMS upload inventory instock to ERP (Use 2.2 configuration)
3.7.1 WMSInventorySyncClient：WMS upload inventory instock to ERP

3.8 Resend event (Use 2.1 configuration)
3.8.1 ErpEventResendClient: Resend existing event to queue.

4, Two ways to create a client 
   1,//Add config to config file
     var client = new CommerceCentralInvoiceClient(); 
   2,//No config file
     var baseUrl="https://digitbridge-erp-integration-api-dev.azurewebsites.net"
     var authoCode="aa4QcFoSH4ADcXEROimDtbPa4h0mY/dsNFuK1GfHPAhqx5xMJRAaHw==";
     var client = new CommerceCentralInvoiceClient(baseUr,authoCode); 

4, Get api invoked result
   client.Messages : Include both SDK error and api invoked error
   client.ResopneData : Api response data.  
    
	 