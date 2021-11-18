using System.Collections.Generic;
using UnityEngine;

public class DialogueNode
{
    public string text;
    public DialogueNode left;
    public DialogueNode right;
    public List<DialogueCondition> conditions;

    public DialogueNode(string newText, DialogueNode newLeft, DialogueNode newRight, List<DialogueCondition> newConditions)
    {
        text = newText;
        left = newLeft;
        right = newRight;
        conditions = newConditions;
    }
}
