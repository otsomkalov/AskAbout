using AskAbout.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AskAbout.ViewModels.Questions
{
    public class CreateQuestionViewModel
    {
        public IEnumerable<Topic> Topics { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
    }
}
