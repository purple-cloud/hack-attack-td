using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackupManager : Singleton<BackupManager> {

    [SerializeField] // A reference to the backup popup panel
    private GameObject backupPanel;

    [SerializeField] // A reference to the backup selection panel
    private GameObject backupSelectionPanel;

    [SerializeField] // A reference to the name of the game object holding all components
    private string structureCanvasName;

    // Checks to see if user can select component to backup
    public bool BackupReady { get; set; }

    // The selected component to backup
    private GameObject selectedGameObject;

    // Object pool containing backupped components
    private List<GameObject> listOfBackupedComponents;

    public GameObject structureCanvas { get; private set; }

    // Checks if user have already selected a backup component to replace existing component
    private bool backupComponentSelected;

    private void Awake() {
        structureCanvas = GameObject.Find(structureCanvasName);
        if (structureCanvas == null) {
            throw new System.SystemException("CompController has invalid reference to structure canvas. Please check the serialized fields.");
        }
        this.backupComponentSelected = false;
        this.BackupReady = false;
    }

    private void Start() {
        this.listOfBackupedComponents = new List<GameObject>();
    }

    public void InitBackup(GameObject componentToBackup) {
        this.selectedGameObject = componentToBackup;
    }

    public void CancelBackup(GameObject backup) {
        Defenses.CompController.Instance.HighlightAllStructures(false);
        Destroy(backup);
    }

    public void OpenPanel(Vector3 itemSlotPos) {
        this.backupPanel.transform.position = new Vector3(itemSlotPos.x, (itemSlotPos.y + 50), 0);
        this.backupPanel.SetActive(!(this.backupPanel.activeSelf));
    }

    public void ReplaceComponent(GameObject selectedBackup, GameObject objectToReplace) {
        if (this.backupComponentSelected) {
            // Get the type of the object to replace
            System.Type type = ((Component) objectToReplace.GetComponent(typeof(Component))).GetType();
            // Get input and output fields of the object to replace
            System.Reflection.FieldInfo[] fields = type.GetFields();
            // Add the input and output fields extracted above into the backup to be placed
            foreach (System.Reflection.FieldInfo field in fields) {
                field.SetValue((Component) selectedBackup.GetComponent(typeof(Component)), field.GetValue((Component) objectToReplace.GetComponent(typeof(Component))));
            }
            // Destroy the object to replace from canvas
            Destroy(objectToReplace);
            // Add the selected backup to the canvas where the object to replace was
            Instantiate(selectedBackup);
        }
    }

    public void AddComponentToBackup() {
        this.ShowBackupPanel(false);
        Debug.Log("Select component to backup...");
        this.BackupReady = true;
        Defenses.CompController.Instance.HighlightAllStructures(true);
    }

    public void AddToBackupPool(GameObject gameObject) {
        this.BackupReady = false;
        Defenses.CompController.Instance.HighlightAllStructures(false);
        Debug.Log("Component Clicked: " + gameObject.name);
        // TODO The cloned gameobject added to backup contains default component values
        System.Type type = ((Component) gameObject.GetComponent(typeof(Component))).GetType();
        GameObject clone = Instantiate(gameObject);
        // Copy all the values of the component inside gameObject and add the to the clone
        System.Reflection.FieldInfo[] fields = type.GetFields();
        System.Reflection.PropertyInfo[] properties = type.GetProperties();
        clone.AddComponent(gameObject.GetComponents(typeof(Component)).GetType());
        // Assign properties
        for (int i = 0; i <= 17; i++) {
            System.Reflection.PropertyInfo property = properties[i];
                property.SetValue((Component) clone.GetComponent(typeof(Component)), property.GetValue((Component)  gameObject.GetComponent(typeof(Component))));
        }
        this.listOfBackupedComponents.Add(clone);
    }

    public void RestoreBackup() {
        Debug.Log("Restoring Backup...");
    }

    /// <summary>
    /// Highlights only the available structures you can swap
    /// the selected backup component with
    /// </summary>
    /// <param name="component">selected backup component</param>
    /// <param name="condition">condition for if it should be highligted or not</param>
    public void HighlightReplacableComponents(Component component, bool condition) {
        foreach (Transform child in structureCanvas.transform) {
            Component comp;
            if ((comp = child.gameObject.GetComponent(typeof(Component)) as Component) != null) {
                if (comp.GetType() == component.GetType()) {
                    comp.ShowHighlight(condition);
                }
            }
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
