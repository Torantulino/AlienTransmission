using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class HelpCommand : ICommand
{
    public CommandEnum CommandType
    {
        get
        {
            return CommandEnum.Help;
        }
    }

    public bool Completed { get; set; }
    public SoldierInfo SoldierToHeal { get; set; }
    public Vector2Int TargetPosition { get; set; }
}
