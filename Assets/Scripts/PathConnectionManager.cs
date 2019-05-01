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
public class PathConnectionManager : Singleton<PathConnectionManager> {
	public bool IsSelectingStructure { get; private set; }
	public enum DirectionType { INPUT, OUTPUT };

	// References assigned inside Unity Inspector
	[SerializeField]
	private GameObject panel;

	[SerializeField]
	private GameObject connectionRowPrefab;

	[SerializeField]
	private GameObject connectionList;

	// Panel variables
	[SerializeField]
	private Image topImage;

	[SerializeField]
	private Text topText;

	public void OpenPanelEvent() {
		// Notifies components that the user is about to alter path connections
		IsSelectingStructure = true;
		CompController.Instance.HighlightAllStructures(true);
	}

	void Update() {
		// Check if escape or right mouse is clicked when selecting structure
		if (IsSelectingStructure) {
			if (Input.GetMouseButtonUp(1) || Input.GetButtonDown("Cancel")) {
				Clear();
				panel.SetActive(false);
			}
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
		ShowPanel();
		Clear();

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

		FinishDisplaying();
	}

	private void AddRowToPanel(Component comp1, DirectionType direction, Component comp2) {
		// Instantiate new row for the panel and grab its layout
		GameObject newConnectionRow = Instantiate(connectionRowPrefab);
		ConnectionRowLayout rowLayout = newConnectionRow.GetComponent<ConnectionRowLayout>();

		// Assign visuals
		rowLayout.Assign(comp1, direction, comp2);

		// Group it along the other connection rows
		newConnectionRow.transform.SetParent(connectionList.transform);
		newConnectionRow.transform.localScale = new Vector3(1, 1, 1);
	}

	private void ShowPanel() {
		panel.SetActive(true);
	}

	public void UnlinkPath(Component inputTo, Component outputFrom) {
		if (CanUnlink(inputTo, outputFrom)) {
			inputTo.RemoveInput(outputFrom.gameObject);
			outputFrom.RemoveOutput(inputTo.gameObject);

			Clear();
		} else {
			Debug.Log("Can't unlink path: One of the components doesn't have" +
				" more than one path.");
		}
	}

	private bool CanUnlink(Component inputTo, Component outputFrom) {
		return inputTo.input.Count > 1 && outputFrom.outputs.Count > 1;
	}
}
