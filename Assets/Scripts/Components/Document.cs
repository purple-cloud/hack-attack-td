using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

/// <summary>
/// Class representing the document component
/// </summary>
public class Document : Component, IPointerClickHandler {

    [SerializeField] // Contains the sprites for the different upgrades
    private Sprite[] documentSprites;

    // Initialization
    private void Start() {
        if (AlreadyInitialized == false) {
            Upgrades = new ComponentUpgrade[] {
                new ComponentUpgrade("Encrypted doucument", 100, documentSprites[1], 25.0f, 75, 150),
                new ComponentUpgrade("Advanced encrypted document", 500, documentSprites[2], 50.0f, 150, 300),
            };
            Name = "Document";
            ComponentLevel = 1;
            Status = true;
            Sellable = false;
            Price = NextUpgrade.Price;
            RepairPrice = 50;
            Encryption = 5.0f;
            Sprite = documentSprites[0];
            BackupPrice = 30;
            BackupRestorePrice = 75;
            AlreadyInitialized = true;
        }
    }

}