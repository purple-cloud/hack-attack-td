using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class FirewallPort : MonoBehaviour {
	public int Port { get; set; }
	public bool IsActive { get; set; }
	public string Activity { get; set; }

	private Button btnStatus;

	void Start() {
		btnStatus = gameObject.transform.Find("Status").GetComponent<Button>();
		//Add event when the button is pressed
		btnStatus.onClick.AddListener(() => OnButtonPress());
	}

	public void LinkApplicationToPort() { }

	/// <summary>
	/// Sets the port and inital status.
	/// </summary>
	/// <param name="port">Port</param>
	/// <param name="isActive">Allow or disallow activity through this port</param>
	public void Set(int port, bool isActive) {
		Set(port, isActive, "No recent activities");
	}
	/// <summary>
	/// Sets the port and inital status.
	/// </summary>
	/// <param name="port">Port</param>
	/// <param name="isActive">Allow or disallow activity through this port</param>
	/// <param name="activity">Latest activity source domain that passed though</param>
	public void Set(int port, bool isActive, string activity) {
		Port = port;
		IsActive = isActive;
		Activity = activity;

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
	public void UpdateGUI() {
		//This check prevents a NullPointer despite this field being defined in Start()
		if (btnStatus == null) {
			btnStatus = gameObject.transform.Find("Status").GetComponent<Button>();
		}

		gameObject.transform.Find("Port").GetComponent<Text>().text = Port.ToString();
		btnStatus.gameObject.transform.Find("Text").GetComponent<Text>().text = (IsActive) ? "Allow" : "Disallow";
		gameObject.transform.Find("Activity").GetComponent<Text>().text = Activity;
		btnStatus.gameObject.GetComponent<Image>().color = (IsActive) ? Color.green : Color.red;
	}
}