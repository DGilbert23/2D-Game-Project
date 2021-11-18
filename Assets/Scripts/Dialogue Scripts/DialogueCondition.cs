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
}
