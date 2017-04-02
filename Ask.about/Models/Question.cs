using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ask.about.Models
{
    public class Question
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public int UserId { get; set; }
        public int Likes { get; set; }
        public short RepliesNumber { get; set; }
        public DateTime Date { get; set; }

        public virtual User User { get; set; }
        public virtual Topic Topic { get; set; }
    }
}
