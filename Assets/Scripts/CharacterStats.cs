using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    [SerializeField]
    private int _baseMaxHealth = 5;
    public int BaseMaxHealth { get => _baseMaxHealth; }
    [SerializeField]
    private int baseStrength = 5;
    [SerializeField]
    private int baseIntelligence = 5;
    [SerializeField]
    private int baseDexterity = 5;
    [SerializeField]
    private int baseArmour = 0;
    [SerializeField]
    private int baseWeaponStrength = 1;
    [SerializeField]
    private int baseAccuracy = 1;

    public int currentMaxHealth = 0;
    public int currentHealth = 0;
    public int currentStrength = 0;
    public int currentIntelligence = 0;
    public int currentDexterity = 0;
    public int currentArmour = 0;
    public int currentWeaponStrength = 0;
    public int currentAccuracy = 0;


    // Start is called before the first frame update
    void Start()
    {
        currentMaxHealth = BaseMaxHealth;
        currentHealth = BaseMaxHealth;
        currentStrength = baseStrength;
        currentIntelligence = baseIntelligence;
        currentDexterity = baseDexterity;
        currentArmour = baseArmour;
        currentWeaponStrength = baseWeaponStrength;
        currentAccuracy = baseAccuracy;
    }

    //statValue parameter will always be added to current value. If intending to remove a stat, pass a negative value.
    public void ModifyCurrentStat(string statType, int statValue)
    {
        switch (statType.ToUpper())
        {
            case "MAXHEALTH":
                currentMaxHealth = currentMaxHealth + statValue;
                break;
            case "HEALTH":
                currentHealth = currentHealth + statValue;
                break;
            case "STRENGTH":
                currentStrength = currentStrength + statValue;
                break;
            case "INTELLIGENCE":
                currentIntelligence = currentIntelligence + statValue;
                break;
            case "DEXTERITY":
                currentDexterity = currentDexterity + statValue;
                break;
            case "ARMOUR":
                currentArmour = currentArmour + statValue;
                break;
            case "WEAPONSTRENGTH":
                currentWeaponStrength = currentWeaponStrength + statValue;
                break;
            case "ACCURACY":
                currentAccuracy = currentAccuracy + statValue;
                break;
            default:
                //make invalid item type error?
                break;
        }
    }

    public void ResetStats()
    {
        currentMaxHealth = BaseMaxHealth;
        currentHealth = BaseMaxHealth;
        currentStrength = baseStrength;
        currentIntelligence = baseIntelligence;
        currentDexterity = baseDexterity;
        currentArmour = baseArmour;
        currentWeaponStrength = baseWeaponStrength;
        currentAccuracy = baseAccuracy;
    }
}
