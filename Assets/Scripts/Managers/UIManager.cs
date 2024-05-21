using TMPro;
using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Runtime.CompilerServices;


public class UIManager : MonoBehaviour
{
    //Managers
    public GameManager gameManager;
    public LevelManager levelManager;
    public SoundManager soundManager;
    public PuzzleManager puzzleManager;

    //UI Panels
    public GameObject MainMenuUI;
    public GameObject AcknowledgementUI;
    public GameObject GameplayUI;
    public GameObject DialogueUI;
    public GameObject DialogueUIOptions;
    public GameObject NextDialogueButton;
    public GameObject CreditsUI;
    public GameObject ControlsUI;
    public GameObject PauseUI;
    public GameObject OptionsUI;
    public GameObject EndGameUI;
    public GameObject ProjectInfoButton;

    // Confirmation UI
    public GameObject ConfirmationUI;
    public TextMeshProUGUI confirmationText;
    public Button yesButton;

    // UI for puzzles
    public bool overlayActive;
    public GameObject Room1Puzzle;
    public GameObject Room2Puzzle;
    public GameObject Room3Puzzle;

    // shows Books for room 1 & Artifacts for room 2
    public GameObject InfoBookArtifact;
    public TextMeshProUGUI bookArtifactText;
    public Image bookArtifactImage;

    //Artifact for room 2
    public GameObject ArtifactUI1;
    public GameObject ArtifactUI2;
    public GameObject ArtifactUI3;
    public GameObject ArtifactUI4;

    // picture frames for puzzle 3
    public GameObject horizontalPictureUI;
    public Image horizontalCurrentImage;
    public TextMeshProUGUI horizontalImageDescript;
    public GameObject verticalPictureUI;
    public Image verticalCurrentImage;
    public TextMeshProUGUI verticalImageDescript;

    // Picture UI for puzzle 3
    public GameObject pictureUI1;
    public GameObject pictureUI2;
    public GameObject pictureUI3;
    public GameObject pictureUI4;

    //player settings
    public GameObject player;
    private PlayerInput playerInput;
    private SpriteRenderer playerSprite;

    // target buttons -- needed for controller support
    public Button mainMenuTarget;
    public Button optionsTarget;
    public Button acknowledgmentTarget;
    public Button creditsTarget;
    public Button pauseTarget;
    public Button controlsTarget;
    public Button confirmationTarget;

    //public TMP_InputField puzzle1Target;
    public Button puzzle2Target;
    public Button puzzle3Target;
    public Button dialogueTarget;
    public Button dialogueOptionsTarget;

    // other settings needed for controller
    public bool isHoldingItem;
    public bool puzzle2IsOpen;
    public bool puzzle3IsOpen;
    public GameObject selector; //needed to attach objects for drag and drop. 
    public EventSystem eventSystem; // Add a reference to the EventSystem
    private bool isGamepadConnected;

    // ui for binding
    public GameObject keyboardBindings;
    public GameObject gamepadBindings;
    public GameObject gamepadButton;

    private void Start()
    {
        isHoldingItem = false;
        puzzle2IsOpen = false;
        puzzle3IsOpen = false;
        overlayActive = false;
        playerSprite = player.GetComponent<SpriteRenderer>();
        playerInput = player.GetComponent<PlayerInput>();
        soundManager = FindObjectOfType<SoundManager>();

        eventSystem = EventSystem.current;

        CheckControllerConnection();
    }

    void Update()
    {
        if (EventSystem.current.currentSelectedGameObject != null)
        {
            if (puzzle2IsOpen || puzzle3IsOpen)
            {
                selector.transform.position = EventSystem.current.currentSelectedGameObject.transform.position;
            }
        }
    }

    #region GameState UI
    public void UI_MainMenu()
    {
        puzzle3IsOpen = false;
        puzzle2IsOpen = false;
        PlayerMovement(false);
        CurrentUI(MainMenuUI, false);
    }

    public void UI_Acknowledgement()
    {
        CurrentUI(AcknowledgementUI, false);
    }

