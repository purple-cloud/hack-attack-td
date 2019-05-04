using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Defenses;

public class Clone : MonoBehaviour, IPointerDownHandler {
	public GameObject defensePrefab;
	public bool isDragging;

	/// <summary>
	/// Alert the controller when this game object is created.
	/// </summary>
	void Start() {
		CompController.Instance.InitClone(gameObject);

		// When escape is pressed, cancel placement of the structure 
		EventManager.onCancel += CompController.Instance.CancelPlacement;

		// When canvas is pressed AND structure is not being placed, cancel placement
		EventManager.onCanvasClick += () => {
			if (!isDragging) {
				CompController.Instance.CancelPlacement();
			}
		};
	}

	/// <summary>
	/// Tell the controller that the clone is ready to be placed.
	/// </summary>
	/// <param name="eventData"></param>
	public void OnPointerDown(PointerEventData eventData) {
		CompController.Instance.FinishPlacement();
		CompController.Instance.setNewStructureOutput = true;
	}

	void Update() {
		if (isDragging) {
			// Snap the clone object to the mouse
			gameObject.transform.position = new Vector3(Input.mousePosition.x, Input.mousePosition.y);
		}
	}
}
