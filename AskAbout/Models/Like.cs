using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AskAbout.Models
{
    public class Like
    {
        [Key]
        public int Id { get; set; }
        public bool? IsLiked { get; set; }

        public virtual User User { get; set; }
        public virtual Question Question { get; set; }
        public virtual Reply Reply { get; set; }
        public virtual Comment Comment { get; set; }
    }
}
