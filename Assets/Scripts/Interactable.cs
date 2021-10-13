using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    //public bool door;
    //public bool chest;
    public bool open;
    public bool locked;
    [SerializeField]
    private string _keyName;
    public string KeyName
    {
        get => _keyName; 
    }
    [SerializeField]
    private string _type;
    public string Type
    {
        get => _type;
    }
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
        return Type;
    }

    public void OpenDoor(bool save)
    {
        if (Type == "DOOR")
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
        if (Type == "door")
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

        if (Type == "CHEST")
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
        GameStateVariables.UpdateVariableStateInCurrentScene(this.Type, new DoorState(this.gameObject.name, this.open, this.locked));
    }
}
