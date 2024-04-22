using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public enum GameState
    {
        MainMenu,
        Acknowledgment,
        Gameplay,
        Pause,
        Credits,
        Controls,
        Options,
        EndGame,
    }

    public GameState gameState;
    // currentState is used to test to make sure the UI is working properly.
    //private GameState currentState;

    [Header("Managers")]
    public UIManager uiManager;

    private void Start()
    {
        SetState(GameState.MainMenu);
       // currentState = gameState;
    }

    public void Update()
    {
        //if (gameState != currentState)
        //    SetState(gameState);
    }

    // Changes the state and UI
    private void SetState(GameState state)
    {
        gameState = state;

        switch (state)
        {
            case GameState.MainMenu: MainMenu(); break;
            case GameState.Acknowledgment: Acknowledgment(); break;
            case GameState.Gameplay: Gameplay(); break;
            case GameState.Pause: Pause(); break;
            case GameState.Credits: Credits(); break;
            case GameState.Controls: Controls(); break;
            case GameState.Options: Options(); break;
            case GameState.EndGame: EndGame(); break;
        }
       // currentState = gameState;
    }

    // Allows state to be set by string which converts it to a gameState
    public void LoadState(string state)
    {
        if (Enum.TryParse(state, out GameState gameState))
            SetState(gameState);
        else
            Debug.LogError("Invalid state: " + state);
    }

    // This can be changed but I used this function to decide what happens when pressing the key for pause depending on the current state.
    public void PausingState()
    {
        if (gameState == GameState.Pause) 
            SetState(GameState.Gameplay);
        else if(gameState == GameState.Gameplay) 
            SetState(GameState.Pause);
    }

    // These will be used for SoundManager, UIManager & any other things that may need to change with each state
    #region GameStates
    private void MainMenu()
    {
        uiManager.UI_MainMenu();
    }

    private void Acknowledgment()
    {
        uiManager.UI_Acknowledgement();
    }

    private void Gameplay()
    {
        uiManager.UI_Gameplay();
    }

    private void Pause()
    {
        uiManager.UI_Pause();
    }

    private void Credits()
    {
        uiManager.UI_Credits();
    }

    private void Options()
    {
        uiManager.UI_Options();
    }

    private void Controls()
    {
        uiManager.UI_Controls();
    }

    private void EndGame()
    {

    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quitting game");
    }
    #endregion
}
