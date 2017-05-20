using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace AskAbout.Models
{
    public class User : IdentityUser
    {
        public bool IsExpert { get; set; }
        public string Photo { get; set; }

        public virtual List<Comment> Comments { get; set; }
        public virtual List<Reply> Replies { get; set; }
        public virtual List<Question> Questions { get; set; }
        public virtual List<Like> Likes { get; set; }
        public virtual List<Rating> Rating { get; set; }
     }
}
