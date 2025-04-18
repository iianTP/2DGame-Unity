using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerScript : MonoBehaviour
{

    public Rigidbody2D rb;
    public BoxCollider2D bc2d;

    public InputActionReference movement, interaction, attack, jump;

    public GameObject sword;
    public Transform wallTransform;
    public Transform groundTransform;
    public Transform extraJumpTransform;
    public LayerMask plataformLayer;
    public LayerMask extraJumpLayer;

    public float speed;
    public float jumpForce;
    public string direction;

    private bool isWallJumping = false;
    private float wallJumpCounter = 0;
    private float wallJumpDirection;
    public Vector2 wallJumpForce;
    public float wallJumpTime;

    void Update()
    {
        PlayerMovement();
        
        WallSlide();

        UpdateDirection();
    }


    void PlayerMovement()
    {

        if (!isWallJumping)
        {
            rb.linearVelocityX = movement.action.ReadValue<Vector2>().x * speed;
        }
        
        Jump();
        if (IsOnWall() && !IsOnGround())
        {
            WallJump();
        }

    }

    void Jump()
    {

        if (jump.action.triggered && (IsOnGround() || IsInExtraJump()) && !IsOnWall())
        {
            rb.linearVelocityY = jumpForce;
        }

    }

    void WallJump()
    {
        if (IsOnWall())
        {
            isWallJumping = false;
            wallJumpDirection = -transform.localScale.x;
            wallJumpCounter = wallJumpTime;
            CancelInvoke(nameof(StopWallJump));
        }
        else if (isWallJumping)
        {
            wallJumpCounter -= Time.deltaTime;
        }

        if (jump.action.triggered && wallJumpCounter > 0)
        {
            isWallJumping = true;
            rb.linearVelocity = new Vector2(wallJumpDirection*wallJumpForce.x,wallJumpForce.y);
            wallJumpCounter = 0;
            Invoke(nameof(StopWallJump), 0.1f);
        }

    }

    void StopWallJump()
    {
        isWallJumping = false;
        wallJumpCounter = 0;
    }

    void UpdateDirection()
    {

        if (rb.linearVelocityX != 0)
        {

            direction = (rb.linearVelocityX < 0) ? "left" : "right";
            transform.localScale = (rb.linearVelocityX < 0) ? new Vector3(-0.5f, 0.5f, 0.5f) : new Vector3(0.5f, 0.5f, 0.5f);
        }

        if (rb.linearVelocityY != 0)
        {
            direction = (rb.linearVelocityY < 0) ? "down" : "up";
        }
    }

    bool IsOnGround()
    {
        return Physics2D.OverlapCircle(groundTransform.position, 0.01f, plataformLayer);
    }

    bool IsOnWall()
    {
        return Physics2D.OverlapCircle(wallTransform.position, 0.01f, plataformLayer);
    }

    bool IsInExtraJump()
    {
        return Physics2D.OverlapBox(extraJumpTransform.position, new Vector2(1,1), 0, extraJumpLayer);
    }

    void WallSlide()
    {
        if (IsOnWall() && !IsOnGround())
        {
            rb.linearVelocity = new Vector2(rb.linearVelocityX, Mathf.Clamp(rb.linearVelocityY, -2f, float.MaxValue));
        }
    }

}
