using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class FaceCommand : ICommand
{
    public CommandEnum CommandType
    {
        get
        {
            return CommandEnum.FaceDirection;
        }
    }

    public DirectionEnum Direction { get; set; }
}
