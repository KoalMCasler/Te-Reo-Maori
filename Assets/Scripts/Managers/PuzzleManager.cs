using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

public class PuzzleManager : MonoBehaviour
{
    // managers
    private SoundManager soundManager;

    // door
    public GameObject door;

    // puzzle information
    public PuzzleAsset[] puzzlesToComplete;
    public bool puzzle3TextDone;

    // pepeha puzzle 1
    public TMP_InputField[] puzzleFields;
    public TMP_InputField[] inputFields;
    public GameObject createOwn;

    // artifact puzzle 2
    public ArtifactSlot[] artifactSlots;

    // picture label puzzle 3
    public PictureSlot[] pictureSlots;
    public TMP_InputField[] pictureInputFields;

    private void Start()
    {
        puzzle3TextDone = false;
        soundManager = FindObjectOfType<SoundManager>();
        ResetAllPuzzles();
    }

    private void Update()
    {
        if(door == null)
            door = GameObject.Find("Doors");
    }


    #region Puzzle 1
    // Should check through each input field to see if the input matches the answer of the puzzle
    public void CheckFirstPuzzle()
    {
        Debug.Log("Puzzle 1 Checked");
        int interactableCount = 0;

        if (puzzleFields[0].text.ToLower() == "mountain")
        {
            //soundManager.PlaySfxAudio("EnterText");
            puzzleFields[0].interactable = false;
        }
        if ((puzzleFields[1].text.ToLower() == "water") || (puzzleFields[1].text.ToLower() == "river"))
        {
            //soundManager.PlaySfxAudio("EnterText");
            puzzleFields[1].interactable = false;
        }
        if ((puzzleFields[2].text.ToLower() == "tribe") || (puzzleFields[2].text.ToLower() == "people"))
        {
            //soundManager.PlaySfxAudio("EnterText");
            puzzleFields[2].interactable = false;
        }
        if (puzzleFields[3].text.ToLower() == "name")
        {
            //soundManager.PlaySfxAudio("EnterText");
            puzzleFields[3].interactable = false;
        }
        //soundManager.PlaySfxAudio("EnterText");
        foreach (TMP_InputField field in puzzleFields)
        {
            if (!field.interactable)
            {
                StartPuzzle(puzzlesToComplete[0]);
                interactableCount++;
            }
        }

        if (interactableCount == puzzleFields.Length)
            createOwn.SetActive(true);
    }

    public void CheckCompletedPepeha()
    {
        int interactableCount = 0;

        foreach (TMP_InputField field in inputFields)
        {
            if (!string.IsNullOrEmpty(field.text))
                interactableCount++;
        }
        if (interactableCount == puzzleFields.Length)
            CompletePuzzle(puzzlesToComplete[0]);
    }

    #endregion


    #region Puzzle 2
    public void CheckSecondPuzzle(PuzzleAsset puzzleAsset)
    {
        Debug.Log("Puzzle 2 Checked");
        int slotedCorrectly = 0;

        foreach (ArtifactSlot artifact in artifactSlots)
        {
            if (artifact.isSlotedCorrectly)
                slotedCorrectly++;
        }
        if (slotedCorrectly == artifactSlots.Length)
            CompletePuzzle(puzzleAsset);
    }

    #endregion

    #region Puzzle 3
    public void CheckThirdPuzzle(PuzzleAsset puzzleAsset)
    {
        Debug.Log("Puzzle 3 Checked");
        int slotedCorrectly = 0;
        int interactableCount = 0;
        if (pictureInputFields[0].text.ToLower() == "kingitanga" /*|| pictureInputFields[0].text.ToLower() == "a"*/)
        {
            //soundManager.PlaySfxAudio("EnterText");
            pictureInputFields[0].interactable = false;
        }
        if (pictureInputFields[1].text.ToLower() == "rangiriri" /*|| pictureInputFields[1].text.ToLower() == "b"*/)
        {
            //soundManager.PlaySfxAudio("EnterText");
            pictureInputFields[1].interactable = false;
        }
        if (pictureInputFields[2].text.ToLower() == "raupatu" /*|| pictureInputFields[2].text.ToLower() == "c"*/)
        {
            //soundManager.PlaySfxAudio("EnterText");
            pictureInputFields[2].interactable = false;
        }
        if (pictureInputFields[3].text.ToLower() == "rangatahi" /*|| pictureInputFields[3].text.ToLower() == "d"*/)
        {
            //soundManager.PlaySfxAudio("EnterText");
            pictureInputFields[3].interactable = false;
        }
        foreach (TMP_InputField field in pictureInputFields)
        {
            if (!field.interactable)
            {
                interactableCount++;
            }
            if(interactableCount == pictureInputFields.Length)
            {
                puzzle3TextDone = true;
            }
        }
        foreach(PictureSlot pictures in pictureSlots)
        {
            if(pictures.isSlotedCorrectly)
            {
                slotedCorrectly++;
            }
        }
        if(slotedCorrectly == pictureSlots.Length && interactableCount == pictureInputFields.Length)
        {
            CompletePuzzle(puzzleAsset);
        }
    }
    #endregion
    public void StartPuzzle(PuzzleAsset puzzle)
    {
        puzzle.status = PuzzleAsset.Status.InProgress;
    }

    public void CompletePuzzle(PuzzleAsset puzzle)
    {
        puzzle.status = PuzzleAsset.Status.Finished;
        Debug.Log(puzzle.name + " Completed");
        //enter door stuff
        door.GetComponent<InteractableObject>().isLocked = false;
        door.GetComponent<InteractableObject>().doorLight.SetActive(true);
        soundManager.PlaySfxAudio("Door");
    }

    //Resets all puzzles to not started.
    private void ResetAllPuzzles()
    {
        foreach (PuzzleAsset puzzle in puzzlesToComplete)
        {
            puzzle.status = PuzzleAsset.Status.NotStarted;
        }
        foreach(TMP_InputField puzzlefield in puzzleFields)
        {
            puzzlefield.text = "";
        } 
        foreach(TMP_InputField inputfield in inputFields)
        {
            inputfield.text = "";
        }
    }
}
