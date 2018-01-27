using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public static class Commands
{
    public const string MoveCommand = "MOVE";
    public const string FaceCommand = "FACE";
    public const string AttackCommand = "ATTACK";

    private static string[] CommandList;

    static Commands()
    {
        var commandList = new List<String>();

        commandList.Add(MoveCommand);
        commandList.Add(FaceCommand);
        commandList.Add(AttackCommand);

        CommandList = commandList.ToArray();
    }

    public static bool IsValidCommand(string command)
    {
        return CommandList.Contains(command);
    }
}

