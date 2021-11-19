using System.Xml;
using UnityEngine;
using UnityEngine.UI;

public class DialogueController : MonoBehaviour
{
    private DialogueTree dialogueTree = new DialogueTree();
    [SerializeField]
    private string dialogueFileName;
    private bool activeDialogue = false;
    private Text textBox;
    private Text personName;
    private Image personImage;
    private int buttonCoolDown = 8;

    private XmlDocument dialogueXml;

    void Awake()
    {
        TextAsset xmlTextAsset = Resources.Load<TextAsset>("OldManDialogue");
        dialogueXml = new XmlDocument();
        dialogueXml.LoadXml(xmlTextAsset.text);
        GenerateDialogueTree();
        //dialogueTree.ReadTreeRight();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (activeDialogue)
        {
            if (Input.GetKeyDown(KeyCode.K))
            {
                DialogueNode currNode = dialogueTree.NextNode();
                if (currNode != null)
                    textBox.text = currNode.text;
                else
                {
                    activeDialogue = false;
                    GameObject.Find("GameManager").GetComponent<GameManager>().StopDialogue();
                }
            }
        }
    }

    void FixedUpdate()
    {
        
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
    }

    public void BeginDialogue()
    {
        textBox = GameObject.Find("DialogueTextBox").GetComponent<Text>();
        //personName = GameObject.Find("DialogueNameTextBox").GetComponent<Text>();

        textBox.text = dialogueTree.NextNode().text;
        activeDialogue = true;
    }
}
