using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

/// <summary>
/// Class representing the firewall component
/// </summary>
public class Firewall : Component, IPointerClickHandler {

	[SerializeField]
	private string firewallPanelReference;

	[SerializeField]
	private GameObject firewallPanelRow;

    [SerializeField] // Contains the sprites for the different upgrades
    private Sprite[] firewallSprites;

	[Header("Firewall Port")]
	[SerializeField]
	private FirewallPortDummy[] ports;

	private GameObject firewallPanel;

    public void Start() {
        Upgrades = new ComponentUpgrade[] {
            new ComponentUpgrade("Awesome Firewall", 200, firewallSprites[1], 100, 100),
            new ComponentUpgrade("Awesome Firewall", 200, firewallSprites[1], 100, 100)
		};
        Name = "Shitty Firewall";
        Status = false;
        Sellable = true;
        RepairPrice = 50;
        SellValue = 50;
        InitialPrice = 100;
        Price = NextUpgrade.Price;
        Sprite = firewallSprites[0];
        // Sets virus immune to true
        ImmuneToVirus = true;

		firewallPanel = GameObject.Find(firewallPanelReference);

		if (firewallPanel == null) {
			throw new System.Exception("Firewall has invalid reference to statistics panel. Please check the serialized fields.");
		}

		CreateGameObjects();
	}

	/// <summary>
	/// Instansiates and adjusts the firewall port statuses on the firewall panel.
	/// </summary>
	private void CreateGameObjects() {
		foreach (FirewallPortDummy fpd in ports) {
			GameObject obj = Instantiate(firewallPanelRow) as GameObject;
			FirewallPort fp = obj.AddComponent<FirewallPort>();

			fp.Set(fpd.port, fpd.isActive);
			obj.transform.SetParent(firewallPanel.transform);
			((RectTransform) obj.transform).localScale = new Vector3(1, 1, 1);
		}
	}


	/// <summary>
	/// Return the FirewallPort from the port integer.
	/// </summary>
	/// <param name="port">Integer of port to fetch</param>
	/// <returns>FirewallPort from the port number</returns>
	public FirewallPort GetPort(int port) {
		FirewallPort fp = null;

		foreach (Transform child in firewallPanel.transform) {
			// Filters out the other children that does not show firewall port (i.e. title of the panel on top)
			if (true) {
				if (child.gameObject.GetComponent<FirewallPort>().Port == port) {
					fp = child.gameObject.GetComponent<FirewallPort>();
				}
			}
		}

		return fp;
	}

	/// <summary>
	/// Allow/disallow activity through a port.
	/// </summary>
	/// <param name="port">Port to alter</param>
	/// <param name="newState">Allow/disallow activity</param>
	public void AlterPortState(int port, bool newState) {
		FirewallPort fp = GetPort(port);
		fp.IsActive = newState;
	}

}

/// <summary>
/// This object is parsed and used in the Unity Inspector to insert starting port and statuses.
/// It got no other use than to extract data.
/// </summary>
[System.Serializable]
public class FirewallPortDummy {
	public int port;
	public bool isActive;
}
