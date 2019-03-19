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
            new ComponentUpgrade("Encrypted doucuments", 100, documentSprites[1], 500f),
            new ComponentUpgrade("Advanced Encryption", 500, documentSprites[2], 1000f),
        };
        Name = "Document";
        Status = true;
        Sellable = false;
        Price = NextUpgrade.Price;
        RepairPrice = 50;
        Encryption = 250f;
        Sprite = documentSprites[0];
    }

}

