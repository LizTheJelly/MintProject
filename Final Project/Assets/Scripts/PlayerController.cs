using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float playerForce;
    [SerializeField]
    private Rigidbody2D playerRigidBody;

    private Vector2 movePlayer = Vector2.zero;
    private bool facingRight = true;
    private Animator animControl;

    // Start is called before the first frame update
    // Grabbed Player Animator in Start() and set "Speed" to 0 so the player's character
    // remains in its idle animation until it moves.
    void Start()
    {
        animControl = GetComponent<Animator>();
        if (animControl != null)
        {
            animControl.SetFloat("Speed", 0);
        }

        if(PlayerPrefs.HasKey("playerPosX"))
        {
            Vector2 moveAtSavedPos = new Vector2(PlayerPrefs.GetFloat("playerPosX"), PlayerPrefs.GetFloat("playerPosY"));
            transform.position = moveAtSavedPos;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            movePlayer = new Vector2(0, playerForce);
        }
        if (Input.GetKey(KeyCode.S))
        {
            movePlayer = new Vector2(0, -playerForce);
        }
        if (Input.GetKey(KeyCode.A))
        {
            movePlayer = new Vector2(-playerForce, 0);

            if (facingRight)
            {
                facingRight = false;
                FlipCharacter();
            }
        }
        if (Input.GetKey(KeyCode.D))
        {
            movePlayer = new Vector2(playerForce, 0);
            
            if (!facingRight)
            {
                facingRight = true;
                FlipCharacter();
            }
        }
    }

    // For the Animation Assignment, I implemented two things for the animation of the character:
    // 1) I run the character's idle animation when the player stops pressing the W, A, S, and D keys.
    // 2) I run the character's run animation when the player presses either  W, A, S, and D keys.
    void FixedUpdate()
    {
        if (movePlayer == Vector2.zero)
        {
            playerRigidBody.velocity = Vector2.zero;
            if (animControl != null)
            {
                animControl.SetFloat("Speed", 0);
            }
        }
        else
        { 
            playerRigidBody.AddForce(movePlayer - playerRigidBody.velocity);
            if (animControl != null)
            {
                animControl.SetFloat("Speed", 0.2f);
            }
        }
        movePlayer = Vector2.zero;
    }
    
    // FlipCharacter() flips the sprite horizontally depending on the direction the
    // player is heading. If the player moves to the right, the sprite looks to the right.
    // If left, the sprite looks to the left.
    private void FlipCharacter()
    {
        Vector3 playerScale = transform.localScale;
        playerScale.x *= -1;
        transform.localScale = playerScale;
    }
}
