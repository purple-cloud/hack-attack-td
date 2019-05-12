using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class <c>BtnFX</c> adds different sounds to button depending on different events
/// (Currently not used)
/// </summary>
public class BtnFX : MonoBehaviour {

    public AudioSource myFx;
    public AudioClip hoverFx;
    public AudioClip clickFx;

    /// <summary>
    /// Plays a sound when hovering over button
    /// </summary>
    public void HoverSound() {
        myFx.PlayOneShot(hoverFx);
    }

    /// <summary>
    /// Plays a sound when clicking a button
    /// </summary>
    public void ClickSound() {
        myFx.PlayOneShot(clickFx);
    }
}
