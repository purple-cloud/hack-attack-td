using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class <c>ComponentUpgrade</c> represent each upgrade that a component can have.
/// It updates the values of existing components when they are upgraded
/// </summary>
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
    public int Durability { get; private set; }

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
    /// constructor for a default upgrade
    /// </summary>
    /// <param name="name">The new name of the component</param>
    /// <param name="price">The new upgrade price of the component</param>
    /// <param name="sprite">The new sprite of the component</param>
    /// <param name="repairPrice">The new repair price of the component</param>
    /// <param name="sellVal">The new sell value of the component</param>
    /// <param name="backupPrice">The new backup price of the component</param>
    /// <param name="backupRestorePrice">The new restore price for the component</param>
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
    /// constructor for upgrades in use of durability
    /// </summary>
    /// <param name="name">The new name of the component</param>
    /// <param name="price">The new upgrade price of the component</param>
    /// <param name="sprite">The new sprite of the component</param>
    /// <param name="repairPrice">The new repair price of the component</param>
    /// <param name="sellVal">The new sell value of the component</param>
    /// <param name="durability">The new durability of the component</param>
    /// <param name="backupPrice">The new backup price of the component</param>
    /// <param name="backupRestorePrice">The new restore price for the component</param>
    public ComponentUpgrade(string name, int price, Sprite sprite, int repairPrice, int sellVal, int durability, int backupPrice, int backupRestorePrice) {
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
    /// Constructor for upgrades in use of encryption
    /// </summary>
    /// <param name="name">The new name of the component</param>
    /// <param name="price">The new upgrade price of the component</param>
    /// <param name="sprite">The new sprite of the component</param>
    /// <param name="repairPrice">The new repair price of the component</param>
    /// <param name="sellVal">The new sell value of the component</param>
    /// <param name="encryption">The new encryption of the component</param>
    /// <param name="backupPrice">The new backup price of the component</param>
    /// <param name="backupRestorePrice">The new restore price for the component</param>
    public ComponentUpgrade(string name, int price, Sprite sprite, int repairPrice, int sellVal, float encryption, int backupPrice, int backupRestorePrice) {
        this.Name = name;
        this.Price = price;
        this.Sprite = sprite;
        this.RepairPrice = repairPrice;
        this.SellValue = sellVal;
        this.Encryption = encryption;
        this.BackupPrice = backupPrice;
        this.BackupRestorePrice = backupRestorePrice;
    }

}
