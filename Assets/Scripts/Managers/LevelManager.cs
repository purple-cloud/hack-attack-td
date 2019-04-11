using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : Singleton<LevelManager> {

    public int currentLevel { get; set; }

    private void Awake() {
        // TODO Change this later. for now it just sets the current level manually to 1 as it is the only level available
        this.currentLevel = 1;
    }

    private void Start() {
        switch (this.currentLevel) {
            case 1:
                InitLevel1();
                break;

            default:
                break;
        }
    }

    /// <summary>
    /// Initializes level 1 related values and information
    /// </summary>
    public void InitLevel1() {
        // Set the initial player currency
        GameManager.Instance.SetCurrency(1000);
        // Set the information panel title
        GameManager.Instance.informationPanelTitle.text = "Level 1 Information";
        // Set the information panel text
        GameManager.Instance.informationPanelText.text = "In this level you will have to defend yourself from incoming attacks from the web. You have access to specific component features, firewall ports control and backup management tool.";
    }

}
