using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackPathfinding : MonoBehaviour {

    [SerializeField] // A reference to the starting point for the pathfinder
    private GameObject initialGameObject;

    [SerializeField]
    private GameObject attackGameObject;

    // Currently selected game object
    private GameObject selectedGameObject;

    // Currently selected component
    private Component selectedComponent;

    private bool conditionToStopSearching;

    // Array containing the adjacent game objects for the attack
    private GameObject[] listOfAdjacentGameObjects;

    public void Initialize() {
        SelectGameObject(this.initialGameObject);
        this.conditionToStopSearching = false;
    }

    /// <summary>
    /// Recursive function that will be called until an available targetable component is found
    /// </summary>
    public GameObject FindAvailableTargetComponent() {
        while (!this.conditionToStopSearching) {
            FindOutputs();
        }
        return this.selectedGameObject;
    }

    /// <summary>
    /// Finds the adjacent outputs
    /// </summary>
    public void FindOutputs() {
        Debug.Log("Current Place: " + this.selectedGameObject.GetComponent(typeof(Component)).name);
        // TODO Change this to use while loop over output array from Component.outputs[]
        Move(((Component) this.selectedGameObject.GetComponent(typeof(Component))).output);
        Debug.Log("Current Place: " + this.selectedGameObject.GetComponent(typeof(Component)).name);
        if (ScanComponent(this.selectedComponent)) {
            this.conditionToStopSearching = true;
        }
    }

    /// <summary>
    /// Moves the "attack" object to the specified game object
    /// </summary>
    /// <param name="targetGameObject">the game object to move to</param>
    public void Move(GameObject targetGameObject) {
        SelectGameObject(targetGameObject);
        this.attackGameObject.transform.position = this.selectedGameObject.transform.position;
    }

    /// <summary>
    /// Scans the specified component
    /// </summary>
    /// <param name="componentToScan">component to scan</param>
    public bool ScanComponent(Component componentToScan) {
        // TODO Change this to be more abstract or extract the method to Virus script instead
        if (componentToScan.ImmuneToVirus == true) {
            return false;
        } else {
            return true;
        }
    }

    /// <summary>
    /// Selects a GameObject
    /// </summary>
    /// <param name="gameObject">The game object to select</param>
    public void SelectGameObject(GameObject gameObject) {
        this.selectedGameObject = gameObject;
        this.selectedComponent = (Component) this.selectedGameObject.GetComponent(typeof(Component));
    }

}
