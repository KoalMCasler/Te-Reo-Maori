using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    public enum InteractType
    {
        Info,
        Dialogue,
        Book,
        Door
    }

    public InteractType interactType;

    public Dialogue dialogue;

    public void Dialogue()
    {
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
    }
     
}