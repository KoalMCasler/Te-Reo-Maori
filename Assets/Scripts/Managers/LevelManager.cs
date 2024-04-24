using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;

public class LevelManager : MonoBehaviour
{
    [Header("Game Manager")]
    [SerializeField]
    private GameManager gameManager;
    //used for the camrea and bounding shape, lets each scene have its own shape. 
    [Header("Camera & bounding shape")]
    [SerializeField]
    public GameObject mainCamera;
    public Collider2D foundBoundingShape;
    public CinemachineConfiner2D confiner2D;


    public void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        // makes sure on scene loaded works. 
        SceneManager.sceneLoaded += OnSceneLoaded;
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
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // used to find bouding shape and set it to the virtual camera. 
        foundBoundingShape = GameObject.FindWithTag("Confiner").GetComponent<Collider2D>();
        confiner2D.m_BoundingShape2D = foundBoundingShape;
    }
}