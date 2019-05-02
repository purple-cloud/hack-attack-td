using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// TODO This class reuses WebAttack script and spams
/// </summary>
public class DdosAttack : GenericAttack {

    private Component componentToAttack;

    public DdosAttack(Component initialComponent) : base(initialComponent) {

    }

    public override void Run(Component initialComponent, System.Type type) {
        Init(initialComponent);
        try {
            if (FindAttackableGameObject(type).GetType() == type) {
                this.componentToAttack = FindAttackableGameObject(type);
                AttackComponent();
            }
        } catch (NullReferenceException) {
            DeleteAttack();
            Debug.Log("FindAttackableGameObject returns null.....");
        }
    }

    private void AttackComponent() {
        // Each attack does 10 dmg to the durability
        this.componentToAttack.Durability -= 10;
        Debug.Log("Durability left: " + SelectedComponent.Durability);
        // Destroy Attack
        DeleteAttack();
    }
}
   