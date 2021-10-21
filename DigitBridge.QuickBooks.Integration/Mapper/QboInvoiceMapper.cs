using DigitBridge.Base.Utility;
using DigitBridge.CommerceCentral.ERPDb;
using Intuit.Ipp.Data;
using System;
using System.Collections.Generic;

namespace DigitBridge.QuickBooks.Integration
{
    public class QboInvoiceMapper
    {
        private QboIntegrationSetting _setting { get; set; }
        public QboInvoiceMapper(QboIntegrationSetting setting)
        {
            this._setting = setting;
            PrepareSetting();
        }

        protected void PrepareSetting()
        {
            if (this._setting == null)
            {
                _setting = new QboIntegrationSetting();
            }
            if (string.IsNullOrEmpty(_setting.QboDefaultItemId))
            {
                _setting.QboDefaultItemId = QboMappingConsts.QboDefaultItemId;
            }
            if (string.IsNullOrEmpty(_setting.QboShippingItemId))
            {
                _setting.QboShippingItemId = QboMappingConsts.SippingCostRefValue;
            }
            if (string.IsNullOrEmpty(_setting.QboMiscItemId))
            {
                _setting.QboMiscItemId = QboMappingConsts.QboMiscItemId;
            }
            if (string.IsNullOrEmpty(_setting.QboChargeAndAllowanceItemId))
            {
                _setting.QboChargeAndAllowanceItemId = QboMappingConsts.QboChargeAndAllowanceItemId;
            }
            if (string.IsNullOrEmpty(_setting.QboSalesTaxItemId))
            {
                _setting.QboSalesTaxItemId = QboMappingConsts.QboSalesTaxItemId;
            }
            if (string.IsNullOrEmpty(_setting.QboDiscountItemId))
            {
                _setting.QboDiscountItemId = QboMappingConsts.DiscountRefValue;
            }

            if (string.IsNullOrEmpty(_setting.QboInvoiceNumberFieldID))
            {
                _setting.QboInvoiceNumberFieldID = QboMappingConsts.QboInvoiceNumberFieldID;
            }
            if (string.IsNullOrEmpty(_setting.QboChnlOrderIdCustFieldId))
            {
                _setting.QboChnlOrderIdCustFieldId = QboMappingConsts.QboChnlOrderIdCustFieldId;
            }
            if (string.IsNullOrEmpty(_setting.Qbo2ndChnlOrderIdCustFieldId))
            {
                _setting.Qbo2ndChnlOrderIdCustFieldId = QboMappingConsts.Qbo2ndChnlOrderIdCustFieldId;
            }
        }

        #region Qbo lines 

        protected List<Line> ToQboLines(InvoiceData invoiceData)
        {
            var lines = new List<Line>();
            foreach (var item in invoiceData.InvoiceItems)
            {
                ////DiscountRate and  DiscountAmount only one applied.
                //var qboLine = item.DiscountRate.IsZero() ? ItemToQboLine_DiscountAmount(item) : ItemToQboLine_DiscountRate(item);
                //lines.Add(qboLine);

                // Apply both discountRate and discountAmount(Apply discountrate first then apply discountAmount.) 
                lines.Add(ItemToQboLine_DiscountRate(item));
                if (item.DiscountAmount != 0)
                {
                    lines.Add(ItemToQboLine_DiscountAmount(item));
                }
            }
            //SubTotalToQboLine(invoiceData.InvoiceHeader);
            lines.Add(DiscountToQboLine(invoiceData.InvoiceHeader));
            lines.Add(ShippingCostToQboLine(invoiceData.InvoiceHeader));
            lines.Add(MiscCostToQboLine(invoiceData.InvoiceHeader));
            lines.Add(ChargeAndAllowanceCostToQboLine(invoiceData.InvoiceHeader));
            if (_setting.SalesTaxExportRule == (int)TaxExportRule.ItemLine)
                lines.Add(TaxCostToQboLine(invoiceData.InvoiceHeader));
            return lines;
        }

        #region Last version: DiscountRate and  DiscountAmount only one applied.
        //protected Line ItemToQboLine_DiscountRate(InvoiceItems item)
        //{
        //    Line line = new Line();

