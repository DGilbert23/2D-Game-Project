using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private List<Item> items = new List<Item>();
    // Start is called before the first frame update
    void Start()
    {
        Item[] startItems;

        startItems = GetComponents<Item>();

        foreach (Item i in startItems)
        {
            AddItem(i);
        }
    }
    
    public void AddItem(Item newItem)
    {
        items.Add(newItem);
    }

    public void RemoveItem(Item removeItem)
    {
        items.Remove(removeItem);
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

    // Update is called once per frame
    void Update()
    {
        
    }
}
