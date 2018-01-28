using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class Objective : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<SoldierInfo>() != null)
        {
            Debug.Log("You won the game");
        }
    }
}

