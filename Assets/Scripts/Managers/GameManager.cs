using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Delegate for the currency changed event
public delegate void CurrencyChanged();

/*
    Class documentation 
*/
public class GameManager : Singleton<GameManager> {

    #region EVENTS

    // Event is triggered when currency changes
    public event CurrencyChanged changed;
        
    #endregion

    [SerializeField] // Panel containing the stats of the component / module
    private GameObject statsPanel;

    [SerializeField] // Panel's sprite
    private Image panelSprite;

    [SerializeField] // Panel's name
    private Text panelName;

    [SerializeField] // Panel's status
    private Text panelStatus;

    [SerializeField]
    private Sprite[] componentSprites;

    #region VARIABLES

    // The current selected component
    private ComponentScript selectedComponent;

    // The player's currency
    private int currency;

    // Indicates if the game is over
    private bool gameOver;

    #endregion

    private void Start() {
        this.currency = 50;
    }

    /* Following Section is panel related */

    /// <summary>
    /// Responsible for both opening and closing
    /// the Stats Panel
    /// </summary>
    public void ShowStats() {
        this.statsPanel.SetActive(!statsPanel.activeSelf);
    }

    public void SetPanelName(string name) {
        this.panelName.text = name;
    }

    public void SetPanelSprite(Sprite sprite) {
        this.panelSprite.sprite = sprite;
    }

    public void SetPanelStatus(string status) {
        this.panelStatus.text = status;
    }

    /* End of panel related section */

}