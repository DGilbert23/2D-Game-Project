using UnityEngine;

public class EquipmentManager : MonoBehaviour
{
    [SerializeField]
    private EquipableItem headSlot = null;
    [SerializeField]
    private EquipableItem bodySlot = null;
    [SerializeField]
    private EquipableItem handSlot = null;
    [SerializeField]
    private EquipableItem footSlot = null;
    [SerializeField]
    private EquipableItem mainHand = null;
    [SerializeField]
    private EquipableItem offHand = null;

    public bool Equip(string slot, EquipableItem item, int recursionCount = 0)
    {
        bool success = false;

        if (recursionCount > 1)
        {
            Debug.LogError("Equipment caused recursive calls deeper than expected. Breaking out of method. Equipment state may be incorrect!");
            return false;
        }

        switch (slot.ToUpper())
        {
            case "HEAD":
                if (headSlot == null)
                {
                    headSlot = item;
                    AddItemStats(headSlot);
                    success = true;
                }
                else
                {
                    UnEquip(slot);
                    success = Equip(slot, item, recursionCount + 1);
                }
                break;
            case "BODY":
                if (bodySlot == null)
                {
                    bodySlot = item;
                    AddItemStats(bodySlot);
                    success = true;
                }
                else
                {
                    UnEquip(slot);
                    success = Equip(slot, item);
                }
                break;
            case "HAND":
                if (handSlot == null)
                {
                    handSlot = item;
                    AddItemStats(handSlot);
                    success = true;
                }
                else
                {
                    UnEquip(slot);
                    success = Equip(slot, item);
                }
                break;
            case "FOOT":
                if (footSlot == null)
                {
                    footSlot = item;
                    AddItemStats(footSlot);
                    success = true;
                }
                else
                {
                    UnEquip(slot);
                    success = Equip(slot, item);
                }
                break;
            case "MAINHAND":
                if (mainHand == null)
                {
                    mainHand = item;
                    AddItemStats(mainHand);
                    success = true;
                }
                else
                {
                    UnEquip(slot);
                    success = Equip(slot, item);
                }
                break;
            case "OFFHAND":
                if (offHand == null)
                {
                    offHand = item;
                    AddItemStats(offHand);
                    success = true;
                }
                else
                {
                    UnEquip(slot);
                    success = Equip(slot, item);
                }
                break;
            default:
                break;
        }

        return success;
    }

    public bool UnEquip(string slot)
    {
        bool success = false;

        switch (slot)
        {
            case "HEAD":
                if (headSlot != null)
                {
                    RemoveItemStats(headSlot);
                    headSlot = null;
                    success = true;
                }
                break;
            case "BODY":
                if (bodySlot != null)
                {
                    RemoveItemStats(bodySlot);
                    bodySlot = null;
                    success = true;
                }
                break;
            case "HAND":
                if (handSlot != null)
                {
                    RemoveItemStats(handSlot);
                    handSlot = null;
                    success = true;
                }
                break;
            case "FOOT":
                if (footSlot != null)
                {
                    RemoveItemStats(footSlot);
                    footSlot = null;
                    success = true;
                }
                break;
            case "MAINHAND":
                if (mainHand != null)
                {
                    RemoveItemStats(mainHand);
                    mainHand = null;
                    success = true;
                }
                break;
            case "OFFHAND":
                if (offHand != null)
                {
                    RemoveItemStats(offHand);
                    offHand = null;
                    success = true;
                }
                break;
            default:
                break;
        }
        return success;
    }

    private void AddItemStats(EquipableItem item)
    {
        this.gameObject.GetComponent<CharacterStats>().ModifyCurrentStat("MAXHEALTH", item.maxHealth);
        this.gameObject.GetComponent<CharacterStats>().ModifyCurrentStat("STRENGTH", item.strength);
        this.gameObject.GetComponent<CharacterStats>().ModifyCurrentStat("INTELLIGENCE", item.intelligence);
        this.gameObject.GetComponent<CharacterStats>().ModifyCurrentStat("DEXTERITY", item.dexteritiy);
        this.gameObject.GetComponent<CharacterStats>().ModifyCurrentStat("ARMOUR", item.armour);
        this.gameObject.GetComponent<CharacterStats>().ModifyCurrentStat("WEAPONSTRENGTH", item.weaponStrength);
        this.gameObject.GetComponent<CharacterStats>().ModifyCurrentStat("ACCURACY", item.accuracy);
    }

    private void RemoveItemStats(EquipableItem item)
    {
        this.gameObject.GetComponent<CharacterStats>().ModifyCurrentStat("MAXHEALTH", -item.maxHealth);
        this.gameObject.GetComponent<CharacterStats>().ModifyCurrentStat("STRENGTH", -item.strength);
        this.gameObject.GetComponent<CharacterStats>().ModifyCurrentStat("INTELLIGENCE", -item.intelligence);
        this.gameObject.GetComponent<CharacterStats>().ModifyCurrentStat("DEXTERITY", -item.dexteritiy);
        this.gameObject.GetComponent<CharacterStats>().ModifyCurrentStat("ARMOUR", -item.armour);
        this.gameObject.GetComponent<CharacterStats>().ModifyCurrentStat("WEAPONSTRENGTH", -item.weaponStrength);
        this.gameObject.GetComponent<CharacterStats>().ModifyCurrentStat("ACCURACY", -item.accuracy);
    }
}
