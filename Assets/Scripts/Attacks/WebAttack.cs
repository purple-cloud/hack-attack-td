using System;
using System.Collections;
using UnityEngine;

/// <summary>
/// Class <c>WebAttack</c>
/// </summary>
public class WebAttack : GenericAttack {

    // Component to attack is stored here if found
    private Component componentToAttack;

    public WebAttack(Component initialComponent) : base(initialComponent) {

    }

    /// <summary>
    /// Calls its parent Init function <seealso cref="Pathfinder"/>, then
    /// tries to find the specified component to attack
    /// </summary>
    /// 
    /// <param name="initialComponent">The component to start at</param>
    /// <param name="type">type of component to find</param>
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

    /// <summary>
    /// If component to attack is found, subtract dmg from its durability
    /// </summary>
    private void AttackComponent() {
        this.componentToAttack.Durability -= 50;
        Debug.Log("Durability left: " + SelectedComponent.Durability);
        // Destroy Attack
        DeleteAttack();
    }
}
