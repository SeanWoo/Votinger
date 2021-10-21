using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Votinger.Protos;

namespace Votinger.Gateway.Web.Models
{
    public class ApiError
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }

        public ApiError() { }

        public ApiError(int statusCode, string message)
        {
            StatusCode = statusCode;
            Message = message;
        }
    }
}
