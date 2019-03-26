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
        DoSomething();
    }

    private void DoSomething() {
        // TODO Set a condition which stops creation of enemy when true
        Debug.Log("Spawning random enemy");
        int randomInt = 0;
        float rand = 0;
        Debug.Log("Random number: " + rand);
        // If condition is true create Web Attack
        // If condition is true, create document attack
    }

}
