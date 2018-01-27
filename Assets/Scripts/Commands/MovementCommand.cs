﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class MovementCommand : ICommand
{
    public CommandEnum CommandType
    {
        get
        {
            return CommandEnum.Movement;
        }
    }

    public Vector3 Destination { get; set; }
}
