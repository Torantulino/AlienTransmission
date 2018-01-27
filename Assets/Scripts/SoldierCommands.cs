using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.AI;

public class SoldierCommands : MonoBehaviour
{
    public List<ICommand> CommandList { get; set; }
    public bool CanAction { get; set; }

    private NavMeshAgent agent;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        CommandList = new List<ICommand>();
        CanAction = true;
    }

    private void Update()
    {
        if (CanAction && CommandList.Any())
        {
            var currentCommand = CommandList.First();

            switch(currentCommand.CommandType)
            {
                case CommandEnum.Movement:
                    HandleMovemenCommand((MovementCommand) currentCommand);
                    break;
            }
        }
    }

    private void HandleMovemenCommand(MovementCommand command)
    {
        agent.destination = command.Destination;
        CanAction = false;
    }

}

