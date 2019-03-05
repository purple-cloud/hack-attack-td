using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Defenses {
	/// <summary>
	/// Global class for managing structures and action bar items.
	/// </summary>
	public class Controller : MonoBehaviour {
		// The structures the new structure is being placed between
		private GameObject targetStructureObj1;
		private GameObject targetStructureObj2;

		
		private GameObject newStructure;        // New structure that is being placed
		private GameObject clone;				// A reference to the current clone (created from the item slot)

		// Contains the component of the target structures
		private BaseStructure targetStructureObj1Comp;
		private BaseStructure targetStructureObj2Comp;
		
		public bool isPlacingStructure = false;

		[SerializeField]
		private string canvasName;

		public GameObject canvas { get; private set; }

		public static Controller Instance { get; private set; }

		private void Awake() {
			if (Instance == null) {
				Instance = this;
				DontDestroyOnLoad(gameObject);
			} else {
				Destroy(gameObject);
			}
		}

		void Start() {
			canvas = GameObject.Find(canvasName);
			DrawStructurePaths();
		}

		/// <summary>
		/// Draws a path between all connected structures.
		/// </summary>
		private void DrawStructurePaths() {
			foreach (Transform child in canvas.transform) {
				GameObject obj = child.gameObject;
				BaseStructure bs = obj.GetComponent(typeof(BaseStructure)) as BaseStructure;

				if (bs != null) {
					if (bs.output != null) {
						obj.GetComponent<LineRenderer>().SetPosition(0, obj.transform.position);
						obj.GetComponent<LineRenderer>().SetPosition(1, bs.output.transform.position);
					}
				}
			}
		}

		/// <summary>
		/// Takes care of click events on structures on canvas.
		/// </summary>
		/// <param name="obj">The reporting object.</param>
		public void OnStructureClickEvent(GameObject obj) {
			if (isPlacingStructure) {
				// Checks if it is a new or on-going placement
				if (targetStructureObj1 == null && targetStructureObj2 == null) {
					// Find the script derived from BaseStructure and assign it to a variable
					BaseStructure comp = obj.GetComponent(typeof(BaseStructure)) as BaseStructure;

					if (comp.GetType().IsSubclassOf(typeof(BaseStructure))) {
						targetStructureObj1 = obj;
						targetStructureObj1Comp = comp;
					}

					if (targetStructureObj1Comp.output == null) {
						EnableCloneDragging();
					}
				} else if (targetStructureObj2 == null) {
					// Find the script derived from BaseStructure and assign it to a variable for the 2nd object
					BaseStructure comp = obj.GetComponent(typeof(BaseStructure)) as BaseStructure;

					if (comp.GetType().IsSubclassOf(typeof(BaseStructure))) {
						targetStructureObj2 = obj;
						targetStructureObj2Comp = comp;
					}

					// Ensures that the defenses are adjecent
					if ((targetStructureObj1Comp.output != targetStructureObj2 || targetStructureObj2Comp.input != targetStructureObj1) &&
						(targetStructureObj2Comp.output != targetStructureObj1 || targetStructureObj1Comp.input != targetStructureObj2)) {
						Debug.Log("Defenses must be adjecent.");
						foreach (Transform child in canvas.transform) {
							Clone c;
							if ((c = child.gameObject.GetComponent<Clone>()) != null) {
								Destroy(child.gameObject);
								isPlacingStructure = false;
							}
						}
						NullifyPlacementObejcts();
					} else {
						EnableCloneDragging();
					}
				}
			}
		}

		/// <summary>
		/// Enables clone dragging
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
			isPlacingStructure = true;
			HighlightAllStructures(true);
		}

		/// <summary>
		/// Places the structure and cleans u
		/// </summary>
		public void FinishPlacement() {
			newStructure = Instantiate(clone.GetComponent<Clone>().defensePrefab) as GameObject;
			newStructure.transform.position = clone.transform.position;
			newStructure.transform.SetParent(canvas.transform);

			SwapInputOutput();
			Destroy(clone);

			NullifyPlacementObejcts();
		}

		/// <summary>
		/// Fixes the input and output for target and new structures.
		/// </summary>
		private void SwapInputOutput() {
			if (targetStructureObj1Comp.output == null) {
				SetInputOutput(targetStructureObj1, newStructure);

				targetStructureObj1.GetComponent<LineRenderer>().SetPosition(0, targetStructureObj1.transform.position);
				targetStructureObj1.GetComponent<LineRenderer>().SetPosition(1, newStructure.transform.position);
			} else if (targetStructureObj2Comp.output == targetStructureObj1) {
				SetInputOutput(targetStructureObj2, newStructure);
				SetInputOutput(newStructure, targetStructureObj1);

				targetStructureObj2.GetComponent<LineRenderer>().SetPosition(0, targetStructureObj2.transform.position);
				targetStructureObj2.GetComponent<LineRenderer>().SetPosition(1, newStructure.transform.position);
				newStructure.GetComponent<LineRenderer>().SetPosition(0, newStructure.transform.position);
				newStructure.GetComponent<LineRenderer>().SetPosition(1, targetStructureObj1.transform.position);
			} else if (targetStructureObj1Comp.output == targetStructureObj2) {
				// Create a rift for the new structure
				SetInputOutput(targetStructureObj1, newStructure);
				SetInputOutput(newStructure, targetStructureObj2);

				targetStructureObj1.GetComponent<LineRenderer>().SetPosition(0, targetStructureObj1.transform.position);
				targetStructureObj1.GetComponent<LineRenderer>().SetPosition(1, newStructure.transform.position);
				newStructure.GetComponent<LineRenderer>().SetPosition(0, newStructure.transform.position);
				newStructure.GetComponent<LineRenderer>().SetPosition(1, targetStructureObj2.transform.position);
			} else {
				Debug.Log("Something wrong happened...");
			}
		}

		/// <summary>
		/// Connects two structures with each other.
		/// </summary>
		/// <param name="input"></param>
		/// <param name="output"></param>
		public void SetInputOutput(GameObject input, GameObject output) {
			BaseStructure inputCore = input.GetComponent(typeof(BaseStructure)) as BaseStructure;
			BaseStructure outputCore = output.GetComponent(typeof(BaseStructure)) as BaseStructure;

			inputCore.output = output;
			outputCore.input = input;
		}

		/// <summary>
		/// Highlights all structures on canvas.
		/// </summary>
		/// <param name="state"></param>
		public void HighlightAllStructures(bool state) {
			foreach (Transform child in canvas.transform) {
				BaseStructure bs;
				if ((bs = child.gameObject.GetComponent(typeof(BaseStructure)) as BaseStructure) != null) {
					bs.ShowHighlight(state);
				}
			}
		}

		/// <summary>
		/// Finds and destroys all visible clones.
		/// </summary>
		private void DestroyAllClones() {
			foreach (Transform child in canvas.transform) {
				Clone c;
				if ((c = child.gameObject.GetComponent<Clone>()) != null) {
					Destroy(child.gameObject);
				}
			}
		}

		/// <summary>
		/// Cleans the mess from placing structures.
		/// </summary>
		public void NullifyPlacementObejcts() {
			clone = null;
			targetStructureObj1 = null;
			targetStructureObj2 = null;
			targetStructureObj1Comp = null;
			targetStructureObj2Comp = null;
			newStructure = null;
			
			isPlacingStructure = false;
			HighlightAllStructures(false);
		}

		/// <summary>
		/// Abandons the placement for the new structure.
		/// </summary>
		/// <param name="clone"></param>
		public void CancelPlacement(GameObject clone) {
			// Removes button highlight on the item when dropping it
			EventSystem.current.SetSelectedGameObject(null);

			isPlacingStructure = false;
			HighlightAllStructures(false);
			DestroyAllClones();
			Destroy(clone);
		}
	}
}
