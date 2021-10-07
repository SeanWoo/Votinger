using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Votinger.AuthServer.Core.Entities
{
    public class RefreshToken : BaseEntity
    {
        public string Token { get; set; }
    }
}
