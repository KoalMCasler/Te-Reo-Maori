using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteraction : MonoBehaviour
{
    [Header("Managers")]
    public UIManager UIManager;
    public GameManager gameManager;
    public SoundManager soundManager;
    [Header("Interactions")]
    public GameObject currentInterObj = null;
    public InteractableObject currentInterObjScript = null;
    public GameObject indicator;

    // Start is called before the first frame update
    void Start()
    {
        indicator.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Interactable") == true)
        {
            currentInterObj = other.gameObject;
            currentInterObjScript = currentInterObj.GetComponent<InteractableObject>();
            indicator.SetActive(true);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if(other.CompareTag("Interactable") == true)  
        {
            currentInterObj = null;
            indicator.SetActive(false);
        }
    }

    void OnInteract()
    {
        if(currentInterObj == true)
        {
            Debug.Log("Interacting with " + currentInterObj.name);
            if(currentInterObjScript.interactType == InteractableObject.InteractType.Info)
            {
                currentInterObjScript.Info();
            }
            if(currentInterObjScript.interactType == InteractableObject.InteractType.Door)
            {
                currentInterObjScript.Door();
            }
            if(currentInterObjScript.interactType == InteractableObject.InteractType.Book)
            {
                currentInterObjScript.Book();
            }
            if(currentInterObjScript.interactType == InteractableObject.InteractType.Artifact)
            {
                currentInterObjScript.Artifact();
            }
            if(currentInterObjScript.interactType == InteractableObject.InteractType.Picture)
            {
                currentInterObjScript.Picture();
            }
            if(currentInterObjScript.interactType == InteractableObject.InteractType.NPC)
            {
                currentInterObjScript.NPC();
            }
        }
        else
        {
            Debug.Log("Nothing to interact with.");
        }
    }

    void OnBackFromUI()
    {
        if(gameManager.gameState == GameManager.GameState.Options)
        {
            gameManager.PausingState();
            soundManager.PlaySfxAudio("TypeEffect");
        }
        else if(gameManager.gameState == GameManager.GameState.Pause || gameManager.gameState == GameManager.GameState.Puzzle)
        {
            gameManager.LoadState("Gameplay");
            soundManager.PlaySfxAudio("Book");
        }
        else if(UIManager.overlayActive)
        {
            UIManager.UI_Gameplay();
            soundManager.PlaySfxAudio("Book");
            gameManager.LoadState("Gameplay");
        }
    }
    void OnOpenPuzzle()
    {
        if(UIManager.ProjectInfoButton.activeSelf && gameManager.gameState != GameManager.GameState.Puzzle)
        {
            gameManager.LoadState("Puzzle");
            soundManager.PlaySfxAudio("Book");
        }
    }
}
