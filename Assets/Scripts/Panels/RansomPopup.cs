using System.Timers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RansomPopup : Singleton<RansomPopup> {

    [SerializeField]
    private GameObject ransomPanel;

    [SerializeField]
    private Text panelTitle;

    [SerializeField]
    private Text panelDescription;

    [SerializeField]
    private Text payText;

    [SerializeField]
    private Text panelTimer;

    private float initTime;

    private float time;

    private int ransomPrice;

    public void Set(string panelTitle, string panelDescription, float time, int ransomPrice) {
        this.panelTitle.text = panelTitle;
        this.panelDescription.text = panelDescription;
        this.initTime = time;
        this.time = time;
        this.ransomPrice = ransomPrice;
        this.payText.text = "Pay (" + this.ransomPrice + ")";
        this.panelTimer.text = this.time + "s";
    }

    private void Update() {
        this.time -= Time.deltaTime;
        this.panelTimer.text = this.time.ToString("0");
        if (this.time <= 0) {
            this.time = this.initTime;
            this.ransomPrice = this.ransomPrice * 2;
            this.payText.text = "Pay (" + this.ransomPrice + ")";
        }
    }

    public void PayRansom() {
        GameManager.Instance.SetCurrency(GameManager.Instance.GetCurrency() - this.ransomPrice);
        ShowRansomPanel(false);
        Debug.Log("Ransom payed");

    }
    
    public void DeclineRansom() {
        ShowRansomPanel(false);
        Debug.Log("Ransom declined");
    }

    public void ShowRansomPanel(bool active) {
        this.ransomPanel.SetActive(active);
    }

}
