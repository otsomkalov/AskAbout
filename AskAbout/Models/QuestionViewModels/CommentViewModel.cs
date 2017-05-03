using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AskAbout.Models.QuestionViewModels
{
    public class CommentViewModel
    {
        [Required]
        public string Comment { get; set; }

        public int Qid { get; set; }

        public int Rid { get; set; }
    }
}
