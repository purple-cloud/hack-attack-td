using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Virus : Pathfinder {

    private bool isInfectable = false;

    public Virus(Component initialComponent) : base(initialComponent) {

    }

    /// <summary>
    /// Runs the virus
    /// </summary>
    private void Run(Component initialComponent) {
        Init(initialComponent);
        FindInfectableGameObject();
    }

    /// <summary>
    /// Finds an infectable game object.
    /// </summary>
    public void FindInfectableGameObject() {
        while (!this.isInfectable) {
            if (ScanComponent(SelectedComponent)) {
                this.isInfectable = true;
            } else {
                MoveToNextOutput();
            }
        }
        InfectGameObject();
    }

    public void InfectGameObject() {
        Debug.Log("Infecting... " + SelectedComponent.name);
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

}
