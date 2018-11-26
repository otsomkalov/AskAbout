using System;
using System.Collections.Generic;

namespace AskAbout.Models
{
    public class Reply : BaseEntity
    {
//        [Required(ErrorMessageResourceName = "EmptyText",
//            ErrorMessageResourceType = typeof(Resources.Models.Reply))]
//        [StringLength(5000, ErrorMessageResourceName = "Text",
//            ErrorMessageResourceType = typeof(Resources.Models.Reply), MinimumLength = 20)]
        public string Text { get; set; }
        public DateTime Date { get; set; }
        public string Attachment { get; set; }

        public string UserId { get; set; }
        public int QuestionId { get; set; }

        public virtual User User { get; set; }
        public virtual Question Question { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Like> Likes { get; set; }
    }
}