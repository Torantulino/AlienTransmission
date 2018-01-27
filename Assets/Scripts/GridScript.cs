using UnityEngine;
using System.Collections;
using System;

public class GridScript : MonoBehaviour
{

    //public GameObject plane;

	float gridSizeX;
    float gridSizeY;
    float gridSizeZ;

    float largeStep;

	Camera cam;

    float startX;
    float startY;
    float startZ;

    private Material lineMaterial;

    public Color mainColor = new Color(0f, 1f, 0f, 1f);
	public Color selectColor = new Color(1f, 1f, 1f, 0.5f);

	public int xSelected = -1;
	public int ySelected = -1;

	void Start() {
		cam = GetComponent<Camera>();
	}

    void CreateLineMaterial()
    {
        if (!lineMaterial)
        {
            // Unity has a built-in shader that is useful for drawing
            // simple colored things.
            var shader = Shader.Find("Hidden/Internal-Colored");
            lineMaterial = new Material(shader);
            lineMaterial.hideFlags = HideFlags.HideAndDontSave;
            // Turn on alpha blending
            lineMaterial.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
            lineMaterial.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
            // Turn backface culling off
            lineMaterial.SetInt("_Cull", (int)UnityEngine.Rendering.CullMode.Off);
            // Turn off depth writes
            lineMaterial.SetInt("_ZWrite", 0);
        }
    }

	public Vector2Int GridStringToCoords(string gridString) {
		try {
			int x = gridString.ToUpper().ToCharArray()[0] - 'A';
			int y = int.Parse(gridString.Substring(1)) - 1;
			return new Vector2Int(x, y);
		} catch (Exception e) {
			return new Vector2Int(-1, -1);
		}
	}

	public Vector3 GridCoordstoWorld(Vector2Int gridCoord) {
		float x = gridCoord.x * largeStep - cam.orthographicSize * cam.aspect + largeStep / 2 + 0.5f;
		float z = gridCoord.y * largeStep - cam.orthographicSize + largeStep / 2 + 0.5f;

		return cam.transform.position + new Vector3(x, 0, z);
	}

    void OnPostRender()
    {
		startX = transform.position.x - cam.orthographicSize * cam.aspect + 0.5f;
		startZ = transform.position.z - cam.orthographicSize + 0.5f;
		startY = transform.position.y - 1;

		gridSizeX = (cam.orthographicSize * cam.aspect) * 2 - 1;
		gridSizeZ = cam.orthographicSize * 2 - 1;

		largeStep = gridSizeX / 26.0f;

        CreateLineMaterial();
        // set the current material
        lineMaterial.SetPass(0);

        GL.Begin(GL.LINES);
        GL.Color(mainColor);

        //Layers

        //X axis lines
        for (float i = 0; i < gridSizeZ; i += largeStep)
        {
            GL.Vertex3(startX, startY, startZ + i);
            GL.Vertex3(startX + gridSizeX, startY, startZ + i);
        }

        //Z axis lines
        for (float i = 0; i < gridSizeX; i += largeStep)
        {
            GL.Vertex3(startX + i, startY, startZ);
            GL.Vertex3(startX + i, startY, startZ + gridSizeZ);
        }

		GL.Vertex3(startX + gridSizeX, startY, startZ);
		GL.Vertex3(startX + gridSizeX, startY, startZ + gridSizeZ);

		GL.Vertex3(startX, startY, startZ + gridSizeZ);
		GL.Vertex3(startX + gridSizeX, startY, startZ + gridSizeZ);

        GL.End();

		if (xSelected > -1) {
			GL.Begin(GL.QUADS);
			GL.Color(selectColor);

			GL.Vertex3(startX + xSelected * largeStep, startY, startZ);
			GL.Vertex3(startX + xSelected * largeStep, startY, startZ + gridSizeZ);
			GL.Vertex3(startX + (xSelected + 1) * largeStep, startY, startZ + gridSizeZ);
			GL.Vertex3(startX + (xSelected + 1) * largeStep, startY, startZ);
			GL.End();
		}

		if (ySelected > -1) {
			GL.Begin(GL.QUADS);
			GL.Color(selectColor);

			GL.Vertex3(startX, startY, startZ + ySelected * largeStep);
			GL.Vertex3(startX + gridSizeX, startY, startZ + ySelected * largeStep);
			GL.Vertex3(startX + gridSizeX, startY, startZ + (ySelected + 1) * largeStep);
			GL.Vertex3(startX, startY, startZ + (ySelected + 1) * largeStep);
			GL.End();
		}
    }
}
