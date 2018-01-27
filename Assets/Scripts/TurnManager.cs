using System;
using System.Collections;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class TurnManager : MonoBehaviour
{
    public float TurnTime = 5f;

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
        if ((currentTurn == Turn.PlayerMoving) && !turnSetup)
        {
            StartCoroutine(StartTurn(TurnTime));
        }
    }

    private IEnumerator StartTurn(float timeToWait)
    {
        turnSetup = true;

        yield return new WaitForSeconds(timeToWait);

        var nextTurn = Turn.PlayerPlanning;

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
                EnterMovingMode();
                break;
        }
    }

    private void EnterPlanningMode()
    {
        TransmitButton.interactable = true;

        foreach (var soldier in SoldierList)
        {
            soldier.EndCommands();
        }

        foreach (var alien in AlienList)
        {
            alien.canAct = false;
        }
    }

    private void EnterMovingMode()
    {
        TransmitButton.interactable = false;

        foreach (var soldier in SoldierList)
        {
            soldier.EnableActions();
        }

        foreach (var alien in AlienList)
        {
            alien.canAct = true;
        }
    }
}
