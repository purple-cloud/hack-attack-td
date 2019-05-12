using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/// <summary>
/// Class <c>PricePanel</c> is the panel that displays prices on hover
/// </summary>
public class PricePanel : Singleton<PricePanel> {

    [SerializeField] // Reference to the panel
    private GameObject panel;

    [SerializeField] // Reference to the price
    private Text priceTxt;

    /// <summary>
    /// Display price panel with specified text, price and position
    /// </summary>
    /// <param name="text">text to display</param>
    /// <param name="price">price to display</param>
    /// <param name="position">position of the panel</param>
    public void ShowPricePanel(string text, int price, Vector3 position) {
        this.panel.SetActive(true);
        this.panel.transform.position = position;
        this.priceTxt.text = text + " " + price + "$";
    }

    /// <summary>
    /// Hides the price panel
    /// </summary>
    public void HidePricePanel() {
        this.panel.SetActive(false);
    }

}
