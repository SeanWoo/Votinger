using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Votinger.PollServer.Services.Polls.Model
{
    public class AnswerPollModel
    {
        public int UserId { get; set; }
        public int PollId { get; set; }
        public int[] AnswerOptionIds { get; set; }
    }
}
