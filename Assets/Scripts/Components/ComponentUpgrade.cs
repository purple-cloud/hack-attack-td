using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComponentUpgrade {

    /// <summary>
    /// Generates getter and private setter for upgrade name
    /// </summary>
    public string Name { get; private set; }

    /// <summary>
    /// Generates getter and private setter for upgrade price
    /// </summary>
    public int Price { get; private set; }

    /// <summary>
    /// Getter & setter for the component backup price
    /// </summary>
    public int BackupPrice { get; set; }

    /// <summary>
    /// Getter & setter for the component backup restore price
    /// </summary>
    public int BackupRestorePrice { get; set; }

    /// <summary>
    /// Generates getter and private setter for upgrade durability
    /// </summary>
    public float Durability { get; private set; }

    /// <summary>
    /// Generates getter and private setter for upgrade encryption
    /// </summary>
    public float Encryption { get; private set; }

    /// <summary>
    /// Generates getter and private setter for repair price
    /// </summary>
    public int RepairPrice { get; private set; }

    /// <summary>
    /// Generates getter and private setter for sell value
    /// </summary>
    public int SellValue { get; private set; }

    /// <summary>
    /// Generates getter and private setter for upgrade sprite
    /// </summary>
    public Sprite Sprite { get; private set; }

    /// <summary>
    /// General constructor for regular component upgrades
    /// </summary>
    /// <param name="price">price of the new component upgrade</param>
    /// <param name="sprite">sprite of the new component upgrade</param>
    public ComponentUpgrade(string name, int price, Sprite sprite, int repairPrice, int sellVal, int backupPrice, int backupRestorePrice) {
        this.Name = name;
        this.Price = price;
        this.Sprite = sprite;
        this.RepairPrice = repairPrice;
        this.SellValue = sellVal;
        this.BackupPrice = backupPrice;
        this.BackupRestorePrice = backupRestorePrice;
    }

    /// <summary>
    /// Constructor for Computer Component upgrade
    /// </summary>
    /// <param name="price">price of the new computer upgrade</param>
    /// <param name="durability">durability of the new computer upgrade</param>
    /// <param name="sprite">sprite of the new computer upgrade</param>
    public ComponentUpgrade(string name, int price, Sprite sprite, int repairPrice, int sellVal, float durability, int backupPrice, int backupRestorePrice) {
        this.Name = name;
        this.Price = price;
        this.Sprite = sprite;
        this.RepairPrice = repairPrice;
        this.SellValue = sellVal;
        this.Durability = durability;
        this.BackupPrice = backupPrice;
        this.BackupRestorePrice = backupRestorePrice;
    }

    /// <summary>
    /// Constructor for encryption upgrade
    /// </summary>
    /// <param name="price">price of the new computer upgrade</param>
    /// <param name="encryption">encryption of the new computer upgrade</param>
    /// <param name="sprite">sprite of the new computer upgrade</param>
    public ComponentUpgrade(string name, int price, Sprite sprite, float encryption, int backupPrice, int backupRestorePrice) {
        this.Name = name;
        this.Price = price;
        this.Sprite = sprite;
        this.Encryption = encryption;
        this.BackupPrice = backupPrice;
        this.BackupRestorePrice = backupRestorePrice;
    }

}
