using UnityEngine;

public class InventoryUIController : MonoBehaviour
{
    private Inventory inventory;
    [SerializeField]
    private GameObject itemLineButtonTemplate;

    void Start()
    {
        inventory = GameObject.Find("Player").GetComponent<Inventory>();
        inventory.onItemChangedCallback += UpdateUI;

        foreach (Item i in inventory.ListItems())
        {
            Debug.Log("Creating button for item with name: " + i.displayName);
            GameObject button = Instantiate(itemLineButtonTemplate) as GameObject;

            button.GetComponent<InventoryLineItemController>().SetText(i.displayName);

            button.transform.SetParent(itemLineButtonTemplate.transform.parent, false);
            button.SetActive(true);
        }
    }

    public void ButtonClicked(string inString)
    {
        Debug.Log(inString);
    }

    public void UpdateUI()
    {
        Debug.Log("Updating UI");
    }
}
