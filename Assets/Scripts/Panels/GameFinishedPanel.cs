using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameFinishedPanel : Singleton<GameFinishedPanel> {

    [SerializeField]
    private GameObject panel;

    [SerializeField]
    private Text panelHeader;

    public void ShowGameFinishedPanel(bool state) {
        if (state) {
            this.panelHeader.text = string.Format("<color=#00FF00>VICTORY</color>");
        } else {
            this.panelHeader.text = string.Format("<color=#FF0000>GAME OVER</color>");
        }
        this.panel.SetActive(true);
    }

    public void ExitLevel() {
        SceneManager.LoadScene(0);
    }

    public void RetryLevel() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}