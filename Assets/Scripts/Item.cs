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
    [SerializeField]
    protected string _displayName;
    public string DisplayName
    {
        get => _displayName;
    }
    [SerializeField]
    protected Sprite _inventoryIcon;
    public Sprite InventoryIcon
    {
        get => _inventoryIcon;
    }
    [SerializeField]
    protected string _description;
    public string Description
    {
        get => _description;
    }

    override public string ToString()
    {
        return ItemName;
    }
}
