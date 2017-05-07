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

        public string UserId { get; set; }
        public User User { get; set; }
        public List<Comment> Comments { get; set; }
        public Question Question { get; set; }
    }
}
