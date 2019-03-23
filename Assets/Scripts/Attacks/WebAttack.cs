using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WebAttack : Pathfinder {

    private int port;

    private bool isAttackable = false;

    private void Start() {
        
    }

    /// <summary>
    /// Finds a game object to attack
    /// </summary>
    public void FindInfectableGameObject() {
        while (!this.isAttackable) {
            if (ScanComponent(SelectedComponent)) {
                this.isAttackable = true;
            } else {
                MoveToNextOutput();
            }
        }
        AttackGameObject();
    }

    public void AttackGameObject() {
        Debug.Log("Attacking game object: " + SelectedComponent.name);
    }

    /// <summary>
    /// Create some sort of check if time has expired. if so, delete attack object
    /// this to prevent overflow in worst case....
    /// </summary>
    public void hasTimerExpired() {

    }

    /// <summary>
    /// Create some sort of timer that starts when attack object is born
    /// </summary>
    /// <param name="newTime"></param>
    public void NextTimer(int newTime) {

    }

    /// <summary>
    /// deletes this attack object
    /// </summary>
    private void DeleteAttack() {

    }

    /// <summary>
    /// Scans the specified component
    /// </summary>
    /// <param name="componentToScan">component to scan</param>
    public bool ScanComponent(Component componentToScan) {
        if (componentToScan.GetComponent(typeof(Component)).GetType() == typeof(Firewall)) {
            //TODO Create an if statement checking if the firewall has enabled or disabled access for this attacks port

            return true;
        } else {
            return false;
        }
    }

}
