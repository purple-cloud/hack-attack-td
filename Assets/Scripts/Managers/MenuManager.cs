using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour {

    public void PlayLevel1() {
        // Loading Level 1 scene with its build index
        SceneManager.LoadScene(1);
    }

    public void PlayTutorial() {
        SceneManager.LoadScene(2);
    }
}
