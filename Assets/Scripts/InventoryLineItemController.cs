using UnityEngine;
using UnityEngine.UI;

public class InventoryLineItemController : MonoBehaviour
{
    [SerializeField]
    private Text itemDescriptionText;
    [SerializeField]
    private InventoryUIController inventoryUIController;

    public void SetText(string newText)
    {
        itemDescriptionText.text = newText;
    }

    public void OnClick()
    {
        inventoryUIController.ButtonClicked(itemDescriptionText.text);
    }
}
