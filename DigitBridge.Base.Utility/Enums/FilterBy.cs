using System;
using System.Collections.Generic;
using System.Text;

namespace DigitBridge.Base.Utility
{
    public enum FilterBy : int
    {
        /// <summary>
        /// equal
        /// [Description("=")]  // equal
        /// </summary>
        eq = 1,

        /// <summary>
        /// not equal
        /// [Description("<>")] // not equal
        /// </summary>
        ne = 2,

        /// <summary>
        /// less than
        /// [Description("<")]   // less than
        /// </summary>
        lt = 3,

        /// <summary>
        /// less or equal
        /// [Description("<=")]  // less or equal
        /// </summary>
        le = 4,

        /// <summary>
        /// greater than
        /// [Description(">")]    // greater
        /// </summary>
        gt = 5,

        /// <summary>
        /// greater or equal
        /// [Description(">=")] //greater or equal
        /// </summary>
        ge = 6,

        /// <summary>
        /// begin with
        /// [Description("begin with")]
        /// </summary>
        bw = 7,

        /// <summary>
        /// not begin
        /// [Description("not begin")]
        /// </summary>
        bn = 8,

        /// <summary>
        /// ends with"
        /// [Description("ends with")]
        /// </summary>
        ew = 9,

        /// <summary>
        /// not end
        /// [Description("not end")]
        /// </summary>
        en = 10,

        /// <summary>
        /// contains
        /// [Description("contains")]
        /// </summary>
        cn = 11,

        /// <summary>
        /// not contain
        /// [Description("not contain")]
        /// </summary>
        nc = 12
    }
}
