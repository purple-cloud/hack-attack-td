using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// Class <c>GameFinishedPanel</c> is the panel that contains either Victory or Game Over
/// </summary>
public class GameFinishedPanel : Singleton<GameFinishedPanel> {

    [SerializeField] // Reference to the panel
    private GameObject panel;

    [SerializeField] // Reference to the panel's header
    private Text panelHeader;

    /// <summary>
    /// Displays Victory or Game over depending on if state is true or false
    /// </summary>
    /// <param name="state"></param>
    public void ShowGameFinishedPanel(bool state) {
        if (state) {
            this.panelHeader.text = string.Format("<color=#00FF00>VICTORY</color>");
        } else {
            this.panelHeader.text = string.Format("<color=#FF0000>GAME OVER</color>");
        }
        this.panel.SetActive(true);
    }

    /// <summary>
    /// Exits to Main Menu Scene
    /// </summary>
    public void ExitLevel() {
        SceneManager.LoadScene(0);
    }

    /// <summary>
    /// Reloads the current scene
    /// </summary>
    public void RetryLevel() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}