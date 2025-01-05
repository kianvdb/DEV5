using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    private Camera cam; // Reference to the player's camera
    [SerializeField]
    private float distance = 3f; // Interaction distance
    [SerializeField]
    private LayerMask mask; // Layer mask to define which objects can be interacted with
    private PlayerUI playerUI; // Reference to PlayerUI for updating interaction text
    private InputManager inputManager; // Reference to InputManager to handle player inputs

    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<PlayerLook>().cam; // Get the player's camera from the PlayerLook script
        playerUI = GetComponent<PlayerUI>(); // Get the PlayerUI component
        inputManager = GetComponent<InputManager>(); // Get the InputManager component
    }

    // Update is called once per frame
    void Update()
    {
        playerUI.UpdateText(string.Empty); // Clear any existing text on the screen

        // Create a ray at the center of the camera, shooting outwards.
        Ray ray = new Ray(cam.transform.position, cam.transform.forward);
        Debug.DrawRay(ray.origin, ray.direction * distance); // For debugging, visualize the ray in the scene view
        
        RaycastHit hitInfo; // Variable to store our collision information.

        // Perform the raycast and check if we hit something.
        if (Physics.Raycast(ray, out hitInfo, distance, mask)) 
        {
            // Checks if the game object has an Interactable component.
            if (hitInfo.collider.GetComponent<Interactable>() != null)
            {
                // If the game object has an interactable component, we store it in a variable.
                Interactable interactable = hitInfo.collider.GetComponent<Interactable>();
                playerUI.UpdateText(interactable.promptMessage); // Update on-screen text with the prompt message
                
                // Check if the Interact button is pressed (e.g., 'E' key or controller button).
                if (inputManager.onFoot.Interact.triggered)
                {
                    interactable.BaseInteract(); // Trigger the base interaction on the interactable object
                }
            }
        }
    }
}
