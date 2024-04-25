using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public enum GameState
    {
        MainMenu,
        Acknowledgment,
        Gameplay,
        Puzzle,
        Pause,
        Credits,
        Controls,
        Options,
        GameEnd,
    }

    public GameState gameState;
    public GameState beforeSettings ;
    // currentState is used to test to make sure the UI is working properly.
    private GameState currentState;

    [Header("Managers")]
    public UIManager uiManager;

    [Header("Player")]
    public GameObject player;
    private PlayerInput playerInput;

    internal bool isPaused;

    private void Start()
    {
        playerInput = player.GetComponent<PlayerInput>();
        
        SetState(GameState.MainMenu);
        currentState = gameState;
    }

    //public void Update()
    //{
    //    if (gameState != currentState)
    //        SetState(gameState);
    //}

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
            case GameState.Puzzle: Puzzle(); break;
            case GameState.GameEnd: GameEnd(); break;
        }
        currentState = gameState;
    }

    // Allows state to be set by string which converts it to a gameState
    public void LoadState(string state)
    {
        if (Enum.TryParse(state, out GameState gameState))
            LoadState(gameState);
        else
            Debug.LogError("Invalid state: " + state);
    }

    private void LoadState(GameState state)
    {
        if (state == GameState.Options || state == GameState.Controls)
            beforeSettings = gameState;

        SetState(state);
    }

    // This can be changed but I used this function to decide what happens when pressing the key for pause depending on the current state.
    public void PausingState()
    {
        if (gameState == GameState.Pause)
        {
            SetState(GameState.Gameplay);
            playerInput.actions.FindAction("Move").Enable();
        }
        else if(gameState == GameState.Gameplay)
        {
            SetState(GameState.Pause);
            playerInput.actions.FindAction("Move").Disable();
        }
        else if (gameState == GameState.Options || gameState == GameState.Controls)
            LoadState(beforeSettings);
    }

    // These will be used for SoundManager, UIManager & any other things that may need to change with each state
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
        playerInput.actions.FindAction("Move").Enable();
        playerInput.actions.FindAction("Interact").Enable();
        isPaused = false;
        uiManager.UI_Gameplay();
    }

    private void Pause()
    {
        isPaused = true;
        uiManager.UI_Pause();
    }

    private void Credits()
    {
        isPaused = false;
        uiManager.UI_Credits();
    }

    private void Options()
    {
        isPaused = true;
        uiManager.UI_Options();
    }

    private void Controls()
    {
        isPaused = true;
        uiManager.UI_Controls();
    }

    private void Puzzle()
    {
        playerInput.actions.FindAction("Move").Disable();
        playerInput.actions.FindAction("Interact").Disable();
        uiManager.UI_Puzzle();
    }

    private void GameEnd()
    {
        isPaused = false;
        uiManager.UI_EndGame();
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quitting game");
    }
    #endregion
}
