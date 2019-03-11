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
        Upgrades = new ComponentUpgrade[] {
            new ComponentUpgrade("Modern Laptop", 100, computerSprites[1], 100, 500),
            new ComponentUpgrade("Gaming Laptop", 500, computerSprites[2], 200, 1000),
        };
        Name = "Old Computer";
        Status = true;
        Price = NextUpgrade.Price;
        RepairPrice = 50;
        Durability = 250f;
        Sprite = computerSprites[0];
    }

    /// <summary>
    /// When the component is clicked it checks if the GameManagers selected component = this component,
    /// and if so closes the information panel. If not it updates the information panel with
    /// the specified information from the clicked component and opens up the panel if its not open
    /// </summary>
    public void OnPointerClick(PointerEventData eventData) {
        if (GameManager.Instance.GetSelectedComponent == this) {
            GameManager.Instance.DeselectComponent();
        } else {
            GameManager.Instance.SelectComponent(this);
            GameManager.Instance.UpdateComputerPanel();
        }
    }

}