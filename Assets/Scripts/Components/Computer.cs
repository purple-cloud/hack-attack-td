using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Computer : MonoBehaviour {

    /* Panel related */
    [SerializeField]
    private GameObject panel;

    [SerializeField]
    private Text panelName;

    [SerializeField] // This is unused until array of sprites are implemented
    private Image panelImage;

    private bool panelStatus;

    /* End of panel related */

    [SerializeField]
    private Sprite[] computerSprites;

    private Text[] computerNames;

    private void OnMouseDown () {
        this.panel.SetActive (!panel.activeSelf);
        // TODO Change this to take in name array instead
        this.panelName.text = "Computer";
        this.panelStatus = true;
    }

    private string GetStatus () {
        string status = "";
        if (this.panelStatus) {
            status = string.Format ("<color=#00FF00>Active</color>");
        } else {
            status = string.Format ("<color=#FF0000>Disabled</color>");
        }
        return status;
    }

    private void setPanelStatus(bool status) {
        this.panelStatus = status;
    }

}