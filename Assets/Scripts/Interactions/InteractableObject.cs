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
        Artifact,
        Picture,
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

    //NPCS
    [Header("NPC Variables")]
    public Dialogue dialogue;

    // Managers
    [Header("Managers")]
    [SerializeField] private SoundManager soundManager;
    [SerializeField] private UIManager uiManager;

    // Fog
    [Header("Fog Settings")]
    public bool hasFog;
    public GameObject fog;
    public VisualEffect fogAmount;

    [Header("Image & Text")] // not sure if we're using text for the photos but adding that here anyways
    [SerializeField] private Sprite picture;
    [SerializeField] private string pictureText;



    void Start()
    {
        soundManager = FindObjectOfType<SoundManager>();
        uiManager = FindObjectOfType<UIManager>();

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
        soundManager.PlaySfxAudio("Book");
        uiManager.ShowBook(picture, pictureText);
    }

    public void Artifact()
    {
        soundManager.PlaySfxAudio("Book");
        uiManager.ShowArtifact(picture, pictureText, this.name);
    }

    public void Picture()
    {
        // Sound?
        if(picture != null)
            uiManager.currentImage.sprite = picture;
        uiManager.ShowPicture();
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

    // Changes door sprite
    private void OpenDoor()
    {
        doorSprite.sprite = newDoor;
        isClosed = false;
        FindObjectOfType<SoundManager>().PlaySfxAudio("Door");
    }

    // All needed for info text.
    public void Info()
    {
        if (hasFog)
        {
            fogAmount.SetFloat("FogAmount", 0);
            fogAmount.SetFloat("Lifetime", 1);
            FindObjectOfType<UIManager>().ShowProjectInfo();
        }

        Debug.Log("Reading info from " + this.name);
        //Debug.Log(message);
        StartCoroutine(ShowInfo(message, InfoTextDelay));

        if (hasFog)
            StartCoroutine(GoAwayFog());
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

    // Sets fog inactive after 2 seconds
    IEnumerator GoAwayFog()
    {
        yield return new WaitForSeconds(2);
        fog.SetActive(false);
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
