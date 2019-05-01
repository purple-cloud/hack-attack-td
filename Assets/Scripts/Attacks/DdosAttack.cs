using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DdosAttack : GenericAttack {


    public DdosAttack(Component initialComponent) : base(initialComponent) {

    }

    public void Run(Component initialComponent) {
        Init(initialComponent);
        AttackComponent(FindAttackableGameObject(typeof(Computer)));
    }

    private void AttackComponent(Component component) {
        if (component.GetType() == typeof(Computer) && component.Durability > 0) {
            // Each attack does 10 dmg to the durability
            component.Durability -= 10;
            Debug.Log("Durability left: " + SelectedComponent.Durability);
        } else {
            Debug.Log("Couldnt find target component");
        }
        // Destroy Attack
        DeleteAttack();
    }
}
   