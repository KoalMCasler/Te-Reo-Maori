using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.VFX;

public class InteractableObject : MonoBehaviour
{
    //All objects
    public enum InteractType
    {
        Nothing,
        Info,
        Dialogue,
        Book,
        Door,
        Artifact
    }

    [Header("Interaction Type")]
    public InteractType interactType;

    //Doors
    [Header("Door Variables")]
    public bool isLocked;
    public bool isClosed;
    public string destinantionRoom;
    public LevelManager levelManager;
    public SpriteRenderer doorSprite;
    public Sprite newDoor;
    public GameObject doorLight;

    //Info
    [Header("Info Object variables")]
    [SerializeField]
    private TextMeshProUGUI infoText;
    private GameObject infoImage;
    public float InfoTextDelay = 3;
    public string message;
    public float textSpeed = 0.01f;
    public bool hasFog;
    public GameObject fog;
    public VisualEffect fogAmount;

    //NPCS
    [Header("NPC Variables")]
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
            doorLight.SetActive(false);
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

    public void Artifact()
    {
        UIManager uiMan = FindObjectOfType<UIManager>();
        uiMan.ShowArtifact(this.name);
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
            levelManager.LoadScene(destinantionRoom);
        }
    }

    private void OpenDoor()
    {
        doorSprite.sprite = newDoor;
        isClosed = false;
    }

    // All needed for info text.
    public void Info()
    {
        if(hasFog)
        {
            fogAmount.SetFloat("FogAmount", 0);
            fogAmount.SetFloat("Lifetime", 1);
        }
        Debug.Log("Reading info from " + this.name);
        //Debug.Log(message);
        StartCoroutine(ShowInfo(message, InfoTextDelay));

        if(hasFog)
            fog.SetActive(false);
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
