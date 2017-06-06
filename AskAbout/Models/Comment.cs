using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AskAbout.Models
{
    public class Comment
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessageResourceName = "EmptyText",
            ErrorMessageResourceType = typeof(Resources.Models.Comment))]
        [StringLength(1000, ErrorMessageResourceName = "Text",
            ErrorMessageResourceType = typeof(Resources.Models.Comment), MinimumLength = 20)]
        public string Text { get; set; }
        public DateTime Date { get; set; }
        public string Attachment { get; set; }

        public virtual User User { get; set; }
        public virtual Reply Reply { get; set; }
        public virtual List<Like> Likes { get; set; }
    }
}