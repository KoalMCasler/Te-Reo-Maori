using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialogue : MonoBehaviour
{
    public string nameOfInteraction;
    public string[] introDialogue;
    [TextArea(2,10)]
    public string[] optionalDialogue1;
    [TextArea(2,10)]
    public string[] optionalDialogue2;
    [TextArea(2,10)]
    public string[] optionalDialogue3;
    [TextArea(2,10)]
    public string[] optionalDialogue4;
    [TextArea(2,10)]
    public string[] optionalDialogue5;
    [TextArea(2,10)]
    public string[] optionalDialogue6;
    public string[] outroDialogue;
    public int dialogueOptionCount;
}
