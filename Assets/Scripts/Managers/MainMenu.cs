using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Class <c>MainMenu</c> handles the main menu actions
/// </summary>
public class MainMenu : MonoBehaviour {

    /// <summary>
    /// Loads the level 1 scene
    /// </summary>
    public void PlayLevel1() {
        SceneManager.LoadScene(1);
    }

    /// <summary>
    /// Loads the tutorial scene
    /// </summary>
    public void PlayTutorial() {
        SceneManager.LoadScene(2);
    }

    /// <summary>
    /// Quits the game
    /// </summary>
    public void QuitGame() {
        Application.Quit();
    }

}
