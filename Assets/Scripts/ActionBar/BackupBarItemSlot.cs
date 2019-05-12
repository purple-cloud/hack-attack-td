using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// Class <c>BackupBarItemSlot</c> represent each individual item slot in 
/// the backup selection panel that contains all backups
/// </summary>
public class BackupBarItemSlot : ItemSlot {

    /// <summary>
    /// Is called every frame and updates the border
    /// around the item slot depending on user currency
    /// </summary>
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

    /// <summary>
    /// When a BackupBarItemSlot is clicked it will highlight all
    /// appropriate components in the system and store the selected backup
    /// so it can be replaced by the later selected component in the system.
    /// </summary>
    /// <param name="eventData"></param>
    public override void OnPointerUp(PointerEventData eventData) {
        PricePanel.Instance.HidePricePanel();
        BackupManager.Instance.HighlightReplacableComponents(((Component) this.backuppedComponent.GetComponent(typeof(Component))), true);
        BackupManager.Instance.BackuppedComponent = this.backuppedComponent;
        BackupManager.Instance.BackupComponentSelected = true;
        BackupManager.Instance.ShowBackupSelectionPanel();
    }

}
