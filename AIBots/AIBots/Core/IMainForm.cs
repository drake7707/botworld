﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AIBots
{
    public interface IMainForm<Bot>
    {
        Bot SelectedBot { get; }
    }
}
