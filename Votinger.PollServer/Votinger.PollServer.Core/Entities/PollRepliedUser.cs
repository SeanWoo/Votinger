using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Votinger.PollServer.Core.Entities
{
    public class PollRepliedUser : BaseEntity
    {
        public int PollAnswerOptionId { get; set; }
        public PollAnswerOption PollAnswerOption { get; set; }
        public int UserId { get; set; }
    }
}