        //    line.Description = item.Description;
        //    line.Amount = item.IsAr ? item.ExtAmount : 0;//TODO check this one
        //    line.AmountSpecified = true;
        //    //line.LineNum = item.InvoiceItemsUuid;
        //    line.AnyIntuitObject = new SalesItemLineDetail()
        //    {
        //        ItemRef = new ReferenceType()
        //        {
        //            Value = _setting.QboDefaultItemId,// All sku mapping to DefaultItemId in qbo. TODO mapping to qbo inventory sku. 
        //        },
        //        Qty = item.ShipQty,
        //        QtySpecified = true,
        //        AnyIntuitObject = item.IsAr ? item.DiscountPrice : 0,//TODO check this one
        //        ItemElementName = ItemChoiceType.UnitPrice,
        //        //DiscountAmt = item.DiscountRate.IsZero() ? item.DiscountAmount : 0,
        //    };
        //    line.DetailType = LineDetailTypeEnum.SalesItemLineDetail;
        //    line.DetailTypeSpecified = true;
        //    return line;
        //}
        //protected Line ItemToQboLine_DiscountAmount(InvoiceItems item)
        //{
        //    Line line = new Line();

        //    line.Description = item.Description;
        //    line.Amount = item.IsAr ? item.ExtAmount : 0;//TODO check this one
        //    line.AmountSpecified = true;
        //    //line.LineNum = item.InvoiceItemsUuid;
        //    line.AnyIntuitObject = new SalesItemLineDetail()
        //    {
        //        ItemRef = new ReferenceType()
        //        {
        //            Value = _setting.QboDefaultItemId,// All sku mapping to DefaultItemId in qbo. TODO mapping to qbo inventory sku. 
        //        },
        //        Qty = item.ShipQty,
        //        QtySpecified = true,
        //        //TODO add this logic.
        //        //AnyIntuitObject = item.IsAr ? item.Price : 0,
        //        //ItemElementName = ItemChoiceType.RatePercent,
        //        //DiscountAmt = item.IsAr && item.DiscountRate.IsZero() ? item.DiscountAmount : 0,
        //    };
        //    line.DetailType = LineDetailTypeEnum.SalesItemLineDetail;
        //    line.DetailTypeSpecified = true;
        //    return line;
        //}
        #endregion

        #region Apply both discountRate and discountAmount(Apply discountrate first, then apply discountAmount.)
        protected Line ItemToQboLine_DiscountRate(InvoiceItems item)
        {
            Line line = new Line();

            line.Description = item.Description;
            line.Amount = item.IsAr ? item.ExtAmount + item.DiscountAmount : 0;//TODO check this one
            line.AmountSpecified = true;
            //line.LineNum = item.InvoiceItemsUuid;
            line.AnyIntuitObject = new SalesItemLineDetail()
            {
                ItemRef = new ReferenceType()
                {
                    Value = _setting.QboDefaultItemId,// All sku mapping to DefaultItemId in qbo. TODO mapping to qbo inventory sku. 
                },
                Qty = item.ShipQty,
                QtySpecified = true,
                AnyIntuitObject = item.IsAr ? item.DiscountPrice : 0,//TODO check this one
                ItemElementName = ItemChoiceType.UnitPrice,
                //DiscountAmt = item.DiscountRate.IsZero() ? item.DiscountAmount : 0,
            };
            line.DetailType = LineDetailTypeEnum.SalesItemLineDetail;
            line.DetailTypeSpecified = true;
            return line;
        }
        protected Line ItemToQboLine_DiscountAmount(InvoiceItems item)
        {
            Line line = new Line();

            line.Description = $"Apply item total discount amount for sku: {item.SKU} ";
            line.Amount = item.IsAr ? item.DiscountAmount * (-1) : 0;//TODO check this one
            line.AmountSpecified = true;
            //line.LineNum = item.InvoiceItemsUuid;
            line.AnyIntuitObject = new SalesItemLineDetail()
            {
                ItemRef = new ReferenceType()
                {
                    Value = _setting.QboDefaultItemId,// All sku mapping to DefaultItemId in qbo. TODO mapping to qbo inventory sku. 
                },
                Qty = item.ShipQty,
                QtySpecified = true,
                //TODO add this logic.
                //AnyIntuitObject = item.IsAr ? item.Price : 0,
                //ItemElementName = ItemChoiceType.RatePercent,
                //DiscountAmt = item.IsAr && item.DiscountRate.IsZero() ? item.DiscountAmount : 0, 
            };
            line.DetailType = LineDetailTypeEnum.SalesItemLineDetail;
            line.DetailTypeSpecified = true;
            return line;
        }
        #endregion

