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

		var mousePos = Input.mousePosition;

		var rect = GetComponent<RectTransform>();
		var posRect = rect.rect;

		if (evt.type == EventType.MouseDown && evt.button == 1 && rect.rect.Contains(mousePos - rect.position))
        {
            Destroy(this.gameObject);
        }
    }
}
