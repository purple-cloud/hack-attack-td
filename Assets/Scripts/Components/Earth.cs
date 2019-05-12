using System;
using System.Timers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

/// <summary>
/// Class <c>Earth</c> is the main enemy spawner currently active in the game
/// </summary>
public class Earth : Component, IPointerClickHandler {

    [SerializeField] // Starting position for all enemy attacks
    private GameObject initialGameObject;

    [SerializeField] // List containing the different attack prefabs
    private GameObject[] listOfAttacks;

    // is used to suspend execution until given yield instruction finishes
    private IEnumerator coroutine;

    /// <summary>
    /// Checks if the currently loaded scene is not equals the tutorial scene.
    /// If this is true start the coroutine
    /// </summary>
    private void Start() {
        Name = "Earth";
        // Set true when you want earth spawner to be active
        if (true && SceneManager.GetActiveScene() != SceneManager.GetSceneByName("Tutorial")) {
            // Time needs to be dynamic
            this.coroutine = StartSpawningAttacks();
            StartCoroutine(this.coroutine);
        }
    }

    /// <summary>
    /// Forever loop continously spawning enemy when every yield instruction WaitForSeconds is finished
    /// </summary>
    /// <returns></returns>
    private IEnumerator StartSpawningAttacks() {
        Debug.Log("Starting to spawn attacks...");
        while (true) { 
            Debug.Log("Waiting...");
            yield return new WaitForSeconds(UserBehaviourProfile.Instance.SpawnTime);
            CreateRandomEnemy();
            Debug.Log("Done Waiting!");
        }
    }

    /// <summary>
    /// Creates a random enemy for depending on random numbers each time its called.
    /// After enemy has been created it will run. Spawner is suspended until the yield instruction above
    /// is finished
    /// </summary>
    private void CreateRandomEnemy() {
        Debug.Log("Spawning random enemy");
        int randomInt = UnityEngine.Random.Range(0, 2);
        float rand = UnityEngine.Random.Range(0f, 1.0f);
        // If condition is true create Web Attack
        if (randomInt == 0 && (rand < UserBehaviourProfile.Instance.WebAttackProb)) {
            UserBehaviourProfile.Instance.SpawnTime = 3.0f;
            Debug.Log("Creating WebAttack...");
            WebAttack webAttack = (new GameObject("WebAttack")).AddComponent<WebAttack>();
            webAttack.Run((Component) this.initialGameObject.GetComponent(typeof(Component)),
                typeof(Computer));
        }
        // If condition is true, create document attack
        else if (randomInt == 1 && (rand) < UserBehaviourProfile.Instance.DocumentAttackProb) {
            Debug.Log("Preparing to create Document Attack...");
            if (UserBehaviourProfile.Instance.documentHacked == false) {
                Debug.Log("Creating DocumentAttack...");
                DocumentAttack documentAttack = (new GameObject("DocumentAttack")).AddComponent<DocumentAttack>();
                documentAttack.Run((Component) this.initialGameObject.GetComponent(typeof(Component)),
                    typeof(Document));
            } else {
                Debug.Log("Document has already been taken control of...");
            }
        }
    }
}

