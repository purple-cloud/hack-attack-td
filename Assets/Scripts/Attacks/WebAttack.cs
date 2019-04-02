using System;
using System.Collections;
using UnityEngine;

public class WebAttack : GenericAttack {

    private string name;

    private int port;

    private bool isAttackable = false;

    private int lifeTicks;

    private bool stopScript = false;

    public WebAttack(Component initialComponent) : base(initialComponent) {

    }

    public void Run(Component initialComponent) {
        Init(initialComponent);
        AttackComponent(FindAttackableGameObject((Component) GetComponent(typeof(Computer))));
    }

    private void AttackComponent(Component component) {
        // Each attack does 10 dmg to the durability
        component.Durability -= 10;
        Debug.Log("Durability left: " + SelectedComponent.Durability);
        Debug.Log("Lifeticks: " + this.lifeTicks);
        // TODO Update Module panel with new Computer Durability Values
    }
}
