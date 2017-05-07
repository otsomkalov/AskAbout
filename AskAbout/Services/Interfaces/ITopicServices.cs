﻿using AskAbout.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AskAbout.Services.Interfaces
{
    public interface ITopicServices
    {
        Topic Get(string title);

        List<Topic> GetTopics();
    }
}
