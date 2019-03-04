using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Component : MonoBehaviour {

    // TODO Extract all panel related info for component
    // into own Panel class??

    // a reference to the panel popup for the computer
    [SerializeField]
    private GameObject panel;

    // A reference to the name on the panel
    [SerializeField]
    private Text panelName;

    // A reference to the component image on panel
    [SerializeField] // This is unused until array of sprites are implemented
    private Image panelImage;

    [SerializeField]
    private Button upgradeButton;

    // A reference to the text price
    [SerializeField]
    private Text txtPrice;

    // The computers upgrade price
    private int upgradePrice;

    // A reference to the computer status text on panel
    private bool panelStatus;

    // X coordinate of the component
    private int X { get; set; }

    // Y coordinate of the component
    public int Y { get; set; }

    // List containing all the specific upgrades for desired component
    public ComponentUpgrade[] Upgrades { get; protected set; }

    public int ComponentLevel { get; protected set; }

    public int Price { get; set; }

    private void Start() {
        // Assign the value to the upgrade button
        GetTxtPriceReference().text = "Upgrade (Cost: " + this.upgradePrice + ")";

        // Assign the PriceCheck function to the changed event on the GameManager
        GameManager.Instance.Changed += new CurrencyChanged(PriceCheck);

        PriceCheck();
    }

    private void Awake() {
        this.ComponentLevel = 1;
    }

    public ComponentUpgrade NextUpgrade {
        get {
            if (this.Upgrades.Length > this.ComponentLevel - 1) {
                return this.Upgrades[this.ComponentLevel - 1];
            }
            return null;
        }
    }

    public void Upgrade() {
        GameManager.Instance.SetCurrency(GameManager.Instance.GetCurrency() - NextUpgrade.Price);
        this.Price += NextUpgrade.Price;
        this.panelName.text = "Lvl " + this.ComponentLevel + ": " + NextUpgrade.Name;
        this.ComponentLevel++;
    }

    /// <summary>
    /// Checks if we have enough money 
    /// TODO Maybe change this to only being able via upgrade button (Own class maybe)
    /// </summary>
    private void PriceCheck() {
        if (this.upgradePrice <= GameManager.Instance.GetCurrency()) {
            this.upgradeButton.interactable = true;
            this.upgradeButton.GetComponent<Image>().color = Color.green;
            GetTxtPriceReference().color = Color.white;
        } else {
            this.upgradeButton.interactable = false;
            this.upgradeButton.GetComponent<Image>().color = Color.grey;
            GetTxtPriceReference().color = Color.black;
        }
    }

    /// <summary>
    /// Takes in the status boolean and returns a text
    /// depending on if boolean is true or false
    /// </summary>
    /// <returns>
    /// Return either "Active" or "Disabled" depending on status boolean
    /// </returns>
    private string GetStatus() {
        string status = "";
        if (this.panelStatus) {
            status = string.Format("<color=#00FF00>Active</color>");
        } else {
            status = string.Format("<color=#FF0000>Disabled</color>");
        }
        return status;
    }

    /// <summary>
    /// Sets the status of the computer component
    /// </summary>
    /// <param name="status">the new status of the computer component</param>
    private void SetStatus(bool status) {
        this.panelStatus = status;
    }

    /// <summary>
    /// Returns the price of the upgrade
    /// </summary>
    /// <returns>Returns the price of the upgrade</returns>
    public int GetUpgradePrice() {
        return this.upgradePrice;
    }

    /// <summary>
    /// Returns the price text reference in the panel
    /// </summary>
    /// <returns>Returns the price text reference in the panel</returns>
    public Text GetTxtPriceReference() {
        return this.txtPrice;
    }

    // TODO Create functions for automatically displaying data on panel
    // TODO Also make the panel popup over the clicked component
}
