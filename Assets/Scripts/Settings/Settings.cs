using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// Class <c>Settings</c> is a class dedicated to the settings panel that pops 
/// when clicking the top right settings icon
/// </summary>
public class Settings : Singleton<Settings> {

    [SerializeField] // Reference to the settings panel
    private GameObject settingsPanel;

    [SerializeField] // Reference to the exit button
    private Button exitBtn;

    [SerializeField] // Reference to the continue button
    private Button continueBtn;

    [SerializeField] // Reference to the fullscreen button
    private Button fullscreenBtn;

    [SerializeField] // Reference to the help button
    private Button helpBtn;

    [SerializeField] // Reference to the concede button
    private Button concedeBtn;

    [SerializeField] // Reference to the next level button
    private Button nextLevelBtn;

    [SerializeField] // Reference to the music volume slider
    private Slider musicSlider;

    [SerializeField] // Reference to the sound volume slider
    private Slider soundSlider;

    /// <summary>
    /// Display or hide the settings panel depending on what is active and not
    /// </summary>
    public void ShowSettingsPanel () {
        this.settingsPanel.SetActive (!this.settingsPanel.activeSelf);
    }

    /// <summary>
    /// Displays or hide either the concede button or the next level button
    /// depedning on which is active and which is not. it just "switches" place
    /// </summary>
    public void ChangeConcedeAndNextLevel() {
        this.concedeBtn.gameObject.SetActive(!this.concedeBtn.gameObject.activeSelf);
        this.nextLevelBtn.gameObject.SetActive(!this.nextLevelBtn.gameObject.activeSelf);
    }

    /// <summary>
    /// Exits the level and go to main menu
    /// </summary>
    public void ExitLevel() {
        SceneManager.LoadScene(0);
    }

    /// <summary>
    /// Reload the currently run level
    /// </summary>
    public void RetryLevel() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    /// <summary>
    /// Loads the next level
    /// </summary>
    public void NextLevel() {
        SceneManager.LoadScene(1);
    }

}