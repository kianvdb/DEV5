using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private PlayerInput playerInput; // Reference to PlayerInput, the main class that handles player input.
    public PlayerInput.OnFootActions onFoot; // Reference to the "OnFoot" action map, which contains the player's movement and actions.
    private PlayerMotor motor; // Reference to the PlayerMotor script that handles movement.
    private PlayerLook look; // Reference to the PlayerLook script that handles camera control (looking around).

    void Awake()
    {
        // Initialize PlayerInput and the "OnFoot" action map.
        playerInput = new PlayerInput();
        onFoot = playerInput.OnFoot;

        // Get references to the PlayerMotor and PlayerLook components on the same GameObject.
        motor = GetComponent<PlayerMotor>();
        look = GetComponent<PlayerLook>();

        // Set up input event listeners.
        onFoot.Jump.performed += ctx => motor.Jump(); // Call Jump method on PlayerMotor when the Jump action is performed.
        onFoot.Crouch.performed += ctx => motor.Crouch(); // Call Crouch method on PlayerMotor when the Crouch action is performed.
        onFoot.Sprint.performed += ctx => motor.Sprint(); // Call Sprint method on PlayerMotor when Sprint action is performed.
    }

    void FixedUpdate()
    {
        // Process movement input every fixed frame and pass it to the PlayerMotor.
        motor.ProcessMove(onFoot.Movement.ReadValue<Vector2>());
    }

    private void LateUpdate()
    {
        // Process camera look input every frame (this happens after movement to ensure smooth camera control).
        look.ProcessLook(onFoot.Look.ReadValue<Vector2>());
    }

    private void OnEnable()
    {
        // Enable the input actions when the script is enabled.
        onFoot.Enable();
    }

    private void OnDisable()
    {
        // Disable the input actions when the script is disabled to stop receiving input.
        onFoot.Disable();
    }
}
