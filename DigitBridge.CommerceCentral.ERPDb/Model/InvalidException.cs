using System;
using System.Collections.Generic;
using System.Text;

namespace DigitBridge.CommerceCentral.ERPDb
{
    /// <summary>
    /// Invalid parameter exception
    /// </summary>
    [Serializable()]
    public class InvalidParameterException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the InvalidParameterException class with a specified
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public InvalidParameterException(string message) : base(message)
        { }
        public InvalidParameterException() : base()
        { }

    }

    public class NoContentException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the InvalidParameterException class with a specified
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public NoContentException(string message) : base(message)
        { }
        public NoContentException() : base()
        { }
    }
    public class InvalidRequestException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the InvalidParameterException class with a specified
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public InvalidRequestException(string message= "Invalid request.") : base(message)
        { } 
    }
}
