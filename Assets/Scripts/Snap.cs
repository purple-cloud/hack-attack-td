
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class Snap : MonoBehaviour {
	private Grid grid;
	private GameObject obj;

	void Start() {
		grid = GameObject.Find("Grid").GetComponent<Grid>();
		obj = GameObject.Find("wf");
	}

	void Update() {
		Vector3Int mousePosVector = new Vector3Int((int) Input.mousePosition.x, (int)Input.mousePosition.y, 1);
		obj.transform.position = grid.LocalToWorld(grid.CellToLocalInterpolated(mousePosVector));
	}
}
