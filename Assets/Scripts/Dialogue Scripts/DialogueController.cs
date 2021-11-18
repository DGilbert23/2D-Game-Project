using System.Xml;
using UnityEngine;

public class DialogueController : MonoBehaviour
{
    private DialogueTree dialogueTree = new DialogueTree();
    [SerializeField]
    private string dialogueFileName;

    private XmlDocument dialogueXml;

    void Awake()
    {
        TextAsset xmlTextAsset = Resources.Load<TextAsset>("OldManDialogue");
        dialogueXml = new XmlDocument();
        dialogueXml.LoadXml(xmlTextAsset.text);
        GenerateDialogueTree();
    }

    private void GenerateDialogueTree()
    {
        Debug.Log("Generating Dialogue Tree");

        XmlNodeList dialogueNodes = dialogueXml.SelectNodes("DialogueTree/DialogueNode");

        int rootCount = 0;
        foreach (XmlNode node in dialogueNodes)
        {
            if (rootCount == 0)
            {
                rootCount++;
                dialogueTree.GenerateTree(node);
            }
            else
                Debug.Log("BAD XML FILE - Multiple DialogueTree root nodes");
        }

        dialogueTree.ReadTreeRight();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
