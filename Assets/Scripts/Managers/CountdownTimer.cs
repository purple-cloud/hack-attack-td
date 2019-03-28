using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountdownTimer : MonoBehaviour {

    [SerializeField] // A reference to the countdown text
    private Text timerReference;

    [SerializeField] // A reference to the countdown time (manually set depending on level)
    private float countdownTime;

    /// <summary>
    /// Updates the countdown timer text for each second passed
    /// </summary>
    private void Update() {
        this.timerReference.text = Mathf.Floor(this.countdownTime / 60).ToString("00") + ":" + Mathf.RoundToInt(this.countdownTime % 60).ToString("00");
        this.countdownTime -= Time.deltaTime;
    }
}
