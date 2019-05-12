using System;
using System.Collections;
using UnityEngine;

/// <summary>
/// Abstract class <c>GenericAttack</c> is the parent of all attacks that wants to find
/// a specific component in the system and do something with it
/// </summary>
public abstract class GenericAttack : Pathfinder {

    // Condition for checking if selected component is attackable
    private bool isAttackable;

    // Condition for exiting script
    protected bool shouldDestroy;

    public GenericAttack(Component initialComponent) : base(initialComponent) {
        
    }

    /// <summary>
    /// While loop that continously scans the selected component and compares it 
    /// with the component to find. If the component is found, return this component.
    /// if not, set shouldDestroy == true and exit loop
    /// </summary>
    public Component FindAttackableGameObject(System.Type component) {
        Component foundComponent = null;
        int maxMoves = 10;
        this.isAttackable = false;
        this.shouldDestroy = false;
        try {
            while (this.isAttackable == false && this.shouldDestroy == false) {
                if (ScanComponent(SelectedComponent, component)) {
                    this.isAttackable = true;
                    foundComponent = SelectedComponent;
                } else {
                    if (maxMoves <= 0) {
                        Debug.Log("Couldnt find target component");
                        this.shouldDestroy = true;
                    } else {
                        maxMoves--;
                        MoveToNextOutput();
                    }
                }
            }
        } catch (NullReferenceException e) {
            Debug.LogException(e);
        }
        return foundComponent;
    }

    /// <summary>
    /// Scans the specified component and returns true if componentToScan == componentToFind
    /// and false if not. 
    /// 
    /// If the selected component is a firewall, the status of the firewall is checked. 
    /// If the firewall is active, the port of the attack and the port status of the firewall is compared. 
    /// If they are the same, and the firewall port is open, the attack will pass. If not, the attack gets blocked. 
    /// </summary>
    /// 
    /// <param name="componentToScan">The selected component</param>
    /// <param name="componentToFind">The component to find</param>
    /// 
    /// <returns>true if componentToScan == componentToFind
    ///          and false if not. </returns>
    private bool ScanComponent(Component componentToScan, System.Type componentToFind) {
        bool componentFound = false;
        try {
            if ((System.Type) componentToScan.GetType() == componentToFind) {
                componentFound = true;
            } else if (componentToScan.GetType() == typeof(Firewall)) {
                //TODO Change this check to find the status of port in real firewall script (Wait for liban to finish firewall)
                Firewall firewall = (Firewall) componentToScan;
                FirewallPort firewallPort = firewall.GetPort(port);
                firewallPort.Activity = string.Format("<color=#FF0000>" + GetAttackName() + "</color>");
                bool portStatus = firewallPort.IsActive;
                Debug.Log("Selected component is a firewall, port status: " + portStatus);
                if (firewall.Status) {
                    if (portStatus == false) {
                        if (componentToScan.GetType() == componentToFind) {
                            componentFound = true;
                        }
                    } else {
                        Debug.Log("Firewall port closed");
                        componentFound = false;
                        shouldDestroy = true;
                    }
                }
            } else {
                Debug.Log("Component is of type: " + componentToScan.Name);
            }
        } catch (NullReferenceException nre) {
            Debug.LogException(nre);
        }
        return componentFound;
    }

    public abstract void Run(Component initialComponent, System.Type type);

    /// <summary>
    /// deletes this attack object
    /// </summary>
    public void DeleteAttack() {
        Debug.Log("Deleting attack..");
        Destroy(gameObject);
    }

}