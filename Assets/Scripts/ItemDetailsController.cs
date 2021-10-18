using UnityEngine;
using UnityEngine.UI;

public class ItemDetailsController : MonoBehaviour
{
    private Item item;
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

        if (item is EquipableItem eItem)
        {
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
        }

        if (item is ConsumableItem cItem)
        {
            itemType.text = cItem.consumableTypeDescription;
            consumableItemSection.SetActive(true);
            equipableItemSection.SetActive(false);
        }
    }

}
