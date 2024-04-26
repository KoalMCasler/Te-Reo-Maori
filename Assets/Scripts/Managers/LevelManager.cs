using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;

public class LevelManager : MonoBehaviour
{
    [Header("Managers")]
    [SerializeField] private GameManager gameManager;
    [SerializeField] private PuzzleManager puzzleManager;
    [SerializeField] private SoundManager soundManager;

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
        // makes sure on scene loaded works. 
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    public void LoadScene(string sceneName)
    {
        Fade("FadeOut", () =>
        {
            SceneManager.sceneLoaded += OnSceneLoaded;

            switch (sceneName)
            {
                case "MainMenu":
                    gameManager.LoadState(sceneName);
                    soundManager.PlayAudio("MainMenu");
                    GameplayMusicIsPlaying = false;
                    break;
                case string name when name.StartsWith("Room"):
                    gameManager.LoadState("Gameplay");
                    break;
                case "GameEnd":
                    soundManager.PlayAudio("MainMenu");
                    gameManager.LoadState(sceneName);
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
        // used to find bouding shape and set it to the virtual camera. 
        foundBoundingShape = GameObject.FindWithTag("Confiner").GetComponent<Collider2D>();
        confiner2D.m_BoundingShape2D = foundBoundingShape;

        if (scene.name.StartsWith("Room"))
        {
            if (!GameplayMusicIsPlaying)
            {
                soundManager.PlayAudio("Gameplay");
                GameplayMusicIsPlaying = true;
            }
            playerSpawn = GameObject.FindWithTag("Spawn").GetComponent<Transform>();
            player.transform.position = playerSpawn.position;
            puzzleManager.door = GameObject.Find("Door");
        }

        Fade("FadeIn");
        SceneManager.sceneLoaded -= OnSceneLoaded;
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