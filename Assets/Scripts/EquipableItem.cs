using UnityEngine;

[CreateAssetMenu(fileName = "New Equipable", menuName = "Item/Equipable", order = 2)]
public class EquipableItem : Item
{
    public string equipmentSlot;
    public int intelligence;
    public int strength;
    public int dexteritiy;
    public int armour;
    public int maxHealth;
    public int weaponStrength;
    public int accuracy;
}
