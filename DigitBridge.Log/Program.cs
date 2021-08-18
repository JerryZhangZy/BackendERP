using System;
using System.Collections.Generic;
using System.Reflection;

namespace DigitBridge.Log
{
    class Program
    {
        static void Main(string[] args)
        {
             LogCenter.CaptureMessage(" Info Message", EventLevel.Info);
            LogCenter.CaptureMessage(" Debug Message", EventLevel.Debug);
            LogCenter.CaptureMessage(" Fatal Message ", EventLevel.Fatal);
            LogCenter.CaptureMessage("  Warning Message", EventLevel.Warning);
            //// demo 1 : capture exception with tagname and tagvalue
            //try
            //{
            //    var result = 10 / int.Parse("0");
            //}
            //catch (Exception ex)
            //{
            //    LogCenter.CaptureException(ex, "write tag name here", "write tag value here");

            //}

            //// demo 2 : capture exception with tagname and details
            //try
            //{ 
            //    throw new NotFiniteNumberException("{code:20233;message:1023423423:}");
            //}
            //catch (Exception ex)
            //{

            //    //var jsonStr = "{\"employees\": [{\"firstName\": \"Bill\",\"lastName\": \"Gates\"},{\"firstName\": \"George\",\"lastName\": \"Bush\"}]}";

            //    // not support JObeject
            //    var parameters = new object[]
            //    {
            //        new { Name = "Token", Value = "Token Value" },
            //        new { Name = "PageIndex", Value = 100 } ,
            //        new { Name = "YourName", Value = 100 },
            //        new { SubObject = new{ SubName="SubName" ,SubValue="SubValue"}, Value = 100 }
            //    };
            //    var valuePairs = new Dictionary<string, object>
            //    {
            //        { "Header", "this is header" },
            //        { "Url", "http://myurl" },
            //        { "Parameters", parameters}
            //    };
            //    LogCenter.CaptureException(ex, "write tag name here", valuePairs);

            //}



            //var config = ConfigHelper.GetConfiguration();
            //var log = new LoggerConfiguration()
            //    .ReadFrom.Configuration(config)
            //    .Enrich.FromLogContext()
            //    .CreateLogger();

            //log.Error(new Exception("this is test for Error"), "");

            //LogCenter.Info("only message in program");

            // var parameters = new object[]
            // {
            //         new { Name = "Token", Value = "Token Value" },
            //         new { Name = "PageIndex", Value = 100 } ,
            //         new { Name = "YourName", Value = 100 },
            //         new { SubObject = new{ SubName="SubName" ,SubValue="SubValue"}, Value = 100 }
            // };
            // var valuePairs = new Dictionary<string, object>
            //     {
            //         { "Header", "this is header" },
            //         { "Url", "http://myurl" },
            //         { "Parameters", parameters}
            //     };

            // ////var list = new List<Person>()
            // ////{ new Person(){ name="test person 1",value="hello 1"},
            // ////new Person(){ name="test person 1",value="hello 1"}  };
            //LogCenter.CaptureException( LogEventLevel.Information, new Exception("2.0 test"), valuePairs);

            //try
            //{
            //    throw null;
            //    //ExceptionUtility.WrapException(MethodBase.GetCurrentMethod(), new Exception(" ExceptionUtility.WrapException(MethodBase.GetCurrentMethod()"));
            //}
            //catch (Exception ex)
            //{
            //    LogCenter.CaptureException(ex,"test fulsh");
            //}
        }
    }
}
