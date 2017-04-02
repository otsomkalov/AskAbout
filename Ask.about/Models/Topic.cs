using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Ask.about.Models
{
    public class Topic
    {
        [Key]
        public string Title { get; set; }
        public int QuestionsNumber { get; set; }
        public int RepliesNumber { get; set; }
        public int Rating { get; set; }
        public int UsersCount { get; set; }

        public virtual List<Question> Questions { get; set; }
    }
}
