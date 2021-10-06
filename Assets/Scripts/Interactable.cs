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
    public Vector3 playerPositionOnLoad;
    public Sprite openSprite;
    public Sprite closedSprite;

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

    public void OpenDoor(bool save)
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
                    this.GetComponent<SpriteRenderer>().sprite = openSprite;
                    if (save)
                    UpdateStateVariables();                    
                }
            }
        }
    }

    public void CloseDoor(bool save)
    {
        if (type == "door")
        {
            if (open)
            {
                GetComponent<BoxCollider2D>().enabled = true;
                open = false;
                this.GetComponent<SpriteRenderer>().sprite = closedSprite;
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

    public void Unlock(bool save)
    {
        locked = false;
        if(save)
        UpdateStateVariables();
    }

    public void Lock(bool save)
    {
        locked = true;
        if(save)
        UpdateStateVariables();
    }

    private void UpdateStateVariables()
    {
        GameStateVariables.UpdateVariableStateInCurrentScene(this.type, new DoorState(this.gameObject.name, this.open, this.locked));
        Debug.Log("open?: " + open + " :locked?: " + locked);
    }
}
