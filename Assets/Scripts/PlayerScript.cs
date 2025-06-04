using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerScript : MonoBehaviour
{

    public static PlayerScript instance;

    public Rigidbody2D rb;
    public BoxCollider2D bc2d;
    public GameObject projectile;

    public InputActionReference movement, interaction, attack, jump;

    private float coyoteTime = 0.5f;
    private float coyoteTimeCounter;

    public int stage;

    public Transform wallTransform;
    public Transform groundTransform;
    public Transform genericTransform;
    public LayerMask plataformLayer;
    public LayerMask extraJumpLayer;
    public LayerMask gravityShifter1Layer;
    public LayerMask deathLayer;

    private bool isWallJumping = false;
    private float wallJumpDirection;
    public Vector2 wjForceAbs;
    private Vector2 wallJumpForce;
    public float wallJumpTime;

    public float energy;
    public float speedAbs;
    public float jumpForceAbs;
    public Vector3 spawnPoint;
    private float speed;
    private float jumpForce;
    private string gravity = "down";
    public float maxFallSpeed;

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
        wallJumpForce = wjForceAbs;
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {

        Death();

        if (isDashing) { return; }
        if (isWallJumping) { return; }

        if (IsOnGround()) {  coyoteTimeCounter = coyoteTime; }
        else { coyoteTimeCounter -= Time.deltaTime; }

        PlayerMovement();

        FallSpeed();

        if (IsOnWall() && !IsOnGround() && jump.action.triggered && movement.action.ReadValue<Vector2>().x != 0) { StartCoroutine(WallJump()); }

        if (interaction.action.triggered && hasDash) { StartCoroutine(Dash()); }

        Shoot();

        UpdateDirection();



        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SceneControllerScript.instance.LoadLevel(5);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SceneControllerScript.instance.LoadLevel(10);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SceneControllerScript.instance.LoadLevel(15);
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            SceneControllerScript.instance.LoadLevel(3);
        }
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
        
        DoubleJump();

    }

    void FallSpeed()
    {
        if ((gravity == "up" || gravity == "down") && Math.Abs(rb.linearVelocityY) > maxFallSpeed)
        {
            float sign = rb.linearVelocityY / Math.Abs(rb.linearVelocityY);
            rb.linearVelocityY = sign * maxFallSpeed;
        }
        if ((gravity == "left" || gravity == "right") && Math.Abs(rb.linearVelocityX) > maxFallSpeed)
        {
            float sign = rb.linearVelocityX / Math.Abs(rb.linearVelocityX);
            rb.linearVelocityX = sign * maxFallSpeed;
        }
    }

    void Shoot()
    {
        if (attack.action.triggered && hasAttack)
        {
            GameObject.Instantiate(projectile);
        }
    }

    public void Jump(InputAction.CallbackContext context)
    {

        if (coyoteTimeCounter > 0f && context.performed)
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

        if (context.canceled && !IsOnGround())
        {
            if ((gravity == "up" && rb.linearVelocityY < 0f) || (gravity == "down" && rb.linearVelocityY > 0f))
            {
                rb.linearVelocityY *= 0.5f;
            }
            if ( (gravity == "left" && rb.linearVelocityX > 0f) || (gravity == "right" && rb.linearVelocityX < 0f))
            {
                rb.linearVelocityX *= 0.5f;
            }
            coyoteTimeCounter = 0f;
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
        if (coyoteTimeCounter <= 0 && hasDoubleJump && jump.action.triggered && !isDoubleJumping && !IsOnGround())
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

    IEnumerator WallJump()
    {

        isWallJumping = true;
        wallJumpDirection = -transform.localScale.x;

        if (gravity == "up" || gravity == "down")
        {
            rb.linearVelocity = new Vector2(wallJumpDirection * wallJumpForce.x, wallJumpForce.y);
        }
        if (gravity == "left" || gravity == "right")
        {
            rb.linearVelocity = new Vector2(wallJumpForce.y, wallJumpDirection * wallJumpForce.x);
        }

        yield return new WaitForSeconds(wallJumpTime);

        isWallJumping = false;

        yield return new WaitForSeconds(0.1f);

    }


    public void GravityShift(string direction)
    {
        Vector2 gravityDirection = new Vector2(0,-9.81f);
        Vector3 newScale = new Vector3(0.5f, 0.5f, 0.5f);
        switch (direction)
        {
            case "up":
                if (gravity == "up") { return; }
                gravityDirection = new Vector2(0, 9.81f);
                speed = speedAbs;
                jumpForce = -jumpForceAbs;

                wallJumpForce.x = wjForceAbs.x;
                wallJumpForce.y = -wjForceAbs.y;

                dashForce = Math.Abs(dashForce);

                transform.rotation = Quaternion.Euler(0, 0, 0);
                newScale.y = - newScale.y;
                transform.localScale = newScale;

                break;

            case "down":

                if (gravity == "down") { return; }
                gravityDirection = new Vector2(0, -9.81f);
                speed = speedAbs;
                jumpForce = jumpForceAbs;

                wallJumpForce.x = wjForceAbs.x;
                wallJumpForce.y = wjForceAbs.y;

                dashForce = Math.Abs(dashForce);

                transform.rotation = Quaternion.Euler(0, 0, 0);
                transform.localScale = newScale;

                break;

            case "left":
                if (gravity == "left") { return; }
                gravityDirection = new Vector2(-9.81f, 0);

                speed = -speedAbs;
                jumpForce = jumpForceAbs;

                wallJumpForce.x = wjForceAbs.x;
                wallJumpForce.y = wjForceAbs.y;

                dashForce = -Math.Abs(dashForce);

                transform.rotation = Quaternion.Euler(0,0,-90);
                transform.localScale = newScale;

                break;

            case "right":
                if (gravity == "right") { return; }
                gravityDirection = new Vector2(9.81f, 0);
                speed = speedAbs;
                jumpForce = -jumpForceAbs;

                wallJumpForce.x = wjForceAbs.x;
                wallJumpForce.y = -wjForceAbs.y;

                dashForce = Math.Abs(dashForce);

                transform.rotation = Quaternion.Euler(0, 0, 90);
                transform.localScale = newScale;

                break;
        }



        Physics2D.gravity = gravityDirection;
        gravity = direction;
        //rb.linearVelocity = Vector2.zero;

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
        return Physics2D.OverlapCircle(groundTransform.position, 0.1f, plataformLayer);
    }

    bool IsOnWall()
    {
        return Physics2D.OverlapCircle(wallTransform.position, 0.01f, plataformLayer);
    }

    bool IsInGenericObject(float sizeX, float sizeY, LayerMask layer)
    {
        return Physics2D.OverlapBox(genericTransform.position, new Vector2(sizeX, sizeY), 0, layer);
    }

}
