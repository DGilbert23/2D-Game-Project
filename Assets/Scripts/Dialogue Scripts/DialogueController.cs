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
    private GameObject choiceCanvas;
    public bool playerChoice = false;
    private int buttonCoolDown = 8;

    private XmlDocument dialogueXml;

    void Awake()
    {
        TextAsset xmlTextAsset = Resources.Load<TextAsset>("DialogueFiles/"+ dialogueFileName);
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
            if (Input.GetKeyDown(KeyCode.Space) && !choiceCanvas.activeInHierarchy)
            {
                ContinueDialogue();
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

    public void BeginDialogue(GameObject dialogueChoiceCanvas)
    {
        textBox = GameObject.Find("DialogueTextBox").GetComponent<Text>();
        choiceCanvas = dialogueChoiceCanvas;
        //personName = GameObject.Find("DialogueNameTextBox").GetComponent<Text>();

        ContinueDialogue();
        activeDialogue = true;
    }

    public void ContinueDialogue()
    {
        DialogueNode currNode = dialogueTree.NextNode(playerChoice);
        if (currNode != null)
        {
            textBox.text = currNode.text;
            if (currNode.requiresUserChoice)
            {
                choiceCanvas.SetActive(true);
                choiceCanvas.transform.Find("YesButton").gameObject.GetComponent<Button>().onClick.RemoveAllListeners();
                choiceCanvas.transform.Find("YesButton").gameObject.GetComponent<Button>().onClick.AddListener(OnClickYes);
                choiceCanvas.transform.Find("NoButton").gameObject.GetComponent<Button>().onClick.AddListener(OnClickNo);

            }
            else if (choiceCanvas.activeInHierarchy)
            {
                choiceCanvas.SetActive(false);
            }
        }
        else
        {
            activeDialogue = false;
            GameObject.Find("GameManager").GetComponent<GameManager>().StopDialogue();
        }
    }

    public void OnClickYes()
    {
        playerChoice = true;
        ContinueDialogue();
    }

    public void OnClickNo()
    {
        playerChoice = false;
        ContinueDialogue();
    }
}
