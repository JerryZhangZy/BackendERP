


//-------------------------------------------------------------------------
// This document is generated by T4
// It will overwrite your changes, please keep it as it is
// <copyright company="DigitBridge">
//     Copyright (c) DigitBridge Inc.  All rights reserved.
// </copyright>
//-------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Xunit;
using DigitBridge.CommerceCentral.YoPoco;
using DigitBridge.Base.Utility;
using DigitBridge.CommerceCentral.XUnit.Common;
using DigitBridge.CommerceCentral.ERPDb;
using Bogus;
using DigitBridge.Base.Common;
using DigitBridge.CommerceCentral.ERPDb.Tests.Integration;

namespace DigitBridge.CommerceCentral.ERPMdl.Tests.Integration
{
    public partial class SalesOrderCalculatorTests
    { 
        [Fact()]
        public void Calculate_Test1()
        {
            var dObj = CalculateTestHelper.GetSumCase1();
            Calculate_Test(dObj);
        }

        [Fact()]
        public void Calculate_Test2()
        {
            var dObj = CalculateTestHelper.GetSumCase2();
            Calculate_Test(dObj);
        }

        [Fact()]
        public void Calculate_Test3()
        {
            var dObj = CalculateTestHelper.GetSumCase3();
            Calculate_Test(dObj);
        }

        private void Calculate_Test(dynamic dObj)
        {
            //var dObj = CalculateTestHelper.GetData1();
            int length = dObj.Items.Length;
            var testData = GetFakerData(length);

            for (int i = 0; i < dObj.Items.Length; i++)
            {
                CalculateTestHelper.CopyItem(testData.SalesOrderItems[i], dObj.Items[i].Item);
            }
            CalculateTestHelper.CopySum(testData.SalesOrderHeader, dObj.Sum);

            var calculator = new SalesOrderServiceCalculatorDefault(DataBaseFactory);
            calculator.Calculate(testData, ProcessingMode.Edit);

            var success = testData.SalesOrderHeader.TotalAmount == dObj.TotalAmount;
            if (!success)
            {
                success = CheckErrInRange(dObj.TotalAmount, testData.SalesOrderHeader.TotalAmount);
            }
            Assert.False(success == false, $"Summary doesn't pass test. Actual result is  {testData.SalesOrderHeader.TotalAmount},expect is {dObj.TotalAmount} ");

            for (int i = 0; i < dObj.Items.Length; i++)
            {
                success = dObj.Items[i].Result.ItemTotalAmount == testData.SalesOrderItems[i].ItemTotalAmount;
                if (!success)
                {
                    success = CheckErrInRange(dObj.Items[i].Result.ItemTotalAmount, testData.SalesOrderItems[i].ItemTotalAmount);
                    Assert.False(success == false, $"item {i} doesn't pass test. Actual result is  {testData.SalesOrderItems[i].ItemTotalAmount},expect is {dObj.Items[i].Result.ItemTotalAmount} ");
                }
            }
        }
        //check error range.
        private bool CheckErrInRange(decimal expectedResult, decimal actualResult)
        {
            var errorRange = 0.0001m;
            var rate = expectedResult / actualResult;
            return rate < (1 + errorRange) && rate >= (1 - errorRange);
        }

    }
}



