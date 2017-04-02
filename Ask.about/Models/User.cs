using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ask.about.Models
{
    public class User
    {
        public int Id { get; set;}
        public string Login { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public int Rating { get; set; }
        public bool IsExpert { get; set; }

        public virtual List<Comment> Comments { get; set; }
        public virtual List<Reply> Replies { get; set; }
        public virtual List<Question> Questions { get; set; }
    }
}
