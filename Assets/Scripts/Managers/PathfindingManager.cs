using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathfindingManager : Singleton<PathfindingManager> {

    // Currently selected game object
    private GameObject selectedGameObject;

    // Currently selected component
    private Component selectedComponent;

    /// <summary>
    /// Finds the adjacent outputs
    /// </summary>
    public void FindOutputs() {
        Debug.Log("Current Place: " + ((Computer) GameObject.Find("Computer").GetComponent(typeof(Computer))).Name);
        // TODO Change this to use while loop over output array from Component.outputs[]
        GameObject selectedOutput = ((Component) this.selectedComponent.GetComponent(typeof(Component))).output;
        SelectGameObject(selectedOutput);
        Debug.Log("New Place: " + this.selectedComponent.Name);
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
