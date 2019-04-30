using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script holds on state values that can be used
/// to "analyze" user behaviour and determine attack patterns
/// </summary>
public class UserBehaviourProfile : Singleton<UserBehaviourProfile> {

    #region PLAYER

    public float PlayerWillingToPay { get; set; }

    public float PlayerPayDenial { get; set; }

    #endregion

    #region ENEMY

    public float WebAttackProb { get; set; }

    public float DocumentAttackProb { get; set; }

    #endregion

    private void Start() {
        this.PlayerWillingToPay = 0;
        this.PlayerPayDenial = 0;
        this.WebAttackProb = 0.85f;
        this.DocumentAttackProb = 0.3f;
    }
}
