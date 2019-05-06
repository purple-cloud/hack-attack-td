using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script holds on state values that can be used
/// to "analyze" user behaviour and determine attack patterns
/// </summary>
public class UserBehaviourProfile : Singleton<UserBehaviourProfile> {

    public bool tutorialLvl = false;

    public bool IsPlacingTutorialStructure = false;

    #region PLAYER

    public bool documentHacked;

    public float PlayerWillingToPay { get; set; }

    public float PlayerPayDenial { get; set; }

    #endregion

    #region ENEMY

    public float SpawnTime { get; set; }

    public float WebAttackProb { get; set; }

    public float DocumentAttackProb { get; set; }

    public float DdosAttackProb { get; set; }

    #endregion

    private void Awake() {
        this.documentHacked = false;
        this.PlayerWillingToPay = 0;
        this.PlayerPayDenial = 0;
        this.WebAttackProb = 2.0f;
        this.DocumentAttackProb = 2.0f;
        // TODO Uncomment when done
        //this.DdosAttackProb = 0.14f;
        this.SpawnTime = 15.0f;
    }
}
