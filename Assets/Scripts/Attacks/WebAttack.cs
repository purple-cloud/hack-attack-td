using System;
using System.Collections;
using UnityEngine;

public class WebAttack : GenericAttack {

    private Component componentToAttack;

    public WebAttack(Component initialComponent) : base(initialComponent) {

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
        this.componentToAttack.Durability -= 50;
        Debug.Log("Durability left: " + SelectedComponent.Durability);
        // Destroy Attack
        DeleteAttack();
    }
}
