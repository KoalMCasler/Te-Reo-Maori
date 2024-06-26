using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Object Referances")]
    [SerializeField] private Rigidbody2D playerRB;
    [SerializeField] private Animator playerAnim;

    [Header("Move Variables")]
    [SerializeField] private Vector3 moveDirection;
    public float moveSpeed = 5f;

    // Managers
    private GameManager gameManager;
    private SoundManager soundManager;

    private bool SFXPlaying;

    // Start is called before the first frame update
    void Start()
    {
        SFXPlaying = false;
        gameManager = FindObjectOfType<GameManager>();
        soundManager = FindObjectOfType<SoundManager>();
        playerRB = gameObject.GetComponent<Rigidbody2D>();
        playerAnim = gameObject.GetComponent<Animator>();
    }

    void OnEnable()
    {
        //used to make it so the character is idle and facing up on scene change. 
        playerAnim.SetBool("IsIdle", true);
        playerAnim.SetBool("IsFacingUp", true);
        playerAnim.SetBool("IsFacingLeft", false);
        playerAnim.SetBool("IsFacingDown", false);
        playerAnim.SetBool("IsFacingRight", false);
    }

    void FixedUpdate()
    {
        Move();
    }

    void OnPause()
    {
        gameManager.PausingState();
        soundManager.PlaySfxAudio("Book");
    }

    void OnMove(InputValue movementValue)
    {
        //Movement logic
        Vector2 moveVector2 = movementValue.Get<Vector2>();
        moveDirection = new Vector3(moveVector2.x, moveVector2.y, 0);

    }

    void Move()
    {
        if (moveDirection.x == 0 && moveDirection.y == 0)
        {
            playerAnim.SetBool("IsIdle", true);
            if (SFXPlaying)
            {
                soundManager.StopPlayerSFX();
                SFXPlaying = false;
            }
        }
        else
        {
            if (!SFXPlaying)
            {
                soundManager.PlayPlayerSFX();
                SFXPlaying = true;
            }
            playerAnim.SetBool("IsIdle", false);
            playerAnim.SetFloat("InputX", moveDirection.x);
            playerAnim.SetFloat("InputY", moveDirection.y);
            playerRB.MovePosition(transform.position + moveDirection.normalized * moveSpeed * Time.fixedDeltaTime);
            CheckDirection();
        }
    }

    void CheckDirection()
    {
        if (moveDirection.x > 0)
        {
            playerAnim.SetBool("IsFacingRight", true);
            playerAnim.SetBool("IsFacingUp", false);
            playerAnim.SetBool("IsFacingLeft", false);
            playerAnim.SetBool("IsFacingDown", false);

        }
        if (moveDirection.y > 0)
        {
            playerAnim.SetBool("IsFacingUp", true);
            playerAnim.SetBool("IsFacingLeft", false);
            playerAnim.SetBool("IsFacingDown", false);
            playerAnim.SetBool("IsFacingRight", false);
        }
        if (moveDirection.x < 0)
        {
            playerAnim.SetBool("IsFacingLeft", true);
            playerAnim.SetBool("IsFacingDown", false);
            playerAnim.SetBool("IsFacingRight", false);
            playerAnim.SetBool("IsFacingUp", false);
        }
        if (moveDirection.y < 0)
        {
            playerAnim.SetBool("IsFacingDown", true);
            playerAnim.SetBool("IsFacingUp", false);
            playerAnim.SetBool("IsFacingLeft", false);
            playerAnim.SetBool("IsFacingRight", false);
        }
    }
}
