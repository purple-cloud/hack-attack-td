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
            new ComponentUpgrade("Awesome Firewall", 200, firewallSprites[1])
        };
        Name = "Shitty Firewall";
        Status = true;
        Price = NextUpgrade.Price;
        Sprite = firewallSprites[0];
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
