using System;
using System.Timers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DocumentAttack : Pathfinder {

    private Timer timer;

    private bool foundTarget = false;

    private bool stopScript = false;

    public void Run() {
        Init();
        FindAttackableGameObject();
    }

    /// <summary>
    /// Finds a game object to attack
    /// </summary>
    private void FindAttackableGameObject() {
        while (!this.foundTarget && this.stopScript == false) {
            if (ScanComponent(SelectedComponent)) {
                this.foundTarget = true;
            } else {
                MoveToNextOutput();
            }
        }
        if (this.stopScript == false) {
            InitializeDownload();
        }
    }

    /// <summary>
    /// Initializes the downloading state
    /// </summary>
    private void InitializeDownload() {
        // TODO Deplete documents encryption level
        Debug.Log("Document found!");
        this.timer = new Timer(SelectedComponent.Encryption * 120);
        this.timer.Elapsed += DownloadDocument;
        this.timer.Start();
    }

    private void DownloadDocument(System.Object source, ElapsedEventArgs e) {
        // TODO add some condition to prevent download (defensive mechanism) also check document encryption level maybe?
        Debug.Log("Downloading started....");
    }

    private void DemandRansom() {
        // TODO Demand ransom from user (Use popup?)
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
    /// Scans the specified component
    /// </summary>
    /// <param name="componentToScan"></param>
    /// <returns>true if document component is found and false if not</returns>
    private bool ScanComponent(Component componentToScan) {
       if (componentToScan.GetComponent(typeof(Component)).GetType() == typeof(Computer)) {
            return true;
       } else {
            return false;
        }
    }
}
