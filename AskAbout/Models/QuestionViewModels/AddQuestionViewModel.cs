using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AskAbout.Models.QuestionViewModels
{
    public class AddQuestionViewModel
    {
        [Required]
        public string Text { get; set; }

        [Required]
        public string Topic { get; set; }
    }
}
