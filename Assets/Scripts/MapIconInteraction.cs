using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MapIconInteraction : MonoBehaviour
{
 
    void OnGUI()
    {
        var evt = Event.current;

        if (evt.type == EventType.MouseDown && evt.button == 1)
        {
            Destroy(this.gameObject);
        }
    }
}
