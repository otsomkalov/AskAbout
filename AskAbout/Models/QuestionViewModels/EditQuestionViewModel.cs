using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace AskAbout.Models.QuestionViewModels
{
    public class EditQuestionViewModel
    {
        [Required]
        public string Text { get; set; }

        public int Qid { get; set; }
    }
}
