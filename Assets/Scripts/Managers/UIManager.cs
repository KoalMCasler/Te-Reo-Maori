using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIManager : MonoBehaviour
{

    [Header("Managers")]
    public GameManager gameManager;
    public LevelManager levelManager;
    public SoundManager soundManager;
    public PuzzleManager puzzleManager;

    [Header("UI Panels")]
    public GameObject MainMenuUI;
    public GameObject AcknowledgementUI;
    public GameObject GameplayUI;
    public GameObject DialogueUI;
    public GameObject CreditsUI;
    public GameObject ControlsUI;
    public GameObject PauseUI;
    public GameObject OptionsUI;
    public GameObject EndGameUI;
    public GameObject ProjectInfoButton;

    [Header("Confirmation Exit")]
    public GameObject ConfirmationUI;
    public TextMeshProUGUI confirmationText;
    public Button yesButton;

    // UI for puzzles
    [Header("Puzzle UI")]
    public bool overlayActive;
    public GameObject Room1Puzzle;
    public GameObject Room2Puzzle;
    public GameObject Room3Puzzle;

    //Books for room 1
    [Header("Puzzle 1/2 - Info UI")]
    public GameObject InfoBookArtifact;
    public TextMeshProUGUI bookArtifactText;
    public Image bookArtifactImage;
   
    //Artifact for room 2
    [Header("Puzzle 2 UI")]
    public GameObject ArtifactUI1;
    public GameObject ArtifactUI2;
    public GameObject ArtifactUI3;
    public GameObject ArtifactUI4;

    [Header("Puzzle 3")]
    public GameObject horizontalPictureUI;
    public Image horizontalCurrentImage; 
    public TextMeshProUGUI horizontalImageDescript;
    public GameObject verticalPictureUI;
    public Image verticalCurrentImage;
    public TextMeshProUGUI verticalImageDescript;

    [Header("Puzzle 3 UI")]
    public GameObject pictureUI1;
    public GameObject pictureUI2;
    public GameObject pictureUI3;
    public GameObject pictureUI4;
    
    [Header("Player Settings")]
    public GameObject player;
    private PlayerInput playerInput;
    private SpriteRenderer playerSprite;

    [Header("Target Buttons")] //Needed for controller suport for UI.
    public Button mainMenuTarget;
    public Button optionsTarget;
    public Button acknowledgmentTarget;
    public Button creditsTarget;
    public Button pauseTarget;
    public Button controlsTarget;
    public Button confermationTarget;
    //public TMP_InputField puzzle1Target;
    public Button puzzle2Target;
    public Button puzzle3Target;

    [Header("Needed for Controller")]
    public bool isHoldingItem;
    public bool puzzle2IsOpen;
    public bool puzzle3IsOpen;
    public GameObject selector; //needed to attach objects for drag and drop. 

    [Header("UI for Binding")]
    public GameObject keyboardBindings;
    public GameObject gamepadBindings;


   

    private void Start()
    {
        keyboardBindings.SetActive(true);
        gamepadBindings.SetActive(false);
        isHoldingItem = false;
        puzzle2IsOpen =false;
        puzzle3IsOpen =false;
        overlayActive = false;
        playerSprite = player.GetComponent<SpriteRenderer>();
        playerInput = player.GetComponent<PlayerInput>();
        soundManager = FindObjectOfType<SoundManager>();
    }

    void Update()
    {
        if(EventSystem.current.currentSelectedGameObject != null)
        {
            if(puzzle2IsOpen || puzzle3IsOpen)
            {
                selector.transform.position = EventSystem.current.currentSelectedGameObject.transform.position;
            }
        }
    }

    #region GameState UI
    public void UI_MainMenu()
    {
        puzzle3IsOpen =false;
        puzzle2IsOpen =false;
        PlayerMovement(false);
        CurrentUI(MainMenuUI, false);
        mainMenuTarget.Select();
    }

    public void UI_Acknowledgement()
    {
        CurrentUI(AcknowledgementUI, false);
        acknowledgmentTarget.Select();
    }

    public void UI_Gameplay()
    {
        puzzle3IsOpen =false;
        puzzle2IsOpen = false;
        PlayerMovement(true);
        CurrentUI(GameplayUI, true);
    }

    // Shows The UI for the puzzle depending on the room name.
    public void UI_Puzzle(string name)
    {
        PlayerMovement(false);
        switch (name)
        {
            case "Room 1":
                CurrentUI(Room1Puzzle, true); break; //text imput for controller still WIP.
            case "Room 2":
                CurrentUI(Room2Puzzle, true); puzzle2Target.Select(); puzzle2IsOpen = true; break;
            case "Room 3":
                CurrentUI(Room3Puzzle, true); puzzle3Target.Select(); puzzle3IsOpen = true;break;
        }
        soundManager.PlaySfxAudio("Book");
    }

    public void UI_Pause()
    {
        puzzle3IsOpen =false;
        puzzle2IsOpen = false;
        PlayerMovement(false);
        CurrentUI(PauseUI, true);
        pauseTarget.Select();
    }

    public void UI_Options()
    {
        puzzle3IsOpen =false;
        puzzle2IsOpen = false;
        CurrentUI(OptionsUI, true);
        optionsTarget.Select();
    }

    public void UI_EndGame()
    {
        puzzle3IsOpen =false;
        puzzle2IsOpen = false;
        PlayerMovement(false);
        CurrentUI(EndGameUI, false);
    }

    #endregion



    #region Puzzle UI

    // Depending on the book that's opening, it will open the corresponding UI. (I'd like to change this if I find time.)
    public void ShowBook(Sprite image, string infoText)
    {
        PlayerMovement(false);
        CurrentUI(InfoBookArtifact, true);
        bookArtifactImage.sprite = image;
        bookArtifactText.text = infoText;
        overlayActive = true;
    }

    //Artifact objects work like books.
    public void ShowArtifact(Sprite image, string infoText, string artifactName)
    {
        PlayerMovement(false);
        CurrentUI(InfoBookArtifact, true);
        bookArtifactImage.sprite = image;
        bookArtifactText.text = infoText;
        overlayActive = true;
        switch (artifactName)
        {
            case "Artifact1": ArtifactUI1.SetActive(true); break;
            case "Artifact2": ArtifactUI2.SetActive(true); break;
            case "Artifact3": ArtifactUI3.SetActive(true); break;
            case "Artifact4": ArtifactUI4.SetActive(true); break;
            default: Debug.Log($"{artifactName} doesnt exist"); break;
        }
    }

    // Shows picture. The plan is to use 1 UI object and just change the image depending on the object (Image should be stored on the interactable)
    public void ShowPicture(int pictureIndex, bool isVertical)
    {
        PlayerMovement(false);
        switch (pictureIndex)
        {
            case 1: pictureUI1.SetActive(true); break;
            case 2: pictureUI2.SetActive(true); break;
            case 3: pictureUI3.SetActive(true); break;
            case 4: pictureUI4.SetActive(true); break;
            default: Debug.Log($"No Matching picture index"); break;
        }
        if(isVertical == false)
            CurrentUI(horizontalPictureUI, true);
            if(isVertical == true)
            CurrentUI(verticalPictureUI, true);
        overlayActive = true;
    }

    #endregion

    // UI confirmation for exiting to main menu or quit
    public void UI_Confirmation(string name)
    {
        yesButton.onClick.RemoveAllListeners(); // removes listeners from the yes button.
        CurrentUI(ConfirmationUI, true);
        confermationTarget.Select();
        // Based on the string name, it will decide what's shown on the confirmation page.
        switch (name)
        {
            case "quit":
                confirmationText.text = "Are you sure you want to quit?";
                yesButton.onClick.AddListener(() => gameManager.QuitGame());
                break;
            case "mainmenu":
                confirmationText.text = "Are you sure you want to go to Main Menu? All progress will not be saved";
                yesButton.onClick.AddListener(() => levelManager.LoadScene("MainMenu"));
                break;
            default:
                confirmationText.text = "";
                break;
        }
    }

    // This is used at the beginning to turn on the project info UI for after you've interacted with the sign
    public void ShowProjectInfo()
    {
        ProjectInfoButton.SetActive(true);
    }

    public void UI_Dialogue()
    {
        CurrentUI(DialogueUI, true);
    }

    public void UI_Controls()
    {
        CurrentUI(ControlsUI, false);
        controlsTarget.Select();
    }

    public void UI_Credits()
    {
        CurrentUI(CreditsUI, false);
        creditsTarget.Select();
    }

    public void ShowBinding(GameObject activeBind)
    {
        keyboardBindings.SetActive(false);
        gamepadBindings.SetActive(false);

        activeBind.SetActive(true);
    }

    // Sets UI to the required panel & enables or disables player sprite
    void CurrentUI(GameObject activeUI, bool isActive)
    {
        AcknowledgementUI.SetActive(false);
        MainMenuUI.SetActive(false);
        GameplayUI.SetActive(false);
        ControlsUI.SetActive(false);
        CreditsUI.SetActive(false);
        PauseUI.SetActive(false);
        OptionsUI.SetActive(false);
        EndGameUI.SetActive(false);
        Room1Puzzle.SetActive(false);
        Room2Puzzle.SetActive(false);
        Room3Puzzle.SetActive(false);
        InfoBookArtifact.SetActive(false);
        ConfirmationUI.SetActive(false);
        horizontalPictureUI.SetActive(false);
        verticalPictureUI.SetActive(false);
        overlayActive = false;

        activeUI.SetActive(true);
        playerSprite.enabled = isActive; // Enables or disables player sprite

        if (gameManager.isPaused)
            Time.timeScale = 0f;
        else
            Time.timeScale = 1f;
    }

    // Enable and disable player Movement & interaction
    private void PlayerMovement(bool playerMove)
    {
        if (playerMove == false)
        {
            playerInput.actions.FindAction("Move").Disable();
            playerInput.actions.FindAction("Interact").Disable();
        }
        if (playerMove == true)
        {
            playerInput.actions.FindAction("Move").Enable();
            playerInput.actions.FindAction("Interact").Enable();
        }
    }
    
    public void OnSlotSelect()
    {
        if(EventSystem.current.currentSelectedGameObject.transform.childCount == 1 && isHoldingItem == false)
        {
            GameObject itemToMove = EventSystem.current.currentSelectedGameObject.transform.GetChild(0).gameObject;
            itemToMove.transform.SetParent(selector.transform);
            isHoldingItem = true;
            Debug.Log("Item Picked Up");
        }
        else if(EventSystem.current.currentSelectedGameObject.transform.childCount == 0 && isHoldingItem == true)
        {
            GameObject itemToMove = selector.transform.GetChild(0).gameObject;
            selector.transform.GetChild(0).SetParent(EventSystem.current.currentSelectedGameObject.transform);
            isHoldingItem = false;
            EventSystem.current.currentSelectedGameObject.GetComponent<ArtifactSlot>().OnControllerDrop(itemToMove);
            puzzleManager.CheckSecondPuzzle(puzzleManager.puzzlesToComplete[1]);
            puzzle2Target.Select();
            Debug.Log("Item Dropped");
        }
        else
        {
            Debug.Log("nothing to move from slot");
        }
    }
}