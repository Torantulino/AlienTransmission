using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MapAnnotator : MonoBehaviour
{
    private GameObject selectedIcon;
    private GameObject targetedIcon;
    public RectTransform panelRect;
    public Camera mainCam;

	// Use this for initialization
	void Start () {
		
	}

    public void placeIcon(Vector2 pos)
    {

    }

    void OnGUI()
    {
        Event evnt = Event.current;
        Vector2 localCoord = new Vector2(0,0);
        if (Input.GetMouseButtonDown(0))
        {
            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(panelRect, evnt.mousePosition, mainCam, out localCoord))
            {
                Debug.Log(localCoord);
            }
        }
    }


    public void deleteIcon()
    {
        
    }

}
