using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

/// <summary>
/// Class representing the firewall component
/// </summary>
public class Firewall : Component, IPointerClickHandler {

    [SerializeField] // Contains the sprites for the different upgrades
    private Sprite[] firewallSprites;

    private void Start() {
        Upgrades = new ComponentUpgrade[] {
            new ComponentUpgrade("Awesome Firewall", 200, firewallSprites[1], 100, 100)
        };
        Name = "Shitty Firewall";
        Status = false;
        RepairPrice = 50;
        SellValue = 50;
        Price = NextUpgrade.Price;
        Sprite = firewallSprites[0];
    }

}
