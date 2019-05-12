using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Class <c>FirewallManager</c> handles the visual parts of the firewall system
/// </summary>
public class FirewallManager : Singleton<FirewallManager> {

    [SerializeField] // Reference to the firewall management panel
    private GameObject firewallPanel;

    private bool updateGUI = true;

    /// <summary>
    /// Generates ports from the specified firewall and adds them to the
    /// firewall management panel
    /// </summary>
    /// <param name="firewall">The firewall to get the ports from</param>
    public void GenerateFirewallPorts(Firewall firewall) {
        foreach (FirewallPort firewallPort in firewall.GetFirewallPorts()) {
            Button portStatusBtn = firewallPort.GetBtn();
            firewallPort.transform.Find("Port").GetComponent<Text>().text = firewallPort.Port.ToString();
            portStatusBtn.gameObject.transform.Find("Text").GetComponent<Text>().text = (firewallPort.IsActive) ? "Active" : "Disabled";
            firewallPort.transform.Find("Activity").GetComponent<Text>().text = firewallPort.Activity;
            portStatusBtn.gameObject.GetComponent<Image>().color = (firewallPort.IsActive) ? Color.green : Color.red;

            firewallPort.transform.SetParent(GameObject.Find("FirewallPanelCanvas").transform);
            firewallPort.transform.localScale = new Vector3(1, 1, 1);
        }
    }

    /// <summary>
    /// Switch between enabled and disabled
    /// </summary>
    public void ShowFirewallPanel(Firewall firewall) {
        GameManager.Instance.ShowInformationPanel();
        this.firewallPanel.SetActive(!this.firewallPanel.activeSelf);
        if (this.updateGUI) {
            GenerateFirewallPorts(firewall);
        }
        this.updateGUI = !this.updateGUI;
    }

    /// <summary>
    /// Sets either bool true to enable panel or 
    /// bool false to disable it
    /// </summary>
    /// <param name="condition">condition for showing panel</param>
    public void ShowFirewallPanel(bool condition) {
        this.firewallPanel.SetActive(condition);
    }

    /// <summary>
    /// Updates the firewall port enable/disable status
    /// </summary>
    /// <param name="firewallPort"></param>
    public void UpdateStatus(FirewallPort firewallPort) {
        firewallPort.GetBtn().gameObject.transform.Find("Text").GetComponent<Text>().text = (firewallPort.IsActive) ? "Active" : "Disabled";
        firewallPort.GetBtn().gameObject.GetComponent<Image>().color = (firewallPort.IsActive) ? Color.green : Color.red;
    }

}
