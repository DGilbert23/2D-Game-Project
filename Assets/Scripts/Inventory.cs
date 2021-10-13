using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField]
    private List<Item> items = new List<Item>();
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AddItem(Item newItem)
    {
        items.Add(newItem);
    }

    public void RemoveItem(Item removeItem)
    {
        items.Remove(removeItem);
    }

    public Item GetItemByName(string searchItemName)
    {
        return items.Find(x => x.ItemName == searchItemName);
    }

    public List<Item> ListItems()
    {
        return items;
    }

    public void EmptyItems()
    {
        items.Clear();
    }

    public bool HasItem(string searchItem)
    {
        bool found = false;
        int i = 0;
        while(i < items.Count && !found)
        {
            if (items[i].ToString() == searchItem)
            {
                found = true;
            }
            else
            {
                i++;
            }
        }

        return found;
    }
    
}
