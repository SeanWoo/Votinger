using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Votinger.Gateway.Web.Models.Poll
{
    public class VoteInPollRequest
    {
        public int PollId { get; set; }
        public IEnumerable<int> AnsweredOptions { get; set; }
    }
}
