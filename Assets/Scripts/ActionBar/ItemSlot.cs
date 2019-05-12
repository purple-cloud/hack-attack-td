using System;
using Defenses;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// Class <c>ItemSlot</c> is an abstract parent class for <c>ActionBarSlot</c> and <c>BackupBarItemSlot</c>
/// and handles "hover" events and chooses correct colour format depending on user currency, price of component etc.
/// </summary>
public abstract class ItemSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerUpHandler {

    [SerializeField]
    public Image canvasImage;

    [SerializeField] // The actual defensive structure representing the item in the itemslot
    public GameObject defensePrefab;

    [HideInInspector] // A reference to the actual backupped component 
    public GameObject backuppedComponent;

    // Border color when the item slot is active (i.e. hovering over it, mouse clicking etc.)
    internal Color itemBorderColorActive = new Color(0f, 0f, 1f);

    // Border color when the item slot is unfocused
    internal Color itemBorderColorDefault = new Color(1f, 1f, 1f);

    // Border color if player has insufficient amount of currency
    internal Color itemBorderColorInsufficientCurrency = new Color(1f, 0f, 0f);

    internal bool insufficientCurrency = false;

    /// <summary>
	/// Set border color when mouse pointer is on the object depending on user currency
    /// and display price panel above the hovered item slot.
	/// </summary>
	/// <param name="eventData"></param>
	public void OnPointerEnter(PointerEventData eventData) {
        if (insufficientCurrency) {
            SetItemBorderColor(this.itemBorderColorInsufficientCurrency);
        } else {
            SetItemBorderColor(this.itemBorderColorActive);
        }
        // Show Price Panel
        if ((this).GetType() == typeof(ActionBarSlot)) {
            if (((Component) this.defensePrefab.GetComponent(typeof(Component))).GetType() == typeof(Firewall)) {
                PricePanel.Instance.ShowPricePanel("Defense Cost:", 100, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 115, gameObject.transform.position.z));
            }
        } else if ((this).GetType() == typeof(BackupBarItemSlot)) {
            Component component = ((Component) this.backuppedComponent.GetComponent(typeof(Component)));
            PricePanel.Instance.ShowPricePanel("Restore Cost:", component.BackupRestorePrice, new Vector3(gameObject.transform.position.x + 180, gameObject.transform.position.y, gameObject.transform.position.z));
        }
    }

    /// <summary>
	/// Remove border color and price panel when mouse pointer hovers away from object.
	/// </summary>
	/// <param name="eventData"></param>
	public void OnPointerExit(PointerEventData eventData) {
        if (insufficientCurrency) {
            SetItemBorderColor(this.itemBorderColorInsufficientCurrency);
        } else {
            SetItemBorderColor(this.itemBorderColorDefault);
        }
        PricePanel.Instance.HidePricePanel();
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
