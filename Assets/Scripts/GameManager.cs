using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;

public class GameManager : MonoBehaviour
{
    private static GameManager gameManagerInstance;
    private Party party;
    private PlayerController player;
    private GameObject objInFront = null;
    private Interactable interactObject;
    private bool enablePlayerMovement = true;
    private bool enableInteraction = true;
    private bool enableMenuControls = true;
    private DialogueController currentDialogueController;
    private bool inDialogue = false;

    [SerializeField]
    private GameObject inventoryCanvas;
    [SerializeField]
    private GameObject menuCanvas;
    [SerializeField]
    private GameObject dialogueCanvas;
    [SerializeField]
    private GameObject dialogueChoiceCanvas;
    [SerializeField]
    private GameObject inBuildingFog;
    [SerializeField]
    private GameObject outOfBuildingFog;


    void Awake()
    {
        DontDestroyOnLoad(this);

        if (gameManagerInstance == null)
            gameManagerInstance = this;
        else
            Destroy(gameObject);

        player = GameObject.Find("Player").GetComponent<PlayerController>();
        party = GameObject.Find("Party").GetComponent<Party>();
    }

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

        if (enableMenuControls)
        {
            if (Input.GetKeyDown(KeyCode.I))
            {
                if (!inventoryCanvas.activeInHierarchy)
                {
                    inventoryCanvas.SetActive(true);
                    enablePlayerMovement = false;
                    if (!menuCanvas.activeInHierarchy)
                        menuCanvas.SetActive(true);
                }
                else
                {
                    inventoryCanvas.SetActive(false);
                    enablePlayerMovement = true;
                    if (menuCanvas.activeInHierarchy)
                        menuCanvas.SetActive(false);

                }
            }

            if (Input.GetKeyDown(KeyCode.M))
            {
                if (!menuCanvas.activeInHierarchy)
                {
                    menuCanvas.SetActive(true);
                    enablePlayerMovement = false;
                }
                else
                {
                    menuCanvas.SetActive(false);
                    inventoryCanvas.SetActive(false);
                    enablePlayerMovement = true;
                }
            }

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                CloseAllMenus();
                enablePlayerMovement = true;
            }

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
            case "DOOR":
                if (!interactObject.open)
                    if (!interactObject.locked)
                    {
                        interactObject.OpenDoor(true);
                    }
                    else
                    {
                        string keyName = interactObject.KeyName;
                        bool hasKey = player.GetComponent<Inventory>().HasItem(keyName);
                        Debug.Log(hasKey);
                        if (hasKey)
                        {
                            interactObject.Unlock(true);
                            interactObject.OpenDoor(true);
                        }
                    }
                break;
            case "CHEST":
                Inventory targetInventory;
                if (!interactObject.open)
                {
                    Debug.Log("Begin chest opening");
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
                if (!inDialogue)
                {
                    enablePlayerMovement = false;
                    enableInteraction = false;
                    enableMenuControls = false;

                    currentDialogueController = objInFront.GetComponent<DialogueController>();
                    dialogueCanvas.SetActive(true);
                    inDialogue = true;
                    currentDialogueController.BeginDialogue(dialogueChoiceCanvas);
                }
                break;
            default:
                Debug.Log(interactObject.ExamineText);
                break;
        }
    }

    public void StopDialogue()
    {
        enablePlayerMovement = true;
        enableInteraction = true;
        enableMenuControls = true;
        dialogueCanvas.SetActive(false);
        inDialogue = false;
        currentDialogueController = null;
    }

    public void HandleTransitionObject(string objectName)
    {
        StartCoroutine(MoveToAssociatedScene(objectName));
    }

    private IEnumerator MoveToAssociatedScene(string objectName)
    {
        TransitionObject objectTransitionFrom = GameObject.Find(objectName).GetComponent<TransitionObject>();
        Vector3 playerPosition = GameObject.Find(objectName).GetComponent<TransitionObject>().playerPositionOnLoad;

        Scene currentScene = SceneManager.GetActiveScene();
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(objectTransitionFrom.sceneToLoad);
        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        if(objectTransitionFrom.fogStateOnLoad.x == 1)
        {

        }

        //Handle Player's position on transfer
        player.gameObject.GetComponent<BoxCollider2D>().enabled = false;
        switch (player.lastDirection)
        {
            case 1:
                GameObject.Find("Player").transform.position = playerPosition + new Vector3(-1,0,0);
                break;
            case 2:
                GameObject.Find("Player").transform.position = playerPosition + new Vector3(1, 0, 0);
                break;
            case 3:
                GameObject.Find("Player").transform.position = playerPosition + new Vector3(0, 1, 0);
                break;
            case 4:
                GameObject.Find("Player").transform.position = playerPosition + new Vector3(0, -1, 0);
                break;
            default:
                break;
        }
        player.gameObject.GetComponent<BoxCollider2D>().enabled = true;
        player.ResetMoveTarget();
        UpdateFogGrids();
        SetFogGridStartState(objectTransitionFrom.fogStateOnLoad);

        GameStateVariables.LoadSceneVariables(objectTransitionFrom.sceneToLoad);
    }

    private void CloseAllMenus()
    {
        //TODO - expand on this. Sub-panes of inventory should be made inactive as well so they don't display upon reopen.
        inventoryCanvas.SetActive(false);
        menuCanvas.SetActive(false);
    }

    private void UpdateFogGrids()
    {
        if (inBuildingFog == null)
            inBuildingFog = GameObject.Find("InBuildingFog");
        if (outOfBuildingFog == null)
            outOfBuildingFog = GameObject.Find("OutOfBuildingFog");
    }

    private void SetFogGridStartState(Vector2 state)
    {
        if (state.x == 1)
            inBuildingFog.GetComponent<TilemapRenderer>().enabled = true;
        else if (state.x == 0)
            inBuildingFog.GetComponent<TilemapRenderer>().enabled = false;

        if (state.y == 1)
            outOfBuildingFog.GetComponent<TilemapRenderer>().enabled = true;
        else if (state.y == 0)
            outOfBuildingFog.GetComponent<TilemapRenderer>().enabled = false;
    }

    public void AlternateFogGrids()
    {
        if (inBuildingFog.GetComponent<TilemapRenderer>().enabled)
        {
            inBuildingFog.GetComponent<TilemapRenderer>().enabled = false;
            outOfBuildingFog.GetComponent<TilemapRenderer>().enabled = true;
        }
        else
        {
            inBuildingFog.GetComponent<TilemapRenderer>().enabled = true;
            outOfBuildingFog.GetComponent<TilemapRenderer>().enabled = false;
        }
    }




}
