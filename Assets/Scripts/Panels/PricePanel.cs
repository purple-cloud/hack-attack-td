using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PricePanel : Singleton<PricePanel> {

    [SerializeField]
    private GameObject panel;

    [SerializeField]
    private Text priceTxt;

    public void ShowPricePanel(string text, int price, Vector3 position) {
        this.panel.SetActive(true);
        this.panel.transform.position = position;
        this.priceTxt.text = text + " " + price + "$";
    }

    public void HidePricePanel() {
        this.panel.SetActive(false);
    }

}
