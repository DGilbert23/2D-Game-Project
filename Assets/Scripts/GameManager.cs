using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public PlayerController player;
    private GameObject objInFront = null;
    private Interactable interactObject;
    //public DialogueManager dialogueMananger;
    private bool enablePlayerMovement = true;
    private bool enableInteraction = true;

    private int buttonCoolDown = 8;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (enableInteraction)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                objInFront = player.GetObjectInFront();
                if (CanInteract(objInFront))
                {
                    Interact();
                }
            }
        }
        //else
        //{
        //    if (dialogueMananger.inDialogue)
        //    {
        //        if (buttonCoolDown > 0)
        //        {
        //            buttonCoolDown--;
        //        }
        //        else if (Input.GetKeyDown(KeyCode.E))
        //        {
        //            buttonCoolDown = 8;
        //            ContinueDialogue();
        //        }
        //    }
        //}

        if (Input.GetKeyDown(KeyCode.I))
        {
            Canvas InventoryCanvas = FindObjectOfType<Canvas>();

            if (InventoryCanvas.enabled)
            {
                InventoryCanvas.enabled = false;
                enablePlayerMovement = true;
            }
            else
            {
                InventoryCanvas.enabled = true;
                enablePlayerMovement = false;
            }

            //foreach (Item i in player.GetComponent<Inventory>().ListItems())
            //{
            //    Debug.Log("Player Item: " + i.ToString());
            //}
        }
    }

    private void FixedUpdate()
    {
        if (enablePlayerMovement)
        {
            player.MovePlayer();
        }
    }

    public void SetEnablePlayerMovement(bool inState)
    {
        enablePlayerMovement = inState;
    }

    private bool CanInteract(GameObject inObject)
    {
        bool interactable = false;

        if (inObject != null)
        {
            interactObject = inObject.GetComponent<Interactable>();
            if (interactObject != null)
            {
                interactable = true;
            }
            else
                interactable = false;
        }
        else
            interactable = false;

        return interactable;
    }

    private void Interact()
    {
        Interactable interactObject = objInFront.GetComponent<Interactable>();
        string objectType = interactObject.GetInteractableType();

        switch (objectType)
        {
            case "door":
                if (!interactObject.open)
                    if (!interactObject.locked)
                    {
                        interactObject.OpenDoor();
                    }
                    else
                    {
                        string keyName = interactObject.keyName;
                        bool hasKey = player.GetComponent<Inventory>().HasItem(keyName);
                        Debug.Log(hasKey);
                        if (hasKey)
                        {
                            interactObject.Unlock();
                            interactObject.OpenDoor();
                        }
                    }
                break;
            case "chest":
                Inventory targetInventory;
                if (!interactObject.open)
                {
                    targetInventory = interactObject.OpenChest();
                    List<Item> removeList = new List<Item>();
                    foreach (Item i in targetInventory.ListItems())
                    {
                        Debug.Log(i.ToString());
                        player.GetComponent<Inventory>().AddItem(i);
                        removeList.Add(i);
                    }
                    foreach (Item i in removeList)
                    {
                        targetInventory.RemoveItem(i);
                    }
                }
                break;
            case "NPC":
                interactObject.OpenDialogue();
                enablePlayerMovement = false;
                enableInteraction = false;
                buttonCoolDown = 5;
                break;
            case "stair":
                StartCoroutine(moveToScene(interactObject.sceneToLoad, interactObject.playerPositionOnLoad));
                break;
            default:
                break;
        }
    }

    public IEnumerator moveToScene(string sceneName, Vector3 newPlayerPosition)
    {
        Scene currentScene = SceneManager.GetActiveScene();

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);

        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        SceneManager.MoveGameObjectToScene(player.gameObject, SceneManager.GetSceneByName(sceneName));
        player.transform.position = newPlayerPosition;
        SceneManager.MoveGameObjectToScene(GetComponent<Canvas>().gameObject, SceneManager.GetSceneByName(sceneName));
        SceneManager.MoveGameObjectToScene(this.gameObject, SceneManager.GetSceneByName(sceneName));

        GameStateVariables.LoadSceneVariables(sceneName);

        SceneManager.UnloadSceneAsync(currentScene);
    }

    //public void ContinueDialogue()
    //{
    //    if (dialogueMananger.inDialogue)
    //    {
    //        dialogueMananger.DisplayNextSentence();
    //    }

    //    if (!dialogueMananger.inDialogue)
    //    {
    //        enablePlayerMovement = true;
    //        enableInteraction = true;
    //    }
    //}
}
