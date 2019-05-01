using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

/// <summary>
/// Class representing the computer component
/// </summary>
public class Computer : Component, IPointerClickHandler {

    [SerializeField] // Contains the sprites for the different upgrades
    private Sprite[] computerSprites;

    // Initialization
    private void Start() {
        if (AlreadyInitialized == false) {
            Upgrades = new ComponentUpgrade[] {
                new ComponentUpgrade("Modern Laptop", 100, computerSprites[1], 100, 100, 250, 75, 150),
                new ComponentUpgrade("Gaming Laptop", 500, computerSprites[2], 200, 200, 500, 150, 300),
            };
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

    private void Update() {
        if (Durability <= 0) {
            GameFinishedPanel.Instance.ShowGameFinishedPanel(false);
        }
    }

}