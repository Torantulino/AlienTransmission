using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridCoords {
	public Camera AOCam;
	public Vector2Int gridPartitions = new Vector2Int(20, 20);

	/*public Vector3 GridCoordToWorld(string gridCoord) {
		int x = gridCoord.ToUpper().ToCharArray()[0] - 'A';
		int y = int.Parse(gridCoord.Substring(1)) - 1;

		return new Vector3(x * gridSize, y * gridSize);
	}*/

	void Start() {
		
	}
	
	void Update() {
		
	}
}
