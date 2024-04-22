using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    private GameManager gameManager;

    public void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }


    public void LoadScene(string sceneName)
    {
        switch(sceneName)
        {
            case "MainMenu": gameManager.LoadState(sceneName); break;
            case string name when name.StartsWith("Room"): gameManager.LoadState("Gameplay"); break;
        }
        SceneManager.LoadScene(sceneName);
    }
}
