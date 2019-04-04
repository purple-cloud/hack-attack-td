using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Defenses;

public class Clone : MonoBehaviour, IPointerClickHandler {
	public GameObject defensePrefab;
	public bool isDragging;

	/// <summary>
	/// Alert the controller when this game object is created.
	/// </summary>
	void Start() {
		CompController.Instance.InitClone(gameObject);
	}

	/// <summary>
	/// Tell the controller that the clone is ready to be placed.
	/// </summary>
	/// <param name="eventData"></param>
	public void OnPointerClick(PointerEventData eventData) {
		CompController.Instance.FinishPlacement();
	}

	void Update() {
		// Check if escape or right mouse is clicked and if the clone is on the display
		if (Input.GetMouseButtonUp(1) || Input.GetButtonDown("Cancel")) {
			CompController.Instance.CancelPlacement();
		}

		if (isDragging) {
			// Snap the clone object to the mouse
			gameObject.transform.position = new Vector3(Input.mousePosition.x, Input.mousePosition.y);
		}
	}
}