        protected Line DiscountToQboLine(InvoiceHeader invoiceHeader)
        {
            Line line = new Line();
            line.Amount = invoiceHeader.DiscountAmount;
            line.AmountSpecified = true;
            line.DetailType = LineDetailTypeEnum.DiscountLineDetail;
            line.DetailTypeSpecified = true;
            line.AnyIntuitObject = new DiscountLineDetail()
            {
                PercentBased = false,
                DiscountAccountRef = new ReferenceType()
                {
                    Value = QboMappingConsts.DiscountRefValue,
                    //name = QboMappingConsts.DiscountRefName,
                }
            };
            line.Description = QboMappingConsts.SummaryDiscountLineDescription + invoiceHeader.InvoiceNumber;
            return line;
        }
        protected Line ShippingCostToQboLine(InvoiceHeader invoiceHeader)
        {
            Line line = new Line();
            line.Amount = invoiceHeader.ShippingAmount;
            line.AmountSpecified = true;
            line.AnyIntuitObject = new SalesItemLineDetail()
            {
                ItemRef = new ReferenceType()
                {
                    Value = _setting.QboShippingItemId,
                    //name = _setting.QboShippingItemName,
                },
                //Qty = 1,
                //QtySpecified = true,
                //AnyIntuitObject = invoiceHeader.ShippingAmount,//TODO check this one
                //ItemElementName = ItemChoiceType.UnitPrice,
            };
            line.DetailType = LineDetailTypeEnum.SalesItemLineDetail;
            line.DetailTypeSpecified = true;
            line.Description = QboMappingConsts.SummaryShippingLineDescription + invoiceHeader.InvoiceNumber;
            return line;
        }
        protected Line MiscCostToQboLine(InvoiceHeader invoiceHeader)
        {
            Line line = new Line();
            line.Amount = invoiceHeader.MiscAmount;
            line.AmountSpecified = true;
            line.AnyIntuitObject = new SalesItemLineDetail()
            {
                ItemRef = new ReferenceType()
                {
                    Value = _setting.QboMiscItemId,
                    //name = _setting.QboMiscItemName,
                }
            };
            line.DetailType = LineDetailTypeEnum.SalesItemLineDetail;
            line.DetailTypeSpecified = true;
            line.Description = QboMappingConsts.SummaryMiscLineDescription + invoiceHeader.InvoiceNumber;
            return line;
        }
        protected Line ChargeAndAllowanceCostToQboLine(InvoiceHeader invoiceHeader)
        {
            Line line = new Line();
            line.Amount = invoiceHeader.ChargeAndAllowanceAmount;
            line.AmountSpecified = true;
            line.AnyIntuitObject = new SalesItemLineDetail()
            {
                ItemRef = new ReferenceType()
                {
                    Value = _setting.QboChargeAndAllowanceItemId,
                    //name = _setting.QboChargeAndAllowanceItemName,
                }
            };
            line.DetailType = LineDetailTypeEnum.SalesItemLineDetail;
            line.DetailTypeSpecified = true;
            line.Description = QboMappingConsts.SummaryChargeAndAllowanceLineDescription + invoiceHeader.InvoiceNumber;
            return line;
        }
        /// <summary>
        ///  Mapping tax To Qbo line. 
        /// </summary>
        /// <param name="invoiceHeader"></param>
        /// <returns></returns>
        protected Line TaxCostToQboLine(InvoiceHeader invoiceHeader)
        {
            Line line = new Line();
            line.Amount = invoiceHeader.TaxAmount;
            line.AmountSpecified = true;
            line.AnyIntuitObject = new SalesItemLineDetail()
            {
                ItemRef = new ReferenceType()
                {
                    Value = _setting.QboSalesTaxItemId.ToString(),
                    //name = _setting.QboSalesTaxItemName,
                }
            };
            line.DetailType = LineDetailTypeEnum.SalesItemLineDetail;
            line.DetailTypeSpecified = true;
            line.Description = QboMappingConsts.SalesTaxItemDescription + invoiceHeader.InvoiceNumber;
            return line;
        }

