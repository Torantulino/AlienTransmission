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

    public void SelectIcon()
    {
        selectedIcon = EventSystem.current.currentSelectedGameObject.transform.GetChild(0).gameObject;
    }

    public void PlaceIcon(Vector2 pos)
    {
       // Instantiate(selectedIcon, new Vector3(2, 0, 0), Quaternion.identity);
    }

    void OnGUI()
    {
		Vector2 localCoord;
		AOCam.aspect = panelRect.rect.width / panelRect.rect.height;

        if (Input.GetMouseButtonDown(0))
        {
			if (RectTransformUtility.ScreenPointToLocalPointInRectangle(panelRect, Input.mousePosition, null, out localCoord))
            {
				localCoord += panelRect.rect.size / 2;
				float camX = localCoord.x * AOCam.pixelRect.width / panelRect.rect.width;
				float camY = localCoord.y * AOCam.pixelRect.height / panelRect.rect.height;
				Ray worldRay = AOCam.ScreenPointToRay(new Vector2(camX, camY));	
				Vector3 worldPoint = worldRay.GetPoint(5);
            }
            PlaceIcon((Input.mousePosition));
        }
    }


    public void deleteIcon()
    {
        
    }

}
