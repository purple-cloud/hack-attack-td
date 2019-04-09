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
	private GameObject lineObjectParent;
	private bool renderLine;
	private Vector3 componentCenterPoint;

	void Update() {
		if (renderLine) {
			foreach (KeyValuePair<GameObject, GameObject> lineObj in targetObjs) {
				DrawLine(lineObj);
			}
		}
	}

	public void AddList(params GameObject[] outputObjs) {
		if (lineObjectParent == null) {
			if (gameObject.transform.Find("LineHandler") != null) {
				lineObjectParent = gameObject.transform.Find("LineHandler").gameObject;
			} else {
				lineObjectParent = Instantiate(CompController.Instance.emptyPrefab);
				lineObjectParent.name = "LineHandler";
				lineObjectParent.transform.SetParent(gameObject.transform);
			}

			componentCenterPoint = gameObject.transform.position;
			lineObjectParent.transform.position = componentCenterPoint;
			Debug.Log(lineObjectParent.name + " is the child of " + gameObject.name);
			targetObjs = new Dictionary<GameObject, GameObject>();
		}
		
		// Adds every target objects in a list and creates an object to draw the lines
		foreach (GameObject obj in outputObjs) {
			GameObject lineObj = Instantiate(CompController.Instance.emptyPrefab);
			lineObj.name = "Line to " + obj.name;
			lineObj.AddComponent<LineRenderer>().material = CompController.Instance.pathLineMaterial;
			lineObj.transform.SetParent(lineObjectParent.transform);
			lineObj.transform.position = componentCenterPoint;
			Debug.Log(lineObj.name + " is the child of " + lineObjectParent.name);

			targetObjs.Add(obj, lineObj);
		}

		renderLine = (targetObjs.Count > 0) ? true : false;
	}

	public void Add(GameObject obj) {
		GameObject lineObj = Instantiate(CompController.Instance.emptyPrefab);
		lineObj.name = "Line to " + obj.name;
		lineObj.AddComponent<LineRenderer>().material = CompController.Instance.pathLineMaterial;
		lineObj.transform.SetParent(lineObjectParent.transform);
		lineObj.transform.position = componentCenterPoint;
		Debug.Log(lineObj.name + " is the child of " + lineObjectParent.name);

		targetObjs.Add(obj, lineObj);
	}

	private void DrawLine(KeyValuePair<GameObject, GameObject> pair) {
		if (pair.Key != null || pair.Value != null) {
			LineRenderer lr = pair.Value.GetComponent<LineRenderer>();

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
			GameObject lineObj = targetObjs[key: outputStructure];
			targetObjs.Remove(outputStructure);
			Destroy(lineObj);
		}

		return targetObjs.ContainsKey(outputStructure);
	}
}
