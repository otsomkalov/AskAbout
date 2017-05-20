using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AskAbout.Models
{
    public class Comment
    {
        [Key]
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int Rating { get; set; }
        public string Text { get; set; }
        public string Attachment { get; set; }

        public virtual User User { get; set; }
        public virtual Reply Reply { get; set; }
    }
}
