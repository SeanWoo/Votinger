using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Votinger.AuthServer.Core.Enums
{
    public enum ApiErrorStatusEnum
    {
        /// <summary>
        /// Invalid login information
        /// </summary>
        ERROR_NOT_VALID_CREDENTIALS = 1000,
        /// <summary>
        /// Invalid refresh token
        /// </summary>
        ERROR_NOT_VALID_REFRESH_TOKEN = 1001,
        /// <summary>
        /// Login is busy
        /// </summary>
        ERROR_LOGIN_IS_BUSY = 1002,
    }
}
