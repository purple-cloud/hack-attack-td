using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BackupBarItemSlot : ItemSlot {

    // A reference to the actual backupped component 
    public GameObject backuppedComponent;

    public override void OnPointerUp(PointerEventData eventData) {
        BackupManager.Instance.HighlightReplacableComponents(((Component) this.backuppedComponent.GetComponent(typeof(Component))), true);
        BackupManager.Instance.BackuppedComponent = this.backuppedComponent;
        BackupManager.Instance.BackupComponentSelected = true;
        BackupManager.Instance.ShowBackupSelectionPanel();
    }

}
