using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Threading.Tasks;

namespace DigitBridge.CommerceCentral.ApiCommon
{
    public class ResponseResult<T>
    {
        /// <summary>
        /// Respone Message
        /// </summary>
        public string Message { get; set; }

        public HttpStatusCode StatusCode = HttpStatusCode.OK;

        /// <summary>
        /// Request result 
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// Respone data
        /// </summary>
        public T Data { get; set; }

        /// <summary>
        /// success respone data 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="success"></param> 
        public ResponseResult(T data, bool success = true)
        {
            this.Success = true;
            this.Data = data;
        }
        /// <summary>
        /// respone message,default is errro message
        /// </summary>
        /// <param name="message"></param>
        /// <param name="success"></param> 
        public ResponseResult(string message, bool success = false)
        {
            this.Success = success;
            this.Message = message;
        }

        public ResponseResult(Exception ex)
        {
            this.Success = false;
            this.Message = ex?.Message;
            this.StatusCode = HttpStatusCode.InternalServerError;
        }
    }
    /// <summary>
    /// The result of the request
    /// </summary>
    public class Response<T> : ResponseResult<T>, IActionResult
    {
        /// <summary>
        /// success respone data 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="success"></param> 
        public Response(T data, bool success = true) : base(data, success)
        {
            this.Success = true;
            this.Data = data;
        }
        /// <summary>
        /// respone message,default is errro message
        /// </summary>
        /// <param name="message"></param>
        /// <param name="success"></param> 
        public Response(string message, bool success = false) : base(message, success)
        {
            this.Success = success;
            this.Message = message;
        }

        public async Task ExecuteResultAsync(ActionContext context)
        {
            await context.HttpContext.Response.Output(this, base.StatusCode);
        }
    }
}
