using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Defenses;
using UnityEngine.UI;

/// <summary>
/// This is hooked up to the ModifyConnectionPanel.
/// </summary>
public class PathConnectionManager : Singleton<PathConnectionManager> {
	private Component targetCompInputTo;
	private Component targetCompOutputFrom;

	public enum DirectionType { INPUT, OUTPUT };

	/// <summary>
	/// State used for opening panel.
	/// </summary>
	public bool IsSelectingStructure { get; private set; }

	/// <summary>
	/// State used for opening panel.
	/// </summary>
	public bool IsSelectingStructureLink { get; private set; }

	#region UNITY_DEFINED

	[SerializeField]
	private GameObject panel;                   // Panel to show the path links

	[SerializeField]
	private GameObject connectionList;          // The gameobject to list all the path link rows

	[SerializeField]
	private GameObject connectionRowPrefab;		// The layout of the path link row

	// Panel variables
	[SerializeField]
	private Image topImage;						// Panel's top image

	[SerializeField]
	private Text topText;						// Panel's top text title

	#endregion

	#region EVENTS

	/// <summary>
	/// Notifies components that the user want to show the panel.
	/// This is used by the 'ModifyPathConnection' prefab.
	/// </summary>
	public void OpenPanelEvent() {
		IsSelectingStructure = true;
		CompController.Instance.HighlightAllStructures(true);
	}

	/// <summary>
	/// Notifies components that the user is about to link structures.
	/// This is used by the 'AddPathBtn' prefab.
	/// </summary>
	public void LinkComponentsEvent() {
		IsSelectingStructureLink = true;
		CompController.Instance.HighlightAllStructures(true);
	}

	#endregion

	#region LINKING

	/// <summary>
	/// Stores the components that will be linked together.
	/// This is used by <code>Component</code>.
	/// </summary>
	/// <param name="comp">Component to store.</param>
	/// <seealso cref="Component.OnPointerClick(PointerEventData)"/>
	public void OnSelectingStructureLink(Component comp) {
		if (IsSelectingStructureLink) {
			if (targetCompInputTo == null) {
				// If the target input is not selected yet, store the currenct component 'comp' as that.
				targetCompInputTo = comp;
			} else if (targetCompInputTo != null && targetCompOutputFrom == null) {
				// If the target output is not selected yet, store the currenct component 'comp' as that.
				targetCompOutputFrom = comp;

				// Start the linking process
				LinkComponents(targetCompOutputFrom, targetCompInputTo);
			} else {
				Debug.Log(this.name + ": something went wrong.");
			}
		}
	}

	/// <summary>
	/// Link components with each other and create a path.
	/// </summary>
	/// <param name="inputTo">What component the link is going to</param>
	/// <param name="outputFrom">What component the link is comming from</param>
	/// <seealso cref="PathConnectionManager.OnSelectingStructureLink(Component)"/>
	public void LinkComponents(Component inputTo, Component outputFrom) {
		// Input and output cannot point to the same component
		if (inputTo != outputFrom) {
			CompController.Instance.SetInputOutput(outputFrom, inputTo);
		} else {
			Debug.Log("Input and output can't be the same component.");
		}

		Clear();
	}


	/// <summary>
	/// Unlinks the path between components if they exist and if <code>inputTo</code> has more than 1 inputs.
	/// </summary>
	/// <param name="inputTo">What component the link is going to</param>
	/// <param name="outputFrom">What component the link is comming from</param>
	public void UnlinkPath(Component inputTo, Component outputFrom) {
		if (inputTo.input.Count > 1) {
			inputTo.RemoveInput(outputFrom.gameObject);
			outputFrom.RemoveOutput(inputTo.gameObject);

			Clear();
		} else {
			Debug.Log("Can't unlink path: One of the components doesn't have" +
				" more than one path.");
		}
	}

	#endregion

	#region GUI

	/// <summary>
	/// Lists all paths going in and out of the selected components on the panel.
	/// This is used by <code>Component</code>
	/// </summary>
	/// <param name="selectedComponent">Component to show the pats of</param>
	/// <seealso cref="Component.OnPointerUp(PointerEventData)"/>
	public void ShowComponentInputOutput(Component selectedComponent) {
		Clear();
		ShowPanel(true);

		// Update the panels text and image to the name and image of the selected component
		topImage.sprite = selectedComponent.GetCanvasImage().sprite;
		topText.text = selectedComponent.GetType().ToString();

		// Add all inputs of the selected components to the panel
		foreach (Component inputComponent in selectedComponent.GetInputComponents()) {
			AddRowToPanel(selectedComponent, DirectionType.INPUT, inputComponent);
		}

		// Add all outputs of the selected components to the panel
		foreach (Component outputComponent in selectedComponent.GetOutputComponents()) {
			AddRowToPanel(selectedComponent, DirectionType.OUTPUT, outputComponent);
		}

		// User has selected the structure to list the paths of
		IsSelectingStructure = false;
	}


	/// <summary>
	/// Creates a new path link row and adds to the list after assigning its values.
	/// </summary>
	/// <param name="comp1">First component</param>
	/// <param name="direction">Direction of the path from the first component to second</param>
	/// <param name="comp2">Second component</param>
	private void AddRowToPanel(Component comp1, DirectionType direction, Component comp2) {
		// Instantiate new row for the panel and grab its layout
		GameObject newConnectionRow = Instantiate(connectionRowPrefab);
		PathLinkRowLayout rowLayout = newConnectionRow.GetComponent<PathLinkRowLayout>();

		// Assign visuals
		rowLayout.Assign(comp1, direction, comp2);

		// Group it along the other connection rows
		newConnectionRow.transform.SetParent(connectionList.transform);
		newConnectionRow.transform.localScale = new Vector3(1, 1, 1);
	}

	/// <summary>
	/// Shows or hides path link panel.
	/// </summary>
	/// <param name="state"><code>True</code> to show, else set <code>false</code></param>
	private void ShowPanel(bool state) {
		panel.SetActive(state);
	}

	#endregion

	/// <summary>
	/// Resets all variables, removes the path links on the list and closes the panel.
	/// </summary>
	private void Clear() {
		IsSelectingStructure = false;
		IsSelectingStructureLink = false;

		targetCompOutputFrom = null;
		targetCompInputTo = null;

		CompController.Instance.HighlightAllStructures(false);

		// Prevent NRE
		if (connectionList != null) {
			// Removes all connection rows
			foreach (Transform connectionRow in connectionList.transform) {
				Destroy(connectionRow.gameObject);
			}
		}

		ShowPanel(false);
	}

	void Start() {
		// Interrupt linking structures and hide the panel when cancel event is triggered
		EventManager.onCancel += Clear;
	}
}
