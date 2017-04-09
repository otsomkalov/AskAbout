using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Ask.about.Models
{
    public class Reply
    {
        [Key]
        public int QuestionId { get; set; }
        [Key]
        public int UserId { get; set; }
        public int Rating { get; set; }
        [Key]
        public DateTime Date { get; set; }
        public int CommentariesNumber { get; set; }
        public string Text { get; set; }

        public virtual User User { get; set; }
        public virtual List<Comment> Comments { get; set; }
    }
}
