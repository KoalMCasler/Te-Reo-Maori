using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    [Header("Managers")]
    public GameManager gameManager;
    public LevelManager levelManager;

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
    public GameObject ProjectInfo;

    [Header("Confirmation Exit")]
    public GameObject ConfirmationUI;
    public TextMeshProUGUI confirmationText;
    public Button yesButton;

    // UI for puzzles
    [Header("Puzzle UI")]
    public GameObject Room1Puzzle;
    public GameObject Room2Puzzle;
    public GameObject Room3Puzzle;

    //Books for room 1
    [Header("Puzzle 1 UI")]
    public GameObject Book1;
    public GameObject Book2;
    public GameObject Book3;
    public GameObject Book4;

    //Artifact for room 2
    [Header("Puzzle 2 UI")]
    public GameObject Artifact1;
    public GameObject Artifact2;
    public GameObject Artifact3;
    public GameObject Artifact4;
    public GameObject ArtifactUI1;
    public GameObject ArtifactUI2;
    public GameObject ArtifactUI3;
    public GameObject ArtifactUI4;

    [Header("Puzzle 3")]
    public GameObject PictureUI;
    public Image currentImage; // Might try to use only 1 UI object but just change the image that's shown

    [Header("Player Settings")]
    public GameObject player;
    private PlayerInput playerInput;
    private SpriteRenderer playerSprite;
   

    private void Start()
    {
        playerSprite = player.GetComponent<SpriteRenderer>();
        playerInput = player.GetComponent<PlayerInput>();
    }

    #region GameState UI
    public void UI_MainMenu()
    {
        CurrentUI(MainMenuUI, false);
    }

    public void UI_Acknowledgement()
    {
        CurrentUI(AcknowledgementUI, false);
    }

    public void UI_Gameplay()
    {
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
                CurrentUI(Room1Puzzle, true); break;
            case "Room 2":
                CurrentUI(Room2Puzzle, true); break;
            case "Room 3":
                CurrentUI(Room3Puzzle, true); break;
        }
    }

    public void UI_Pause()
    {
        PlayerMovement(false);
        CurrentUI(PauseUI, true);
    }

    public void UI_Options()
    {
        CurrentUI(OptionsUI, true);
    }

    public void UI_EndGame()
    {
        PlayerMovement(false);
        CurrentUI(EndGameUI, false);
    }

    #endregion



    #region Puzzle UI

    // Depending on the book that's opening, it will open the corresponding UI. (I'd like to change this if I find time.)
    public void ShowBook(string name)
    {
        PlayerMovement(false);

        switch (name)
        {
            case "Book1": CurrentUI(Book1, true); break;
            case "Book2": CurrentUI(Book2, true); break;
            case "Book3": CurrentUI(Book3, true); break;
            case "Book4": CurrentUI(Book4, true); break;
            default: Debug.Log($"{name} doesnt exist"); break;
        }
    }

    //Artifact objects work like books.
    public void ShowArtifact(string name)
    {
        PlayerMovement(false);

        switch (name)
        {
            case "Artifact1": CurrentUI(Artifact1, true); ArtifactUI1.SetActive(true); break;
            case "Artifact2": CurrentUI(Artifact2, true); ArtifactUI2.SetActive(true); break;
            case "Artifact3": CurrentUI(Artifact3, true); ArtifactUI3.SetActive(true); break;
            case "Artifact4": CurrentUI(Artifact4, true); ArtifactUI4.SetActive(true); break;
            default: Debug.Log($"{name} doesnt exist"); break;
        }
    }

    // Shows picture. The plan is to use 1 UI object and just change the image depending on the object (Image should be stored on the interactable)
    public void ShowPicture()
    {
        PlayerMovement(false);

        CurrentUI(PictureUI, true);
    }

    #endregion

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
                confirmationText.text = "";
                break;
        }
    }

    // This is used at the beginning to turn on the project info UI for after you've interacted with the sign
    public void ShowProjectInfo()
    {
        ProjectInfo.SetActive(true);
    }

    public void UI_Dialogue()
    {
        CurrentUI(DialogueUI, true);
    }

    public void UI_Controls()
    {
        CurrentUI(ControlsUI, false);
    }

    public void UI_Credits()
    {
        CurrentUI(CreditsUI, false);
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
        Book1.SetActive(false);
        Book2.SetActive(false);
        Book3.SetActive(false);
        Book4.SetActive(false);
        Artifact1.SetActive(false);
        Artifact2.SetActive(false);
        Artifact3.SetActive(false);
        Artifact4.SetActive(false);
        ConfirmationUI.SetActive(false);

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
}