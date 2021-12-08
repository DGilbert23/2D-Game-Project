using UnityEngine;

public class MenuController : MonoBehaviour
{
    [SerializeField]
    private GameObject inventoryCanvas;
    [SerializeField]
    private GameObject inventoryDetailPane;
    [SerializeField]
    private GameObject characterDataCanvas;
    private static MenuController menuControllerInstance;

    public void ToggleObject()
    {
        this.gameObject.SetActive(!this.gameObject.activeInHierarchy);
    }

    void Awake()
    {
        DontDestroyOnLoad(this);

        if (menuControllerInstance == null)
            menuControllerInstance = this;
        else
            Destroy(gameObject);
    }

    public void ToggleInventory()
    {

    }

    public void ToggleCharacterData()
    {

    }

    public void ToggleAll()
    {

    }
}
