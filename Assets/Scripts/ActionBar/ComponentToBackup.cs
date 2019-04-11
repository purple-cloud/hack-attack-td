using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ComponentToBackup : MonoBehaviour, IPointerClickHandler {

    private void Start() {
        BackupManager.Instance.InitBackup(gameObject);
    }

    private void Update() {
        if (Input.GetMouseButtonUp(1) || Input.GetButtonDown("Cancel")) {
            BackupManager.Instance.CancelBackup(gameObject);
        }
    }

    public void OnPointerClick(PointerEventData eventData) {
        BackupManager.Instance.AddToBackupPool(gameObject);
    }

}
