using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class CommandController : MonoBehaviour
{
    public GameObject Agent;

    public InputField[] CommandList;

    public void Start()
    {
        if (CommandList != null)
        {
            foreach(var command in CommandList)
            {
                command.onEndEdit.AddListener(delegate { SubmitCommand(command); });
            }
        }
    }

    public void SubmitCommand(InputField commandField)
    {
        var commandText = commandField.text;

        var splitText = commandText.Split(' ');

        var mainCommand = splitText[0];

        if (!Commands.IsValidCommand(mainCommand))
        {
            HandleInvalid(commandField);
        }

        //Otherwise add action to soldier

    }

    private void HandleInvalid(InputField commandField)
    {
        commandField.text = "ERROR: INVALID COMMAND";
        commandField.GetComponent<Text>().color = Color.red;

        //commandField.text = Color.red;
    }
}

