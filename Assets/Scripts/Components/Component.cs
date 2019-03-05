using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Component : MonoBehaviour {

    [SerializeField] // A reference to the image displayed in canvas
    private Image canvasImage;

    // List containing all the specific upgrades for desired component
    public ComponentUpgrade[] Upgrades { get; protected set; }

    // Getter & setter for the component level
    public int ComponentLevel { get; protected set; }

    // Getter & setter for the component name
    public string Name { get; set; }

    // Getter & setter for the component status
    public bool Status { get; set; }

    // Getter & setter for the component sprite
    public Sprite Sprite { get; set; }

    // Getter & setter for the component price
    public int Price { get; set; }

    // Getter & setter for the component durability
    public float Durability { get; set; }

    // Awake is called after all objects are initialized
    private void Awake() {
        this.ComponentLevel = 1;
    }

    /// <summary>
    /// Returns the next upgrade for the component if there are any.
    /// Otherwise return null
    /// </summary>
    public ComponentUpgrade NextUpgrade {
        get {
            if (this.Upgrades.Length > this.ComponentLevel - 1) {
                return this.Upgrades[this.ComponentLevel - 1];
            }
            return null;
        }
    }

    /// <summary>
    /// Is called by the GameManager when pressing Upgrade on the stats panel
    /// after selecting a component. 
    /// </summary>
    public void Upgrade() {
        if (this.NextUpgrade != null) {
            try {
                Debug.Log("Component Level: " + this.ComponentLevel);
                GameManager.Instance.SetCurrency(GameManager.Instance.GetCurrency() - NextUpgrade.Price);
                this.Sprite = NextUpgrade.Sprite;
                this.Name = NextUpgrade.Name;

                // Assign the value to the upgrade button
                Debug.Log("this.Price: " + Price);
                Debug.Log("this.NextUpgrade.Price" + NextUpgrade.Price);
                ComponentLevel++;
                // UpdateComputerPanel();
            } catch (NullReferenceException nre) {
                Debug.LogException(nre);
            }
        } else {
            Debug.Log("Currently no Upgrades left");
        }
    }

    /*
    public virtual string GetStats() {

    }*/

    /// <summary>
    /// Takes in the status boolean and returns a text
    /// depending on if boolean is true or false
    /// </summary>
    /// <returns>
    /// Return either "Active" or "Disabled" depending on status boolean
    /// </returns>
    private string GetStatus() {
        string status = "";
        if (this.Status) {
            status = string.Format("<color=#00FF00>Active</color>");
        } else {
            status = string.Format("<color=#FF0000>Disabled</color>");
        }
        return status;
    }

    /// <summary>
    /// Sets the component's canvas image
    /// </summary>
    /// <param name="sprite">the sprite to set to the canvas image</param>
    public void SetCanvasSprite(Sprite sprite) {
        this.canvasImage.sprite = sprite;
    }

}
