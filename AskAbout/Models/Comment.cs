using System;
using System.Collections.Generic;

namespace AskAbout.Models
{
    public class Comment : BaseEntity
    {
//        [Required(ErrorMessageResourceName = "EmptyText",
//            ErrorMessageResourceType = typeof(Resources.Models.Comment))]
//        [StringLength(1000, ErrorMessageResourceName = "Text",
//            ErrorMessageResourceType = typeof(Resources.Models.Comment), MinimumLength = 20)]
        public string Text { get; set; }
        public DateTime Date { get; set; }
        public string Attachment { get; set; }

        public string UserId { get; set; }
        public int ReplyId { get; set; }

        public virtual User User { get; set; }
        public virtual Reply Reply { get; set; }
        public virtual ICollection<Like> Likes { get; set; }
    }
}