    public void UI_Gameplay()
    {
        puzzle3IsOpen = false;
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
                CurrentUI(Room3Puzzle, true); puzzle3Target.Select(); puzzle3IsOpen = true; break;
        }
        soundManager.PlaySfxAudio("Book");
    }

    public void UI_Pause()
    {
        puzzle3IsOpen = false;
        puzzle2IsOpen = false;
        PlayerMovement(false);
        CurrentUI(PauseUI, true);
    }

    public void UI_Options()
    {
        puzzle3IsOpen = false;
        puzzle2IsOpen = false;
        CurrentUI(OptionsUI, true);
    }

    public void UI_EndGame()
    {
        puzzle3IsOpen = false;
        puzzle2IsOpen = false;
        PlayerMovement(false);
        CurrentUI(EndGameUI, false);
    }

    // UI confirmation for exiting to main menu or quit
    public void UI_Confirmation(string name)
    {
        yesButton.onClick.RemoveAllListeners(); // removes listeners from the yes button.
        CurrentUI(ConfirmationUI, true);
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
                confirmationText.text = "Yes button will not work.";
                break;
        }
    }

    public void UI_Dialogue()
    {
        PlayerMovement(false);
        CurrentUI(DialogueUI, true);
        overlayActive = true;
        StartCoroutine(dialogueSelectDelay());
    }

    public void UI_DialogueOptions(bool isActive)
    {
        if (isActive == false)
        {
            DialogueUIOptions.SetActive(false);
        }
        else
        {
            DialogueUIOptions.SetActive(true);
        }
    }

    public void UI_Controls()
    {
        CurrentUI(ControlsUI, false);
    }

    public void UI_Credits()
    {
        CurrentUI(CreditsUI, false);
    }
    #endregion

    #region Controller Connection
    public void CheckControllerConnection()
    {
        // Initial check for connected gamepads
        if (Gamepad.all.Count > 0)
        {
            isGamepadConnected = true;
            ShowBinding(gamepadBindings);
            SelectButtonForActiveUI(); // Ensure the correct button is selected
        }
        else
        {
            isGamepadConnected = false;
            ShowBinding(keyboardBindings);
        }

        // Subscribe to device change events
        InputSystem.onDeviceChange += (device, change) =>
        {
            switch (change)
            {
                case InputDeviceChange.Added:
                    if (device is Gamepad)
                    {
                        isGamepadConnected = true;
                        ShowBinding(gamepadBindings);
                        SelectButtonForActiveUI(); // Ensure the correct button is selected
                    }
                    break;

                case InputDeviceChange.Removed:
                    if (device is Gamepad)
                    {
                        isGamepadConnected = false;
                        ShowBinding(keyboardBindings);
                        UnselectCurrentButton();
                    }
                    break;
            }
        };
    }

    private void SelectButtonForActiveUI()
    {
        gamepadButton.SetActive(true);

        if (MainMenuUI.activeSelf == true) mainMenuTarget.Select();
        if (AcknowledgementUI.activeSelf == true) acknowledgmentTarget.Select();
        if (Room2Puzzle.activeSelf == true) puzzle2Target.Select();
        if (Room3Puzzle.activeSelf == true) puzzle3Target.Select();
        if (PauseUI.activeSelf == true) pauseTarget.Select();
        if (OptionsUI.activeSelf == true) optionsTarget.Select();
        if (CreditsUI.activeSelf == true) creditsTarget.Select();
        if (ConfirmationUI.activeSelf == true) confirmationTarget.Select();
        if (ControlsUI.activeSelf == true) controlsTarget.Select();
        if (DialogueUIOptions.activeSelf == true) dialogueOptionsTarget.Select();
    }

    private void UnselectCurrentButton()
    {
        gamepadButton.SetActive(false);

        if (eventSystem.currentSelectedGameObject != null)
        {
            eventSystem.SetSelectedGameObject(null);
        }
    }

    private IEnumerator SelectButtonAfterDelay()
    {
        yield return new WaitForEndOfFrame(); // Wait until the end of the frame to ensure the UI transition is complete
        SelectButtonForActiveUI(); // Select the appropriate button for the currently active UI
    }

    #endregion

    #region Puzzle UI
    // Depending on the book that's opening, it will open the corresponding UI.
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

    // Shows picture. (Image should be stored on the interactable)
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
        if (isVertical == false)
            CurrentUI(horizontalPictureUI, true);
        if (isVertical == true)
            CurrentUI(verticalPictureUI, true);
        overlayActive = true;
    }

    // This is used at the beginning to turn on the project info UI for after you've interacted with the sign
    public void ShowProjectInfo()
    {
        ProjectInfoButton.SetActive(true);
    }

    #endregion

    public void ShowBinding(GameObject activeBind)
    {
        keyboardBindings.SetActive(false);
        gamepadBindings.SetActive(false);

        activeBind.SetActive(true);
        StartCoroutine(SelectButtonAfterDelay());
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
        DialogueUI.SetActive(false);
        DialogueUIOptions.SetActive(false);
        activeUI.SetActive(true);

        playerSprite.enabled = isActive; // Enables or disables player sprite


        if (gameManager.isPaused)
            Time.timeScale = 0f;
        else
            Time.timeScale = 1f;

        if (isGamepadConnected)
            SelectButtonForActiveUI();
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
        if (EventSystem.current.currentSelectedGameObject.transform.childCount == 1 && isHoldingItem == false)
        {
            if (EventSystem.current.currentSelectedGameObject.GetComponent<PictureSlot>() != null && !puzzleManager.puzzle3TextDone)
            {
                Debug.Log("Finish the text first"); //this is to prevent issues with the controller having the dragable moved to a text box.
                return;
            }
            GameObject itemToMove = EventSystem.current.currentSelectedGameObject.transform.GetChild(0).gameObject;
            itemToMove.transform.SetParent(selector.transform);
            isHoldingItem = true;
            Debug.Log("Item Picked Up");
        }
        else if (EventSystem.current.currentSelectedGameObject.transform.childCount == 0 && isHoldingItem == true)
        {
            GameObject itemToMove = selector.transform.GetChild(0).gameObject;
            selector.transform.GetChild(0).SetParent(EventSystem.current.currentSelectedGameObject.transform);
            isHoldingItem = false;
            if (EventSystem.current.currentSelectedGameObject.GetComponent<ArtifactSlot>() != null)
            {
                EventSystem.current.currentSelectedGameObject.GetComponent<ArtifactSlot>().OnControllerDrop(itemToMove);
            }
            else
            {
                EventSystem.current.currentSelectedGameObject.GetComponent<PictureSlot>().OnControllerDrop(itemToMove);
            }
            if (puzzleManager.puzzlesToComplete[1].status != PuzzleAsset.Status.Finished)
            {
                puzzle2Target.Select();
            }
            if (puzzleManager.puzzlesToComplete[2].status != PuzzleAsset.Status.Finished && puzzleManager.puzzlesToComplete[1].status == PuzzleAsset.Status.Finished)
            {
                puzzle3Target.Select();
            }
            Debug.Log("Item Dropped");
        }
        else
        {
            Debug.Log("nothing to move from slot");
        }
    }

    private IEnumerator dialogueSelectDelay()
    {
        // this is needed to prevent skiping into text on controller use.
        yield return new WaitForSeconds(0.1f);
        dialogueTarget.Select();
    }
}