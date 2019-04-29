using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Defenses;
using UnityEngine.UI;

// You have created a panel to show input/output of components
// TODO add feature to remove and add new connections

/// <summary>
/// This is hooked up to the ModifyConnectionPanel.
/// </summary>
public class PathConnection : Singleton<PathConnection> {
	public bool IsSelectingStructure { get; private set; }
	public enum DirectionType { INPUT, OUTPUT };

	// References assigned inside Unity Inspector
	public GameObject connectionRowPrefab;
	public GameObject connectionList;

	// Panel variables
	public Image topImage;
	public Text topText;

	public void ButtonPressed() {
		// Notifies components that the user is about to alter path connections
		IsSelectingStructure = true;
		CompController.Instance.HighlightAllStructures(true);
	}

	void Update() {
		// Check if escape or right mouse is clicked
		if (Input.GetMouseButtonUp(1) || Input.GetButtonDown("Cancel")) {
			Clear();
		}
	}

	private void FinishDisplaying() {
		IsSelectingStructure = false;
	}

	private void Clear() {
		IsSelectingStructure = false;
		CompController.Instance.HighlightAllStructures(false);

		// Removes all connection rows
		foreach (Transform connectionRow in connectionList.transform) {
			Destroy(connectionRow.gameObject);
		}
	}

	public void ShowComponentInputOutput(Component selectedComponent) {
		Clear();

		topImage.sprite = selectedComponent.GetCanvasImage().sprite;
		topText.text = selectedComponent.GetType().ToString();

		// Add all inputs of the selected components to the panel
		foreach (Component inputComponent in selectedComponent.GetInputComponents()) {
			AddRowToPanel(selectedComponent.GetCanvasImage(), DirectionType.INPUT, inputComponent.GetCanvasImage());
		}

		// Add all outputs of the selected components to the panel
		foreach (Component outputComponent in selectedComponent.GetOutputComponents()) {
			AddRowToPanel(selectedComponent.GetCanvasImage(), DirectionType.OUTPUT, outputComponent.GetCanvasImage());
		}

		FinishDisplaying();
	}

	private void AddRowToPanel(Image comp1Image, DirectionType direction, Image comp2Image) {
		// Instantiate new row for the panel and grab its layout
		GameObject newConnectionRow = Instantiate(connectionRowPrefab);
		ConnectionRowLayout rowLayout = newConnectionRow.GetComponent<ConnectionRowLayout>();

		// Assign visuals
		rowLayout.comp1Image.sprite = comp1Image.sprite;
		rowLayout.comp2Image.sprite = comp2Image.sprite;
		rowLayout.SetDirection(direction);

		// Group it along the other connection rows
		newConnectionRow.transform.SetParent(connectionList.transform);
		newConnectionRow.transform.localScale = new Vector3(1, 1, 1);
	}
}
