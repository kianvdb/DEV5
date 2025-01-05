using System.Collections; // Import for using collections like arrays or lists
using System.Collections.Generic; // Import for using generic collections like List<T>
using UnityEngine; // Import for Unity-specific functions and components like MonoBehaviour
using UnityEngine.InputSystem; // Import for using the new Input System for handling player input

// InputManager handles all the player's input, managing movement, camera control, and actions like jump, crouch, and sprint.
public class InputManager : MonoBehaviour
{
    private PlayerInput playerInput; // Reference to PlayerInput, which handles all player input.
    public PlayerInput.OnFootActions onFoot; // Reference to the "OnFoot" action map that contains actions related to player movement and actions.
    private PlayerMotor motor; // Reference to PlayerMotor, which is responsible for player movement.
    private PlayerLook look; // Reference to PlayerLook, which handles the player's camera control (looking around).

    // Awake is called when the script instance is being loaded
    void Awake()
    {
        // Initialize the PlayerInput system and get the "OnFoot" action map
        playerInput = new PlayerInput();
        onFoot = playerInput.OnFoot;

        // Get references to the PlayerMotor and PlayerLook components attached to the same GameObject
        motor = GetComponent<PlayerMotor>();
        look = GetComponent<PlayerLook>();

        // Set up event listeners for player actions.
        // When these actions are triggered, the respective methods in PlayerMotor are called.
        onFoot.Jump.performed += ctx => motor.Jump(); // Jump action triggers PlayerMotor's Jump method
        onFoot.Crouch.performed += ctx => motor.Crouch(); // Crouch action triggers PlayerMotor's Crouch method
        onFoot.Sprint.performed += ctx => motor.Sprint(); // Sprint action triggers PlayerMotor's Sprint method
    }

    // FixedUpdate is called at a fixed interval, used for physics-related updates like movement.
    void FixedUpdate()
    {
        // Process movement input from the player and pass it to PlayerMotor for handling
        motor.ProcessMove(onFoot.Movement.ReadValue<Vector2>());
    }

    // LateUpdate is called after all Update calls, used for actions like camera control.
    private void LateUpdate()
    {
        // Process camera look input from the player and pass it to PlayerLook for camera control
        // This happens after movement to ensure smooth camera movement that is not affected by physics
        look.ProcessLook(onFoot.Look.ReadValue<Vector2>());
    }

    // OnEnable is called when the script is enabled, used to enable input actions.
    private void OnEnable()
    {
        // Enable the "OnFoot" action map to start receiving player input
        onFoot.Enable();
    }

    // OnDisable is called when the script is disabled, used to disable input actions.
    private void OnDisable()
    {
        // Disable the "OnFoot" action map to stop receiving player input
        onFoot.Disable();
    }
}
