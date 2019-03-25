using System;
using System.Timers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WebAttack : Pathfinder {

    private string name;

    private int port;

    private bool isAttackable = false;

    private Timer timer;

    private int lifeTicks = 5;

    private bool stopScript = false;

    public void Run() {
        Init();
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
            StartAttackingTimer();
        }
    }

    private void StartAttackingTimer() {
        Debug.Log(SelectedComponent.name + " is under attack!");
        this.timer = new Timer(2000);
        this.timer.Elapsed += AttackComponent;
        this.timer.Start();
    }

    private void AttackComponent(System.Object source, ElapsedEventArgs e) {
        if (this.lifeTicks < 1) {
            this.timer.Stop();
            this.timer.Dispose();
            DeleteAttack();
        } else {
            // Each attack does 10 dmg to the durability
            SelectedComponent.Durability -= 10;
            this.lifeTicks--;
            Debug.Log("Durability left: " + SelectedComponent.Durability);
            Debug.Log("Lifeticks: " + this.lifeTicks);
            // TODO Update Module panel with new Computer Durability Values
        }
    }

    /// <summary>
    /// deletes this attack object
    /// </summary>
    private void DeleteAttack() {
        Debug.Log("Deleting attack..");
        this.stopScript = true;
        Destroy(GetAttackObject());
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
            if (componentToScan.GetComponent(typeof(Component)).GetType() == typeof(Computer)) {
                computerComponentFound = true;
            } else if (componentToScan.GetComponent(typeof(Component)).GetType() == typeof(Firewall)) {
                //TODO Change this check to find the status of port in real firewall script (Wait for liban to finish firewall)
                Firewall firewall = (Firewall) componentToScan.GetComponent(typeof(Component));
                Debug.Log("Selected component is a firewall, port status: " + firewall.PortStatus);
                if (!firewall.PortStatus) {
                    if (componentToScan.GetComponent(typeof(Component)).GetType() == typeof(Computer)) {
                        computerComponentFound = true;
                    }
                } else {
                    computerComponentFound = false;
                    DeleteAttack();
                }
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
