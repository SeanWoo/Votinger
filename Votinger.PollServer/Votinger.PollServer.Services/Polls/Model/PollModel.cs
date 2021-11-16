using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Votinger.PollServer.Core.Entities;

namespace Votinger.PollServer.Services.Polls.Model
{
    public class PollModel : BaseEntity
    {
        public int UserId { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public List<PollAnswerOptionModel> AnswerOptions { get; set; }
    }
}
