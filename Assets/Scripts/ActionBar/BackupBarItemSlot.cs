using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BackupBarItemSlot : ItemSlot {

    private void Update() {
        if (((Component) this.backuppedComponent.GetComponent(typeof(Component))).BackupRestorePrice > GameManager.Instance.GetCurrency()) {
            this.gameObject.GetComponent<Button>().enabled = false;
            SetItemBorderColor(itemBorderColorInsufficientCurrency);
            insufficientCurrency = true;
        } else {
            insufficientCurrency = false;
            this.gameObject.GetComponent<Button>().enabled = true;
        }
    }

    public override void OnPointerUp(PointerEventData eventData) {
        BackupManager.Instance.HighlightReplacableComponents(((Component) this.backuppedComponent.GetComponent(typeof(Component))), true);
        BackupManager.Instance.BackuppedComponent = this.backuppedComponent;
        BackupManager.Instance.BackupComponentSelected = true;
        BackupManager.Instance.ShowBackupSelectionPanel();
    }

}
