using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class FirewallPort : MonoBehaviour {
	public int Port { get; set; }
	public bool IsActive { get; set; }

	void Start() {
		//Add event when the button is pressed
		gameObject.transform.Find("Button").GetComponent<Button>().onClick.AddListener(() => OnButtonPress());
	}

	public void LinkApplicationToPort() { }

	/// <summary>
	/// Sets the port and inital status.
	/// </summary>
	/// <param name="port">Port</param>
	/// <param name="isActive">Allow or disallow activity through this port</param>
	public void Set(int port, bool isActive) {
		Port = port;
		IsActive = isActive;
		UpdateGUI();
	}

	/// <summary>
	/// Allows/disallows activity though this port when pressed.
	/// </summary>
	private void OnButtonPress() {
		IsActive = !IsActive;
		UpdateGUI();
	}

	/// <summary>
	/// Keeps the GUI up to date with the states.
	/// </summary>
	private void UpdateGUI() {
		gameObject.transform.Find("Port").GetComponent<Text>().text = Port.ToString();
		gameObject.transform.Find("State").GetComponent<Text>().text = (IsActive) ? "Allow" : "Disallow";
		gameObject.transform.Find("State").GetComponent<Text>().color = (IsActive) ? Color.green : Color.red;
	}
}