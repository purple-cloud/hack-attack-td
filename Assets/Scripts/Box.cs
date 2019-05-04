using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

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
