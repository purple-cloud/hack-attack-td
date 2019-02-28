using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour {
	private GameObject clone;
	private bool isDragging;
	private Color itemBorderColor = new Color(0, 0, 1f);
	private Color itemBorderColorDefault = new Color(1f, 1f, 1f);

	void Start() {
		isDragging = false;
	}

	void Update() {
		// Check if escape or right mouse is clicked and if the clone is on the display
		if (clone != null) {
			if (Input.GetMouseButtonUp(1) || Input.GetButtonDown("Cancel")) {
				// Delete the object and reset stage
				isDragging = false;
				Destroy(clone);

				// Removes button highlight on the item when dropping it
				EventSystem.current.SetSelectedGameObject(null);
				SetItemBorderColor(itemBorderColorDefault);
			} else if (Input.GetMouseButtonUp(0)) {
				PlaceStructure();
			}
		}

		if (isDragging) {
			// If the cloned object isn't initialized, create a new one
			if (clone == null) {
				CreateClone();
			}

			SetItemBorderColor(itemBorderColor);
			clone.transform.position = new Vector3(Input.mousePosition.x, Input.mousePosition.y);
		}
	}

	// Drag item or place item when clicking
	void OnMouseUp() {
		if (isDragging) {
			//PlaceStructure();
		} else {
			isDragging = true;
		}
	}

	// Set border color when mouse pointer is on the object
	void OnMouseOver() {
		SetItemBorderColor(itemBorderColor);
	}

	// Remove border color when mouse pointer hovers away from object
	void OnMouseExit() {
		if (!isDragging) {
			SetItemBorderColor(itemBorderColorDefault);
		}
	}

	//Pseudo-code for placing the structure on the canvas.
	private void PlaceStructure() {
		isDragging = false;
		EventSystem.current.SetSelectedGameObject(null);
		SetItemBorderColor(itemBorderColorDefault);

		// TODO: Replace buttom logic that places the structure
		Destroy(clone);
		Debug.Log("Structure placed.");
	}

	// Changes the border color of the item on actionbar
	private void SetItemBorderColor(Color c) {
		gameObject.transform.GetChild(0).GetChild(0).GetComponent<Image>().color = c;
	}

	private void CreateClone() {
		// Save this objects sprite
		Sprite itemSprite = gameObject.transform.GetChild(0).GetComponent<Image>().sprite;

		// Get object from resources
		clone = Instantiate(Resources.Load("Prefabs/ItemSlotShell", typeof(GameObject))) as GameObject;

		// Name the object to see it in the hierarchy
		clone.name = gameObject.name + " Clone (Snapped)";

		// Get child image (named ItemImage in the hierarchy) and assign this objects sprite
		Image cloneItemImage = clone.transform.GetChild(0).GetComponent<Image>();
		cloneItemImage.sprite = itemSprite;

		// Assign "clone" as a child of ActionBar so it shows on screen
		clone.transform.SetParent(GameObject.Find("Canvas").transform);
	}
}
