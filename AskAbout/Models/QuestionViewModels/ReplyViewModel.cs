using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AskAbout.Models.QuestionViewModels
{
    public class ReplyViewModel
    {
        [Required]
        public string Reply { get; set; }

        public int Qid { get; set; }
    }
}
