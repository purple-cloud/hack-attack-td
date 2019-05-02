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
        if (true) {
            // Time needs to be dynamic
            this.coroutine = StartSpawningAttacks();
            StartCoroutine(this.coroutine);
        }
    }

    private IEnumerator StartSpawningAttacks() {
        Debug.Log("Starting to spawn attacks...");
        while (true) { 
            Debug.Log("Waiting...");
            yield return new WaitForSeconds(UserBehaviourProfile.Instance.SpawnTime);
            CreateRandomEnemy();
            Debug.Log("Done Waiting!");
        }
    }

    private void CreateRandomEnemy() {
        Debug.Log("Spawning random enemy");
        int randomInt = UnityEngine.Random.Range(0, 2);
        float rand = UnityEngine.Random.Range(0f, 1.0f);
        // If condition is true create Web Attack
        if (randomInt == 0 && (rand < UserBehaviourProfile.Instance.WebAttackProb)) {
            // TODO Might have to tweak spawn time value
            UserBehaviourProfile.Instance.SpawnTime = 3.0f;
            Debug.Log("Creating WebAttack...");
            WebAttack webAttack = (new GameObject("WebAttack")).AddComponent<WebAttack>();
            webAttack.Run((Component) this.initialGameObject.GetComponent(typeof(Component)), 
                typeof(Computer));
        }
        // If condition is true, create document attack
        else if (randomInt == 1 && (rand) < UserBehaviourProfile.Instance.DocumentAttackProb) {
            Debug.Log("Creating DocumentAttack...");
            DocumentAttack documentAttack = (new GameObject("DocumentAttack")).AddComponent<DocumentAttack>();
            documentAttack.Run((Component) this.initialGameObject.GetComponent(typeof(Component)), 
                typeof(Document));
        }
        // if condition is true, create ddos attack
        // TODO Make it so Ddos attack is only available from level 2 and onwards
        //else if (randomInt == 2 && (rand) < UserBehaviourProfile.Instance.DdosAttackProb) {
        //    // TODO Might have to add some modificators inside DdosAttack script that modifies the time it needs to execute
        //    UserBehaviourProfile.Instance.SpawnTime = 5.0f;
        //    Debug.Log("Creating DdosAttack...");
        //    DdosAttack ddosAttack = (new GameObject("DdosAttack")).AddComponent<DdosAttack>();
        //    ddosAttack.Run((Component) this.initialGameObject.GetComponent(typeof(Component)));
        //}
    }

}

