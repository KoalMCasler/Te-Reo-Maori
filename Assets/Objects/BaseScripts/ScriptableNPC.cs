using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ScriptableNPC", menuName = "Scriptables")]
public class ScriptableNPC : ScriptableObject
{
    public bool HasMetPlayer;
    public bool HasExplainedQuest;
    public bool IsQuestCompleated;
    public bool EverythingFinished;
}