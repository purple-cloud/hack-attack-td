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
        Upgrades = new ComponentUpgrade[] {
            new ComponentUpgrade("Encrypted doucument", 100, documentSprites[1], 500f, 75, 150),
            new ComponentUpgrade("Advanced encrypted document", 500, documentSprites[2], 1000f, 150, 300),
        };
        Name = "Document";
        Status = true;
        Sellable = false;
        Price = NextUpgrade.Price;
        RepairPrice = 50;
        Encryption = 5.0f;
        Sprite = documentSprites[0];
        BackupPrice = 30;
        BackupRestorePrice = 75;
    }

}