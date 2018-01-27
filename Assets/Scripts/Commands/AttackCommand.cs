using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class AttackCommand : ICommand
{
    public CommandEnum CommandType
    {
        get
        {
            return CommandEnum.Attack;
        }
    }
}
