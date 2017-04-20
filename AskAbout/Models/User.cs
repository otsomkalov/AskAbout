using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace AskAbout.Models
{
    public class User : IdentityUser
    {
        public int Rating { get; set; }
        public bool IsExpert { get; set; }

        public List<Comment> Comments { get; set; }
        public List<Reply> Replies { get; set; }
        public List<Question> Questions { get; set; }
    }
}
