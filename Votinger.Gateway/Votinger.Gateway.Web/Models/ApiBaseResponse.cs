using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Votinger.Gateway.Web.Models
{
    public class ApiBaseResponse
    {
        /// <summary>
        /// Operation status
        /// </summary>
        public string Status { get; set; }
        /// <summary>
        /// Object with result
        /// </summary>
        public object Result { get; set; }
        /// <summary>
        /// Operation error. Stores an object if Status = Error
        /// </summary>
        public ApiError Error { get; set; }

        public ApiBaseResponse(string status, object result)
        {
            Status = status;
            Result = result;
        }
        public ApiBaseResponse(string status, ApiError error)
        {
            Status = status;
            Error = error;
        }
    }
}
