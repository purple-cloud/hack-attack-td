using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Delegate for the currency changed event
public delegate void CurrencyChanged();

public class GameManager : Singleton<GameManager> {

    #region EVENTS

    // Event is triggered when currency changes
    public event CurrencyChanged Changed;
        
    #endregion

    [SerializeField] // A reference to the panel object
    private GameObject statsPanel;

    [SerializeField] // A reference to the panel image
    private Image panelImage;

    [SerializeField] // A reference to the panel name
    private Text panelName;

    [SerializeField] // A reference to panel status
    private Text panelStatus;

    [SerializeField] // A reference to the upgrade button
    private Button upgradeButton;
    
    [SerializeField] // A reference to the text price
    private Text txtPrice;
    
    [SerializeField] // A reference to the currency text
    private Text currencyText;

    #region VARIABLES

    // The current selected component
    private Component selectedComponent;

    /// <summary>
    /// if there is a current selected component,
    /// return it, and if not return null
    /// </summary>
    public Component GetSelectedComponent {
        get {
            if (this.selectedComponent != null) {
                return this.selectedComponent;
            } else {
                return null;
            }
        }
    }

    // The player's currency
    private int currency;

    // Indicates if the game is over
    private bool gameOver;

    #endregion

    #region PROPERTIES

    /// <summary>
    /// Returns the current currency
    /// </summary>
    /// <returns>Returns the current currency</returns>
    public int GetCurrency() {
        return this.currency;
    }

    /// <summary>
    /// Sets the currency and activates event
    /// </summary>
    public void SetCurrency(int value) {
        this.currency = value;
        this.currencyText.text = "<color=#FDFF00>Resources: " + value.ToString() + "</color>";
        OnCurrencyChanged();
    }

    #endregion

    /// <summary>
    /// Start is called on the frame when a script is enabled just before 
    /// any of the Update methods is called the first time.
    /// </summary>
    private void Start() {
        // Sets the initial currency
        SetCurrency(700);
    }

    /// <summary>
    /// When the currency changes
    /// </summary>
    public void OnCurrencyChanged() {
        Changed?.Invoke();
    }

    /// <summary>
    /// Selects a component by clicking it
    /// </summary>
    /// <param name="component">The clicked component</param>
    public void SelectComponent(Component component) {
        if (this.selectedComponent != null) {
            // Sets the selectedComponent to the clicked component
            this.selectedComponent = component;            
        }
        this.selectedComponent = component;
        UpdateComputerPanel();
        ShowStats(true);
    }

    /// <summary>
    /// Called when the selectedComponent is clicked again
    /// and sets the selectedComponent to null and hides the stats panel
    /// </summary>
    public void DeselectComponent() {
        if (this.selectedComponent != null) {
            this.selectedComponent = null;
            ShowStats(false);
        }
    }

    /// <summary>
    /// Updates the stats panel information and styling 
    /// depending on what component was clicked and if the 
    /// user have enough currency to buy/upgrade etc..
    /// </summary>
    public void UpdateComputerPanel() {
        this.panelName.text = "Lvl " + this.selectedComponent.ComponentLevel + ": " + this.selectedComponent.Name;
        if (this.selectedComponent.NextUpgrade != null && this.selectedComponent.NextUpgrade.Price <= GetCurrency()) {
            this.upgradeButton.interactable = true;
            this.upgradeButton.GetComponent<Image>().color = Color.green;
            this.txtPrice.color = Color.white;
            this.txtPrice.text = "Upgrade (Cost: " + this.selectedComponent.NextUpgrade.Price + ")";
        } else if (this.selectedComponent.NextUpgrade != null && this.selectedComponent.NextUpgrade.Price > GetCurrency()) {
            this.upgradeButton.interactable = false;
            this.upgradeButton.GetComponent<Image>().color = Color.grey;
            this.txtPrice.color = Color.black;
            this.txtPrice.text = "Upgrade (Cost: " + this.selectedComponent.NextUpgrade.Price + ")";
        } else {
            this.upgradeButton.interactable = false;
            this.upgradeButton.GetComponent<Image>().color = Color.grey;
            this.txtPrice.color = Color.black;
            this.txtPrice.text = "Max Upgraded";
        }
        this.panelImage.GetComponent<Image>().sprite = this.selectedComponent.Sprite;
        this.selectedComponent.SetCanvasSprite(this.selectedComponent.Sprite);
    }

    /// <summary>
    /// Calls the selected components upgrade function
    /// if there are any upgrades left and if the user has enouch currency
    /// </summary>
    public void UpgradeComponent() {
        if (this.selectedComponent != null) {
            if (this.selectedComponent.ComponentLevel <= this.selectedComponent.Upgrades.Length && GetCurrency() >= this.selectedComponent.NextUpgrade.Price) {
                this.selectedComponent.Upgrade();
                UpdateComputerPanel();
            }
        }
    }

    /// <summary>
    /// Shows or hides the stats panel
    /// depending on the input param. true for showing
    /// and false for hiding
    /// </summary>
    /// <param name="active">either true or false</param>
    public void ShowStats(bool active) {
        this.statsPanel.SetActive(active);
    }

}