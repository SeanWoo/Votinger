using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Votinger.AuthServer.Web.Models.Requests
{
    public class SignInRequest
    {
        [Required]
        public string Login { get; set; }
        [MinLength(8)]
        public string Password { get; set; }
    }
}
