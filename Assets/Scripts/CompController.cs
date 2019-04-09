using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Defenses {
	/// <summary>
	/// Global class for managing structures and action bar items.
	/// </summary>
	public class CompController : Singleton<CompController> {

		// The structures the new structure is being placed between
		private GameObject targetStructureObj1;
        // New structure that is being placed
        private GameObject newStructure;        

		[SerializeField]
		public GameObject highlightBorder;

		[HideInInspector] // A reference to the current clone (created from the item slot)
        public GameObject clone;				
		
		/// <summary>
		/// An indication on if a structure is in the progress of being placed.
		/// This variable is only altered by the action bar elements.
		/// </summary>
		public bool IsPlacingStructure { get; set; }

		public bool CollisionDetected { get; set; }

		[SerializeField]
		public Material pathLineMaterial;

		[SerializeField]
		public GameObject clonePrefab;

		private void Start() {
			IsPlacingStructure = false;
		}

		/// <summary>
		/// Takes care of click events on structures on canvas.
		/// </summary>
		/// <param name="obj">The reporting object.</param>
		public void OnStructureClickEvent(GameObject obj) {
			if (IsPlacingStructure) {
				// Checks if the game object has an output
				if ((obj.GetComponent(typeof(Component)) as Component).output == null) {
					// Save the game object and enable dragging of the clone
					targetStructureObj1 = obj;
					EnableCloneDragging();
				} else {
					// Abort placement
					Debug.Log(obj.name + " already has an output.");
					CancelPlacement();
				}
			}
		}

		/// <summary>
		/// Enables clone dragging.
		/// </summary>
		private void EnableCloneDragging() {
			clone.GetComponent<Clone>().isDragging = true;
		}

		/// <summary>
		/// Saves a reference of the clone during the placement of new structure.
		/// </summary>
		/// <param name="clone"></param>
		public void InitClone(GameObject clone) {
			this.clone = clone;
			IsPlacingStructure = true;
			HighlightAllStructures(true);
		}

		/// <summary>
		/// It creates an instance of the prefab associated with the clone and makes it
		/// visible on canvas. On placement, the user pays its price. If there is a collision,
		/// cancel it. There is a problem
		/// </summary>
		public void FinishPlacement() {
			try {
                // If there is a collision between component and the new structure, cancel placement
                if (CollisionDetected) {
                    Debug.Log("You cannot place components over each other.");
                    CollisionDetected = false;
                    CancelPlacement();
                    return;
                }

                newStructure = Instantiate(clone.GetComponent<Clone>().defensePrefab) as GameObject;
                newStructure.transform.position = clone.transform.position;

                //TODO Unity is on some serious drugs. Please make sure that the child component in the defense prefab automatically instantaites the child (Image) object.
                GameObject img = Instantiate(clone.GetComponent<Clone>().defensePrefab.transform.GetChild(0).gameObject) as GameObject;
                img.transform.position = clone.transform.position;
                img.transform.SetParent(newStructure.transform);
                newStructure.transform.SetParent(GameObject.Find("ObjectsInCanvas").transform);
                // End maniac code

                // TODO Fill in more components when added to action-bar
                if (this.newStructure.GetComponent(typeof(Component)).GetType() == typeof(Firewall)) {
                    Debug.Log("Component is Firewall: Subtracting 100 from currency");
                    GameManager.Instance.SetCurrency(GameManager.Instance.GetCurrency() - 100);
                }

                SetInputOutput(targetStructureObj1, newStructure);
                Destroy(clone);
                NullifyPlacementObejcts();
            } catch (Exception) {
                Debug.LogError("ERROR: ObjectsInCanvas reference not found. Please check project structure.");
            }
		}

		/// <summary>
		/// Connects two structures with each other.
		/// </summary>
		/// <param name="input"></param>
		/// <param name="output"></param>
		public void SetInputOutput(GameObject input, GameObject output) {
			Component inputCore = input.GetComponent(typeof(Component)) as Component;
			Component outputCore = output.GetComponent(typeof(Component)) as Component;

			//TODO Change this
			inputCore.output = output;
			outputCore.input.Add(input);
		}

		/// <summary>
		/// Highlights all structures on canvas.
		/// </summary>
		/// <param name="state"></param>
		public void HighlightAllStructures(bool state) {
            try {
                foreach (Transform child in (GameObject.Find("ObjectsInCanvas").transform)) {
                    Component comp;
                    if ((comp = child.gameObject.GetComponent(typeof(Component)) as Component) != null) {
                        comp.ShowHighlight(state);
                    }
                }
            } catch (Exception) {
                Debug.LogError("ERROR: ObjectsInCanvas reference not found. Please check project structure.");
            }
        }

		//TODO Remove this.
		/// <summary>
		/// Finds and destroys all visible clones.
		/// </summary>
		private void DestroyAllClones() {
            try {
                foreach (Transform child in (GameObject.Find("ObjectsInCanvas").transform)) {
                    Clone c;
                    if ((c = child.gameObject.GetComponent<Clone>()) != null) {
                        Destroy(child.gameObject);
                    }
                }
            } catch (Exception) {
                Debug.LogError("ERROR: ObjectsInCanvas reference not found. Please check project structure.");
            }
        }

		/// <summary>
		/// Cleans the mess from placing structures.
		/// </summary>
		public void NullifyPlacementObejcts() {
			clone = null;
			targetStructureObj1 = null;
			newStructure = null;
			
			IsPlacingStructure = false;
			HighlightAllStructures(false);
		}

		/// <summary>
		/// Abandons the placement for the new structure.
		/// </summary>
		/// <param name="clone"></param>
		public void CancelPlacement() {
			// Removes button highlight on the item when dropping it
			EventSystem.current.SetSelectedGameObject(null);

			NullifyPlacementObejcts();
			DestroyAllClones();
			Destroy(clone);
		}
	}
}