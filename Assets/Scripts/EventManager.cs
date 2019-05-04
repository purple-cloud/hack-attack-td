using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// Contains all of the custom events needed among scripts.
/// </summary>
public class EventManager : MonoBehaviour {
	#region DELEGATES

	public delegate void Cancel();
	public delegate void CanvasClick();

	#endregion

	/// <summary>
	/// When cancel has been pressed.
	/// </summary>
	public static event Cancel onCancel;

	/// <summary>
	/// When empty space on canvas is pressed with either one of the mouse buttons.
	/// </summary>
	public static event CanvasClick onCanvasClick;

	void OnGUI() {
		if (Input.GetButtonDown("Cancel") || 
			( !EventSystem.current.IsPointerOverGameObject())) {
			// Notify all subscribed classes when user wishes to cancel either by pressing escape, or
			// clicking on a non-occupied area on canvas
			onCancel?.Invoke();
		}

		if (Input.GetMouseButtonUp(0) || Input.GetMouseButtonUp(1)) {
			if (EventSystem.current.IsPointerOverGameObject() == false) {
				// Notify if canvas is pressed (where there are no objects) using mouse buttons
				onCanvasClick?.Invoke();
			}
		}
	}
}
