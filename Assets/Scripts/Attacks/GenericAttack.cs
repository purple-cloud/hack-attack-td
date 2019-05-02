using System;
using System.Collections;
using UnityEngine;

public abstract class GenericAttack : Pathfinder {

    private bool isAttackable;

    protected bool shouldDestroy;

    public GenericAttack(Component initialComponent) : base(initialComponent) {
        
    }

    /// <summary>
    /// Finds a game object to attack
    /// </summary>
    public Component FindAttackableGameObject(System.Type component) {
        Component foundComponent = null;
        int maxMoves = 5;
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
                        DeleteAttack();
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
    /// Scans the specified component and returns true if computer component
    /// is found and false if not
    /// </summary>
    /// <param name="componentToScan"></param>
    /// <returns>true if computer component is found and false if not</returns>
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