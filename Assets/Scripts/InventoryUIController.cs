using System.Collections.Generic;
using UnityEngine;

public class InventoryUIController : MonoBehaviour
{
    private Inventory inventory;
    [SerializeField]
    private GameObject itemLineButtonTemplate;
    private List<GameObject> buttonList = new List<GameObject>();

    void Start()
    {
        inventory = GameObject.Find("Player").GetComponent<Inventory>();
        inventory.onItemChangedCallback += UpdateUI;

        //foreach (Item i in inventory.ListItems())
        //{
        //    Debug.Log("Creating button for item with name: " + i.DisplayName);
        //    GameObject button = Instantiate(itemLineButtonTemplate) as GameObject;

        //    button.GetComponent<InventoryLineItemController>().SetText(i.DisplayName);
        //    button.

        //    button.transform.SetParent(itemLineButtonTemplate.transform.parent, false);
        //    button.SetActive(true);
        //    buttonList.Add(button);
        //}

        RebuildInventoryList();
    }

    public void ButtonClicked(string inString)
    {
        Debug.Log(inString);
    }

    public void UpdateUI()
    {
        Debug.Log("Updating UI");
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

            button.GetComponent<InventoryLineItemController>().SetText(i.DisplayName);
            button.GetComponent<InventoryLineItemController>().SetIcon(i.InventoryIcon);

            button.transform.SetParent(itemLineButtonTemplate.transform.parent, false);
            button.SetActive(true);
            buttonList.Add(button);
        }
    }
}
