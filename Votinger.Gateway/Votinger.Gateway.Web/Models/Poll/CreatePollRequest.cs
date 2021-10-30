using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Votinger.Gateway.Web.Models.Poll
{
    public class CreatePollRequest
    {
        public string Title { get; set; }
        public IEnumerable<string> AnswerOptions { get; set; }
    }
}
