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
            StartCoroutine(InitializeDownload());
        }
    }

    private IEnumerator InitializeDownload() {
        yield return new WaitForSeconds(SelectedComponent.Encryption);
        DownloadDocument();
    }

    private void DownloadDocument() {
        // TODO add some condition to prevent download (defensive mechanism) also check document encryption level maybe?
        Debug.Log("Downloading Document Complete...");
        SelectedComponent.Status = false;
        SelectedComponent.Locked = true;
        // TODO Change image color to be red
        //Image image = SelectedComponent.GetComponent<Image>();
        //image.GetComponent<Image>().color = Color.red;
        //SelectedComponent.SetCanvasImage(image);
        // TODO add event in gamemanager for updating panel

        // Open Panel
        RansomPopup.Instance.Set("Ransom Request", "Testing you for ransom", 30, 100);
        RansomPopup.Instance.ShowRansomPanel(true);
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
