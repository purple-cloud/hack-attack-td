using System.Timers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Class <c>RansomPopup</c> is the panel that displays the ransom message
/// </summary>
public class RansomPopup : Singleton<RansomPopup> {

    [SerializeField] // Reference to the ransom panel
    private GameObject ransomPanel;

    [SerializeField] // Reference to the panel title
    private Text panelTitle;

    [SerializeField] // Reference to the panel description
    private Text panelDescription;

    [SerializeField] // Reference to the panel pay button text
    private Text payText;

    [SerializeField] // Reference to the panel countdown timer
    private Text panelTimer;

    private float initTime;

    private float time;

    private int ransomPrice;

    /// <summary>
    /// Sets the panel information that is displayed
    /// </summary>
    /// <param name="panelTitle">panel title</param>
    /// <param name="panelDescription">panel description</param>
    /// <param name="time">countdown time</param>
    /// <param name="ransomPrice">ransom price</param>
    public void Set(string panelTitle, string panelDescription, float time, int ransomPrice) {
        this.panelTitle.text = panelTitle;
        this.panelDescription.text = panelDescription;
        this.initTime = time;
        this.time = time;
        this.ransomPrice = ransomPrice;
        this.payText.text = "Pay (" + this.ransomPrice + ")";
        this.panelTimer.text = this.time + "s";
    }

    /// <summary>
    /// Updates the panel countdown timer every frame
    /// </summary>
    private void Update() {
        this.time -= Time.deltaTime;
        this.panelTimer.text = this.time.ToString("0");
        if (this.time <= 0) {
            this.time = this.initTime;
            this.ransomPrice = this.ransomPrice * 2;
            this.payText.text = "Pay (" + this.ransomPrice + ")";
        }
    }

    /// <summary>
    /// If ransom is payed update document attack spawn probability, hide panel and give user control over document
    /// </summary>
    public void PayRansom() {
        if (GameManager.Instance.SubtractFromCurrency(this.ransomPrice) == false) {
            Debug.Log("Not enough currency left...");
        } else {
            ShowRansomPanel(false);
            if (UserBehaviourProfile.Instance.DocumentAttackProb > 0.15f) {
                UserBehaviourProfile.Instance.DocumentAttackProb = UserBehaviourProfile.Instance.DocumentAttackProb - 0.15f;
            }
            Debug.Log("Ransom payed");
        }
        UserBehaviourProfile.Instance.documentHacked = false;

		// Refresh panel after payment
		EventManager.TriggerRefreshPanelEvent();
    }
    
    /// <summary>
    /// If ransom is declined update document attack spawn probability and hide panel
    /// </summary>
    public void DeclineRansom() {
        ShowRansomPanel(false);
        UserBehaviourProfile.Instance.DocumentAttackProb = UserBehaviourProfile.Instance.DocumentAttackProb + 0.10f;
        Debug.Log("Ransom declined");

		// Refresh panel after rejection payment request
		EventManager.TriggerRefreshPanelEvent();
    }

    /// <summary>
    /// Displays or hide the panel depending on bool active condition
    /// </summary>
    /// <param name="active"></param>
    public void ShowRansomPanel(bool active) {
        this.ransomPanel.SetActive(active);
    }

}
