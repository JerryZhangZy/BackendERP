using System;
using System.Collections.Generic;
using System.Text;

namespace DigitBridge.CommerceCentral.ERPMdl
{
    public static class NumberGenerate
    {
        /// <summary>
        /// Get unique number by time and prefix type //TODO include more dependency
        /// </summary>
        /// <param name="prefix"></param>
        /// <returns></returns>
        public static string Generate(string prefix)
        {
            return prefix + Generate();
        }

        /// <summary>
        /// Get unique number by time  //TODO include more dependency
        /// </summary>
        /// <returns></returns>
        public static string Generate()
        {

            return DateTime.Now.ToString("yyyyMMddHHmmssfffff");
        }

        /// <summary>
        /// Get unique number by time  //TODO include more dependency
        /// </summary>
        /// <returns></returns>
        public static long GenerateLong()
        {
            //long.MaxValue 9223372036854775807
            return long.Parse(DateTime.Now.ToString("yyyyMMddHHmmssfffff"));
        }

        ///// <summary>
        ///// Get unique number by time  //TODO include more dependency
        ///// </summary>
        ///// <returns></returns>
        //public static long GenerateInt()
        //{
        //    //int.MaxValue 2147483647
        //    return long.Parse(DateTime.Now.ToString("yyyyMMddHHmmssfffff"));
        //}
    }
}
