using AskAbout.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AskAbout.ViewModels.Questions
{
    public class DetailsQuestionViewModel
    {
        public Question Question { get; set; }
        public Reply Reply { get; set; }
        public int id { get; set; }
        public Comment Comment { get; set; }
    }
}
