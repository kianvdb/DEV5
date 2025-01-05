using System.Collections; 
using System.Collections.Generic; 
using UnityEngine; 

// Define the Keypad class that inherits from Interactable
// This class will define the specific interaction behavior for a keypad object in the game
public class Keypad : Interactable
{
    // Serialize the door GameObject so it can be assigned from the Unity editor
    // This represents the door object that will be affected by the keypad interaction
    [SerializeField]
    private GameObject door;

    // A boolean flag to track whether the door is open or closed
    private bool doorOpen;

    // Start is called before the first frame update
    void Start()
    {
        // Currently empty - can be used for initial setup if needed in the future
    }

    // Update is called once per frame
    void Update()
    {
        // Currently empty - can be used for dynamic updates each frame if needed
    }

    // This function is called when the player interacts with the keypad.
    // It toggles the state of the door between open and closed.
    protected override void Interact()
    {
        // Toggle the door's open/closed state
        doorOpen = !doorOpen;

        // Use the Animator component on the door to set its "IsOpen" boolean parameter
        // This will trigger the door's animation (e.g., opening or closing)
        door.GetComponent<Animator>().SetBool("IsOpen", doorOpen);
    }
}
