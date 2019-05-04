using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Settings : Singleton<Settings> {

    [SerializeField]
    private GameObject settingsPanel;

    [SerializeField]
    private Button exitBtn;

    [SerializeField]
    private Button continueBtn;

    [SerializeField]
    private Button fullscreenBtn;

    [SerializeField]
    private Button helpBtn;

    [SerializeField]
    private Button concedeBtn;

    [SerializeField]
    private Button nextLevelBtn;

    [SerializeField]
    private Slider musicSlider;

    [SerializeField]
    private Slider soundSlider;

    /// <summary>
    /// Displays the settings panel
    /// </summary>
    public void ShowSettingsPanel () {
        this.settingsPanel.SetActive (!this.settingsPanel.activeSelf);
    }

    public void ChangeConcedeAndNextLevel() {
        this.concedeBtn.gameObject.SetActive(!this.concedeBtn.gameObject.activeSelf);
        this.nextLevelBtn.gameObject.SetActive(!this.nextLevelBtn.gameObject.activeSelf);
    }

    public void ExitLevel() {
        SceneManager.LoadScene(0);
    }

    public void RetryLevel() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void NextLevel() {
        SceneManager.LoadScene(1);
    }

}