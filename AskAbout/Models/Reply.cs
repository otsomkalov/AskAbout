using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AskAbout.Models
{
    public class Reply
    {
        [Key]
        public int Id { get; set; }
        public int Rating { get; set; }
        public DateTime Date { get; set; }
        public int CommentariesCount { get; set; }
        public string Text { get; set; }
        public string Attachment { get; set; }

        public virtual User User { get; set; }
        public virtual Question Question { get; set; }
        public virtual List<Comment> Comments { get; set; }        
    }
}
