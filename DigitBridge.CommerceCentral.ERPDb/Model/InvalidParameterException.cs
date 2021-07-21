using System;
using System.Collections.Generic;
using System.Text;

namespace DigitBridge.CommerceCentral.ERPDb
{
    /// <summary>
    /// Invalid parameter exception
    /// </summary>
    public class InvalidParameterException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the InvalidParameterException class with a specified
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public InvalidParameterException(string message) : base(message)
        { }

    }
}
