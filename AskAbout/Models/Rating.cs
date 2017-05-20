using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AskAbout.Models
{
    public class Rating
    {
        public int Id { get; set; }
        public int Amount { get; set; }

        public virtual User User { get; set; }
        public virtual Topic Topic { get; set; }       
    }
}
