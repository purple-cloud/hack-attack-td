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
		private GameObject targetStructure;

		private GameObject targetStructure2;

		// New structure that is being placed
		private GameObject newStructure;

		public bool setNewStructureOutput;

		[SerializeField]
		public GameObject emptyPrefab;

		[SerializeField]
		public GameObject highlightBorder;

		[HideInInspector] // A reference to the current clone (created from the item slot)
        public GameObject clone;

        public bool canPlaceTutorialStruct = true;

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
			setNewStructureOutput = false;
			GenerateStructureInputs();
		}

		public void GenerateStructureInputs() {
			foreach (Component comp in GameObject.FindObjectsOfType(typeof(Component))) {
				if (comp.outputs != null) {
					foreach (Component outputComp in comp.GetOutputComponents()) {
						if (!outputComp.input.Contains(comp.gameObject)) {
							outputComp.input.Add(comp.gameObject);
						}
					}
				}
			}
		}

		/// <summary>
		/// Takes care of click events on structures on canvas.
		/// </summary>
		/// <param name="obj">The reporting object.</param>
		public void OnStructureClickEvent(GameObject obj) {
			if (IsPlacingStructure) {
				if (Input.GetMouseButtonUp(0)) {
					// Save the game object and enable dragging of the clone
					targetStructure = obj;
					EnableCloneDragging();
				} else {
					if (targetStructure == null) {
						// Grab input structure on first call
						targetStructure = obj;
					} else if (targetStructure != null && targetStructure2 == null) {
						// Grab output structure on second call, turns on positition placement of structure
						targetStructure2 = obj;
						EnableCloneDragging();
					}
				}
			}
		}

		/// <summary>
		/// Enables clone dragging and attaches the clone prefab to mouse pointer. Once enabled, 
		/// '"OnStructureClickEvent()" has left the control to the clone class.
		/// </summary>
		private void EnableCloneDragging() {
			clone.GetComponent<Clone>().isDragging = true;
			clone.transform.GetChild(0).GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
		}

		/// <summary>
		/// Saves a reference of the clone during the placement of new structure.
		/// </summary>
		/// <param name="clone"></param>
		public void InitClone(GameObject clone) {
			this.clone = clone;
			IsPlacingStructure = true;
            if (UserBehaviourProfile.Instance.tutorialLvl != true) {
                HighlightAllStructures(true);
            }
		}

		/// <summary>
		/// It creates an instance of the prefab associated with the clone and makes it
		/// visible on canvas. On placement, the user pays its price. If there is a collision,
		/// cancel it. There is a problem
		/// </summary>
		public void FinishPlacement() {
			try {
				if (canPlaceTutorialStruct) {
                    // If there is a collision between component and the new structure, cancel placement
                    if (CollisionDetected) {
                        Debug.Log("You cannot place components over each other.");
                        CollisionDetected = false;
                        CancelPlacement();
                        return;
                    }

                    newStructure = Instantiate(clone.GetComponent<Clone>().defensePrefab) as GameObject;
                    newStructure.name = clone.GetComponent<Clone>().defensePrefab.name;
                    newStructure.transform.position = clone.transform.position;
                    newStructure.transform.SetParent(GameObject.Find("ObjectsInCanvas").transform);
                    newStructure.transform.localScale = new Vector3(1, 1, 1);

                    // TODO Change this to switch case
                    // TODO Fill in more components when added to action-bar
                    if (this.newStructure.GetComponent(typeof(Component)).GetType() == typeof(Firewall)) {
                        Debug.Log("Component is Firewall: Subtracting 100 from currency");

                        // TODO have to exit method and not buy the selected component to place
                        if (GameManager.Instance.SubtractFromCurrency(100) == false) {
                            Debug.Log("Not enough currency left...");
                        }
                    }

                    // targetStructure --> newStructure
                    SetInputOutput(targetStructure, newStructure);

                    // If targetStructure2 is defined, then user has chosen an output aswell
                    if (targetStructure2 != null) {
                        //  targetStructure --> newStructure --> targetStructure2
                        SetInputOutput(newStructure, targetStructure2);
                    }

                    Destroy(clone);
                    NullifyPlacementObejcts();

                    EventManager.TriggerComponentPlacedEvent();
                }
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
			Component inputComp = input.GetComponent(typeof(Component)) as Component;
			Component outputComp = output.GetComponent(typeof(Component)) as Component;

			SetInputOutput(inputComp, outputComp);
		}

		/// <summary>
		/// Connects two structures with each other.
		/// </summary>
		/// <param name="input"></param>
		/// <param name="output"></param>
		public void SetInputOutput(Component input, Component output) {
			input.AddOutput(output.gameObject);
			output.AddInput(input.gameObject);
		}

		public static void RemoveInputOutput(Component inputComp, Component outputComp) {
			inputComp.RemoveOutput(outputComp.gameObject);
			outputComp.RemoveInput(inputComp.gameObject);
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
                        comp.ShowHighlight(state, Color.green);
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
			targetStructure = null;
			targetStructure2 = null;

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

		/// <summary>
		/// Extract the component from the structure without the need of type casting.
		/// </summary>
		/// <param name="structure">The structure to get the component from</param>
		/// <returns>Component of structure</returns>
		public Component ExtractComponent(GameObject structure) {
			Component comp = null;

			try {
				comp = structure.GetComponent(typeof(Component)) as Component;
			} catch (NullReferenceException) {
				Debug.LogWarning("Component not found inside gameobject. (" + structure.name + ")");
			}

			return comp;
		}


		/// <summary>
		/// Retrives the component of structure and sends it to the other method below that will remove it.
		/// </summary>
		/// <param name="structure">The structure that will be removed</param>
		/// <seealso cref="DeleteStructure(Component)"/>
		public void DeleteStructure(GameObject structure) {
			Component comp = ExtractComponent(structure);
			DeleteStructure(comp);
		}


		/// <summary>
		/// Deletes the structure and unlinks paths connected to it if they exists. This prevents disruptions
		/// on the adjacent structure(s).
		/// </summary>
		/// <param name="comp">The component of the structure</param>
		public void DeleteStructure(Component comp) {
			foreach (Component inputComp in comp.GetInputComponents()) {
				// Accesses the input components and removes its output, 'comp'
				inputComp.RemoveOutput(comp.gameObject);
			}

			foreach (Component outputComp in comp.GetOutputComponents()) {
				// Accesses the output components and removes its input, 'comp'
				outputComp.RemoveInput(comp.gameObject);
			}

			// Safe to remove 
			Destroy(comp.gameObject);
		}
	}
}