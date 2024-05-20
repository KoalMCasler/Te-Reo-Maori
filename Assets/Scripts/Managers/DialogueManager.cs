using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    private Queue<string> sentences;
    private GameManager gameManager;
    private UIManager uIManager;
    public PlayerMovement playerMovement;
    public Dialogue activeDialogue;

    [Header("Dialogue UI")]
    public TMP_Text dialogueName;
    public TMP_Text dialogueText;

    [Header("Typewriter Settings")]
    [SerializeField] private float typingSpeed = 0.06f;
    [SerializeField] private bool NPCTalking;
    public bool canContinueNextLine = true;
    private Coroutine displayLineCoroutine;
    private bool isAddingRichText;
    private bool skipText;
    private bool introTextPlayed;
    private bool isAskingQustions;
    private bool outroHasBeenPlayed;


    // Start is called before the first frame update
    void Start()
    {
        uIManager = FindObjectOfType<UIManager>();
        gameManager = FindObjectOfType<GameManager>();
        sentences = new Queue<string>();
        introTextPlayed = false;
        isAskingQustions = false;
    }


    // starts dialogue and clears previous queue.
    public void StartDialogue(Dialogue dialogue)
    {
        introTextPlayed = false;
        isAskingQustions = false;
        Debug.Log("Starting Dialogue");
        gameManager.LoadState("Dialogue");
        activeDialogue = dialogue;
        dialogueName.text = activeDialogue.nameOfInteraction;
        sentences.Clear();
        foreach(string currentLine in activeDialogue.introDialogue)
        {
            sentences.Enqueue(currentLine);
        }
        DisplayNextSentece();
    }

    public void DisplayNextSentece()
    {
        if(canContinueNextLine)
        {
            if(sentences.Count == 0 && introTextPlayed == false)
            {
                introTextPlayed = true;
                OpenDialogueOptions();
                return;
            }
            else if(sentences.Count == 0 && isAskingQustions)
            {
                isAskingQustions = false;
                OpenDialogueOptions();
                return;
            }
            else if(sentences.Count == 0 && outroHasBeenPlayed == false)
            {
                PlayOutroDialogue();
                return;
            }
            else if(sentences.Count == 0 && outroHasBeenPlayed == true)
            {
                EndDialogue();
                return;
            }

            string sentence = sentences.Dequeue();
            skipText = false;
            displayLineCoroutine = StartCoroutine(DisplayLine(sentence));
        }
    }

    private void OpenDialogueOptions()
    {
        Debug.Log("Showing Dialogue Options");
        sentences.Clear();
        if(isAskingQustions)
        {
            foreach(string currentLine in activeDialogue.optionalDialogue6)
            {
                sentences.Enqueue(currentLine);
            }
        }
        else
        {
            foreach(string currentLine in activeDialogue.optionalDialogue1)
            {
                sentences.Enqueue(currentLine);
            }
        }
        uIManager.UI_DialogueOptions(true);
        DisplayNextSentece();
    }

    private void PlayOutroDialogue()
    {
        Debug.Log("Player Outro Dialogue");
        sentences.Clear();
        outroHasBeenPlayed = true;
        foreach(string currentLine in activeDialogue.outroDialogue)
        {
            sentences.Enqueue(currentLine);
        }
        DisplayNextSentece();
    }

    public void SelectDialogOptions(int optionNumber)
    {
        Debug.Log("Selecting option " + optionNumber);
        isAskingQustions = true;
        //option number should be 1-4. 
        if(optionNumber > activeDialogue.dialogueOptionCount || optionNumber < 0)
        {
            Debug.Log("Invalid Option number, defulting to 1");
            optionNumber = 1;
        }
        if(optionNumber == 1)
        {
            sentences.Clear();
            foreach(string currentLine in activeDialogue.optionalDialogue2)
            {
                sentences.Enqueue(currentLine);
            } 
            uIManager.UI_DialogueOptions(false);
            uIManager.dialogueTarget.Select();
            DisplayNextSentece();    
        }
        if(optionNumber == 2)
        {
            sentences.Clear();
            foreach(string currentLine in activeDialogue.optionalDialogue3)
            {
                sentences.Enqueue(currentLine);
            }
            uIManager.UI_DialogueOptions(false);
            uIManager.dialogueTarget.Select();
            DisplayNextSentece(); 
        }
        if(optionNumber == 3)
        {
            sentences.Clear();
            foreach(string currentLine in activeDialogue.optionalDialogue4)
            {
                sentences.Enqueue(currentLine);
            }
            uIManager.UI_DialogueOptions(false);
            uIManager.dialogueTarget.Select();
            DisplayNextSentece();     
        }
        if(optionNumber == 4)
        {
            sentences.Clear();
            foreach(string currentLine in activeDialogue.optionalDialogue5)
            {
                sentences.Enqueue(currentLine);
            }
            uIManager.UI_DialogueOptions(false);
            uIManager.dialogueTarget.Select();
            DisplayNextSentece();     
        }
    }

    // Creates typewriter effect
    private IEnumerator DisplayLine(string line)
    {
        //empty text
        dialogueText.text = "";

        //canContinueNextLine = false;

        foreach(char letter in line.ToCharArray())
        {
            // if the player chooses to skip the text OR if the player is looking at a piece of text AND the game isnt paused
            if((skipText || !NPCTalking)  && !gameManager.isPaused)
            {
                dialogueText.text = line;
                skipText = false;
                break;
            }

            if(letter == '<' || isAddingRichText)
            {
                isAddingRichText = true;
                dialogueText.text += letter;

                if (letter == '>')
                    isAddingRichText = false;
            }
            else
            {
                dialogueText.text += letter;
                yield return new WaitForSeconds(typingSpeed);
            }

            canContinueNextLine = true;
        }
    }

    // Clears sentences queue & loads the new gamestate
    private void EndDialogue()
    {
        Debug.Log("Ending Dialogue");
        sentences.Clear();
        gameManager.LoadState("Gameplay");
        uIManager.UI_Gameplay();
    }
}
