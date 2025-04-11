using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerScript : MonoBehaviour
{

    
    public Rigidbody2D rb;

    public InputActionReference movement, interaction, attack;

    public GameObject sword;

    public float speed;
    public string direction;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        playerMovement();
        updateDirection();
    }


    void playerMovement()
    {

        rb.linearVelocity = movement.action.ReadValue<Vector2>() * speed;

        if (GameObject.Find("Sword(Clone)")) { rb.linearVelocity *= 0; }

    }

    void updateDirection()
    {
        

        if (rb.linearVelocityX != 0)
        {
            direction = (rb.linearVelocityX < 0) ? "left" : "right";
        }

        if (rb.linearVelocityY != 0)
        {
            direction = (rb.linearVelocityY < 0) ? "down" : "up";
        }
    }

    private void action(InputAction.CallbackContext context)
    {
        Instantiate(sword);
    }

    private void OnEnable()
    {
        attack.action.performed += action;
    }

    private void OnDisable()
    {
        attack.action.performed -= action;
    }

    


}
