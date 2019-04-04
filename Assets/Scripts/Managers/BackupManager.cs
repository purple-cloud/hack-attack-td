using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackupManager : Singleton<BackupManager> {

    [SerializeField] // A reference to the backup popup panel
    private GameObject backupPanel;

    // Checks to see if user can select component to backup
    public bool BackupReady { get; set; }

    // The selected component to backup
    private GameObject selectedGameObject;

    // Object pool containing backupped components
    private List<GameObject> listOfBackupedComponents;

    private void Start() {
        this.listOfBackupedComponents = new List<GameObject>();
        this.BackupReady = false;
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

    public void AddComponentToBackup() {
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

        for (int i = 0; i <= 17; i++) {
            // Assign properties
            System.Reflection.PropertyInfo property = properties[i];
                property.SetValue((Component) clone.GetComponent(typeof(Component)), property.GetValue((Component)  gameObject.GetComponent(typeof(Component))));
        }
        foreach (System.Reflection.FieldInfo field in fields) {
            // Assign fields
            field.SetValue((Component) clone.GetComponent(typeof(Component)), field.GetValue((Component) gameObject.GetComponent(typeof(Component))));
        }
        this.listOfBackupedComponents.Add(clone);
    }

    public void RestoreBackup() {
        Debug.Log("Restoring Backup...");
    }

    T CopyComponent<T>(T original, GameObject destination) where T : UnityEngine.Component {
        System.Type type = original.GetType();
        UnityEngine.Component copy = destination.AddComponent(type);
        System.Reflection.FieldInfo[] fields = type.GetFields();
        foreach (System.Reflection.FieldInfo field in fields) {
            field.SetValue(copy, field.GetValue(original));
        }
        return copy as T;
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

}
