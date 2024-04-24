using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class PuzzleManager : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;

    [Header("Puzzle Information")]
    public PuzzleAsset[] puzzlesToComplete;

    [Header("Pepeha Puzzle")]
    public TMP_InputField[] inputFields;
    
    private void Start() 
    {
        gameManager = FindObjectOfType<GameManager>();
        ResetAllPuzzles();
    }

    

    // Should check through each input field to see if the input matches the answer of the puzzle
    public void CheckFirstPuzzle()
    {
        if (inputFields[0].text == puzzlesToComplete[0].answers[0].ToString())
            inputFields[0].interactable = false;
        else if (inputFields[1].text == puzzlesToComplete[0].answers[1].ToString())
            inputFields[1].interactable = false;
        else if ((inputFields[2].text == puzzlesToComplete[0].answers[2].ToString()) || (inputFields[2].text == puzzlesToComplete[0].answers[3].ToString()))
            inputFields[2].interactable = false;
        else if (inputFields[3].text == puzzlesToComplete[0].answers[4].ToString())
            inputFields[3].interactable = false;
    }


    public void StartPuzzle(PuzzleAsset puzzle)
    {
        puzzle.status = PuzzleAsset.Status.InProgress;
    }


    public void CompletePuzzle(PuzzleAsset puzzle)
    {
        puzzle.status = PuzzleAsset.Status.Finished;
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
