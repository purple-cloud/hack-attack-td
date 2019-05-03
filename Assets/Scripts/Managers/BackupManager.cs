using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackupManager : Singleton<BackupManager> {

    [SerializeField] // A reference to the backup popup panel
    private GameObject backupPanel;

    [SerializeField] // A reference to the backup selection panel
    private GameObject backupSelectionPanel;

    [SerializeField] // A reference to the prefab used to display backup selection
    private GameObject backupBarItemSlotPrefab;

    // Checks to see if user can select component to backup
    public bool BackupReady { get; set; }

    // The selected component to backup
    private GameObject selectedGameObject;

    // The backupped component to replace the current component with
    public GameObject BackuppedComponent { get; set; }

    // Object pool containing backupped components
    private List<GameObject> listOfBackuppedComponents;

    // Checks if user have already selected a backup component to replace existing component
    public bool BackupComponentSelected { get; set; }

    private void Start() {
        this.listOfBackuppedComponents = new List<GameObject>();
        this.BackupComponentSelected = false;
        this.BackupReady = false;

		EventManager.onCancel += () => {
			Defenses.CompController.Instance.HighlightAllStructures(false);

			// Clears variables from adding backup to pool
			selectedGameObject = null;
			BackupReady = false;
			PricePanel.Instance.HidePricePanel();
			// Clears variables from replacing component from backup pool
			BackuppedComponent = null;
		};
	}

    public void InitBackup(GameObject componentToBackup) {
        this.selectedGameObject = componentToBackup;
    }

    public void CancelBackup(GameObject backup) {
        HighlightBackupableComponents(false);
        Destroy(backup);
    }

    public void OpenPanel(Vector3 itemSlotPos) {
        this.backupPanel.transform.position = new Vector3(itemSlotPos.x, (itemSlotPos.y + 115), 0);
        this.backupPanel.SetActive(!(this.backupPanel.activeSelf));
    }

    /// <summary>
    /// Replaces the current component in canvas with the selected backup component
    /// </summary>
    /// <param name="selectedBackup">selected backup component</param>
    /// <param name="objectToReplace">component to be replaced</param>
    public void ReplaceComponent(GameObject selectedBackup, GameObject objectToReplace) {
		HighlightBackupableComponents(false);
		if (this.BackupComponentSelected) {
			// Add the selected backup to the canvas where the object to replace was
			//selectedBackup = Instantiate(selectedBackup);

            GameObject newObject = Instantiate(selectedBackup);

			// Store the component for both backup and the object to replace
			Component objectToReplaceComp = objectToReplace.GetComponent(typeof(Component)) as Component;
			Component selectedBackupComp = newObject.GetComponent(typeof(Component)) as Component;

			selectedBackupComp.input = new List<GameObject>();

			// Insert the new outputs (from the current component's input)
			foreach (Component comp in objectToReplaceComp.GetInputComponents()) {
				Defenses.CompController.RemoveInputOutput(comp, objectToReplaceComp);

				// Link selectedBackups input to selectedBackups outputs input (which is selectedOutput)
				Defenses.CompController.Instance.SetInputOutput(comp, selectedBackupComp);
			}

            System.Type type = ((Component) objectToReplace.GetComponent(typeof(Component))).GetType();
            // Copy all the values of the component inside gameObject and add the to the clone
            System.Reflection.PropertyInfo[] properties = type.GetProperties();
            newObject.AddComponent(gameObject.GetComponents(typeof(Component)).GetType());
            // Assign properties
            for (int i = 0; i <= 17; i++) {
                System.Reflection.PropertyInfo property = properties[i];
                property.SetValue((Component) newObject.GetComponent(typeof(Component)), property.GetValue((Component) selectedBackup.GetComponent(typeof(Component))));
            }

            // Mirror outputs
            selectedBackupComp.outputs = objectToReplaceComp.outputs;

			// Readjust the inputs for all components
			Defenses.CompController.Instance.GenerateStructureInputs();

            // Set the selected backup position to that of the current component position
            //selectedBackup.transform.position = objectToReplace.transform.position;

            newObject.transform.position = objectToReplace.transform.position;

            // Subtract the cost of restoring backup from currency 
            
            if (GameManager.Instance.SubtractFromCurrency(((Component) objectToReplace.GetComponent(typeof(Component))).BackupRestorePrice) == false) {
                Debug.Log("Not enough currency left...");
            }

			// Set the selected backup in the object in canvas layer
			newObject.transform.SetParent(GameObject.Find("ObjectsInCanvas").transform);
            newObject.transform.localScale = new Vector3(1f, 1f, 1f);

			// Destroy the object to replace from canvas
			Destroy(objectToReplace);
			// Reset values
			ResetAll();
		}
	}

    /// <summary>
    /// Resets all values and sets all inputs and outputs
    /// for each game object in list of backupped components
    /// to null. to prevent connection where it isnt supposed to be
    /// </summary>
    public void ResetAll() {
        this.BackupComponentSelected = false;
        this.BackuppedComponent = null;
        foreach (GameObject obj in this.listOfBackuppedComponents) {
            ((Component) obj.GetComponent(typeof(Component))).input = new List<GameObject>();
            ((Component) obj.GetComponent(typeof(Component))).outputs = new List<GameObject>();
        }
    }

    /// <summary>
    /// Is fired when "Add Backup" button 
    /// in backup panel is clicked, and sets global
    /// variables to "waiting" status.
    /// </summary>
    public void AddComponentToBackup() {
        this.ShowBackupPanel(false);
        this.BackupReady = true;
        HighlightBackupableComponents(true);
    }

    /// <summary>
    /// After AddComponent() is fired and user clicks a component in canvas, 
    /// then this component will be taken a backup off and added to 
    /// backup pool/backup selection manager
    /// </summary>
    /// <param name="gameObject">gameobject to backup</param>
    public void AddToBackupPool(GameObject gameObject) {
        this.backupSelectionPanel.SetActive(true);
        try {
            this.BackupReady = false;
            HighlightBackupableComponents(false);
            System.Type type = ((Component) gameObject.GetComponent(typeof(Component))).GetType();
            // TODO FIX this so it isnt instantiated and added to project structure folder
            if (GameObject.Find("ListOfBackuppedGameObjects").transform.childCount >= 7) {
                Destroy(GameObject.Find("ListOfBackuppedGameObjects").transform.GetChild(0).gameObject);
            }
            GameObject clone = Instantiate(gameObject);
            clone.transform.SetParent(GameObject.Find("ListOfBackuppedGameObjects").transform);
            // Copy all the values of the component inside gameObject and add the to the clone
            System.Reflection.PropertyInfo[] properties = type.GetProperties();
            clone.AddComponent(gameObject.GetComponents(typeof(Component)).GetType());
            // Assign properties
            for (int i = 0; i <= 17; i++) {
                System.Reflection.PropertyInfo property = properties[i];
                property.SetValue((Component) clone.GetComponent(typeof(Component)), property.GetValue((Component) gameObject.GetComponent(typeof(Component))));
            }
            ((Component) clone.GetComponent(typeof(Component))).input = null;
            ((Component) clone.GetComponent(typeof(Component))).outputs = null;

			clone.GetComponent<LineHandler>().ResetHandler();
			AddBackupToListOfBackups(clone);
            // Subtract the cost of setting up backup from currency
            if (GameManager.Instance.SubtractFromCurrency(((Component) gameObject.GetComponent(typeof(Component))).BackupPrice) == false) {
                Debug.Log("Not enough currency left...");
            }
        } catch (Exception) {
            Debug.LogError("ERROR: ListOfBackuppedGameObjects reference not found. Please check project structure.");
            //TODO Make something to notify the user
        }
    }

    /// <summary>
    /// Creates a backup slot from the given backup object
    /// </summary>
    /// <param name="backupObject">backup object to create backup slot from</param>
    public void ConvertGameObjectToBackupBarItemSlot(GameObject backupObject) {
        try {
            // Instantiate new backupslot
            GameObject backupSlot = Instantiate(this.backupBarItemSlotPrefab);
            // Set the backupslot to be placed in BackupSelection
            backupSlot.transform.SetParent(GameObject.Find("BackupSelection").transform);
            // Set the backupped game object in backupslot prefab
            ((BackupBarItemSlot) backupSlot.GetComponent(typeof(BackupBarItemSlot))).backuppedComponent = backupObject;
            // Set the backup slot image to be the same of backupObject
            ((BackupBarItemSlot) backupSlot.GetComponent(typeof(BackupBarItemSlot))).canvasImage.sprite = ((Component) backupObject.GetComponent(typeof(Component))).Sprite;
            // Set the scaling of the backupslot
            backupSlot.transform.localScale = new Vector3(1f, 1f, 1f);
        } catch (Exception) {
            Debug.LogError("ERROR: BackupSelection reference not found. Please Check project structure.");
            //TODO Make something to notify the user
        }
    }

    /// <summary>
    /// Adds the backupobject to the list of backupped components.
    /// If the list contains more then 6 backups delete the oldest backup
    /// </summary>
    /// <param name="backupObject">backup object to add to the list of backupped components</param>
    public void AddBackupToListOfBackups(GameObject backupObject) {
        try {
            // If list of backupped components is equal to 5 delete the oldest and add new
            if (this.listOfBackuppedComponents.Count >= 6) {
                this.listOfBackuppedComponents.RemoveAt(0);
            }
            // Add the component to the list of backupped components
            this.listOfBackuppedComponents.Add(backupObject);
            // Delete all current backup slots (TODO Add feature so we dont have to delete all. just replace single)
            foreach (Transform obj in (GameObject.Find("BackupSelection").transform)) {
                Destroy(obj.gameObject);
            }
            // In the end, refresh the backup selection manager to show updated list of backupped components
            foreach (GameObject obj in this.listOfBackuppedComponents) {
                ConvertGameObjectToBackupBarItemSlot(obj);
            }
        } catch (Exception) {
            Debug.LogError("ERROR: BackupSelection reference not found. Please Check project structure.");
            //TODO Make something to notify the user
        }
    }

    /// <summary>
    /// Highlights only the available structures you can swap
    /// the selected backup component with
    /// </summary>
    /// <param name="component">selected backup component</param>
    /// <param name="condition">condition for if it should be highligted or not</param>
    public void HighlightReplacableComponents(Component component, bool condition) {
        try {
            foreach (Transform child in GameObject.Find("ObjectsInCanvas").transform) {
                Component comp;
                if ((comp = child.gameObject.GetComponent(typeof(Component)) as Component) != null) {
                    if (comp.GetType() == component.GetType()) {
                        comp.ShowHighlight(condition, Color.green);
                    }
                }
            }
        } catch (Exception) {
            Debug.LogError("ERROR: ObjectsInCanvas reference not found. Please check project structure.");
        }
    }

    /// <summary>
    /// Highlights all backupable components
    /// </summary>
    /// <param name="state">true to highlight, false to not</param>
    public void HighlightBackupableComponents(bool state) {
        try {
            foreach (Transform child in (GameObject.Find("ObjectsInCanvas").transform)) {
                Component comp;
                if ((comp = child.gameObject.GetComponent(typeof(Component)) as Component) != null) {
                    if (comp.GetType() != typeof(Earth)) {
                        if (comp.BackupPrice <= GameManager.Instance.GetCurrency()) {
                            comp.ShowHighlight(state, Color.green);
                        } else {
                            comp.ShowHighlight(state, Color.red);
                        }
                    }
                }
            }
        } catch (Exception) {
            Debug.LogError("ERROR: ObjectsInCanvas reference not found. Please check project structure.");
        }
    }

    /// <summary>
    /// Shows or hides the backup panel
    /// depending on the input param. true for showing
    /// and false for hiding
    /// </summary>
    /// <param name="active">either true or false</param>
    public void ShowBackupPanel(bool active) {
        this.backupPanel.SetActive(active);
    }

    /// <summary>
    /// Shows and hides the backup selection panel
    /// </summary>
    public void ShowBackupSelectionPanel() {
        ShowBackupPanel(false);
        this.backupSelectionPanel.SetActive(!this.backupSelectionPanel.activeSelf);
    }
}
