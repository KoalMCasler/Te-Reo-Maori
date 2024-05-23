using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;
using System;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;

public class LevelManager : MonoBehaviour
{
    [Header("Managers")]
    [SerializeField] private GameManager gameManager;
    [SerializeField] private GameObject gameManagerObject;
    [SerializeField] private PuzzleManager puzzleManager;
    [SerializeField] private SoundManager soundManager;
    public Singleton singleton;

    //used for the camera and bounding shape, lets each scene have its own shape. 
    [Header("Camera & bounding shape")]
    public GameObject mainCamera;
    public Collider2D foundBoundingShape;
    public CinemachineConfiner2D confiner2D;

    [Header("Player Spawn Location")]
    public GameObject player;
    public Transform playerSpawn;
    private bool GameplayMusicIsPlaying;

    [Header("Scene Fade")]
    public Animator fadeAnimator;

    // Callback function to be invoked adter fade animation completes
    private System.Action fadeCallback;


    public void Start()
    {
        GameplayMusicIsPlaying = false;
        gameManager = FindObjectOfType<GameManager>();
        fadeAnimator = GetComponent<Animator>();
        fadeAnimator.updateMode = AnimatorUpdateMode.UnscaledTime;
        // makes sure on scene loaded works. 
        //SceneManager.sceneLoaded += OnSceneLoaded; <--- where the problem was.
    }

    public void LoadScene(string sceneName)
    {
        Fade("FadeOut", () =>
        {
            SceneManager.sceneLoaded += OnSceneLoaded; // <---- only call needed. 

            switch (sceneName)
            {
                case "MainMenu":
                    gameManager.LoadState(sceneName);
                    soundManager.PlayAudio("MainMenu");
                    GameplayMusicIsPlaying = false;
                    singleton.ClearInstance();
                    SceneManager.MoveGameObjectToScene(gameManagerObject, SceneManager.GetActiveScene());
                    break;
                case string name when name.StartsWith("Room"):
                    gameManager.LoadState("Gameplay");
                    break;
                case "GameEnd":
                    soundManager.StopMusic();
                    gameManager.LoadState("GameEnd");
                    GameplayMusicIsPlaying = false;
                    break;
                default:
                    Debug.Log($"{sceneName} doesnt exist");
                    break;
            }
            SceneManager.LoadScene(sceneName);

        });
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if(scene.name == "MainMenu")
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
            return;
        }
        else
        {
            foundBoundingShape = null;
            // used to find bouding shape and set it to the virtual camera. 
            foundBoundingShape = GameObject.FindWithTag("Confiner").GetComponent<Collider2D>();
            confiner2D.m_BoundingShape2D = foundBoundingShape;
            player = GameObject.FindWithTag("Player");
            //Debug.Log("Level manager Has Player Referance");
            playerSpawn = GameObject.FindWithTag("Spawn").GetComponent<Transform>();
            //Debug.Log("Level manager Has Player Spawn Referance");
            player.transform.position = playerSpawn.position;
            fadeAnimator = gameObject.GetComponent<Animator>();
            //Debug.Log("Level manager Has Fade Animator Referance");
            if (scene.name.StartsWith("Room"))
            {
                if (!GameplayMusicIsPlaying)
                {
                    soundManager.StopMusic();
                    soundManager.PlayAudio("Gameplay");
                    GameplayMusicIsPlaying = true;
                }
                puzzleManager.door = GameObject.Find("Door");
            }

            Fade("FadeIn");
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }
    }

    public void Fade(string fadeDir, System.Action callback = null)
    {
        fadeCallback = callback;
        fadeAnimator.SetTrigger(fadeDir);
    }

    public void FadeAnimationComplete()
    {
        // Invoke the callback if it's not null
        fadeCallback?.Invoke();
    }
}