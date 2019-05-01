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
        if (component.GetType() == typeof(Computer)) {
            // Each attack does 10 dmg to the durability
            component.Durability -= 10;
            Debug.Log("Durability left: " + SelectedComponent.Durability);
            // TODO Update Module panel with new Computer Durability Values
        } else {
            Debug.Log("Couldnt find target component");
        }
        // Destroy Attack
        DeleteAttack();
    }
}
