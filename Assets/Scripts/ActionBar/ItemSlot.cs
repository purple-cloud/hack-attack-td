using System;
using Defenses;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// This class is for the item slots on the action bar. Its job is to handle mouse events (such as border coloring when mouse is hovering over it)
/// and create a visible clone.
/// </summary>
public abstract class ItemSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerUpHandler {

    [SerializeField]
    public Image canvasImage;

    // Border color when the item slot is active (i.e. hovering over it, mouse clicking etc.)
    internal Color itemBorderColorActive = new Color(0, 0, 1f);

    // Border color when the item slot is unfocused
    internal Color itemBorderColorDefault = new Color(1f, 1f, 1f);	

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
    internal void SetItemBorderColor(Color c) {
        gameObject.transform.GetChild(0).GetChild(0).GetComponent<Image>().color = c;
    }

    public abstract void OnPointerUp(PointerEventData eventData);
}
