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
    public PlayerMovement playerMovement;

    [Header("Dialogue UI")]
    public TMP_Text dialogueName;
    public TMP_Text dialogueText;

    [Header("Typewriter Settings")]
    [SerializeField] private float typingSpeed = 0.06f;
    [SerializeField] private bool NPCTalking;
    private bool canContinueNextLine = true;
    private Coroutine displayLineCoroutine;
    private bool isAddingRichText;
    private bool skipText;


    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }


    // starts dialogue and clears previous queue.
    public void StartDialogue(Dialogue dialogue)
    {
        gameManager.LoadState("Dialogue");
        sentences.Clear();

        dialogueName.text = dialogue.nameOfInteraction;

    }

    public void DisplayNextSentece()
    {
        if(canContinueNextLine)
        {
            if(sentences.Count == 0)
            {
                EndDialogue();
                return;
            }

            string sentence = sentences.Dequeue();
            skipText = false;
            displayLineCoroutine = StartCoroutine(DisplayLine(sentence));
        }
    }

    // Creates typewriter effect
    private IEnumerator DisplayLine(string line)
    {
        //empty text
        dialogueText.text = "";

        canContinueNextLine = false;

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
        sentences.Clear();
        gameManager.LoadState("Gameplay");
    }
}
