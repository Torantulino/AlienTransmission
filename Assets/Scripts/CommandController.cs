using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class CommandController : MonoBehaviour
{
    public List<GameObject> SoldierList;

    private InputField commandField;

    public void Start()
    {
        commandField = GetComponent<InputField>();
        commandField.onEndEdit.AddListener(delegate { SubmitCommand(); });
    }

    public void SubmitCommand()
    {
        var commandText = commandField.text;

        HandleCommand(commandText);
    }

    public void HandleCommand(string commandText)
    {
        var splitText = commandText.Split(' ');

        if (splitText.Length != 3)
            HandleInvalid(commandField);

        var unitName = splitText[0];
        var mainCommand = splitText[1];
        var commandInfo = splitText[2];

        var solider = SoldierList.FirstOrDefault(x => x.name.ToUpper() == unitName);

        if (solider == null)
            HandleInvalid(commandField);

        if (!Commands.IsValidCommand(mainCommand))
        {
            HandleInvalid(commandField);
        }

        //Otherwise add action to soldier
        var commands = solider.GetComponent<SoldierCommands>();
        var movementCommand = new MovementCommand()
        {
            Destination = new Vector3(180f, 0.5f, 1456.52f)
        };

        var faceCommand = new FaceCommand()
        {
            //Destination = new Vector3(180f, 0.5f, 1456.52f)
        };

        commands.CommandList.Add(movementCommand);
        commands.CommandList.Add(faceCommand);

    }

    private void HandleInvalid(InputField commandField)
    {
        commandField.text = "ERROR: INVALID COMMAND";
        commandField.GetComponent<Text>().color = Color.red;

        //commandField.text = Color.red;s
    }
}

