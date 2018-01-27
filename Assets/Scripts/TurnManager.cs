using System;
using System.Collections.Generic;
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

    private void Start()
    {
        EnterTurn(Turn.PlayerPlanning);
    }

    public void EnterTurn(Turn turn)
    {
        currentTurn = turn;

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
            alien.canAct = false;
        }
    }

    private void EnterPlayerMovingMode()
    {
        TransmitButton.interactable = true;

        foreach (var soldier in SoldierList)
        {
            soldier.CanAction = true;
        }
    }

    private void EnterAlienMovingMode()
    {
        foreach (var alien in AlienList)
        {
            alien.canAct = false;
        }
    }
}
