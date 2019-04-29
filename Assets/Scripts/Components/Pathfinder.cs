using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour {

    // TODO Maybe extract these lists to another script

    private string[] listOfAttackNames;

    // TODO maybe make ports accessable and retrieved instead of adding manually here
    private int[] listOfFirewallPorts;

    private string name;

    public int port;

    // A reference to the starting point for the pathfinder
    private Component initialComponent;

    // Array containing the adjacent game objects for the attack
    private List<Component> listOfAdjacentGameObjects;

    /// <summary>
    /// Getter and setter for the component part of the currently selected game object
    /// </summary>
    public Component SelectedComponent { get; private set; }

    public Pathfinder(Component initialComponent) {
        this.initialComponent = initialComponent;
        
    }

    /// <summary>
    /// Initialization of the pathfinder
    /// </summary>
     public void Init(Component initialComponent) {
        SelectedComponent = initialComponent;
        this.listOfAdjacentGameObjects = new List<Component>();
        this.listOfAttackNames = new string[] { "microsoft update", "microsoft security", "microsoft service", "microsoft force update" };
        this.listOfFirewallPorts = new int[] { 21, 22, 80 };

        this.name = this.listOfAttackNames[UnityEngine.Random.Range(0, this.listOfAttackNames.Length)];
        this.port = this.listOfFirewallPorts[UnityEngine.Random.Range(0, this.listOfFirewallPorts.Length)];
        Debug.Log("Attack Name: " + this.name + ", Attack Port: " + this.port);
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
        this.listOfAdjacentGameObjects.Clear();
        Debug.Log("Current Place: " + this.SelectedComponent.Name);
        // TODO Inputs & Outputs = [0] FIX THIS SHIT
        this.listOfAdjacentGameObjects.AddRange(SelectedComponent.GetInputComponents());
        foreach (Component comp in SelectedComponent.GetOutputComponents()) {
            this.listOfAdjacentGameObjects.Add(comp);
        }
        //this.listOfAdjacentGameObjects.AddRange(SelectedComponent.GetOutputComponents());
        // See if list containing all outputs is bigger then 1
        if (this.listOfAdjacentGameObjects.Count > 1) {
            selectedOutput = this.listOfAdjacentGameObjects[UnityEngine.Random.Range(0, this.listOfAdjacentGameObjects.Count - 1)];
        } else if (this.listOfAdjacentGameObjects.Count == 0) {
            Destroy(this.gameObject);
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

}
