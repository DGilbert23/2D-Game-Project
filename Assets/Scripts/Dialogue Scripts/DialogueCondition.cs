using UnityEngine;

public class DialogueCondition
{
    public string conditionType;
    public string itemName;
    public int playerChoice = 0;

    public DialogueCondition(string newType, string newName, int newChoice)
    {
        conditionType = newType;
        itemName = newName;
        playerChoice = newChoice;
    }

    public bool EvaluateCondition(bool playerChoice = false)
    {
        bool success = false;
        switch(conditionType)
        {
            case "Choice":
                success = playerChoice;
                break;
            case "ItemCheck":
                if (GameObject.Find("Player").GetComponent<Inventory>().HasItem(itemName))
                    success = true;
                break;
            case "Phrase":
                break;
            default:
                break;
        }

        return success;
    }
}
