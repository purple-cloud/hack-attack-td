using System;
using System.Collections;
using UnityEngine;

public class GenericAttack : Pathfinder {

    private string name;

    private int port;

    private bool isAttackable = false;

    private bool repeatAttack = false;

    public GenericAttack(Component initialComponent) : base(initialComponent) {

    }

    private void AttackComponent() {
        // Each attack does 10 dmg to the durability
        SelectedComponent.Durability -= 10;
        Debug.Log("Durability left: " + SelectedComponent.Durability);
        // TODO Update Module panel with new Computer Durability Values
    }

    /// <summary>
    /// deletes this attack object
    /// </summary>
    private void DeleteAttack() {
        Debug.Log("Deleting attack..");
        Destroy(this.gameObject);
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
            if (componentToScan.GetType() == componentToFind.GetType()) {
                componentFound = true;
            } else if (componentToScan.GetType() == typeof(Firewall)) {
                //TODO Change this check to find the status of port in real firewall script (Wait for liban to finish firewall)
                Firewall firewall = (Firewall) componentToScan;
                bool portStatus = firewall.GetPort(this.port).IsActive;
                Debug.Log("Selected component is a firewall, port status: " + portStatus);
                if (!portStatus) {
                    if (componentToScan.GetType() == componentToFind.GetType()) {
                        componentFound = true;
                    }
                } else {
                    componentFound = false;
                    DeleteAttack();
                }
            } else {
                Debug.Log("Component is of type: " + componentToScan.Name);
            }
        } catch (NullReferenceException nre) {
            Debug.LogException(nre);
        }
        return componentFound;
    }

    /// <summary>
    /// Returns the name of the attack. Could be domain, location etc.
    /// </summary>
    /// <returns>Returns the name of the attack</returns>
    public string GenericAttackName() {
        return this.name;
    }

    /// <summary>
    /// Finds a game object to attack
    /// </summary>
    public Component FindAttackableGameObject(System.Type component) {
        Component foundComponent = null;
        try {
            while (!this.isAttackable) {
                if (ScanComponent(SelectedComponent, component)) {
                    this.isAttackable = true;
                    foundComponent = SelectedComponent;
                } else {
                    MoveToNextOutput();
                }
            }
        } catch (NullReferenceException e) {
            Debug.LogException(e);
        }
        return foundComponent;
    }



}