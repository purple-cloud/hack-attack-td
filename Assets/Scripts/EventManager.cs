using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Contains all of the custom events needed among other scripts.
/// </summary>
public class EventManager : MonoBehaviour {
	public delegate void Cancel();

	/// <summary>
	/// When user has pressed cancel.
	/// </summary>
	public static event Cancel onCancel;

	void OnGUI() {
		if (Input.GetButtonDown("Cancel")) {
			// Notify all subscribed classes when 
			onCancel?.Invoke();
		}
	}
}
