using System.Collections.Generic;
using UnityEngine;

public class DialogueNode
{
    public string text;
    public DialogueNode left;
    public DialogueNode right;
    public List<DialogueCondition> conditions;
    public bool requiresUserChoice;

    public DialogueNode(string newText, DialogueNode newLeft, DialogueNode newRight, List<DialogueCondition> newConditions)
    {
        text = newText;
        left = newLeft;
        right = newRight;
        conditions = newConditions;
        if (conditions.Find(x => x.conditionType == "Choice") != null)
            requiresUserChoice = true;
        else
            requiresUserChoice = false;
    }
    
    public bool EvaluateConditions(bool playerChoice = false)
    {
        bool success = true;

        foreach(DialogueCondition c in conditions)
        {
            if (!c.EvaluateCondition(playerChoice))
                success = false;
        }

        return success;
    }
}
