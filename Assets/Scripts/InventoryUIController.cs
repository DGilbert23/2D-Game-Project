using System.Collections.Generic;
using UnityEngine;

public class InventoryUIController : MonoBehaviour
{
    private Inventory inventory;
    [SerializeField]
    private GameObject itemLineButtonTemplate;
    [SerializeField]
    private GameObject itemDetailsPanel;
    private List<GameObject> buttonList = new List<GameObject>();

    void Start()
    {
        inventory = GameObject.Find("Player").GetComponent<Inventory>();
        inventory.onItemChangedCallback += UpdateUI;

        RebuildInventoryList();
    }

    public void ExpandItemDetails(Item itemToDisplay)
    {
        itemDetailsPanel.GetComponent<ItemDetailsController>().SetItem(itemToDisplay);
        itemDetailsPanel.SetActive(true);
    }

    public void UpdateUI()
    {
        RebuildInventoryList();
    }
    
    public void RebuildInventoryList()
    {
        foreach(GameObject i in buttonList)
        { 
            Destroy(i);            
        }

        buttonList.Clear();

        foreach (Item i in inventory.ListItems())
        {
            Debug.Log("Creating button for item with name: " + i.DisplayName);
            GameObject button = Instantiate(itemLineButtonTemplate) as GameObject;

            button.GetComponent<InventoryLineItemController>().CreateLineItem(i, this);

            button.transform.SetParent(itemLineButtonTemplate.transform.parent, false);
            button.SetActive(true);
            buttonList.Add(button);
        }
    }
}
