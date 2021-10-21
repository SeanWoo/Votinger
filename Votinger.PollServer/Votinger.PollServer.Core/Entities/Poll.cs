using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Votinger.PollServer.Core.Entities
{
    public class Poll : BaseEntity
    {
        public string Title { get; set; }
        public List<PollAnswerOption> AnswerOptions { get; set; }
    }
}
