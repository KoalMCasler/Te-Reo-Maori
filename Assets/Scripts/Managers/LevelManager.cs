using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;

public class LevelManager : MonoBehaviour
{
    [Header("Managers")]
    [SerializeField]
    private GameManager gameManager;
    public PuzzleManager puzzleManager;

    //used for the camera and bounding shape, lets each scene have its own shape. 
    [Header("Camera & bounding shape")]
    public GameObject mainCamera;
    public Collider2D foundBoundingShape;
    public CinemachineConfiner2D confiner2D;
    
    [Header("Game Manager")]
    public Transform playerSpawn;


    public void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        gameManager.player.SetActive(false);
        // makes sure on scene loaded works. 
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    //Will be used for inspector
    public void LoadScene(string sceneName)
    {
        switch (sceneName)
        {
            case "MainMenu": LoadScene("MainMenu", 0f); break;
            case "Room 1": LoadScene("Room 1", 0f); break;
            case "Room 2": LoadScene("Room 2", 0f); break;
            case "Room 3": LoadScene("Room 3", 0f); break;
            case "GameEnd": LoadScene("GameEnd", 0f); break;
            default: Debug.Log($"{sceneName} doesnt exist"); break;
        }
    }

    // Used for scripts
    public void LoadScene(string sceneName, float delay)
    {
        switch(sceneName)
        {
            case "MainMenu": 
                gameManager.LoadState(sceneName); 
                break;
            case string name when name.StartsWith("Room"): 
                gameManager.LoadState("Gameplay"); 
                break;
            case "GameEnd":
                gameManager.LoadState(sceneName);
                break;
        }
        StartCoroutine(LevelMoveWithDeley(delay,sceneName));
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // used to find bouding shape and set it to the virtual camera. 
        foundBoundingShape = GameObject.FindWithTag("Confiner").GetComponent<Collider2D>();
        confiner2D.m_BoundingShape2D = foundBoundingShape;
        if(scene.name.StartsWith("Room"))
        {
            playerSpawn = GameObject.FindWithTag("Spawn").GetComponent<Transform>();
            gameManager.player.transform.position = playerSpawn.position;
            gameManager.player.SetActive(true);
            puzzleManager.door = GameObject.Find("Door");
        }
        
    }
    IEnumerator LevelMoveWithDeley(float delay, string sceneName)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(sceneName);
    }
}