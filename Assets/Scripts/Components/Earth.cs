using System;
using System.Timers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Earth : Component {

    [SerializeField]
    private GameObject initialGameObject;

    [SerializeField]
    private GameObject[] listOfAttacks;

    private float webAttackProb = 0.9f;

    private float documentAttackProb = 0.1f;

    private Timer timer;

    private Timer interval;

    private void Start() {
        // Some initialization stuff
        Name = "Earth";
        Countdown();
    }

    private void Countdown() {
        Debug.Log("In Countdown...");
        this.timer = new Timer(1000);
        this.timer.Elapsed += StartAttacks;
        this.timer.Start();
    }

    private void StartAttacks(System.Object source, ElapsedEventArgs e) {
        Debug.Log("Starting attacks...");
        this.timer.Stop();
        this.timer.Dispose();
        SpawnEnemies();
    }

    /// <summary>
    /// Spawn x number of enemies with n interval
    /// </summary>
    private void SpawnEnemies() {
        Debug.Log("Starting interval timer...");
        // TODO See if game timer is still active if not stop spawning enemies 
        // TODO Game timer should freeze the game once time has passed though
        this.interval = new Timer(5000);
        this.interval.Elapsed += CreateRandomEnemy;
        this.interval.Start();
    }

    private void CreateRandomEnemy(System.Object source, ElapsedEventArgs e) {
        this.interval.Stop();
        this.interval.Dispose();
        DoSomething();
    }

    private void DoSomething() {
        // TODO Set a condition which stops creation of enemy when true
        Debug.Log("Spawning random enemy");
        int randomInt = 0;
        float rand = 0;
        Debug.Log("Random number: " + rand);        
        // If condition is true create Web Attack
        if (randomInt == 0 && (rand < this.webAttackProb)) {
            WebAttack webAttack = new WebAttack(this, listOfAttacks[0]);
            webAttack.Run();
        }
        // If condition is true, create document attack
        else if (randomInt == 1 && (rand) < this.documentAttackProb) {

        }
    }

}
