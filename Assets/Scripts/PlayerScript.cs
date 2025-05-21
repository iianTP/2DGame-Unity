using System;
using System.Collections;
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
    public LayerMask deathLayer;

    public float energy;
    public float speedAbs;
    public float jumpForceAbs;
    public Vector3 spawnPoint;
    private float speed;
    private float jumpForce;
    private string gravity = "down";

    public bool hasDoubleJump;
    public bool hasDash;
    public bool hasAttack;

    public bool isDoubleJumping = false;

    public bool isDashing = false;
    public float dashForce = 15f;

    public bool isAttacking = false;

    void Start()
    {
        speed = speedAbs;
        jumpForce = jumpForceAbs;
    }

    void Update()
    {
        if (isDashing) { return; }
        
        PlayerMovement();

        if (interaction.action.triggered && hasDash) { StartCoroutine(Dash()); }

        Death();

        UpdateDirection();
    }

    void PlayerMovement()
    {

        if (gravity == "up" || gravity == "down")
        {
            rb.linearVelocityX = movement.action.ReadValue<Vector2>().x * speed;
        }
        if (gravity == "left" || gravity == "right")
        {
            rb.linearVelocityY = movement.action.ReadValue<Vector2>().x * speed;
        }
        
        Jump();
        DoubleJump(); 

    }

    void Jump()
    {

        if (jump.action.triggered && (IsOnGround() || IsInGenericObject(1,1,extraJumpLayer)))
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

    void Death()
    {
        if (IsInGenericObject(0.5f,0.5f,deathLayer))
        {
            transform.position = spawnPoint;
            GravityShift("down");
        }
    }

    IEnumerator Dash()
    {

        float originalGravity = rb.gravityScale;
        float direction = transform.localScale.x/Math.Abs(transform.localScale.x);
        rb.gravityScale = 0f;
        isDashing = true;

        if (gravity == "up" || gravity == "down")
        {
            rb.linearVelocityX = dashForce * direction;
            rb.linearVelocityY = 0;
        }
        if (gravity == "left" || gravity == "right")
        {
            rb.linearVelocityY = dashForce * direction;
            rb.linearVelocityX = 0;
        }

        yield return new WaitForSeconds(0.2f);
        rb.gravityScale = originalGravity;
        isDashing = false;
        yield return new WaitForSeconds(2f);

    }

    void DoubleJump()
    {
        if (hasDoubleJump && jump.action.triggered && !isDoubleJumping && !IsOnGround())
        {
            if (gravity == "up" || gravity == "down")
            {
                rb.linearVelocityY = jumpForce;
            }
            if (gravity == "left" || gravity == "right")
            {
                rb.linearVelocityX = jumpForce;
            }
            isDoubleJumping = true;
        }
        if (IsOnGround()) { isDoubleJumping = false; }
    }


    public void GravityShift(string direction)
    {
        Vector2 gravityDirection = new Vector2(0,-9.81f);
        Vector3 newScale = new Vector3(0.5f, 1f, 1f);
        switch (direction)
        {
            case "up":
                if (gravity == "up") { return; }
                gravityDirection = new Vector2(0, 9.81f);
                speed = speedAbs;
                jumpForce = -jumpForceAbs;


                transform.rotation = Quaternion.Euler(0, 0, 0);
                newScale.y -= 2 * newScale.y;
                transform.localScale = newScale;

                break;

            case "down":

                if (gravity == "down") { return; }
                gravityDirection = new Vector2(0, -9.81f);
                speed = speedAbs;
                jumpForce = jumpForceAbs;

                transform.rotation = Quaternion.Euler(0, 0, 0);
                transform.localScale = newScale;

                break;

            case "left":
                if (gravity == "left") { return; }
                gravityDirection = new Vector2(-9.81f, 0);

                speed = -speedAbs;
                jumpForce = jumpForceAbs;

                transform.rotation = Quaternion.Euler(0,0,-90);
                transform.localScale = newScale;

                break;

            case "right":
                if (gravity == "right") { return; }
                gravityDirection = new Vector2(9.81f, 0);
                speed = speedAbs;
                jumpForce = -jumpForceAbs;

                transform.rotation = Quaternion.Euler(0, 0, 90);
                transform.localScale = newScale;

                break;
        }

        Physics2D.gravity = gravityDirection;
        gravity = direction;

    }

    void UpdateDirection()
    {

        Vector3 newScale = transform.localScale;

        if (movement.action.ReadValue<Vector2>().x != 0)
        {
            newScale.x = 0.5f * movement.action.ReadValue<Vector2>().x;
        }
        transform.localScale = newScale;

    }

    bool IsOnGround()
    {
        return Physics2D.OverlapCircle(groundTransform.position, 0.01f, plataformLayer);
    }

    bool IsInGenericObject(float sizeX, float sizeY, LayerMask layer)
    {
        return Physics2D.OverlapBox(genericTransform.position, new Vector2(sizeX, sizeY), 0, layer);
    }

}
