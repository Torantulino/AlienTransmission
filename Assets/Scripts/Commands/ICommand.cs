﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public interface ICommand
{
    CommandEnum CommandType { get; }

    bool Completed { get; set; }
}

