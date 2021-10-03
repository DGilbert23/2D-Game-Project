using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    //public bool door;
    //public bool chest;
    public bool open;
    public bool locked;
    public string keyName;
    public string type;
    public string sceneToLoad;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public string GetInteractableType()
    {
        return type;
    }

    public void OpenDoor()
    {
        if (type == "door")
        {
            if (!open)
            {
                if (!locked)
                {
                    BoxCollider2D collider = GetComponent<BoxCollider2D>();
                    collider.enabled = false;
                    open = true;
                }
            }
            else
            {
                BoxCollider2D collider = GetComponent<BoxCollider2D>();
                collider.enabled = true;
            }
        }
    }

    public Inventory OpenChest()
    {
        Inventory chestContents = null;

        if (type == "chest")
        {
            if (!open)
            {
                if (!locked)
                {
                    chestContents = GetComponent<Inventory>();
                    open = true;
                }
            }
        }

        return chestContents;
    }

    public void OpenDialogue()
    {
        //FindObjectOfType<DialogueTrigger>().TriggerDialogue();
    }

    public void Unlock()
    {
        locked = false;
    }
}
