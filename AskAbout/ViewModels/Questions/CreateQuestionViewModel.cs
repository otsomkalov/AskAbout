using System.Collections.Generic;
using AskAbout.Models;

namespace AskAbout.ViewModels.Questions
{
    public class CreateQuestionViewModel
    {
        public Question Question { get; set; }
        public List<Topic> Topics { get; set; }
    }
}