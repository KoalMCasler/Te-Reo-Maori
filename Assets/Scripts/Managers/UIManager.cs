using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
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

    [Header("Confirmation Exit")]
    public GameObject ConfirmationUI;
    public TextMeshProUGUI confirmationText;
    public Button yesButton;
    public GameObject yesMenuButton;
    public GameObject yesQuitButton;

    [Header("UI for Puzzles")]
    public GameObject Room1Puzzle;
    public GameObject Room2Puzzle;
    public GameObject Room3Puzzle;

    //Books for room 1
    public GameObject Book1;
    public GameObject Book2;
    public GameObject Book3;
    public GameObject Book4;
    //Artifact for room 2
    public GameObject Artifact1;
    public GameObject Artifact2;
    public GameObject Artifact3;
    public GameObject Artifact4;
    public GameObject ArtifactUI1;
    public GameObject ArtifactUI2;
    public GameObject ArtifactUI3;
    public GameObject ArtifactUI4;

    [Header("Player Settings")]
    public GameObject player;
    private SpriteRenderer playerSprite;
    public bool isPlayerActive;

    private void Start()
    {
        playerSprite = player.GetComponent<SpriteRenderer>();
    }

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
        CurrentUI(GameplayUI, true);
    }

    public void UI_Confirmation(string name)
    {
        yesButton.onClick.RemoveAllListeners();
        CurrentUI(ConfirmationUI, true);

        switch (name)
        {
            case "quit":
                confirmationText.text = "Are you sure you want to quit?";
                yesButton.onClick.AddListener(() => gameManager.QuitGame());
               // yesQuitButton.SetActive(true);
                //yesMenuButton.SetActive(false);
                break;
            case "mainmenu":
                confirmationText.text = "Are you sure you want to go to Main Menu? All progress will not be saved";
                yesButton.onClick.AddListener(() => levelManager.LoadScene("MainMenu"));
                //yesQuitButton.SetActive(false);
                //yesMenuButton.SetActive(true);
                break;
            default:
                confirmationText.text = "";
                break;
        }
    }

    public void UI_Puzzle(string name)
    {
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

    public void ShowBook(string name)
    {
        switch (name)
        {
            case "Book1": CurrentUI(Book1, true); break;
            case "Book2": CurrentUI(Book2, true); break;
            case "Book3": CurrentUI(Book3, true); break;
            case "Book4": CurrentUI(Book4, true); break;
            default: Debug.Log($"{name} doesnt exist"); break;
        }
    }

    //Failed atempt to get artifact objects to work like books.
    public void ShowArtifact(string name)
    {
        switch (name)
        {
            case "Artifact1": CurrentUI(Artifact1, true);ArtifactUI1.SetActive(true); break;
            case "Artifact2": CurrentUI(Artifact2, true);ArtifactUI2.SetActive(true); break;
            case "Artifact3": CurrentUI(Artifact3, true);ArtifactUI3.SetActive(true); break;
            case "Artifact4": CurrentUI(Artifact4, true);ArtifactUI4.SetActive(true); break;
            default: Debug.Log($"{name} doesnt exist"); break;
        }
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

    public void UI_Pause()
    {
        CurrentUI(PauseUI, true);
    }

    public void UI_Options()
    {
        CurrentUI(OptionsUI, true);

    }

    public void UI_EndGame()
    {
        CurrentUI(EndGameUI, false);
    }

    // Sets UI to the required panel
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
        playerSprite.enabled = isActive;
        if (playerSprite.enabled == true)
            isPlayerActive = true;
        else
            isPlayerActive = false;

        if (gameManager.isPaused)
            Time.timeScale = 0f;
        else
            Time.timeScale = 1f;
    }
}

