using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace AskAbout.Models
{
    public class User : IdentityUser
    {
        public bool IsExpert { get; set; }
        public string Photo { get; set; }

        public virtual ICollection<Question> Questions { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Reply> Replies { get; set; }
        public virtual ICollection<Like> Likes { get; set; }
        public virtual ICollection<Rating> Rating { get; set; }
    }
}