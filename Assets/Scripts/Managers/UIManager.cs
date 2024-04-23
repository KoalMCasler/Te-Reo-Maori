using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [Header("UI Manager")]
    public GameObject MainMenuUI;
    public GameObject AcknowledgementUI;
    public GameObject GameplayUI;
    public GameObject CreditsUI;
    public GameObject ControlsUI;
    public GameObject PauseUI;
    public GameObject OptionsUI;
    public GameObject EndGameUI;

    // Sound Manager will need to decreased audio / or change audio during paused or dialogue.

    public void UI_MainMenu()
    {
        CurrentUI(MainMenuUI);
    }

    public void UI_Acknowledgement()
    {
        CurrentUI(AcknowledgementUI);
    }

    public void UI_Gameplay()
    {
        CurrentUI(GameplayUI);
    }

    public void UI_Controls()
    {
        CurrentUI(ControlsUI);
    }

    public void UI_Credits()
    {
        CurrentUI(CreditsUI);
    }

    public void UI_Pause()
    {
        CurrentUI(PauseUI);
    }

    public void UI_Options()
    {
        CurrentUI(OptionsUI);
    }

    public void UI_EndGame()
    {
        CurrentUI(EndGameUI);
    }

    // Sets UI to the required panel
    void CurrentUI(GameObject activeUI)
    {
        AcknowledgementUI.SetActive(false);
        MainMenuUI.SetActive(false);
        GameplayUI.SetActive(false);
        ControlsUI.SetActive(false);
        CreditsUI.SetActive(false);
        PauseUI.SetActive(false);
        OptionsUI.SetActive(false);
        EndGameUI.SetActive(false);

        activeUI.SetActive(true);
    }
}
