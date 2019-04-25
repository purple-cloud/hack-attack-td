using Defenses;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineHandler : MonoBehaviour {
	/// <summary>
	/// First variable is the target output object, the next one is a reference to its object line drawer
	/// that is created automatically.
	/// </summary>
	private Dictionary<GameObject, GameObject> targetObjs;

	/// <summary>
	/// This is where all the individual line gameobjects (to targets) are going to be stored.
	/// </summary>
	private GameObject lineObjectParent;

	/// <summary>
	/// Defines the components center point for gameobject of the handler.
	/// </summary>
	private Vector3 componentCenterPoint;

	void Update() {
		// Checks if there are any lines to render from this gameobject to the target objects
		if (targetObjs != null) {
			if (targetObjs.Count > 0) {
				// Draw lines if component has one or more outputs
				foreach (KeyValuePair<GameObject, GameObject> lineObj in targetObjs) {
					//TODO Instead of iterating over dead gameobjects, remove them
					// Ensure there aren't any destroyed objects being referenced to
					if (lineObj.Key != null) {
						DrawLine(lineObj);
					}
				}
			}
		}
	}

	/// <summary>
	/// Creates a gameobject under the component in the hierarchy that will contain all the gameobjects
	/// that holds all the lines for the target objects. There are no checks needed before using this as 
	/// they are already integrated and should be used as much as possible to prevent exceptions.
	/// </summary>
	private void Init() {
		// Checks if the handlers gameobject exists, if it does, use that, else create a new one
		if (lineObjectParent == null) {
			if (gameObject.transform.Find("LineHandler") != null) {
				lineObjectParent = gameObject.transform.Find("LineHandler").gameObject;
			} else {
				lineObjectParent = Instantiate(CompController.Instance.emptyPrefab);
				lineObjectParent.name = "LineHandler";
				lineObjectParent.transform.SetParent(gameObject.transform);
			}

			// Position and assign what component to put the handlers gameobject under
			componentCenterPoint = gameObject.transform.position;
			lineObjectParent.transform.position = componentCenterPoint;

			targetObjs = new Dictionary<GameObject, GameObject>();
		}
	}


	/// <summary>
	/// Adds gameobject from a list to the target object list.
	/// </summary>
	/// <param name="outputObjs"></param>
	public void AddList(params GameObject[] outputObjs) {
		// Ensures that everything is set up
		Init();
		
		// Adds every target objects in a list and creates an object to draw the lines (if non-existent)
		foreach (GameObject obj in outputObjs) {
			if (!targetObjs.ContainsKey(obj)) {
				Add(obj);
			}
		}
	}

	public void Add(GameObject obj) {
		Debug.Log("+");
		// Ensures that everything is set up
		Init();

		// Initializes an empty "shell" that will contain the single line for the single target object
		GameObject lineObj = Instantiate(CompController.Instance.emptyPrefab);

		// Add and configure the line rendering for the single target object
		lineObj.AddComponent<LineRenderer>().material = CompController.Instance.pathLineMaterial;
		lineObj.transform.SetParent(lineObjectParent.transform);
		lineObj.transform.position = componentCenterPoint;

		// Sets the actual line (LineRenderer) to the anchor (or center) of the handler
		lineObj.transform.localPosition = new Vector3(0, 0, 0);

		targetObjs.Add(obj, lineObj);
	}


	/// <summary>
	/// Activates each linerenderer to draw a line between this component to the target component.
	/// </summary>
	/// <param name="pair">The line gameobject as key, and the target component as value</param>
	private void DrawLine(KeyValuePair<GameObject, GameObject> pair) {
		// Checks for dead gameobjects before proceeding
		if (pair.Key != null || pair.Value != null) {
			LineRenderer lr = pair.Value.GetComponent<LineRenderer>();

			// Draw
			lr.SetPosition(0, pair.Key.transform.position);
			lr.SetPosition(1, pair.Value.transform.position);
		}
	}

	/// <summary>
	/// Removes the line from ouput and input
	/// </summary>
	/// <param name="outputStructure"></param>
	/// <returns></returns>
	public bool RemoveLine(GameObject outputStructure) {
		if (targetObjs.ContainsKey(outputStructure)) {
			// Unlink the line gameobject from the target component, and destroy the line gameobject
			GameObject lineObj = targetObjs[key: outputStructure];
			targetObjs.Remove(outputStructure);
			Destroy(lineObj);
		}

		return targetObjs.ContainsKey(outputStructure);
	}

	/// <summary>
	/// Completely restart the linehandler for new set of outputs.
	/// </summary>
	public void ResetHandler() {
		targetObjs = new Dictionary<GameObject, GameObject>();
		Destroy(lineObjectParent);
	}
}
