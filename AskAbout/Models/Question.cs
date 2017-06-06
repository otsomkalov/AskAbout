using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AskAbout.Models
{
    public class Question
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessageResourceName = "EmptyTitle",
            ErrorMessageResourceType = typeof(Resources.Models.Question))]
        [StringLength(200, ErrorMessageResourceName = "Title",
            ErrorMessageResourceType = typeof(Resources.Models.Question), MinimumLength = 20)]
        public string Title { get; set; }

        [StringLength(5000, ErrorMessageResourceName = "Text",
            ErrorMessageResourceType = typeof(Resources.Models.Question), MinimumLength = 20)]
        public string Text { get; set; }

        public DateTime Date { get; set; }
        public string Attachment { get; set; }

        public virtual User User { get; set; }
        public virtual Topic Topic { get; set; }
        public virtual List<Reply> Replies { get; set; }
        public virtual List<Like> Likes { get; set; }
    }
}