using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public string itemName;
    public string category;
    public int id;
    // Start is called before the first frame update

    public Item(string newName, string newCategory)
    {
        itemName = newName;
        category = newCategory;
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    override public string ToString()
    {
        return itemName;
    }
}
