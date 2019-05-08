using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// Used in tutorial lvl for the highlighted 
/// predefined firewall placable location
/// </summary>
public class Box : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D collision) {
        Defenses.CompController.Instance.canPlaceTutorialStruct = true;
        Debug.Log("enter");
    }

    private void OnTriggerExit2D(Collider2D collision) {
        Defenses.CompController.Instance.canPlaceTutorialStruct = false;
        Debug.Log("sads");
    }

}
