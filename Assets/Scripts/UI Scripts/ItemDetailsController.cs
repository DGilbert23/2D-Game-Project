using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ItemDetailsController : MonoBehaviour
{
    private Item item;
    private EquipmentManager playerEquipment;
    private Inventory playerInventory;
    [SerializeField]
    private GameObject equipableItemSection;
    [SerializeField]
    private GameObject consumableItemSection;
    [SerializeField]
    private Text itemName;
    [SerializeField]
    private Text itemType;
    [SerializeField]
    private Text itemDescription;
    [SerializeField]
    private Image itemIcon;
    [SerializeField]
    private Text intelligenceValue;
    [SerializeField]
    private Text strengthValue;
    [SerializeField]
    private Text dexterityValue;
    [SerializeField]
    private Text hpBonusValue;
    [SerializeField]
    private Text armourValue;
    [SerializeField]
    private Text weaponStrengthValue;
    [SerializeField]
    private Text accuracyValue;
    [SerializeField]
    private GameObject equippedIndicator;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetItem(Item newItem)
    {
        item = newItem;
        PopulateDetails();
    }

    private void PopulateDetails()
    {
        itemName.text = item.DisplayName;
        itemDescription.text = item.Description;
        itemIcon.sprite = item.InventoryIcon;

        playerInventory = GameObject.Find("Player").GetComponent<Inventory>();

        if (item is EquipableItem eItem)
        {
            playerEquipment = GameObject.Find("Player").GetComponent<EquipmentManager>();

            itemType.text = eItem.equipmentSlotDescription;

            intelligenceValue.text = eItem.intelligence.ToString();
            strengthValue.text = eItem.strength.ToString();
            dexterityValue.text = eItem.dexteritiy.ToString();
            hpBonusValue.text = eItem.maxHealth.ToString();
            armourValue.text = eItem.armour.ToString();
            weaponStrengthValue.text = eItem.weaponStrength.ToString();
            accuracyValue.text = eItem.accuracy.ToString();
            equipableItemSection.SetActive(true);
            consumableItemSection.SetActive(false);

            if (playerEquipment.IsItemEquipped(eItem))
            {
                equippedIndicator.SetActive(true);
                GameObject.Find("Button-UseEquip").gameObject.GetComponent<Button>().onClick.RemoveAllListeners();
                GameObject.Find("Button-UseEquip").gameObject.GetComponent<Button>().onClick.AddListener(OnUnEquipClick);
                GameObject.Find("Button-UseEquip-Text").gameObject.GetComponent<Text>().text = "Unequip Item";
            }
            else
            {
                equippedIndicator.SetActive(false);
                GameObject.Find("Button-UseEquip").gameObject.GetComponent<Button>().onClick.RemoveAllListeners();
                GameObject.Find("Button-UseEquip").gameObject.GetComponent<Button>().onClick.AddListener(OnEquipClick);
                GameObject.Find("Button-UseEquip-Text").gameObject.GetComponent<Text>().text = "Equip Item";
            }
            
            GameObject.Find("Button-Drop").gameObject.GetComponent<Button>().onClick.RemoveAllListeners();
            GameObject.Find("Button-Drop").gameObject.GetComponent<Button>().onClick.AddListener(OnDropClick);
        }

        if (item is ConsumableItem cItem)
        {
            itemType.text = cItem.consumableTypeDescription;
            consumableItemSection.SetActive(true);
            equipableItemSection.SetActive(false);
            equippedIndicator.SetActive(false);

            GameObject.Find("Button-UseEquip").gameObject.GetComponent<Button>().onClick.RemoveAllListeners();
            GameObject.Find("Button-UseEquip").gameObject.GetComponent<Button>().onClick.AddListener(OnUseClick);
            GameObject.Find("Button-UseEquip-Text").gameObject.GetComponent<Text>().text = "Use Item";
            GameObject.Find("Button-Drop").gameObject.GetComponent<Button>().onClick.RemoveAllListeners();
            GameObject.Find("Button-Drop").gameObject.GetComponent<Button>().onClick.AddListener(OnDropClick);
        }
    }

    public void ToggleItemDetails(Item itemToDisplay)
    {
        if (this.gameObject.activeInHierarchy)
        {
            if (itemToDisplay == item)
                this.gameObject.SetActive(false);
            else
                SetItem(itemToDisplay);
        }
        else
        {            
            this.gameObject.SetActive(true);
            SetItem(itemToDisplay);
        }
    }

    public void OnEquipClick()
    {
        playerEquipment.EquipItem(item as EquipableItem);
        PopulateDetails();
    }

    public void OnUnEquipClick()
    {
        playerEquipment.UnEquipItem(item as EquipableItem);
        PopulateDetails();
    }

    public void OnUseClick()
    {
        Debug.Log("Use button clicked");
    }

    public void OnDropClick()
    {
        if(item is EquipableItem eitem)
        {
            playerEquipment.UnEquipItem(eitem);
        }
        playerInventory.RemoveItem(item);
        ToggleItemDetails(item);
    }
}
