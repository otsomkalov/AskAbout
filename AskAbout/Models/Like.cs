using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AskAbout.Models
{
    public class Like
    {
        public string UserId { get; set; }
        public int QuestionId{ get; set; }

        public User User { get; set; }
        public Question Question { get; set; }
    }
}
