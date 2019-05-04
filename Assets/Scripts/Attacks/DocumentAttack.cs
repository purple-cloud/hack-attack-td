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

    public override void Run(Component initialComponent, System.Type type) {
        Init(initialComponent);
        try {
            if (FindAttackableGameObject(type).GetType() == type) {
                this.document = FindAttackableGameObject(type);
                AttackDocument();
            }
        } catch (NullReferenceException) {
            DeleteAttack();
            Debug.Log("FindAttackableGameObject returns null.....");
        }
    }

    private void AttackDocument() {
        UserBehaviourProfile.Instance.SpawnTime = this.document.Encryption + 2;
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
        UserBehaviourProfile.Instance.documentHacked = true;
		// TODO Change image color to be red
		//Image image = SelectedComponent.GetComponent<Image>();
		//image.GetComponent<Image>().color = Color.red;
		//SelectedComponent.SetCanvasImage(image);

		EventManager.TriggerRefreshPanelEvent();

        // Open Panel
        RansomPopup.Instance.Set("Ransom Request", 
            "I have taken control of your classified document. If you want it back you have to pay.", 
            10, 100);
        RansomPopup.Instance.ShowRansomPanel(true);
        // Delete the attack
        DeleteAttack();
    }

}
