using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Votinger.AuthServer.Web.Models
{
    /// <summary>
    /// Error codes for API
    /// </summary>
    public enum ApiErrorCode : int
    {
        // validation 100-200

        /// <summary>
        /// You have not filled in all the required fields
        /// </summary>
        UNFILLED_FORM = 100,
        /// <summary>
        /// Too short password
        /// </summary>
        MIN_LENGHT_PASSWORD = 1001,
    }
}
