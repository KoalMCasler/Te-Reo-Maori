using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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
    public bool IsLocked;
    public LevelManager levelManager;
    [Header("Info Object variables")]
    [SerializeField]
    //Info
    private TextMeshProUGUI infoText;
    public float InfoTextDelay;
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
        levelManager = FindObjectOfType<LevelManager>();
        IsLocked = true;
        infoText = GameObject.Find("InfoText").GetComponent<TextMeshProUGUI>();
        infoText.text = null;
        if(interactType == InteractType.Nothing)
        {
            Debug.Log(this.name + " Has a type of nothing, Was this by mistake?");
        }
    }

    public void Dialogue()
    {
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
    }
     
    public void Info()
    {
        Debug.Log("Reading info from " + this.name);
        //Debug.Log(message);
        StartCoroutine(ShowInfo(message, InfoTextDelay));
    }

    IEnumerator ShowInfo(string message, float delay)
    {
        StartCoroutine(ScrollingText(message));
        yield return new WaitForSeconds(delay);
        infoText.text = "";
    }

    private IEnumerator ScrollingText(string currentLine)
    {
        for(int i = 0; i < currentLine.Length + 1; i++)
        {
            infoText.text = currentLine.Substring(0,i);
            yield return new WaitForSeconds(textSpeed);
        }
    }
}
