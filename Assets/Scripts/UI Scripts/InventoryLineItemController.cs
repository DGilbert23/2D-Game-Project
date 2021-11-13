using UnityEngine;
using UnityEngine.UI;

public class InventoryLineItemController : MonoBehaviour
{
    [SerializeField]
    private Text itemDisplayName;
    [SerializeField]
    private Image itemIcon;
    private ItemDetailsController itemDetailsController;
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

    public void CreateLineItem(Item newItem, ItemDetailsController newDetailsController)
    {
        item = newItem;
        itemDetailsController = newDetailsController;

        SetText(item.DisplayName);
        SetIcon(item.InventoryIcon);
    }

    public void OnClick()
    {
        itemDetailsController.ToggleItemDetails(item);
    }
}
