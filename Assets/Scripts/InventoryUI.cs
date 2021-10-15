using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    Inventory inventory;

    // Start is called before the first frame update
    void Start()
    {
        inventory = GameObject.Find("Player").GetComponent<Inventory>();
        inventory.onItemChangedCallback += UpdateUI;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void UpdateUI()
    {
        Debug.Log("Updating UI");
    }
}
