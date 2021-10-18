using UnityEngine;
using UnityEngine.UI;

public class InventoryLineItemController : MonoBehaviour
{
    [SerializeField]
    private Text itemDisplayName;
    [SerializeField]
    private Image itemIcon;
    private InventoryUIController inventoryUIController;
    private Item item;

    private void SetText(string newText)
    {
        if (newText != null && newText != "")
            itemDisplayName.text = newText;
        else
            itemDisplayName.text = "NULL ITEM NAME";
    }

    private void SetIcon(Sprite newSprite)
    {
        if(newSprite != null)
            itemIcon.sprite = newSprite;
    }

    public void CreateLineItem(Item newItem, InventoryUIController newUIController)
    {
        item = newItem;
        inventoryUIController = newUIController;

        SetText(item.DisplayName);
        SetIcon(item.InventoryIcon);
    }

    public void OnClick()
    {
        inventoryUIController.ExpandItemDetails(item);
    }
}
