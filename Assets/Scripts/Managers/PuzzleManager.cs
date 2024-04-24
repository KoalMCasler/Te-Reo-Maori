using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PuzzleManager : MonoBehaviour
{
    public PuzzleAsset[] puzzlesToComplete;
    
    private void Start() 
    {
        ResetAllPuzzles();
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
