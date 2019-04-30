using System;
using System.Timers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Earth : Component, IPointerClickHandler {

    [SerializeField]
    private GameObject initialGameObject;

    [SerializeField]
    private GameObject[] listOfAttacks;

    private IEnumerator coroutine;

    private void Start() {
        Name = "Earth";
        // Set true when you want earth spawner to be active
        if (false) {
            this.coroutine = StartSpawningAttacks(5.0f);
            StartCoroutine(this.coroutine);
        }
    }

    private IEnumerator StartSpawningAttacks(float time) {
        Debug.Log("Starting to spawn attacks...");
        //yield return new WaitForSeconds(time);
        while (true) { 
            Debug.Log("Waiting...");
            Debug.Log("Before: Current thread: " + System.Threading.Thread.CurrentThread);
            yield return new WaitForSeconds(time);
            CreateRandomEnemy();
            Debug.Log("Done Waiting!");
            Debug.Log("After: Current thread: " + System.Threading.Thread.CurrentThread);
        }
    }

    private void CreateRandomEnemy() {
        Debug.Log("Spawning random enemy");
        int randomInt = UnityEngine.Random.Range(0, 2);
        float rand = UnityEngine.Random.Range(0f, 1.0f);
        // If condition is true create Web Attack
        if (randomInt == 0 && (rand < UserBehaviourProfile.Instance.WebAttackProb)) {
            Debug.Log("Creating WebAttack...");
            WebAttack webAttack = (new GameObject("WebAttack")).AddComponent<WebAttack>();
            webAttack.Run((Component) this.initialGameObject.GetComponent(typeof(Component)));
        }
        // If condition is true, create document attack
        else if (randomInt == 1 && (rand) < UserBehaviourProfile.Instance.DocumentAttackProb) {
            Debug.Log("Creating DocumentAttack...");
            DocumentAttack documentAttack = (new GameObject("DocumentAttack")).AddComponent<DocumentAttack>();
            documentAttack.Run((Component) this.initialGameObject.GetComponent(typeof(Component)));
        }

        // TODO Only works if it finds a Computer component. Will use up computer resources and crash unity if not
        //WebAttack webAttack = (new GameObject("WebAttack")).AddComponent<WebAttack>();
        //webAttack.Run((Component) this.initialGameObject.GetComponent(typeof(Component)));
        
        //DocumentAttack documentAttack = (new GameObject("DocumentAttack")).AddComponent<DocumentAttack>();
        //documentAttack.Run((Component) this.initialGameObject.GetComponent(typeof(Component)));

    }

    /* TODO Add ransom decision consequence and variable related stuff in this script */

}

