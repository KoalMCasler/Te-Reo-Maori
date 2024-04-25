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

        if (puzzleFields[0].text == puzzlesToComplete[0].answers[0].ToString())
            puzzleFields[0].interactable = false;
        if (puzzleFields[1].text == puzzlesToComplete[0].answers[1].ToString())
            puzzleFields[1].interactable = false;
        if ((puzzleFields[2].text == puzzlesToComplete[0].answers[2].ToString()) || (puzzleFields[2].text == puzzlesToComplete[0].answers[3].ToString()))
            puzzleFields[2].interactable = false;
        if (puzzleFields[3].text == puzzlesToComplete[0].answers[4].ToString())
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
