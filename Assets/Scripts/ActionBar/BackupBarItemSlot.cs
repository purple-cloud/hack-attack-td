using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BackupBarItemSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerUpHandler {

    // TODO Remove [SerializeField] when done testing this field will be set by BackupManager
    [SerializeField] // A reference to the actual backupped component 
    private GameObject backuppedComponent;

    private GameObject clone;

    private Color itemBorderColorActive = new Color(0, 0, 1f);      // Border color when the item slot is active (i.e. hovering over it, mouse clicking etc.)
    private Color itemBorderColorDefault = new Color(1f, 1f, 1f);	// Border color when the item slot is unfocused

    public void OnPointerUp(PointerEventData eventData) {
        this.BackupComponentSelected = true;
        BackupManager.Instance.HighlightReplacableComponents((Component) this.backuppedComponent.GetComponent(typeof(Component)), true);
        CreateClone();
    }
    
    /// <summary>
    /// Creates a clone and displays it. 
    /// <seealso cref="Clone"/>
    /// </summary>
    private void CreateClone() {
        // Save this objects sprite
        Sprite itemSprite = gameObject.transform.GetChild(0).GetComponent<Image>().sprite;
        // Get object from resources
        clone = Instantiate(Defenses.CompController.Instance.clonePrefab) as GameObject;
        clone.GetComponent<Clone>().defensePrefab = backuppedComponent; // Pass the structure object to the clone
        // Name the object to see it in the hierarchy
        clone.name = gameObject.name + " Clone (Snapped)";
        // Get child image (named ItemImage in the hierarchy) and assign this object's sprite
        Image cloneItemImage = clone.transform.GetChild(0).GetComponent<Image>();
        cloneItemImage.sprite = itemSprite;
        // Assign "clone" as a child of ActionBar so it shows on screen
        clone.transform.SetParent(Defenses.CompController.Instance.structureCanvas.transform);
    }

    /// <summary>
	/// Set border color when mouse pointer is on the object.
	/// </summary>
	/// <param name="eventData"></param>
	public void OnPointerEnter(PointerEventData eventData) {
        SetItemBorderColor(this.itemBorderColorActive);
    }

    /// <summary>
	/// Remove border color when mouse pointer hovers away from object.
	/// </summary>
	/// <param name="eventData"></param>
	public void OnPointerExit(PointerEventData eventData) {
        SetItemBorderColor(this.itemBorderColorDefault);
    }

    /// <summary>
    /// Changes the color of the border.
    /// </summary>
    /// <param name="c">New color</param>
    private void SetItemBorderColor(Color c) {
        gameObject.transform.GetChild(0).GetChild(0).GetComponent<Image>().color = c;
    }
}
