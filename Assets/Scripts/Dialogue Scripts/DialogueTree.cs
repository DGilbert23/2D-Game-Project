using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public class DialogueTree
{
    public DialogueNode firstNode;

    public void AddNode(DialogueNode newNode)
    {
        if (firstNode == null)
            firstNode = newNode;
        else
        {
            bool added = false;
            DialogueNode currentNode = firstNode;
            while (!added)
            {
                if (currentNode.right == null)
                {
                    currentNode.right = newNode;
                    added = true;
                }
                else
                {
                    currentNode = currentNode.right;
                }
            }
        }
    }

    public void GenerateTree(XmlNode node)
    {
        AddNode(node);
    }

    public DialogueNode AddNode(XmlNode newNodeXml)
    {
        if (newNodeXml == null)
            return null;

        DialogueNode newNode;
        string newText = newNodeXml.SelectSingleNode("sentence").InnerText;

        List<DialogueCondition> newConditions = new List<DialogueCondition>();

        string type;
        string itemName;
        int playerChoice = 0;
        foreach(XmlNode n in newNodeXml.SelectSingleNode("conditions").SelectNodes("condition"))
        {
            type = n.SelectSingleNode("type").InnerText;
            itemName = n.SelectSingleNode("itemName").InnerText;
            if (int.TryParse(n.SelectSingleNode("playerChoice").InnerText, out int result))
                playerChoice = result;
            
            newConditions.Add(new DialogueCondition(type, itemName, playerChoice));
        }

        if (firstNode == null)
        {
            newNode = new DialogueNode(newText, null, null, newConditions);
            firstNode = newNode;
            newNode.left = AddNode(newNodeXml.SelectSingleNode("leftChild").SelectSingleNode("DialogueNode"));
            newNode.right = AddNode(newNodeXml.SelectSingleNode("rightChild").SelectSingleNode("DialogueNode"));
        }
        else
        {           
            newNode = new DialogueNode(newText, null, null, newConditions);
            newNode.left = AddNode(newNodeXml.SelectSingleNode("leftChild").SelectSingleNode("DialogueNode"));
            newNode.right = AddNode(newNodeXml.SelectSingleNode("rightChild").SelectSingleNode("DialogueNode"));
        }

        return newNode;
    }

    public void ReadTreeRight()
    {
        DialogueNode currentNode = firstNode;
        while (currentNode != null)
        {
            Debug.Log(currentNode.text);            
            if(currentNode.conditions != null)
                foreach(DialogueCondition condition in currentNode.conditions)
                {
                    Debug.Log("Type: " + condition.conditionType + " - ItemName: " + condition.itemName + " - PlayerChoice: " + condition.playerChoice.ToString());
                }
            currentNode = currentNode.right;
        }
    }


}
