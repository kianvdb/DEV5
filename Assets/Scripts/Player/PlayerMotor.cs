using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotor : MonoBehaviour
{
    private CharacterController controller; // Reference to the CharacterController that handles player collisions and movement.
    private Vector3 playerVelocity; // Stores the player's current velocity (used for gravity and jumping).
    private bool isGrounded; // Whether the player is grounded or not (to handle jumping).
    private bool crouching = false; // Whether the player is currently crouching.
    private bool sprinting = false; // Whether the player is currently sprinting.
    private bool lerpCrouch = false; // Whether the crouch height should be smoothly transitioned.
    private float crouchTimer = 0f; // Timer used for the smooth crouch transition.
    public float speed = 5f; // Walking speed of the player.
    public float sprintSpeed = 8f; // Sprinting speed of the player.
    public float crouchSpeed = 2.5f; // Speed while crouching.
    public float gravity = -9.8f; // Gravity force applied to the player.
    public float jumpHeight = 3f; // Jump height for the player.
    public float crouchHeight = 1f; // Height when the player is crouching.
    private float standingHeight = 2f; // Normal standing height.

    void Start()
    {
        // Get the CharacterController component attached to the player.
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        isGrounded = controller.isGrounded; // Check if the player is grounded.

        // Smoothly transition crouch height.
        if (lerpCrouch)
        {
            crouchTimer += Time.deltaTime;
            float p = crouchTimer / 1f; // Duration of the crouch transition.
            p *= p; // Ease-in-out effect for smooth transition.

            if (crouching)
                controller.height = Mathf.Lerp(controller.height, crouchHeight, p);
            else
                controller.height = Mathf.Lerp(controller.height, standingHeight, p);

            if (p > 1f)
            {
                lerpCrouch = false;
                crouchTimer = 0f;
            }
        }
    }

    public void ProcessMove(Vector2 input)
    {
        // Convert the 2D movement input into a 3D direction (ignoring vertical movement for now).
        Vector3 moveDirection = new Vector3(input.x, 0, input.y);

        // Move the player with different speeds depending on whether the player is sprinting, crouching, or normal.
        if (sprinting)
        {
            controller.Move(transform.TransformDirection(moveDirection) * sprintSpeed * Time.deltaTime);
        }
        else if (crouching)
        {
            controller.Move(transform.TransformDirection(moveDirection) * crouchSpeed * Time.deltaTime);
        }
        else
        {
            controller.Move(transform.TransformDirection(moveDirection) * speed * Time.deltaTime);
        }

        // Apply gravity (falling effect).
        playerVelocity.y += gravity * Time.deltaTime;

        // If grounded and falling, reset the vertical velocity.
        if (isGrounded && playerVelocity.y < 0)
        {
            playerVelocity.y = -2f;
        }

        // Move the player based on velocity.
        controller.Move(playerVelocity * Time.deltaTime);
    }

    public void Jump()
    {
        // Apply upward velocity for jumping, based on jumpHeight and gravity.
        if (isGrounded)
        {
            playerVelocity.y = Mathf.Sqrt(jumpHeight * -2.0f * gravity);
        }
    }

    public void Crouch()
    {
        // Toggle crouching state.
        crouching = !crouching;
        crouchTimer = 0f;
        lerpCrouch = true;

        // Adjust the player's speed based on whether they are crouching or not.
        if (crouching)
        {
            speed = crouchSpeed;
        }
        else
        {
            speed = 5f; // Default speed when standing.
        }
    }

    public void Sprint()
    {
        // Toggle sprinting state.
        sprinting = !sprinting;
    }
}
