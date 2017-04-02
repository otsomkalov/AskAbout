using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ask.about.Models
{
    public class Comment
    {
        [Key, Column(Order = 0)]
        public int UserId { get; set; }

        [Key, Column(Order = 1)]
        public DateTime DateId { get; set; }
        public int Rating { get; set; }
        public string Text { get; set; }

        public virtual User User { get; set; }
        public virtual Question Question { get; set; }
    }
}
