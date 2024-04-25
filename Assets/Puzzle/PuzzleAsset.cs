using UnityEngine;

[CreateAssetMenu(fileName = "PuzzleAssets", menuName = "PuzzleAssets", order = 0)]
public class PuzzleAsset : ScriptableObject 
{
    public string puzzleName;

    public enum Status
    {
        NotStarted,
        InProgress,
        Finished,
    }
    public Status status;
}