using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public GameManager gameManager;

    [Header("UI Manager")]
    public GameObject MainMenuUI;
    public GameObject AcknowledgementUI;
    public GameObject GameplayUI;
    public GameObject DialogueUI;
    public GameObject CreditsUI;
    public GameObject ControlsUI;
    public GameObject PauseUI;
    public GameObject OptionsUI;
    public GameObject EndGameUI;

    [Header("UI for Puzzles")]
    public GameObject Room1Puzzle;
    public GameObject Room2Puzzle;
    public GameObject Room3Puzzle;
    public GameObject Book1;
    public GameObject Book2;
    public GameObject Book3;
    public GameObject Book4;

    //[Header("UI for Interactions")]
    // add interaction UI

    [Header("Player Settings")]
    public GameObject player;
    private SpriteRenderer playerSprite;

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

    public void UI_Puzzle()
    {
        if (SceneManager.GetActiveScene().name == "Room 1")
            CurrentUI(Room1Puzzle, true);
        else if (SceneManager.GetActiveScene().name == "Room 2")
            CurrentUI(Room2Puzzle, true);
        else if (SceneManager.GetActiveScene().name == "Room 3")
            CurrentUI(Room3Puzzle, true);
    }

    public void ShowBook(string name)
    {
        Debug.Log(name);
        switch (name)
        {
            case "Book1": CurrentUI(Book1, true); break;
            case "Book2": CurrentUI(Book2, true); break;
            case "Book3": CurrentUI(Book3, true); break;
            case "Book4": CurrentUI(Book4, true); break;
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

        activeUI.SetActive(true);
        playerSprite.enabled = isActive;

        if (gameManager.isPaused)
            Time.timeScale = 0f;
        else
            Time.timeScale = 1f;
    }
}

