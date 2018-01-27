using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class CommandController : MonoBehaviour
{
    public List<GameObject> SoldierList;

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

        var solider = SoldierList.FirstOrDefault(x => x.name.ToUpper() == unitName);

        //Otherwise add action to soldier
        var commands = solider.GetComponent<SoldierCommands>();

        if (mainCommand == Commands.MoveCommand)
        {
            var movementCommand = new MovementCommand()
            {
                Destination = new Vector3(180f, 0.5f, 1456.52f)
            };

            commands.CommandList.Add(movementCommand);
        }
        else
        {
            var faceCommand = new FaceCommand()
            {
                Target = new Vector3(190f, 0.5f, 1400f)
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

