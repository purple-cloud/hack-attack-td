using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour {

    // A reference to the starting point for the pathfinder
    private Component initialComponent;

    // A reference to the game object using the pathfinder
    private GameObject attackGameObject;

    // Array containing the adjacent game objects for the attack
    private List<Component> listOfAdjacentGameObjects;

    /// <summary>
    /// Getter and setter for the component part of the currently selected game object
    /// </summary>
    public Component SelectedComponent { get; private set; }

    public Pathfinder(Component initialComponent, GameObject attackGameObject) {
        this.initialComponent = initialComponent;
        this.attackGameObject = attackGameObject;
    }

    /// <summary>
    /// Initialization of the pathfinder
    /// </summary>
     public void Init() {
        SelectedComponent = this.initialComponent;
        this.listOfAdjacentGameObjects = new List<Component>();
    }

    /// <summary>
    /// Returns the next output from currently selected game object.
    /// If output.size > 1 then return a random output.
    /// </summary>
    /// <returns>
    /// the next output from currently selected game object.
    /// If output.size > 1 then return a random output.
    /// </returns>
    public Component GetNextOutput() {
        Component selectedOutput = null;
        Debug.Log("Current Place: " + this.SelectedComponent.Name);
        // TODO Change this to use iterator over outputs 
        // TODO (after output can contain more then 1) to find all outputs

        // IT CRASHES HERE!!! CANT FIND COMPONENT!!!!
        this.listOfAdjacentGameObjects.Add((Component) this.SelectedComponent.output.GetComponent(typeof(Component)));
        // See if list containing all outputs is bigger then 1
        if (this.listOfAdjacentGameObjects.Count > 1) {
            selectedOutput = this.listOfAdjacentGameObjects[Random.Range(0, this.listOfAdjacentGameObjects.Count)];
        } else {
            selectedOutput = this.listOfAdjacentGameObjects[0];
        }
        return selectedOutput;
    }

    /// <summary>
    /// Moves the "attack" object to the specified game object
    /// </summary>
    /// <param name="targetGameObject">the game object to move to</param>
    public void MoveToNextOutput() {
        this.SelectedComponent = GetNextOutput();
        Debug.Log("New Place: " + this.SelectedComponent.name);
    }

    public GameObject GetAttackObject() {
        return this.attackGameObject;
    }
}
