using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MapAnnotator : MonoBehaviour
{
	public Camera AOCam;

    private GameObject selectedIcon;
    private GameObject targetedIcon;
    RectTransform panelRect;

	void Start () {
		panelRect = GetComponent<RectTransform>();
	}

    public void placeIcon(Vector2 pos)
    {

    }

    void OnGUI()
    {
		Vector2 localCoord;
        if (Input.GetMouseButtonDown(0))
        {
			if (RectTransformUtility.ScreenPointToLocalPointInRectangle(panelRect, Input.mousePosition, null, out localCoord))
            {
				AOCam.aspect = panelRect.rect.width / panelRect.rect.height;
				localCoord += panelRect.rect.size / 2;
				float x = localCoord.x * AOCam.pixelRect.width / panelRect.rect.width;
				float y = localCoord.y * AOCam.pixelRect.height / panelRect.rect.height;
				Ray worldRay = AOCam.ScreenPointToRay(new Vector2(x, y));
				Vector3 worldPoint = worldRay.GetPoint(5);

				Debug.Log(worldPoint);
            }
        }
    }


    public void deleteIcon()
    {
        
    }

}
