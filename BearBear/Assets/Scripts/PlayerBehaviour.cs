using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    #region Movement Variables
    private float hori;
    private float speed = 8f;
    private float jumpForce = 16f;
    private bool isFacingRight = true;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;

    #endregion
    #region Visual Variables
    private Renderer rend;

    [SerializeField] Color colourToTurnTo;

    #endregion

    #region Reference Variables

    #endregion

    private void Start()
    {
        rend = GetComponent<Renderer>();
    }


    // Update is called once per frame
    void Update()
    {
        rend.material.color = colourToTurnTo;

        //moving side to side
        hori = Input.GetAxisRaw("Horizontal");

        //jumping
        if (Input.GetButtonDown("Jump") && isGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }

        Flip();
        checkState();

    }

    private void checkState()
    {
        switch (GameManager.game.State)
        {
            case GameState.DreamState:
                colourToTurnTo = Color.yellow;
                //print("YELLOW");
                break;
            case GameState.NightmareState:
                colourToTurnTo = Color.magenta;
                //print("PINK");
                break;
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(hori * speed, rb.velocity.y);
    }

    #region GroundCheck
    //checks if the player is grounded, returns a bool
    private bool isGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }
    #endregion

    #region right facing
    //checks which direction the characeter is facing
    private void Flip()
    {
        if (isFacingRight && hori < 0f || isFacingRight && hori > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }
    #endregion

    #region Collisions
    private void OnTriggerEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Mirror"))
        {
            print("collide");
           GameManager.game.UpdateGameState(GameState.NightmareState);
            //if (colourToTurnTo == Color.magenta) GameManager.game.UpdateGameState(GameState.DreamState);
        }
    }
    #endregion
}