        /// <summary>
        ///  Add sub total line for all sku
        /// </summary>
        /// <param name="invoiceHeader"></param>
        /// <returns></returns>
        protected Line SubTotalToQboLine(InvoiceHeader invoiceHeader)
        {
            Line line = new Line();
            line.Amount = invoiceHeader.SubTotalAmount;
            line.AmountSpecified = true;
            //line.AnyIntuitObject = new DescriptionLineDetail
            //{

            //};
            line.DetailType = LineDetailTypeEnum.DescriptionOnly;
            line.DetailTypeSpecified = true;
            line.Description = $"Sub total amount:{invoiceHeader.SubTotalAmount}";
            return line;
        }

        #endregion 

        #region Map Qbo invoice 

        protected Invoice ToQboInvoice(InvoiceData invoiceData, Invoice invoice)
        {
            var invoiceHeader = invoiceData.InvoiceHeader;
            var invoiceInfo = invoiceData.InvoiceHeaderInfo;
            //invoice.DocNumber = _exportLog.DocNumber;
            invoice.Balance = invoiceHeader.Balance;
            invoice.TotalAmt = invoiceHeader.TotalAmount;
            invoice.TotalAmtSpecified = true;
            if (invoiceHeader.DueDate.HasValue)
                invoice.DueDate = invoiceHeader.DueDate.Value;
            //invoice.SalesTermRef // invoiceHeader.Terms
            invoice.TxnDate = invoiceHeader.InvoiceDate;
            invoice.TxnDateSpecified = true;

            if (!invoiceHeader.ShipDate.IsZero())
            {
                invoice.ShipDate = invoiceHeader.ShipDate.Value;
                invoice.ShipDateSpecified = true;
                invoice.TrackingNum = invoiceInfo.OrderShipmentNum.ToString();
                //invoice.ShipMethodRef = new ReferenceType()
                //{
                //    Value = invoiceInfo.ShippingCarrier.ToShipMethodRef(),
                //    name = invoiceInfo.ShippingClass
                //};
            }

            invoice.ApplyTaxAfterDiscount = true;
            if (_setting.SalesTaxExportRule != (int)TaxExportRule.ItemLine)
            {
                invoice.TxnTaxDetail = TaxCostToQboTxnTaxDetail(invoiceHeader);
            }
            //TODO
            //invoice.PrivateNote = invoiceInfo.s//qboSalesOrder.PrivateNote = fulfilledOrder.OrderHeader.SellerPrivateNote;
            //invoice.CustomerMemo = new MemoRef() { Value = qboSalesOrder.CustomerMemo };//qboSalesOrder.CustomerMemo = fulfilledOrder.OrderHeader.SellerPublicNote;

            AppendAddressToInvoice(invoice, invoiceInfo);
            AppendCustomFieldToInvoice(invoice, invoiceInfo, invoiceHeader.InvoiceNumber);
            AppendCustomerToInvoice(invoice, invoiceHeader);

            return invoice;
        }
        /// <summary>
        /// Add CustomField to Qbo invoice. CustomField max num is 3.
        /// </summary>
        /// <param name="invoice"></param>
        /// <param name="invoiceInfo"></param>
        /// <param name="invoiceNumber"></param>
        private void AppendCustomFieldToInvoice(Invoice invoice, InvoiceHeaderInfo invoiceInfo, string invoiceNumber)
        {
            // Map Invoice customized fields
            List<CustomField> customFields = new List<CustomField>();

            CustomField invoiceNumberField = new CustomField();
            invoiceNumberField.DefinitionId = _setting.QboInvoiceNumberFieldID;
            //invoiceNumberField.Name = _setting.QboInvoiceNumberFieldName;
            invoiceNumberField.Type = CustomFieldTypeEnum.StringType;
            invoiceNumberField.AnyIntuitObject = invoiceNumber.ToCustomField();// stirng value for this field.// max length is 31.
            customFields.Add(invoiceNumberField);


            //CustomField endCustoerPoNumCustField = new CustomField();
            //endCustoerPoNumCustField.DefinitionId = _setting.QboEndCustomerPoNumCustFieldId.ToString();
            ////endCustoerPoNumCustField.Name = _setting.QboEndCustomerPoNumCustFieldName;
            //endCustoerPoNumCustField.AnyIntuitObject = invoiceInfo.CustomerPoNum;
            //endCustoerPoNumCustField.Type = CustomFieldTypeEnum.StringType;
            //customFields.Add(endCustoerPoNumCustField);

            CustomField chnlOrderIdCustField = new CustomField();
            chnlOrderIdCustField.DefinitionId = _setting.QboChnlOrderIdCustFieldId.ToString();
            chnlOrderIdCustField.AnyIntuitObject = invoiceInfo.ChannelOrderID.ToCustomField();
            chnlOrderIdCustField.Type = CustomFieldTypeEnum.StringType;
            customFields.Add(chnlOrderIdCustField);

            CustomField secChnlOrderIdCustomField = new CustomField();
            secChnlOrderIdCustomField.DefinitionId = _setting.Qbo2ndChnlOrderIdCustFieldId.ToString();
            secChnlOrderIdCustomField.AnyIntuitObject = invoiceInfo.SecondaryChannelOrderID.ToCustomField();
            secChnlOrderIdCustomField.Type = CustomFieldTypeEnum.StringType;
            customFields.Add(secChnlOrderIdCustomField);

            invoice.CustomField = customFields.ToArray();
        }
        private void AppendCustomerToInvoice(Invoice invoice, InvoiceHeader invoiceHeader)
        {
            string customerId;
            if (_setting.QboCustomerCreateRule == (int)CustomerCreateRule.PerMarketPlace)
            {
                //get ChannelQboCustomerId
                customerId = CustomerMapper.GetMarketPlaceCustomer(_setting);
            }
            else //if (_setting.QboCustomerCreateRule == (int)CustomerCreateRule.PerOrder)
            {
                //customerId = invoiceHeader.CustomerCode;//todo check this.
                customerId = "1";//TODO replace this 
            }
            invoice.CustomerRef = new ReferenceType() { Value = customerId };
        }
        private void AppendAddressToInvoice(Invoice invoice, InvoiceHeaderInfo invoiceInfo)
        {
            if (_setting.QboCustomerCreateRule != (int)CustomerCreateRule.PerMarketPlace)
            {
                return;
            }
            PhysicalAddress shippingAddress = new PhysicalAddress();
            shippingAddress.Line1 = invoiceInfo.ShipToName;
            shippingAddress.Line2 = invoiceInfo.ShipToAddressLine1;
            shippingAddress.Line3 = invoiceInfo.ShipToAddressLine2;
            shippingAddress.Line4 = invoiceInfo.ShipToAddressLine3;
            shippingAddress.PostalCode = invoiceInfo.ShipToPostalCode;
            shippingAddress.City = invoiceInfo.ShipToCity;
            shippingAddress.Country = invoiceInfo.ShipToCountry;
            shippingAddress.CountrySubDivisionCode = invoiceInfo.ShipToState;

            invoice.ShipAddr = shippingAddress;

            PhysicalAddress billingAddress = new PhysicalAddress();
            billingAddress.Line1 = invoiceInfo.BillToName;
            billingAddress.Line2 = invoiceInfo.BillToAddressLine1;
            billingAddress.Line3 = invoiceInfo.BillToAddressLine2;
            billingAddress.Line4 = invoiceInfo.BillToAddressLine3;
            billingAddress.PostalCode = invoiceInfo.BillToPostalCode;
            billingAddress.City = invoiceInfo.BillToCity;
            billingAddress.Country = invoiceInfo.BillToCountry;
            billingAddress.CountrySubDivisionCode = invoiceInfo.BillToState;

            invoice.BillAddr = billingAddress;
        }
        /// <summary>
        ///  Mapping tax To Qbo invoice tax detail.
        /// </summary>
        /// <param name="invoiceHeader"></param>
        /// <returns></returns>
        protected TxnTaxDetail TaxCostToQboTxnTaxDetail(InvoiceHeader invoiceHeader)
        {
            var taxDetail = new TxnTaxDetail();
            taxDetail.TotalTax = invoiceHeader.TaxAmount;

            Line line = new Line()
            {
                DetailType = LineDetailTypeEnum.TaxLineDetail,
                Amount = invoiceHeader.TaxAmount,
                AnyIntuitObject = new TaxLineDetail()
                {
                    PercentBased = false,
                    NetAmountTaxable = invoiceHeader.TaxableAmount,
                    TaxPercent = invoiceHeader.TaxRate * 100
                }
            };
            taxDetail.TaxLine = new Line[] { line };
            return taxDetail;
        }
        #endregion


        public Invoice ToInvoice(InvoiceData invoiceData, Invoice invoice)
        {
            invoice = ToQboInvoice(invoiceData, invoice);
            invoice.Line = ToQboLines(invoiceData).ToArray();
            return invoice;
        }
    }
}
