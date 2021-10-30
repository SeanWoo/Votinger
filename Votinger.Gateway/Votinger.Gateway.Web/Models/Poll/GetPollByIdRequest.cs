using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Votinger.Gateway.Web.Models.Poll
{
    public class GetPollByIdRequest
    {
        public int PollId { get; set; }
        public bool IncludeAnswerOptions { get; set; } = false;
        public bool IncludeRepliedUsers { get; set; } = false;
    }
}
