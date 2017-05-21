using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AskAbout.Models
{
    public class Question
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; }
        public string Attachment { get; set; }

        public virtual User User { get; set; }
        public virtual Topic Topic { get; set; }
        public virtual List<Reply> Replies { get; set; }
        public virtual List<Like> Likes { get; set; }
    }
}
