using AskAbout.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AskAbout.ViewModels.Questions
{
    public class EditQuestionViewModel
    {
        public Question Question { get; set; }

        [Required(ErrorMessageResourceName = "EmptyTitle", ErrorMessageResourceType = typeof(Resources.CreateQuestionViewModel))]
        [StringLength(200, ErrorMessageResourceName = "Title", ErrorMessageResourceType = typeof(Resources.CreateQuestionViewModel), MinimumLength = 20)]
        public string Title { get; set; }

        [StringLength(1000, ErrorMessageResourceName = "Text", ErrorMessageResourceType = typeof(Resources.CreateQuestionViewModel), MinimumLength = 20)]
        public string Text { get; set; }
    }
}
