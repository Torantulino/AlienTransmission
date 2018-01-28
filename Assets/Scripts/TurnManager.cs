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

    public UIIOMan uiManager;

    private Turn currentTurn;
    private bool turnSetup;

    private void Start()
    {
		AlienList = GameObject.FindObjectsOfType<AlienAI>();
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

        EnterTurn(nextTurn, true);
    }

    public void EnterTurn(Turn turn, bool updateUi = false)
    {
        currentTurn = turn;
        turnSetup = false;

        switch (currentTurn)
        {
            case Turn.PlayerPlanning:
                EnterPlanningMode(updateUi);
                break;
            case Turn.PlayerMoving:
                EnterMovingMode();
                break;
        }
    }

    public void StopAllActions()
    {
        foreach (var soldier in SoldierList)
        {
            soldier.EndCommands();
        }

        foreach (var alien in AlienList)
        {
            alien.canAct = false;
        }
    }

    private void EnterPlanningMode(bool endTurn)
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

        if (endTurn)
        {
            uiManager.ClearOrders();

            foreach (var soldier in SoldierList)
            {
                soldier.GetComponent<SoldierReport>().Report();
            }

            foreach(var alien in AlienList)
            {
                if (alien.GetComponent<Health>().isDead)
                {
                    //Need to show the corpse - we could render a sprite at the same position rather than changing the layer
                    alien.gameObject.layer = 0;
                }
            }
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
            if (!alien.GetComponent<Health>().isDead)
            {
                alien.canAct = true;
            }
        }
    }
}
