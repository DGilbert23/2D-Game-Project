using UnityEngine;
using UnityEngine.UI;

public class InventoryLineItemController : MonoBehaviour
{
    [SerializeField]
    private Text itemDescriptionText;
    [SerializeField]
    private Image itemIcon;
    [SerializeField]
    private InventoryUIController inventoryUIController;

    public void SetText(string newText)
    {
        if (newText != null && newText != "")
            itemDescriptionText.text = newText;
        else
            itemDescriptionText.text = "NULL ITEM NAME";
    }

    public void SetIcon(Sprite newSprite)
    {
        if(newSprite != null)
            itemIcon.sprite = newSprite;
    }

    public void OnClick()
    {
        inventoryUIController.ButtonClicked(itemDescriptionText.text);
    }
}
