using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Class <c>CountdownTimer</c> is the main countdown timer in the game.
/// If the countdown time reaches zero the level is over and player wins
/// </summary>
public class CountdownTimer : MonoBehaviour {

    [SerializeField] // A reference to the countdown text
    private Text timerReference;

    [SerializeField] // A reference to the countdown time (manually set depending on level)
    private float countdownTime;

    /// <summary>
    /// Updates the countdown timer text for each second passed
    /// </summary>
    private void Update() {
		if (countdownTime > 0) {
			int min = Mathf.FloorToInt(countdownTime) / 60;
			int sec = Mathf.FloorToInt(countdownTime - (60 * min));

			timerReference.text = System.String.Format("{0:0#}:{1:0#}", min, sec);

			countdownTime -= Time.deltaTime;
		} else {
            GameFinishedPanel.Instance.ShowGameFinishedPanel(true);
		}
	}
}
