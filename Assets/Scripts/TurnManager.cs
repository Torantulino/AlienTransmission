using System;
using System.Collections;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class TurnManager : MonoBehaviour
{
    public float PlayerTurnTime = 20f;
    public float AlienTurnTime = 10f;

    public Button TransmitButton;

    public SoldierCommands[] SoldierList;
    public AlienAI[] AlienList;

    private Turn currentTurn;
    private bool turnSetup;

    private void Start()
    {
        EnterTurn(Turn.PlayerPlanning);
    }

    private void Update()
    {
        if ((currentTurn == Turn.PlayerMoving || currentTurn == Turn.Alien) && !turnSetup)
        {
            float timeToWait = 0f;

            if (currentTurn == Turn.PlayerMoving)
            {
                timeToWait = PlayerTurnTime;
            }
            else
            {
                timeToWait = AlienTurnTime;
            }

            StartCoroutine(StartTurn(timeToWait));
        }
    }

    private IEnumerator StartTurn(float timeToWait)
    {
        turnSetup = true;

        yield return new WaitForSeconds(timeToWait);

        var nextTurn = Turn.Empty;

        if (currentTurn == Turn.PlayerMoving)
        {
            nextTurn = Turn.Alien;
        }
        else
        {
            nextTurn = Turn.PlayerPlanning;
        }

        EnterTurn(nextTurn);
    }

    public void EnterTurn(Turn turn)
    {
        currentTurn = turn;
        turnSetup = false;

        switch (currentTurn)
        {
            case Turn.PlayerPlanning:
                EnterPlanningMode();
                break;
            case Turn.PlayerMoving:
                EnterPlayerMovingMode();
                break;
            case Turn.Alien:
                EnterAlienMovingMode();
                break;
        }
    }

    private void EnterPlanningMode()
    {
        TransmitButton.interactable = true;
        
        foreach(var alien in AlienList)
        {
            alien.canAct = true;
        }
    }

    private void EnterPlayerMovingMode()
    {
        TransmitButton.interactable = false;

        foreach (var soldier in SoldierList)
        {
            soldier.EnableActions();
        }
    }

    private void EnterAlienMovingMode()
    {
        foreach (var soldier in SoldierList)
        {
            soldier.EndCommands();
        }

        foreach (var alien in AlienList)
        {
            alien.canAct = true;
        }
    }
}
