using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AskAbout.Models
{
    public class Reply
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessageResourceName = "EmptyText",
            ErrorMessageResourceType = typeof(Resources.Models.Reply))]
        [StringLength(5000, ErrorMessageResourceName = "Text",
            ErrorMessageResourceType = typeof(Resources.Models.Reply), MinimumLength = 20)]
        public string Text { get; set; }
        public DateTime Date { get; set; }
        public string Attachment { get; set; }

        public virtual User User { get; set; }
        public virtual Question Question { get; set; }
        public virtual List<Comment> Comments { get; set; }
        public virtual List<Like> Likes { get; set; }
    }
}