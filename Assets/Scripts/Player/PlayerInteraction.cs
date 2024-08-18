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
        if (other.CompareTag("Interactable") == true)
        {
            currentInterObj = other.gameObject;
            currentInterObjScript = currentInterObj.GetComponent<InteractableObject>();
            indicator.SetActive(true);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Interactable") == true)
        {
            currentInterObj = null;
            indicator.SetActive(false);
        }
    }

    void OnInteract()
    {
        if (currentInterObj == true)
        {
            switch (currentInterObjScript.interactType)
            {
                case InteractableObject.InteractType.Info: currentInterObjScript.Info(); break;
                case InteractableObject.InteractType.Book: currentInterObjScript.Book(); break;
                case InteractableObject.InteractType.Door: currentInterObjScript.Door(); break;
                case InteractableObject.InteractType.Artifact: currentInterObjScript.Artifact(); break;
                case InteractableObject.InteractType.Picture: currentInterObjScript.Picture(); break;
                case InteractableObject.InteractType.NPC: currentInterObjScript.NPC(); break;
            }
        }
        else
            Debug.Log("Nothing to interact with.");
    }

    void OnBackFromUI()
    {
        if (gameManager.gameState == GameManager.GameState.Options)
        {
            gameManager.PausingState();
            soundManager.PlaySfxAudio("TypeEffect");
        }
        else if (gameManager.gameState == GameManager.GameState.Pause || gameManager.gameState == GameManager.GameState.Puzzle)
        {
            gameManager.LoadState("Gameplay");
            soundManager.PlaySfxAudio("Book");
        }
        else if (UIManager.overlayActive)
        {
            UIManager.UI_Gameplay();
            soundManager.PlaySfxAudio("Book");
            gameManager.LoadState("Gameplay");
        }
    }
    void OnOpenPuzzle()
    {
        if (UIManager.ProjectInfoButton.activeSelf && gameManager.gameState != GameManager.GameState.Puzzle)
        {
            gameManager.LoadState("Puzzle");
            soundManager.PlaySfxAudio("Book");
        }
    }
}
