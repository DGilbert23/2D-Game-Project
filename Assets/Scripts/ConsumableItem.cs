using UnityEngine;

[CreateAssetMenu(fileName = "New Consumable", menuName = "Item/Consumable", order = 1)]
public class ConsumableItem : Item
{
    public string consumableType;
    public string consumableTypeDescription;

    public void UseItem()
    {

    }
}
