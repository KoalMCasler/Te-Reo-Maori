using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.VFX;
using Unity.VisualScripting;

public class InteractableObject : MonoBehaviour
{
    //All objects
    public enum InteractType { Nothing, Info, Book, Door, Artifact, Picture, NPC, }

    // Managers
    [Header("Managers")]
    [SerializeField] private SoundManager soundManager;
    [SerializeField] private UIManager uiManager;
    [SerializeField] private DialogueManager dialogueManager;
    [SerializeField] private LevelManager levelManager;

    [Header("Interaction Type")]
    public InteractType interactType;

    //Doors
    [Header("Door Variables")]
    public bool isLocked;
    public bool isClosed;
    public string destinantionRoom;
    public SpriteRenderer doorSprite;
    public Sprite newDoor;
    public GameObject doorLight;

    //Info
    [Header("Info Object variables")]
    [SerializeField] private TextMeshProUGUI infoText;
    [SerializeField] private GameObject infoGameObject;
    private Image infoPictureImage;
    public float InfoTextDelay = 3;
    public string message;
    public float textSpeed = 0.01f;
    public bool isDisplayingText;
    private SpriteRenderer bookSprite;
    public Sprite newBookSprite;

    //NPCS
    [Header("NPC Variables")]
    public Dialogue dialogue;

    // Fog
    [Header("Fog Settings")]
    public bool hasFog;
    public GameObject fog;
    public VisualEffect fogAmount;

    [Header("Image & Text")]
    public bool isVertical;
    public bool apartOfPuzzle;
    public Sprite picture;
    [SerializeField] private string pictureTitle;
    [TextArea(2, 10)]
    [SerializeField] private string pictureText;
    private string finalText;

    [Header("Picture Index")]
    public int pictureIndex;
    public Coroutine infoCoroutine;

    void Start()
    {
        soundManager = FindObjectOfType<SoundManager>();
        uiManager = FindObjectOfType<UIManager>();

        infoText = GameObject.Find("InfoText").GetComponent<TextMeshProUGUI>();
        infoText.text = null;
        isDisplayingText = false;

        switch (interactType)
        {
            case InteractType.Nothing: Debug.Log(this.name + " Has a type of nothing, was this by mistake?"); break;
            case InteractType.NPC: dialogueManager = FindObjectOfType<DialogueManager>(); break;
            case InteractType.Book: bookSprite = GetComponent<SpriteRenderer>(); break;
            case InteractType.Door:
                levelManager = FindObjectOfType<LevelManager>();
                isLocked = true;
                isClosed = true;
                doorLight.SetActive(false); break;
        }
    }

    public void Book()
    {
        soundManager.PlaySfxAudio("Book");
        uiManager.ShowBook(picture, pictureText);
        bookSprite.sprite = newBookSprite;
    }

    public void Artifact()
    {
        soundManager.PlaySfxAudio("Book");
        uiManager.ShowArtifact(picture, pictureText, this.name);
    }

    public void Picture()
    {
        soundManager.PlaySfxAudio("Book");

        if (apartOfPuzzle) finalText = string.Format("{0} \n {1}", pictureTitle, pictureText);
        else finalText = string.Format("{0}", pictureText);


        if (isVertical == false)
        {
            uiManager.horizontalCurrentImage.sprite = picture;
            uiManager.horizontalImageDescript.text = finalText;
        }
        if (isVertical == true)
        {
            uiManager.verticalCurrentImage.sprite = picture;
            uiManager.verticalImageDescript.text = finalText;
        }

        uiManager.ShowPicture(pictureIndex, isVertical);
    }

    // All needed for door objects to work. 
    public void Door()
    {
        if (isClosed)
        {
            if (isLocked)
            {
                if (isDisplayingText == false)
                {
                    isDisplayingText = true;
                    if (infoCoroutine != null)
                        StopAllCoroutines();

                    infoCoroutine = StartCoroutine(ShowInfo(message, InfoTextDelay));
                }
            }
            else
                StartCoroutine(OpenDoor());
        }
    }

    // Changes door sprite & changes to next room
    private IEnumerator OpenDoor()
    {
        doorSprite.sprite = newDoor;
        isClosed = false;
        FindObjectOfType<SoundManager>().PlaySfxAudio("Door");

        yield return new WaitForSeconds(1f);

        levelManager.LoadScene(destinantionRoom);
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

        if (infoCoroutine != null)
            StopAllCoroutines();

        infoCoroutine = StartCoroutine(ShowInfo(message, InfoTextDelay));

        if (hasFog)
            StartCoroutine(GoAwayFog());
    }

    IEnumerator ShowInfo(string message, float delay)
    {
        infoPictureImage = GameObject.Find("Image_Info").GetComponent<Image>();
        infoPictureImage.enabled = true;
        StartCoroutine(ScrollingText(message));
        yield return new WaitForSeconds(delay);
        infoText.text = "";
        isDisplayingText = false;
        infoPictureImage.enabled = false;

        infoCoroutine = null;
    }

    // Sets fog inactive after 1.5 seconds
    IEnumerator GoAwayFog()
    {
        yield return new WaitForSeconds(1.5f);
        fog.SetActive(false);
    }

    private IEnumerator ScrollingText(string currentLine)
    {
        uiManager = FindObjectOfType<UIManager>();
        uiManager.StopCoroutineBool = false;

        for (int i = 0; i < currentLine.Length + 1; i++)
        {
            if (uiManager.StopCoroutineBool)
                break;
            infoText.text = currentLine.Substring(0, i);
            FindObjectOfType<SoundManager>().PlaySfxAudio("TypeEffect");
            yield return new WaitForSeconds(textSpeed);
        }
    }

    public void NPC()
    {
        dialogueManager.StartDialogue(dialogue);
    }
}