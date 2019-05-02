using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Settings : MonoBehaviour {

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
    private Slider musicSlider;

    [SerializeField]
    private Slider soundSlider;

    /// <summary>
    /// On mouseClick open settings menu
    /// </summary>
    public void OnMouseDown () {
        this.settingsPanel.SetActive (!this.settingsPanel.activeSelf);
    }

    public void ExitLevel() {
        SceneManager.LoadScene(0);
    }

    public void RetryLevel() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}