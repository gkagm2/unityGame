using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName =  "New Equipment", menuName = "Inventory/Enquipment")]
public class Equipment : Item {

    public EquipmentSlot equipSlot;

    public int armorModifier;
    public int damageModifier;

    public override void Use()
    {
        base.Use();
        EquipmentManager.instance.Equip(this);
        RemoveFromInventory();
    }
}
 
public enum EquipmentSlot { Head, Chest, Legs, Weapon, Shield, Feet };