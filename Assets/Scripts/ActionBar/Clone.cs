using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Defenses;

/// <summary>
/// Class <c>Clone</c> creates a literal "clone" 
/// of an selected component when used
/// </summary>
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

    /// <summary>
    /// Is called every frame and updates the clone's position in the canvas
    /// to follow the position of the user's mouse if field isDragging == true
    /// </summary>
	void Update() {
		if (isDragging) {
			gameObject.transform.position = new Vector3(Input.mousePosition.x, Input.mousePosition.y);
		}
	}
}
