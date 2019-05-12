using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

/// <summary>
/// Class <c>Computer</c> represent the computer component
/// </summary>
public class Computer : Component, IPointerClickHandler {

    [SerializeField] // Contains the sprites for the different upgrades
    private Sprite[] computerSprites;

    /// <summary>
    /// Start is called on the frame when a script is enabled just before 
    /// any of the Update methods is called the first time.
    /// </summary>
    private void Start() {
        if (AlreadyInitialized == false) {
            Upgrades = new ComponentUpgrade[] {
                new ComponentUpgrade("Modern Laptop", 100, computerSprites[1], 100, 100, 250, 75, 150),
                new ComponentUpgrade("Gaming Laptop", 500, computerSprites[2], 200, 200, 500, 150, 300),
            };
            // Sets initial values
            Name = "Old Computer";
            ComponentLevel = 1;
            Status = true;
            Price = NextUpgrade.Price;
            RepairPrice = 50;
            SellValue = 50;
            Durability = 250f;
            Sprite = computerSprites[0];
            BackupPrice = 30;
            BackupRestorePrice = 75;
            AlreadyInitialized = true;
        } 

    }

    /// <summary>
    /// If Durability of computer is less or equals to 0, then show the Game Over panel
    /// </summary>
    private void Update() {
        if (Durability <= 0) {
            GameFinishedPanel.Instance.ShowGameFinishedPanel(false);
        }
    }

}