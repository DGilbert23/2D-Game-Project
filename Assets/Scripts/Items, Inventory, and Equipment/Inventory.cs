using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField]
    private List<Item> items = new List<Item>();
    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallback;

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

        if (onItemChangedCallback != null)
            onItemChangedCallback.Invoke();
    }

    public void RemoveItem(Item removeItem)
    {
        if(!removeItem.IsImportant)
            items.Remove(removeItem);
        //else
        //display some box indicating that the object is important and can't be dropped.
            

        if (onItemChangedCallback != null)
            onItemChangedCallback.Invoke();
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
        while (i < items.Count && !found)
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
