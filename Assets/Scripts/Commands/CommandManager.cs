using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class CommandManager : MonoBehaviour
{
    public GameObject CurrentUnit;
    public float CommandTime;

    private IEnumerator DoMovement(NavMeshAgent navAgent, Vector3 position)
    {
        navAgent.isStopped = false;
        navAgent.destination = position;
        yield return new WaitForSeconds(CommandTime);
        navAgent.isStopped = true;
    }

    private void MoveUnit(Vector3 position)
    {
        if (CurrentUnit != null)
        {
            var navAgent = CurrentUnit.GetComponent<NavMeshAgent>();
            navAgent.destination = position;
        }
    }

    //QUick test code
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //TODO: Click on unit
            //Issue command
            //Once unit has three commands
            //Run commands



            //Click on position
            //Find nearest grid position
            //Move for X seconds

            //Player should select grid position
            //Grid position should be converted to world point

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                var hitObj = hit.collider.gameObject;

                if (hitObj.GetComponent<NavigateTo>())
                {
                    CurrentUnit = hitObj;
                    Debug.Log("unit selected is " + CurrentUnit.name);
                }
                else
                {
                    MoveUnit(hit.point);
                }
            }
        }
    }

}

