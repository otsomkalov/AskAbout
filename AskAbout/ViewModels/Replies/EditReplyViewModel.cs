using AskAbout.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AskAbout.ViewModels.Replies
{
    public class EditReplyViewModel
    {
        public Reply Reply { get; set; }
        public int? qid { get; set; }
    }
}
