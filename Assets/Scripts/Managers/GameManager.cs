using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public enum GameState
    { MainMenu, Acknowledgment, Gameplay, Puzzle, Pause, Options, GameEnd, Dialogue, }

    public GameState gameState;
    public GameState beforeSettings; // Used to know what state happened before Settings to know where to go back to.

    [Header("Managers")]
    public UIManager uiManager;
    public SoundManager soundManager;

    internal bool isPaused;

    public GameObject player;

    private void Start()
    {
        SetState(GameState.MainMenu);
    }

    // Changes the state and UI depending on state requested
    private void SetState(GameState state)
    {
        gameState = state;

        switch (state)
        {
            case GameState.MainMenu: MainMenu(); break;
            case GameState.Acknowledgment: Acknowledgment(); break;
            case GameState.Gameplay: Gameplay(); break;
            case GameState.Dialogue: Dialogue(); break;
            case GameState.Pause: Pause(); break;
            case GameState.Options: Options(); break;
            case GameState.Puzzle: Puzzle(); break;
            case GameState.GameEnd: GameEnd(); break;
        }
    }

    // Allows state to be set by string which converts it to a gameState
    public void LoadState(string state)
    {
        if (Enum.TryParse(state, out GameState gameState))
            LoadState(gameState);
        else
            Debug.LogError("Invalid state: " + state);
    }

    // Takes the state and sets the state to the required state. Also saves the state before options.
    private void LoadState(GameState state)
    {
        if (state == GameState.Options)
            beforeSettings = gameState;

        SetState(state);
    }

    // This can be changed but I used this function to decide what happens when pressing the key for pause depending on the current state.
    public void PausingState()
    {
        if (gameState == GameState.Pause)
            SetState(GameState.Gameplay);
        else if(gameState == GameState.Gameplay)
            SetState(GameState.Pause);
        else if (gameState == GameState.Options)
            LoadState(beforeSettings);
    }

    // This is the functions used to call the UI change.
    #region GameStates
    private void MainMenu()
    {
        isPaused = false;
        uiManager.UI_MainMenu();
    }

    private void Acknowledgment()
    {
        isPaused = false;
        uiManager.UI_Acknowledgement();
    }

    private void Gameplay()
    {
        isPaused = false;
        uiManager.UI_Gameplay();
        player.GetComponent<PlayerInteraction>().enabled = true;
    }

    private void Pause()
    {
        isPaused = true;
        uiManager.UI_Pause();
    }

    private void Options()
    {
        isPaused = true;
        uiManager.UI_Options();
    }

    private void Puzzle()
    {
        uiManager.UI_Puzzle(SceneManager.GetActiveScene().name.ToString());
    }

    private void GameEnd()
    {
        isPaused = false;
        uiManager.UI_EndGameVideo();
    }
    private void Dialogue()
    {
        uiManager.UI_Dialogue();
        player.GetComponent<PlayerInteraction>().enabled = false;
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quitting game");
    }
    #endregion
}
