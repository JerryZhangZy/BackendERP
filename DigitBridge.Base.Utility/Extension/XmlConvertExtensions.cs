using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace DigitBridge.Base.Utility
{
    /// <summary>
    /// convert to xml
    /// </summary>
    public static class XmlConvertExtensions
    {
        /// <summary>
        /// number list to xml
        /// </summary>
        /// <param name="numbers"></param>
        /// <returns></returns>
        public static string ListToXml(this IList<string> numbers)
        {
            StringBuilder sb = new StringBuilder("<parameters>");

            foreach (var number in numbers)
            {
                sb.AppendFormat("<value>{0}</value>", number);
            }
            sb.Append("</parameters>");

            return sb.ToString();
        }

        /// <summary>
        /// object list to xml
        /// </summary>
        /// <param name="numbers"></param>
        /// <returns></returns> 
        public static string ListToXml(this List<string> keys, List<string> values)
        {
            if (keys == null || keys.Count == 0)
                throw new Exception("keys cann't be empty.");
            if (values == null || values.Count == 0)
                throw new Exception("values cann't be empty.");
            if (keys.Count != values.Count)
                throw new Exception("Keys Count should be equal values Count.");


            StringBuilder sb = new StringBuilder("<parameters>");

            for (int i = 0; i < keys.Count; i++)
            {
                sb.Append("<parameter>");

                sb.AppendFormat("<key>{0}</key>", keys[i]);
                sb.AppendFormat("<value>{0}</value>", values[i]);

                sb.Append("</parameter>");
            }
            sb.Append("</parameters>");

            return sb.ToString();
        }
    }
}
