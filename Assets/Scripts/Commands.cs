using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public static class Commands
{
    public const string MoveCommand = "MOVE";
    public const string FaceCommand = "FACE";
    public const string EngageCommand = "ENGAGE";
    public const string HelpCommand = "HELP";
    public const string UseCommand = "USE";


    private static string[] CommandList;

    static Commands()
    {
        var commandList = new List<String>();

        commandList.Add(MoveCommand);
        commandList.Add(FaceCommand);
        commandList.Add(EngageCommand);
        commandList.Add(HelpCommand);
        commandList.Add(UseCommand);

        CommandList = commandList.ToArray();
    }

    public static bool IsValidCommand(string command)
    {
        return CommandList.Contains(command);
    }
}

