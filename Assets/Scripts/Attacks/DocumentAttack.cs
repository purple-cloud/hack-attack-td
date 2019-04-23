using System;
using System.Timers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DocumentAttack : GenericAttack {

    private Component document;

    private IEnumerator coroutine;

    public DocumentAttack(Component initialComponent) : base(initialComponent) {

    }

    public void Run(Component initialComponent) {
        Init(initialComponent);
        AttackDocument(FindAttackableGameObject(typeof(Document)));
    }

    private void AttackDocument(Component component) {
        this.document = component;
        this.coroutine = InitializeDownload();
        StartCoroutine(this.coroutine);
    }

    private IEnumerator InitializeDownload() {
        yield return new WaitForSeconds(this.document.Encryption);
        DownloadDocument();
    }

    private void DownloadDocument() {
        // TODO add some condition to prevent download (defensive mechanism) also check document encryption level maybe?
        Debug.Log("Downloading Document Complete...");
        this.document.Status = false;
        this.document.Locked = true;
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

}
