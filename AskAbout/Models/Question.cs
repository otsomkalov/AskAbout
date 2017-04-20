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
        public string Text { get; set; }
        public int Likes { get; set; }
        public short RepliesNumber { get; set; }
        public DateTime Date { get; set; }

        public string UserId { get; set; }
        public string TopicTitle { get; set; }
        public User User { get; set; }
        public Topic Topic { get; set; }
        public List<Reply> Replies { get; set; }
    }
}
