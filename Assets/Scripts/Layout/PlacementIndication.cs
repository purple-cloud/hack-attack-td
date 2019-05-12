using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// Used in tutorial lvl for the highlighted predefined firewall placable location.
/// When in placing a component in tutorial, the player will be restricted to only placed inside
/// the area of this gameobject.
/// </summary>
public class PlacementIndication : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D collision) {
        Defenses.CompController.Instance.canPlaceTutorialStruct = true;
    }

    private void OnTriggerExit2D(Collider2D collision) {
        Defenses.CompController.Instance.canPlaceTutorialStruct = false;
    }
}
