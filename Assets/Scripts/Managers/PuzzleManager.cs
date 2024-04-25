using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class PuzzleManager : MonoBehaviour
{
    [Header("Managers")]
    [SerializeField] 
    private GameManager gameManager;

    [Header("Pepeha Puzzle")]
    public GameObject door;

    [Header("Puzzle Information")]
    public PuzzleAsset[] puzzlesToComplete;

    [Header("Pepeha Puzzle")]
    public TMP_InputField[] puzzleFields;
    public TMP_InputField[] inputFields;
    public GameObject createOwn;
    
    private void Start() 
    {
        gameManager = FindObjectOfType<GameManager>();
        ResetAllPuzzles();
    }


    #region PepehaPuzzle
    // Should check through each input field to see if the input matches the answer of the puzzle
    public void CheckFirstPuzzle()
    {
        int interactableCount = 0;

        if (puzzleFields[0].text.ToLower() == "mountain")
            puzzleFields[0].interactable = false;
        if ((puzzleFields[1].text.ToLower() == "water")|| (puzzleFields[1].text.ToLower() == "river"))
            puzzleFields[1].interactable = false;
        if ((puzzleFields[2].text.ToLower() == "tribe")|| (puzzleFields[2].text.ToLower() == "people"))
            puzzleFields[2].interactable = false;
        if (puzzleFields[3].text.ToLower() == "name")
            puzzleFields[3].interactable = false;

        foreach(TMP_InputField field in puzzleFields)
        {
            if(!field.interactable)
            {
                StartPuzzle(puzzlesToComplete[0]);
                interactableCount++;
            }
        }

        if(interactableCount == puzzleFields.Length)
        {
            createOwn.SetActive(true);
            interactableCount = 0;
        }
    }

    public void CheckCompletedPepeha()
    {
        int interactableCount = 0;

        foreach (TMP_InputField field in inputFields)
        {
            if(field.text != null)
                interactableCount++;
        }

        if (interactableCount == puzzleFields.Length)
        {
            CompletePuzzle(puzzlesToComplete[0]);
            interactableCount = 0;
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

        if(puzzle.name == "Pepehā")
        {
            //enter door stuff
            door.GetComponent<InteractableObject>().isLocked = false;
            door.GetComponent<InteractableObject>().doorLight.SetActive(true);
        }

    }


//Resets all puzzles to not started.
    private void ResetAllPuzzles()
    {
        foreach(PuzzleAsset puzzle in puzzlesToComplete)
        {
            puzzle.status = PuzzleAsset.Status.NotStarted;
        }
    }
}
