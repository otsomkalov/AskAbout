using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AskAbout.Models
{
    public class Question : BaseEntity
    {
//        [Required(ErrorMessageResourceName = "EmptyTitle",
//            ErrorMessageResourceType = typeof(Resources.Models.Question))]
//        [StringLength(200, ErrorMessageResourceName = "Title",
//            ErrorMessageResourceType = typeof(Resources.Models.Question), MinimumLength = 20)]
        public string Title { get; set; }

//        [StringLength(5000, ErrorMessageResourceName = "Text",
//            ErrorMessageResourceType = typeof(Resources.Models.Question), MinimumLength = 20)]
        public string Text { get; set; }

        public DateTime Date { get; set; }
        public string Attachment { get; set; }

        public string UserId { get; set; }
        public int TopicId { get; set; }

        public virtual User User { get; set; }
        public virtual Topic Topic { get; set; }
        public virtual ICollection<Reply> Replies { get; set; }
        public virtual ICollection<Like> Likes { get; set; }
    }
}