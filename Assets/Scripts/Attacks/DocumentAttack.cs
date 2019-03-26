using System;
using System.Timers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DocumentAttack : Pathfinder {

    private Timer timer;

    private bool foundTarget = false;

    private bool stopScript = false;

    public DocumentAttack(Component initialComponent, GameObject attackGameObject) : base(initialComponent, attackGameObject) {

    }

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
        this.timer = new Timer(SelectedComponent.Encryption * 10);
        this.timer.Elapsed += TimerPassed;
        this.timer.Start();

        
    }

    private void TimerPassed(System.Object source, ElapsedEventArgs e) {
        // Stop timer
        this.timer.Stop();
        this.timer.Dispose();
        DownloadDocument();
    }

    private void DownloadDocument() {
        // TODO add some condition to prevent download (defensive mechanism) also check document encryption level maybe?
        Debug.Log("Downloading Document Complete...");
        SelectedComponent.Status = false;
        SelectedComponent.Locked = true;
        // TODO Change image color to be red
        Image image = SelectedComponent.GetComponent<Image>();
        image.GetComponent<Image>().color = Color.red;
        SelectedComponent.SetCanvasImage(image);
        // TODO add event in gamemanager for updating panel

        // Delete the attack
        DeleteAttack();
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
       if (componentToScan.GetComponent(typeof(Component)).GetType() == typeof(Document)) {
            return true;
       } else {
            return false;
        }
    }
}
