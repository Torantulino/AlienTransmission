using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class CommandController : MonoBehaviour
{
    public List<SoldierInfo> SoldierList;
    public GridScript gridScript;

    public void UpdateCommands(string[,] cmdArray)
    {
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                var value = cmdArray[j, i];

                if (!string.IsNullOrEmpty(value))
                {
                    HandleCommand(cmdArray[j, i]);
                }
            }
        }
    }

    public void HandleCommand(string commandText)
    {
        var splitText = commandText.Split(' ');

        var unitName = splitText[0];
        var mainCommand = splitText[1];
        var commandInfo = "";

        if (splitText.Count() > 2)
            commandInfo = splitText[2];

        var solider = SoldierList.FirstOrDefault(x => x.Name.ToUpper() == unitName);

        //Otherwise add action to soldier
        var commands = solider.GetComponent<SoldierCommands>();

        if (mainCommand == Commands.MoveCommand)
        {
            var destPos = gridScript.GridStringToCoords(commandInfo);
            var destination = gridScript.GridCoordstoWorld(destPos);
            destination.y = 0f;
            var movementCommand = new MovementCommand()
            {
                Destination = destination
            };

            commands.CommandList.Add(movementCommand);
        }
        else
        {
            var destPos = gridScript.GridStringToCoords(commandInfo);
            var destination = gridScript.GridCoordstoWorld(destPos);
            destination.y = 0f;

            var faceCommand = new FaceCommand()
            {
                Target = destination
            };

            commands.CommandList.Add(faceCommand);
        }
    }

    private void HandleInvalid(InputField commandField)
    {
        commandField.text = "ERROR: INVALID COMMAND";
        commandField.GetComponent<Text>().color = Color.red;

        //commandField.text = Color.red;s
    }
}

