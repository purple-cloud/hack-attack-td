using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Describes and alters the status of a single port.
/// <seealso cref="Firewall"/>
/// </summary>
public class FirewallPort : MonoBehaviour {

	public int Port { get; set; }           // Port number
	public bool IsActive { get; set; }      // State of the port, either it allows stream of incomming data, or not
	public string Activity { get; set; }    // Source of the last incomming packet

	private Button btnStatus;               // The button that players can press to allow and disallow activity though a port

	void Start() {
        // Fetch and store button ref
		btnStatus = gameObject.transform.Find("Status").GetComponent<Button>();

		// Add event when the button is pressed
		btnStatus.onClick.AddListener(() => OnButtonPress());
	}

	/// <summary>
	/// Sets the port and inital status.
	/// </summary>
	/// <param name="port">Port</param>
	/// <param name="isActive">Allow or disallow activity through this port</param>
    /// <seealso cref="Set(int, bool, string)"/>
	public void Set(int port, bool isActive) {
		Set(port, isActive, "No recent activities");
	}

	/// <summary>
	/// Sets the port and inital status.
	/// </summary>
	/// <param name="port">Port number</param>
	/// <param name="isActive">Allow or disallow activity through this port</param>
	/// <param name="activity">Latest activity source domain that passed though</param>
	public void Set(int port, bool isActive, string activity) {
		Port = port;
		IsActive = isActive;
		Activity = activity;
	}

	/// <summary>
	/// Allows/disallows activity though this port when pressed.
	/// </summary>
	private void OnButtonPress() {
		IsActive = !IsActive;
        FirewallManager.Instance.UpdateStatus(this);
	}

    /// <summary>
    /// Fetches the status button that (dis)allows activity.
    /// </summary>
    /// <returns>Reference to the button.</returns>
    public Button GetBtn() {
        return this.btnStatus;
    }
}