using UnityEngine;

[CreateAssetMenu(fileName = "New Consumable", menuName = "Item")]
public class Item : ScriptableObject
{
    [SerializeField]
    private string _itemName;
    public string ItemName
    {
        get => _itemName;
    }
    public string displayName;

    override public string ToString()
    {
        return ItemName;
    }
}
