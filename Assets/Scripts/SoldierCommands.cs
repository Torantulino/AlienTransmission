using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.AI;

public class SoldierCommands : MonoBehaviour
{
    public float Damping = 10f;

    public List<ICommand> CommandList { get; set; }
    public bool CanAction { get; set; }

    public Animator soldierAnimator;

    private NavMeshAgent agent;
    private Shoot shoot;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        CommandList = new List<ICommand>();
        CanAction = false;
        shoot = GetComponent<Shoot>();
    }

    private void Update()
    {
        if (CanAction && CommandList.Any(x => !x.Completed))
        {
            StartCoroutine(RunAction());
        }
    }

    private IEnumerator RunAction()
    {
        RemoveCommands();
        while (CommandList.Any(x => !x.Completed))
        {
            var currentCommand = CommandList.FirstOrDefault(x => !x.Completed);
            yield return StartCoroutine(HandleCommands(currentCommand));
        }
    }

    private IEnumerator HandleCommands(ICommand currentCommand)
    {
        switch (currentCommand.CommandType)
        {
            case CommandEnum.Movement:
                yield return StartCoroutine(HandleMovementCommand((MovementCommand)currentCommand));
                break;
            case CommandEnum.FaceDirection:
                yield return StartCoroutine(HandleFaceCommand((FaceCommand)currentCommand));
                break;
            case CommandEnum.Attack:
                HandleEngage((AttackCommand)currentCommand);
                break;
        }
    }

    private void RemoveCommands()
    {
        if (CommandList.Any(x => x.Completed))
        {
            CommandList.RemoveAt(0);
        }

        if (!CommandList.Any())
        {
            CommandList.Clear();
            CanAction = true;
        }
    }

    private IEnumerator HandleMovementCommand(MovementCommand command)
    {
        soldierAnimator.SetBool(0, true);
        agent.destination = command.Destination;

        if (agent.pathPending)
            yield return null;

        while (agent.remainingDistance != 0)
        {
            yield return null;
        }

        command.Completed = true;
        soldierAnimator.SetBool(0, false);
    }

    private IEnumerator HandleFaceCommand(FaceCommand command)
    {
        float angle = Vector3.Angle(command.Target, transform.forward);
        var desiredRotQ = Quaternion.Euler(0, angle, 0);
        transform.rotation = Quaternion.Lerp(transform.rotation, desiredRotQ, Time.deltaTime * Damping);

        while(transform.rotation.y != angle)
        {
            yield return null;
        }

        command.Completed = true;
    }

    private void HandleEngage(AttackCommand command)
    {
        shoot.Fire();
        command.Completed = true;
    }

}

