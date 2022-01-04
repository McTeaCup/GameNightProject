using System;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class PlayerMovement : MonoBehaviour
{
    //Debug Stuff
    [SerializeField] bool forceMovement;
    [SerializeField] float maxVelocity;
    [SerializeField] float playerSpeed = 3f;
    [SerializeField] float breakSpeed = 2f;
    
    Vector2 playerMovementInput;
    Rigidbody2D playerRB;

    private void Awake()
    {
        if (GetComponent<Rigidbody2D>() != null)
        {
            playerRB = GetComponent<Rigidbody2D>();
        }
        else
        {
            gameObject.AddComponent<Rigidbody2D>();
            playerRB = GetComponent<Rigidbody2D>();
        }

        playerRB.gravityScale = 0;
    }

    private void Update()
    {
       if (playerMovementInput.x != 0 || playerMovementInput.y != 0)
        {
            if (forceMovement)
            {
                playerRB.AddForce(new Vector2(
                    playerMovementInput.x * playerSpeed, 
                    playerMovementInput.y * playerSpeed));
                
                playerRB.velocity = Vector2.ClampMagnitude(playerRB.velocity, maxVelocity);
            }
            else
            {
                transform.position += new Vector3(playerMovementInput.x * playerSpeed,
                    playerMovementInput.y * playerSpeed,
                    0) * Time.deltaTime;
            }
        }
       else if (forceMovement)
       {
           playerRB.AddForce(-playerRB.velocity * breakSpeed);
       }
    }
    
    //Gets the movement input of the player
    public void OnMovePlayer(InputAction.CallbackContext value)
    {
        playerMovementInput = value.ReadValue<Vector2>();
    }
}
