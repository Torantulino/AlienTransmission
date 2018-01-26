using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class CommandManager : MonoBehaviour
{
    public GameObject CurrentUnit;
    public float CommandTime;

    public void MoveUnit(Vector3 position)
    {
        if (CurrentUnit != null && CurrentUnit.GetComponent<NavMeshAgent>() != null)
        {
            var navMesh = CurrentUnit.GetComponent<NavMeshAgent>();
            navMesh.destination = position;
            StartCoroutine(DoMovement(navMesh, position));
        }
    }

    private IEnumerator DoMovement(NavMeshAgent navMesh, Vector3 position)
    {
        navMesh.isStopped = false;
        navMesh.destination = position;
        yield return new WaitForSeconds(CommandTime);
        navMesh.isStopped = true;
    }

    //QUick test code
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //Click on position
            //Find nearest grid position
            //Move for X seconds

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                MoveUnit(hit.point);
            }
        }
    }

}

