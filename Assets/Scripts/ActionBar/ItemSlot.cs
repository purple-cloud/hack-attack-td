using Defenses;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// This class is for the item slots on the action bar. Its job is to handle mouse events (such as border coloring when mouse is hovering over it)
/// and create a visible clone.
/// </summary>
public class ItemSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerUpHandler {
	private GameObject clone;
	private Color itemBorderColorActive = new Color(0, 0, 1f);		// Border color when the item slot is active (i.e. hovering over it, mouse clicking etc.)
	private Color itemBorderColorDefault = new Color(1f, 1f, 1f);	// Border color when the item slot is unfocused

	[SerializeField]
	private GameObject defensePrefab;	// The actual defensive structure representing the item in the itemslot

    [SerializeField]
    private bool isPlacable = false;

	/// <summary>
	/// Changes the color of the border.
	/// </summary>
	/// <param name="c">New color</param>
	private void SetItemBorderColor(Color c) {
		gameObject.transform.GetChild(0).GetChild(0).GetComponent<Image>().color = c;
	}

	/// <summary>
	/// Creates a clone and displays it. 
	/// <seealso cref="Clone"/>
	/// </summary>
	private void CreateClone() {
		// Save this objects sprite
		Sprite itemSprite = gameObject.transform.GetChild(0).GetComponent<Image>().sprite;

		// Get object from resources
		clone = Instantiate(CompController.Instance.clonePrefab) as GameObject;
		clone.GetComponent<Clone>().defensePrefab = defensePrefab; // Pass the structure object to the clone

		// Name the object to see it in the hierarchy
		clone.name = gameObject.name + " Clone (Snapped)";

		// Get child image (named ItemImage in the hierarchy) and assign this object's sprite
		Image cloneItemImage = clone.transform.GetChild(0).GetComponent<Image>();
		cloneItemImage.sprite = itemSprite;

		// Assign "clone" as a child of ActionBar so it shows on screen
		clone.transform.SetParent(CompController.Instance.structureCanvas.transform);
	}

    // TODO This and down can be refactored into own abstract class

	/// <summary>
	/// Set border color when mouse pointer is on the object.
	/// </summary>
	/// <param name="eventData"></param>
	public void OnPointerEnter(PointerEventData eventData) {
		SetItemBorderColor(itemBorderColorActive);
	}

	/// <summary>
	/// Drag item or place item when clicking.
	/// </summary>
	/// <param name="eventData"></param>
	public void OnPointerUp(PointerEventData eventData) {
		if (CompController.Instance.IsPlacingStructure == true) {
			CompController.Instance.NullifyPlacementObejcts();
		}
        if (this.isPlacable == false) {
            CreateClone();
            SetItemBorderColor(itemBorderColorActive);
        } else {
            if (this.defensePrefab.GetComponent(typeof(Component)).GetType() == typeof(Backup)) {
                BackupManager.Instance.OpenPanel(this.transform.position);
            }
        }
		
	}

	/// <summary>
	/// Remove border color when mouse pointer hovers away from object.
	/// </summary>
	/// <param name="eventData"></param>
	public void OnPointerExit(PointerEventData eventData) {
		SetItemBorderColor(itemBorderColorDefault);
	}

    // TODO Create method for backing up component that is clicked after selecting backup defense feature in actionbar
}
