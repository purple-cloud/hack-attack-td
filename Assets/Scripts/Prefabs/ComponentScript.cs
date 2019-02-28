using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComponentScript : MonoBehaviour {

    [SerializeField]
    private string componentName;

    [SerializeField]
    private Sprite componentSprite;

    private bool status = true;

    public void OnMouseDown() {
        ShowComponentStats();
    }

    public void ShowComponentStats() {

        switch (componentName) {

            case "Computer":
                // Fill in something specific
                break;

        }

        GameManager.Instance.SetPanelName(this.componentName);
        GameManager.Instance.SetPanelStatus(this.GetStatus());
        GameManager.Instance.SetPanelSprite(this.componentSprite);
        GameManager.Instance.ShowStats();
        Debug.Log("Component Clicked: " + componentName);
    }

    private string GetStatus() {
        string statusText = "";
        if (this.status) {
            statusText = string.Format("<color=#00FF00>Active</color>");
        } else {
            statusText = string.Format("<color=#FF0000>Disabled</color>");
        }
        return statusText;
    }

}
