using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace DigitBridge.Base.Utility
{
    public static class SystemCodeNames
    {
        public const string ColorPatternCode = "ColorPatternCode";
        public const string SizeType = "SizeType";
        public const string SizeCode = "SizeCode";
        public const string WidthCode = "WidthCode";
        public const string LengthCode = "LengthCode";
        public const string ClassCode = "ClassCode";
        public const string SubClassCode = "SubClassCode";
        public const string DivisionCode = "DivisionCode";
        public const string DepartmentCode = "DepartmentCode";
        public const string GroupCode = "GroupCode";
        public const string SubGroupCode = "SubGroupCode";
        public const string CategoryCode = "CategoryCode";
        public const string Model = "Model";
        public const string UOM = "UOM";
        public const string Terms = "Terms";
        public const string BusinessType = "BusinessType";

        private static List<string> _list;
        public static List<string> GetList()
        {
            if (_list == null) _list = new List<string>()
                {
                    ColorPatternCode,
                    SizeType,
                    SizeCode,
                    WidthCode,
                    LengthCode,
                    ClassCode,
                    SubClassCode,
                    DivisionCode,
                    DepartmentCode,
                    GroupCode,
                    SubGroupCode,
                    CategoryCode,
                    Model,
                    UOM,
                    Terms,
                    BusinessType,
                };
            return _list;
        }

        public static JObject GetNewCodeField(string codeName)
        {
            if (codeName.EqualsIgnoreSpace(Terms))
                return new JObject()
                {
                    { "codeName", codeName },
                    { "data", new JArray()
                        {
                            new JObject()
                            {
                                { "code", "terms code"},
                                { "days", "terms days"},
                                { "description", "terms description"}
                            }
                        }
                    }
                };

            return new JObject()
                {
                    { "codeName", codeName },
                    { "data", new JArray()
                        {
                            new JObject()
                            {
                                { "code", codeName },
                                { "description", $"{codeName} description" }
                            }
                        }
                    }
                };

        }
    }
}
