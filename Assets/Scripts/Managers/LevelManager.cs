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
    [Header("Game Manager")]
    public Transform playerSpawn;


    public void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        gameManager.player.SetActive(false);
        // makes sure on scene loaded works. 
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    public void LoadFirstScene()
    {
        LoadScene("Room 1", 0f);
    }

    public void LoadScene(string sceneName, float delay)
    {
        switch(sceneName)
        {
            case "MainMenu": gameManager.LoadState(sceneName); break;
            case string name when name.StartsWith("Room"): gameManager.LoadState("Gameplay"); break;
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
        }
        
    }
    IEnumerator LevelMoveWithDeley(float delay, string sceneName)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(sceneName);
    }
}