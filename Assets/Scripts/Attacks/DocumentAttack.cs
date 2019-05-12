using System;
using System.Timers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Class <c>DocumentAttack</c> represent an attack that target document components.
/// </summary>
public class DocumentAttack : GenericAttack {

    // document is stored here if found
    private Component document;

    // is used to suspend execution until given yield instruction finishes
    private IEnumerator coroutine;

    public DocumentAttack(Component initialComponent) : base(initialComponent) {

    }

    /// <summary>
    /// Calls its parent Init function <seealso cref="Pathfinder"/>, then
    /// tries to find the specified component to attack
    /// </summary>
    /// 
    /// <param name="initialComponent">The component to start at</param>
    /// <param name="type">type of component to find</param>
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

    /// <summary>
    /// If the specified component to attack is found, update the spawn time 
    /// for other attacks (used to prevent crash) and start coroutine
    /// </summary>
    private void AttackDocument() {
        UserBehaviourProfile.Instance.SpawnTime = this.document.Encryption + 2;
        this.coroutine = InitializeDownload();
        StartCoroutine(this.coroutine);
    }

    /// <summary>
    /// The coroutine suspends further action until the yield 
    /// instruction WaitForSeconds is finished
    /// </summary>
    /// <returns></returns>
    private IEnumerator InitializeDownload() {
        yield return new WaitForSeconds(this.document.Encryption);
        DownloadDocument();
	}

    /// <summary>
    /// Sets the documentHacked state to true, which updates document state 
    /// and module panel settings, and open up a ransom panel. In the end
    /// delete itself so it doesn't use unecessary resources.
    /// </summary>
    private void DownloadDocument() {
        // TODO add some condition to prevent download (defensive mechanism)
        Debug.Log("Downloading Document Complete...");
        UserBehaviourProfile.Instance.documentHacked = true;
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
