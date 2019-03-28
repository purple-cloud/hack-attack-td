using System;
using System.Collections;
using UnityEngine;

public class WebAttack : Pathfinder {

    private string name;

    private int port;

    private bool isAttackable = false;

    private int lifeTicks;

    private bool stopScript = false;

    public WebAttack(Component initialComponent) : base(initialComponent) {

    }

    public void Run(Component initialComponent) {
        Init(initialComponent);
        this.lifeTicks = 5;
        FindAttackableGameObject();
    }

    /// <summary>
    /// Finds a game object to attack
    /// </summary>
    private void FindAttackableGameObject() {
        while (!this.isAttackable && this.stopScript == false) {
            if (ScanComponent(SelectedComponent)) {
                this.isAttackable = true;
            } else {
                MoveToNextOutput();
            }
        }
        if (this.stopScript == false) {
            StartCoroutine(StartAttackingTimer());
        }
    }

    private IEnumerator StartAttackingTimer() {
        Debug.Log(SelectedComponent.name + " is under attack!");
        while (this.lifeTicks > 0) {
            yield return new WaitForSeconds(2.0f);
            if (SelectedComponent.Durability > 0) {
                AttackComponent();
            } else {
                DeleteAttack();
            }
        }
        DeleteAttack();
    }

    private void AttackComponent() {
        // Each attack does 10 dmg to the durability
        SelectedComponent.Durability -= 10;
        this.lifeTicks--;
        Debug.Log("Durability left: " + SelectedComponent.Durability);
        Debug.Log("Lifeticks: " + this.lifeTicks);
        // TODO Update Module panel with new Computer Durability Values
    }

    /// <summary>
    /// deletes this attack object
    /// </summary>
    private void DeleteAttack() {
        Debug.Log("Deleting attack..");
        this.stopScript = true;
        Destroy(this.gameObject);
    }

    /// <summary>
    /// Scans the specified component and returns true if computer component
    /// is found and false if not
    /// </summary>
    /// <param name="componentToScan"></param>
    /// <returns>true if computer component is found and false if not</returns>
    private bool ScanComponent(Component componentToScan) {
        bool computerComponentFound = false;
        try {
            if (componentToScan.GetType() == typeof(Computer)) {
                computerComponentFound = true;
            } else if (componentToScan.GetType() == typeof(Firewall)) {
                //TODO Change this check to find the status of port in real firewall script (Wait for liban to finish firewall)
                Firewall firewall = (Firewall) componentToScan;
                //Debug.Log("Selected component is a firewall, port status: " + firewall.PortStatus);
                //if (!firewall.PortStatus) {
                //    if (componentToScan.GetType() == typeof(Computer)) {
                //        computerComponentFound = true;
                //    }
                //} else {
                //    computerComponentFound = false;
                //    DeleteAttack();
                //}
            } else {
                Debug.Log("Component is of type: " + componentToScan.Name);
            }
        } catch (NullReferenceException nre) {
            Debug.LogException(nre);
        }
        return computerComponentFound;
    }

    /// <summary>
    /// Returns the name of the attack. Could be domain, location etc.
    /// </summary>
    /// <returns>Returns the name of the attack</returns>
    public string GetWebAttackName() {
        return this.name;
    }

}
