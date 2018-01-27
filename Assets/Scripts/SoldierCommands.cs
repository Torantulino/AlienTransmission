using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.AI;

public class SoldierCommands : MonoBehaviour
{
    public float Damping = 1f;

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
            StartCoroutine(RunAction());
        }
    }

    private IEnumerator RunAction()
    {
        foreach(var command in CommandList)
        {
            yield return StartCoroutine(HandleCommands(command));
        }
    }

    private IEnumerator HandleCommands(ICommand command)
    {
        var currentCommand = CommandList.First();

        switch (currentCommand.CommandType)
        {
            case CommandEnum.Movement:
                yield return StartCoroutine(HandleMovementCommand((MovementCommand)currentCommand));
                break;
            case CommandEnum.FaceDirection:
                yield return StartCoroutine(HandleFaceCommand((FaceCommand)currentCommand));
                break;
        }
    }

    private void RemoveCommand()
    {
        CommandList.RemoveAt(0);
        if (!CommandList.Any())
        {
            CanAction = false;
        }
    }

    private IEnumerator HandleMovementCommand(MovementCommand command)
    {
        agent.destination = command.Destination;
        float dist = agent.remainingDistance;

        while (agent.remainingDistance != 0)
        {
            yield return null;
        }

    }

    private IEnumerator HandleFaceCommand(FaceCommand command)
    {
        var desiredRotQ = Quaternion.Euler(0, 45, 0);
        transform.rotation = Quaternion.Lerp(transform.rotation, desiredRotQ, Time.deltaTime * Damping);

        while (transform.rotation.y != 45)
        {
            yield return null;
        }

    }

}

