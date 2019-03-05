using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Computer : MonoBehaviour{

    #region

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

    #endregion

    // Contains the sprites for the different upgrades
    [SerializeField]
    private Sprite[] computerSprites;

    /*
    public string Name { get { return name; } }

    public bool Status { get { return status; } }

    public Sprite Sprite { get { return sprite; } }

    public int UpgradeCost { get { return upgradeCost; } }
    */

    // This is to be removed later
    private string name;
    private bool status;
    private Sprite sprite;
    private int upgradeCost;
    private float durability;

    // List containing all the specific upgrades for desired component
    public ComponentUpgrade[] Upgrades { get; protected set; }

    public int ComponentLevel { get; protected set; }

    public int Price { get; set; }

    // Initialization
    private void Start() {
        // This is specific for this class (Computer)
        Upgrades = new ComponentUpgrade[] {
            new ComponentUpgrade("Modern Laptop", 100, computerSprites[1], 500),
            new ComponentUpgrade("Gaming Laptop", 500, computerSprites[2], 1000),
        };
        this.upgradePrice = NextUpgrade.Price;
        this.durability = 250f;
        this.sprite = computerSprites[0];
        this.name = "Old Computer";
        // TODO Extract most into Component class when Panel class is created

        // Assign the value to the upgrade button
        GetTxtPriceReference().text = "Upgrade (Cost: 100)";
        // Assign the PriceCheck function to the changed event on the GameManager
        GameManager.Instance.Changed += new CurrencyChanged(PriceCheck);
        
        PriceCheck();

        UpdateComputerPanel();
    }

    private void Awake() {
        this.ComponentLevel = 1;
    }

    public ComponentUpgrade NextUpgrade {
        get {
            if (Upgrades.Length > ComponentLevel - 1) {
                Debug.Log("Returned Component: " + Upgrades[ComponentLevel - 1]);
                return Upgrades[ComponentLevel - 1];
            } else {
                return null;
            }
        }
    }

    public void Upgrade() {
        if (this.NextUpgrade != null) {
            try {
                Debug.Log("Component Level: " + this.ComponentLevel);
                GameManager.Instance.SetCurrency(GameManager.Instance.GetCurrency() - NextUpgrade.Price);
                this.durability = NextUpgrade.Durability;
                this.sprite = NextUpgrade.Sprite;
                this.name = NextUpgrade.Name;

                // Assign the value to the upgrade button
                Debug.Log("this.Price: " + Price);
                Debug.Log("this.NextUpgrade.Price" + NextUpgrade.Price);
                ComponentLevel++;
                UpdateComputerPanel();
            } catch (NullReferenceException nre) {
                Debug.LogException(nre);
            }
        } else {
            Debug.Log("Currently no Upgrades left");
        }
        PriceCheck();
    }

    // TODO This method should not be called here. Should be called from the panel handler.
    // Which should be the GameManager
    public void UpdateComputerPanel() {
        this.panelName.text = "Lvl " + this.ComponentLevel + ": " + this.name;
        if (NextUpgrade != null) {
            GetTxtPriceReference().text = "Upgrade (Cost: " + this.NextUpgrade.Price + ")";
        } else {
            this.upgradeButton.interactable = false;
            this.upgradeButton.GetComponent<Image>().color = Color.grey;
            GetTxtPriceReference().color = Color.black;
            GetTxtPriceReference().text = "Max Upgraded";
        }
        this.panelImage.sprite = this.sprite;
    }

    /// <summary>
    /// Checks if we have enough money 
    /// </summary>
    private void PriceCheck() {
        if (this.NextUpgrade.Price <= GameManager.Instance.GetCurrency()) {
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
    /// Sets the different panel parameters and
    /// shows the dynamic computer panel
    /// </summary>
    public void ShowComputerPanel () {
        this.panel.SetActive (!panel.activeSelf);
        // TODO Change this to take in name array instead
        this.panelStatus = true;
    }

    /// <summary>
    /// Takes in the status boolean and returns a text
    /// depending on if boolean is true or false
    /// </summary>
    /// <returns>
    /// Return either "Active" or "Disabled" depending on status boolean
    /// </returns>
    private string GetStatus () {
        string status = "";
        if (this.panelStatus) {
            status = string.Format ("<color=#00FF00>Active</color>");
        } else {
            status = string.Format ("<color=#FF0000>Disabled</color>");
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
    /// Returns the price text reference in the panel
    /// </summary>
    /// <returns>Returns the price text reference in the panel</returns>
    public Text GetTxtPriceReference() {
        return this.txtPrice;
    }



}