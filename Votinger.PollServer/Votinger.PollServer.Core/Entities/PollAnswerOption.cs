using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Votinger.PollServer.Core.Entities
{
    public class PollAnswerOption : BaseEntity
    {
        public string Text { get; set; }
        public int NumberOfReplies { get; set; }
        public int PollId { get; set; }
        [NotMapped]
        public bool IsAnswered { get; set; }
        public Poll Poll { get; set; }
        public List<PollRepliedUser> RepliedUsers { get; set; }
    }
}
