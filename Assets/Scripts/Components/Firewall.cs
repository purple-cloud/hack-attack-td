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
	private GameObject firewallPanelRow;                // The design of a single firewall port row

    [SerializeField] 
    private Sprite[] firewallSprites;                   // Contains the sprites for the different upgrades

    [Header("Firewall Port")]
	[SerializeField]
	private FirewallPortDummy[] ports;                  // A list of ports and what their statuses (allow / disallow) set in Unity Inspector. Only used for extracting data.

    private List<FirewallPort> listOfFirewallPorts;     // All ports and statuses of set in a firewall

    public void Start() {
        if (AlreadyInitialized == false) {
            Upgrades = new ComponentUpgrade[] {
                new ComponentUpgrade("Awesome Firewall", 200, firewallSprites[1], 100, 100, 100, 200),
                new ComponentUpgrade("Awesome Firewall", 200, firewallSprites[1], 100, 100, 100, 200)
            };
            Name = "Shitty Firewall";
            ComponentLevel = 1;
            Status = true;
            Sellable = true;
            RepairPrice = 50;
            SellValue = 50;
            InitialPrice = 100;
            Price = NextUpgrade.Price;
            Sprite = firewallSprites[0];
            // Sets virus immune to true
            ImmuneToVirus = true;
            BackupPrice = 50;
            BackupRestorePrice = 100;

            this.listOfFirewallPorts = new List<FirewallPort>();

            CreateGameObjects();

            AlreadyInitialized = true;
        }
    }

	/// <summary>
	/// Instansiates and adjusts the firewall port statuses on the firewall panel.
	/// </summary>
	private void CreateGameObjects() {
		foreach (FirewallPortDummy fpd in ports) {
            // Instantiate gameobject from the prefab and add the FirewallPort
			GameObject obj = Instantiate(firewallPanelRow) as GameObject;
			FirewallPort fp = obj.AddComponent<FirewallPort>();

            // Set port and adjust the size of the gameobject
			fp.Set(fpd.port, fpd.isActive);
			((RectTransform) obj.transform).localScale = new Vector3(1, 1, 1);

            // Store the port configuration
            this.listOfFirewallPorts.Add(fp);

            fp.transform.SetParent(GameObject.Find("TemporaryLocation").transform);
		}
	}

    /// <summary>
    /// Return the FirewallPort from the port integer.
    /// </summary>
    /// <param name="port">Integer of port to fetch</param>
    /// <returns>FirewallPort from the port number</returns>
    public FirewallPort GetPort(int port) {
        FirewallPort fp = null;
        foreach (FirewallPort firewallPort in this.listOfFirewallPorts) {
            // Filters out the other children that does not show firewall port (i.e. title of the panel on top)
            if (firewallPort.Port == port) {
                fp = firewallPort;
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

    /// <summary>
    /// Returns the list of firewall ports
    /// </summary>
    /// <returns>the list of firewall ports</returns>
    public List<FirewallPort> GetFirewallPorts() {
        return this.listOfFirewallPorts;
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
