using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerScript : MonoBehaviour
{

    
    public Rigidbody2D rb;

    public InputActionReference movement, interaction, attack;

    public GameObject sword;
    public Transform wallTransform;
    public Transform groundTransform;
    public LayerMask wallLayer;
    public LayerMask groundLayer;

    public float speed;
    public string direction;

    void Update()
    {
        PlayerMovement();
        UpdateDirection();
        WallSlide();
    }


    void PlayerMovement()
    {

        //rb.linearVelocity = movement.action.ReadValue<Vector2>() * speed;

        rb.linearVelocityX = movement.action.ReadValue<Vector2>().x * speed;
        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Z)) && rb.linearVelocityY == 0)
        {
            rb.linearVelocityY = 15;
        }

        if (GameObject.Find("Sword(Clone)")) { rb.linearVelocity *= 0; }

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
        return Physics2D.OverlapCircle(groundTransform.position, 0.01f, groundLayer);
    }

    bool IsOnWall()
    {
        return Physics2D.OverlapCircle(wallTransform.position, 0.01f, wallLayer);
    }

    void WallSlide()
    {
        if (IsOnWall() && !IsOnGround())
        {
            rb.linearVelocity = new Vector2(rb.linearVelocityX, Mathf.Clamp(rb.linearVelocityY, -2f, float.MaxValue));
        }
    }

    private void Action(InputAction.CallbackContext context)
    {
        Instantiate(sword);
    }

    private void T(InputAction.CallbackContext context)
    {

    }

    private void OnEnable()
    {
        interaction.action.performed += T;
        attack.action.performed += Action;
        
    }

    private void OnDisable()
    {
        interaction.action.performed -= T;
        attack.action.performed -= Action;
    }

    


}
