using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Votinger.PollServer.Core.Entities;

namespace Votinger.PollServer.Services.Polls.Model
{
    public class PollAnswerOptionModel : BaseEntity
    {
        public string Text { get; set; }
        public int NumberOfReplies { get; set; }
        public int PollId { get; set; }
        public bool IsAnswered { get; set; }
        public Poll Poll { get; set; }
        public List<PollRepliedUser> RepliedUsers { get; set; }
    }
}
