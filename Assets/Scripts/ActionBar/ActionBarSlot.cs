using Defenses;
using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// Class <c>ActionBarSlot</c> represent each individual item slot in the action-bar
/// that contains a placable defense prefab.
/// </summary>
public class ActionBarSlot : ItemSlot {

    [SerializeField] // Checks if the itemslot is placable
    private bool isPlacable = false;

    private GameObject clone;

    /// <summary>
    /// Creates a clone from the stored defense prefab
    /// and displays it. 
    /// <seealso cref="Clone"/>
    /// </summary>
    private void CreateClone() {
        try {
            // Save this objects sprite
            Sprite itemSprite = gameObject.transform.GetChild(0).GetComponent<Image>().sprite;

            // Get object from resources
            clone = Instantiate(Defenses.CompController.Instance.clonePrefab) as GameObject;
            clone.GetComponent<Clone>().defensePrefab = defensePrefab; // Pass the structure object to the clone

            // Name the object to see it in the hierarchy
            clone.name = gameObject.name + " Clone (Snapped)";

            // Get child image (named ItemImage in the hierarchy) and assign this object's sprite
            Image cloneItemImage = clone.transform.GetChild(0).GetComponent<Image>();
            cloneItemImage.sprite = itemSprite;
			cloneItemImage.color = new Color(1f, 1f, 1f, 0f);

            // Assign "clone" as a child of ActionBar so it shows on screen
            clone.transform.SetParent(GameObject.Find("ObjectsInCanvas").transform);
        } catch (Exception) {
            Debug.LogError("ERROR: TemporaryLocation reference not found. Please check project structure.");
        }
    }

    /// <summary>
    /// Is called every frame and updates the border
    /// around the item slot depending on user currency
    /// </summary>
    private void Update() {
        if (((Component) this.defensePrefab.GetComponent(typeof(Component))).GetType() == typeof(Firewall)) {
            if (GameManager.Instance.GetCurrency() < 100) {
                this.gameObject.GetComponent<Button>().enabled = false;
                SetItemBorderColor(itemBorderColorInsufficientCurrency);
                insufficientCurrency = true;
            } else {
                insufficientCurrency = false;
                this.gameObject.GetComponent<Button>().enabled = true;
            }
        }
    }

    /// <summary>
    /// Drag item or place item when clicking.
    /// </summary>
    /// <param name="eventData"></param>
    public override void OnPointerUp(PointerEventData eventData) {
        if (Defenses.CompController.Instance.IsPlacingStructure) {
            Defenses.CompController.Instance.CancelPlacement();
        }
        if (this.isPlacable) {
            CreateClone();
            SetItemBorderColor(itemBorderColorActive);
        }
    }
}
