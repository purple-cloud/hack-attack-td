using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour {

    [SerializeField] // A reference to the starting point for the pathfinder
    private GameObject initialGameObject;

    [SerializeField] // A reference to the game object using the pathfinder
    private GameObject attackGameObject;

    // Array containing the adjacent game objects for the attack
    private List<GameObject> listOfAdjacentGameObjects;

    /// <summary>
    /// Getter and setter for the currently selected game object 
    /// holding the object using the pathfinder "algorithm"
    /// </summary>

    public GameObject SelectedGameObject { get; private set; }

    /// <summary>
    /// Getter and setter for the component part of the currently selected game object
    /// </summary>
    public Component SelectedComponent { get; private set; }

    /// <summary>
    /// Initialization of the pathfinder
    /// </summary>
    public void Init() {
        SelectGameObject(this.initialGameObject);
        this.listOfAdjacentGameObjects = new List<GameObject>();
        this.attackGameObject.transform.position = this.initialGameObject.transform.position;
    }

    /// <summary>
    /// Returns the next output from currently selected game object.
    /// If output.size > 1 then return a random output.
    /// </summary>
    /// <returns>
    /// the next output from currently selected game object.
    /// If output.size > 1 then return a random output.
    /// </returns>
    public GameObject GetNextOutput() {
        GameObject selectedOutput = null;
        Debug.Log("Current Place: " + this.SelectedGameObject.name);
        // TODO Change this to use iterator over outputs 
        // TODO (after output can contain more then 1) to find all outputs
        this.listOfAdjacentGameObjects.Add(((Component) this.SelectedGameObject.GetComponent(typeof(Component))).output);
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
        this.SelectedGameObject = GetNextOutput();
        this.SelectedComponent = (Component) this.SelectedGameObject.GetComponent(typeof(Component));
        this.attackGameObject.transform.position = this.SelectedGameObject.transform.position;
        Debug.Log("New Place: " + this.SelectedGameObject.name);
    }

    /// <summary>
    /// Selects a GameObject
    /// </summary>
    /// <param name="gameObject">The game object to select</param>
    public void SelectGameObject(GameObject gameObject) {
        this.SelectedGameObject = gameObject;
        this.SelectedComponent = (Component) this.SelectedGameObject.GetComponent(typeof(Component));
    }

    public GameObject GetAttackObject() {
        return this.attackGameObject;
    }
}
