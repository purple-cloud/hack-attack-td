using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BackupSlot : ItemSlot {
    public override void OnPointerUp(PointerEventData eventData) {
        BackupManager.Instance.OpenPanel(this.transform.position);
    }
}
