using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Votinger.Gateway.Web.Models.Poll
{
    public class GetFewRequest
    {
        public int From { get; set; }
        public int To { get; set; } = 10;
        public bool IncludeAnswers { get; set; } = false;
    }
}
