using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerScript : MonoBehaviour
{

    public Rigidbody2D rb;
    public BoxCollider2D bc2d;

    public InputActionReference movement, interaction, attack, jump;

    public Transform wallTransform;
    public Transform groundTransform;
    public Transform genericTransform;
    public LayerMask plataformLayer;
    public LayerMask extraJumpLayer;
    public LayerMask gravityShifter1Layer;

    public float speedAbs;
    public float jumpForceAbs;
    private float speed;
    private float jumpForce;
    private string gravity = "down";

    private bool isWallJumping = false;
    private float wallJumpCounter = 0;
    private float wallJumpDirection;

    public Vector2 wjForceAbs;

    private Vector2 wallJumpForce;

    public float wallJumpTime;

    void Start()
    {
        speed = speedAbs;
        jumpForce = jumpForceAbs;
        wallJumpForce = wjForceAbs;
    }

    void Update()
    {

        PlayerMovement();


        //WallSlide();


        UpdateDirection();
    }


    void PlayerMovement()
    {

        if (!isWallJumping)
        {
            if (gravity == "up" || gravity == "down")
            {
                rb.linearVelocityX = movement.action.ReadValue<Vector2>().x * speed;
            }
            if (gravity == "left" || gravity == "right")
            {
                rb.linearVelocityY = movement.action.ReadValue<Vector2>().x * speed;
            }
        }

        Jump();
        if (IsOnWall() && !IsOnGround())
        {
            WallJump();
        }
     
    }

    void Jump()
    {

        if (jump.action.triggered && (IsOnGround() || IsInGenericObject(extraJumpLayer)) /*&& !IsOnWall()*/)
        {
            if (gravity == "up" || gravity == "down")
            {
                rb.linearVelocityY = jumpForce;
            }
            if (gravity == "left" || gravity == "right")
            {
                rb.linearVelocityX = jumpForce;
            }
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

            if (gravity == "up" || gravity == "down")
            {
                rb.linearVelocity = new Vector2(wallJumpDirection * wallJumpForce.x, wallJumpForce.y);
            }
            if (gravity == "left" || gravity == "right")
            {
                rb.linearVelocity = new Vector2(wallJumpForce.y, wallJumpDirection * wallJumpForce.x);
            }

            wallJumpCounter = 0;
            Invoke(nameof(StopWallJump), 0.1f);
        }

    }

    void StopWallJump()
    {
        isWallJumping = false;
        wallJumpCounter = 0;
    }


    public void GravityShift(string direction)
    {
        Vector2 gravityDirection = new Vector2(0,-9.81f);
        switch (direction)
        {
            case "up":
                if (gravity == "up") { break; }
                gravityDirection = new Vector2(0, 9.81f);
                speed = speedAbs;
                jumpForce = -jumpForceAbs;


                wallJumpForce.x = wjForceAbs.x;
                wallJumpForce.y = -wjForceAbs.y;

                transform.rotation = Quaternion.Euler(0, 0, 0);
                transform.localScale = new Vector3(0.5f, -0.5f, 0.5f);

                break;

            case "down":

                if (gravity == "down") { break; }
                gravityDirection = new Vector2(0, -9.81f);
                speed = speedAbs;
                jumpForce = jumpForceAbs;

                wallJumpForce.x = wjForceAbs.x;
                wallJumpForce.y = wjForceAbs.y;

                transform.rotation = Quaternion.Euler(0, 0, 0);
                transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);

                break;

            case "left":
                if (gravity == "left") { break; }
                gravityDirection = new Vector2(-9.81f, 0);

                speed = -speedAbs;
                jumpForce = jumpForceAbs;

                wallJumpForce.x = wjForceAbs.x;
                wallJumpForce.y = wjForceAbs.y;


                transform.rotation = Quaternion.Euler(0,0,-90);
                transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);

                break;

            case "right":
                if (gravity == "right") { break; }
                gravityDirection = new Vector2(9.81f, 0);
                speed = speedAbs;
                jumpForce = -jumpForceAbs;

                wallJumpForce.x = wjForceAbs.x;
                wallJumpForce.y = -wjForceAbs.y;

                transform.rotation = Quaternion.Euler(0, 0, 90);
                transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);

                break;
        }

        Physics2D.gravity = gravityDirection;
        gravity = direction;

    }

    void UpdateDirection()
    {

        Vector3 newScale = transform.localScale;
            
        if ((Input.GetKeyDown(KeyCode.LeftArrow) && newScale.x > 0) || (Input.GetKeyDown(KeyCode.RightArrow) && newScale.x < 0))
        { 
            newScale.x *= -1; 
        }
        transform.localScale = newScale;

    }

    bool IsOnGround()
    {
        return Physics2D.OverlapCircle(groundTransform.position, 0.01f, plataformLayer);
    }

    bool IsOnWall()
    {
        return Physics2D.OverlapCircle(wallTransform.position, 0.01f, plataformLayer);
    }

    bool IsInGenericObject(LayerMask layer)
    {
        return Physics2D.OverlapBox(genericTransform.position, new Vector2(1, 1), 0, layer);
    }

}
