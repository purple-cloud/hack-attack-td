    using System;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// Simple template for creating a component with upgrade support
/// </summary>
public class ComponentTemplate : Component, IPointerClickHandler {

    [SerializeField] // Contains all the sprites representing the component
    private Sprite[] listOfSprites;

    /// <summary>
    /// Start is called on the frame when a script is enabled just before 
    /// any of the Update methods is called the first time.
    /// </summary>
    private void Start() {
        // If you would like to have upgrades for this specific component, add them here
        Upgrades = new ComponentUpgrade[] {

            // Fill in the appropriate parameters 
            // (string nameOfTheUpgrade, int priceForTheUpgrade, sprite spriteOfTheUpgradedComponent, int priceToRepair, int sellValue)
            // Example shown below:
            new ComponentUpgrade("Nice Upgrade", 1000, this.listOfSprites[0], 100, 100, 100, 100)
        };

        // You also need to instantiate these fields
        Name = "Name of the starting component"; // Sets the init name of the component
        Status = true; // Sets the status to active
        Price = NextUpgrade.Price; // Sets the init upgrade price for the next upgrade of this component
        Sprite = listOfSprites[0]; // Sets the init sprite of the componentz
    }

}
