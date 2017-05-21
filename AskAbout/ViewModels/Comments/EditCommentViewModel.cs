using AskAbout.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AskAbout.ViewModels.Comments
{
    public class EditCommentViewModel
    {
        public Comment Comment { get; set; }
        public int qid { get; set; }
    }
}
