using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AskAbout.Models
{
    public class Question
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public int Likes { get; set; }
        public short RepliesNumber { get; set; }
        public DateTime Date { get; set; }

        public virtual string UserId { get; set; }
        public virtual string TopicTitle { get; set; }
    }
}
