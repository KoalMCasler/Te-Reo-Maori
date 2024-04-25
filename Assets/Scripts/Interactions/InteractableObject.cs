using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class InteractableObject : MonoBehaviour
{
    //All objects
    public enum InteractType
    {
        Nothing,
        Info,
        Dialogue,
        Book,
        Door
    }

    [Header("Interaction Type")]
    [SerializeField]
    public InteractType interactType;

    [Header("Door Variables")]
    [SerializeField]
    //Doors
    public bool isLocked;
    public bool isClosed;
    public string destinantionRoom;
    public LevelManager levelManager;
    public Animator doorAnim;
    public float animDelay = 1f;
    public Animator fadeAnim;

    [Header("Info Object variables")]
    [SerializeField]
    //Info
    private TextMeshProUGUI infoText;
    private GameObject infoImage;
    public float InfoTextDelay = 3;
    public string message;
    public float textSpeed = 0.01f;

    [Header("NPC Variables")]
    [SerializeField]
    //NPCS
    public Dialogue dialogue;

    [Header("Scriptable Object")]
    public ScriptableIO scriptableIO;
    public ScriptableNPC scriptableNPC;


    void Start()
    {
        infoText = GameObject.Find("InfoText").GetComponent<TextMeshProUGUI>();
        infoText.text = null;
        if (interactType == InteractType.Nothing)
        {
            Debug.Log(this.name + " Has a type of nothing, Was this by mistake?");
        }
        if (interactType == InteractType.Door)
        {
            levelManager = FindObjectOfType<LevelManager>();
            isLocked = true;
            isClosed = true;
            doorAnim = this.gameObject.GetComponent<Animator>();
            fadeAnim = GameObject.Find("CrossFade").GetComponent<Animator>();
            fadeAnim.SetTrigger("FadeIn");
        }
    }

    public void Dialogue()
    {
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
    }

    public void Book()
    {
        UIManager uiMan = FindObjectOfType<UIManager>();
        uiMan.ShowBook(this.name);
    }

    // All needed for door objects to work. 
    public void Door()
    {
        if (isClosed)
        {
            if (isLocked)
            {
                StartCoroutine(ShowInfo(message, InfoTextDelay));
                //isLocked = false; //Debug line to test before quest is added.
            }
            else
            {
                OpenDoor();
            }
        }
        else if (!isClosed)
        {
            fadeAnim.SetTrigger("FadeOut");
            levelManager.LoadScene(destinantionRoom, animDelay);
        }
    }
    private void OpenDoor()
    {
        doorAnim.SetBool("IsOpen", true);
        isClosed = false;
    }

    // All needed for info text.
    public void Info()
    {
        Debug.Log("Reading info from " + this.name);
        //Debug.Log(message);
        StartCoroutine(ShowInfo(message, InfoTextDelay));
    }

    IEnumerator ShowInfo(string message, float delay)
    {
        infoImage = GameObject.Find("Image_Info");
        infoImage.GetComponent<Image>().enabled = true;
        StartCoroutine(ScrollingText(message));
        yield return new WaitForSeconds(delay);
        infoText.text = "";
        infoImage.GetComponent<Image>().enabled = false;
    }

    private IEnumerator ScrollingText(string currentLine)
    {
        for (int i = 0; i < currentLine.Length + 1; i++)
        {
            infoText.text = currentLine.Substring(0, i);
            yield return new WaitForSeconds(textSpeed);
        }
    }
}
