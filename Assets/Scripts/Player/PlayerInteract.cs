using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    private Camera cam;
    [SerializeField]
    private float distance = 3f;
    [SerializeField]
    private LayerMask mask;
    private PlayerUI playerUI;
private InputManager inputManager;
    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<PlayerLook>().cam;
        playerUI = GetComponent<PlayerUI>();
        inputManager = GetComponent<InputManager>();
    }

    // Update is called once per frame
    void Update()
    {
       playerUI.UpdateText(string.Empty);
        // Create a ray at the center of the camera, shooting outwards.
        Ray ray = new Ray(cam.transform.position, cam.transform.forward);
        Debug.DrawRay(ray.origin, ray.direction * distance);
        
        RaycastHit hitInfo; // Variable to store our collision information.

        // Perform the raycast and check if we hit something.
        if (Physics.Raycast(ray, out hitInfo, distance, mask))
        {
            //Checks if the game object has a interactable component.
           if (hitInfo.collider.GetComponent<Interactable>() != null)
           {
          // If game object has interactable component we store it in a variable.
        Interactable interactable = hitInfo.collider.GetComponent<Interactable>();
playerUI.UpdateText(interactable.promptMessage); //Update on screen text.
if (inputManager.onFoot.Interact.triggered){
interactable.BaseInteract();
}
            }
           }
           
        }
        }
        
    
