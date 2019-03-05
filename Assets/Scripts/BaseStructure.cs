using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Defenses {
	/// <summary>
	/// Contains the most basic functions for all structures such as base, firewall, etc...
	/// </summary>
	public abstract class BaseStructure : MonoBehaviour, IPointerUpHandler {
		[SerializeField]
		private string inputObjectName;

		[SerializeField]
		private string outputObjectName;

		public GameObject input;
		public GameObject output;

		void Awake() {
			// Sets input and output extracted from Unity editor for predefined levels
			input = (inputObjectName != null) ? GameObject.Find(inputObjectName) : null;
			output = (outputObjectName != null) ? GameObject.Find(outputObjectName) : null;
		}

		/// <summary>
		/// Notifies the controller about the click event.
		/// </summary>
		/// <param name="eventData"></param>
		public void OnPointerUp(PointerEventData eventData) {
			Controller.Instance.OnStructureClickEvent(gameObject);
		}

		/// <summary>
		/// Creates a green border on all compatible combination of structures on the canvas (when an item is picked from the item slot).
		/// </summary>
		/// <param name="state">Controls if the game object will create or remove border.</param>
		public void ShowHighlight(bool state) {
			if (state == true) {
				// Instantiates new game object under this game object and gives it a color
				GameObject borderGO = Instantiate<GameObject>(Resources.Load("Prefabs/HighlightBorder") as GameObject);
				borderGO.GetComponent<Image>().color = new Color(0, 255, 0);	// Green
				borderGO.transform.position = gameObject.transform.position;
				borderGO.transform.SetParent(gameObject.transform);
			} else {
				// Removes the border
				if (gameObject.transform.childCount > 0) {
					Destroy(gameObject.transform.GetChild(0).gameObject);
				}
			}
		}
	}
}