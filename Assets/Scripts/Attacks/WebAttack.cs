using System;
using System.Collections;
using UnityEngine;

public class WebAttack : GenericAttack {

    public WebAttack(Component initialComponent) : base(initialComponent) {

    }

    public void Run(Component initialComponent) {
        Init(initialComponent);
        AttackComponent(FindAttackableGameObject(typeof(Computer)));
    }

    private void AttackComponent(Component component) {
        // Each attack does 10 dmg to the durability
        component.Durability -= 10;
        Debug.Log("Durability left: " + SelectedComponent.Durability);
        // TODO Update Module panel with new Computer Durability Values

        // Destroy Attack
        DeleteAttack();
    }
}
