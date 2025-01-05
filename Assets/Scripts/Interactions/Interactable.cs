using System.Collections;
using System.Collections.Generic; 
using UnityEditor; 
using UnityEngine;

// Define the Interactable class, which is an abstract class that can be attached to GameObjects
// This class will serve as a base for other classes that can be interacted with by the player
public abstract class Interactable : MonoBehaviour
{
    // Boolean flag to determine if InteractionEvent components are used
    // When true, an InteractionEvent component will be added or removed when interacting with the object
    public bool useEvents;

    // A message that will be shown to the player when they look at the object (prompt)
    [SerializeField]
    public string promptMessage;

    // Virtual method that returns the prompt message, can be overridden by subclasses
    // This method is called when the player looks at the interactable object
    public virtual string Onlook(){
        return promptMessage;
    }

    // This function is called when the player interacts with the object
    // It checks if events are to be used and triggers the interaction event if applicable
    public void BaseInteract(){
        // If 'useEvents' is true, invoke the interaction event from the InteractionEvent component
        if (useEvents)
            GetComponent<InteractionEvent>().OnInteract.Invoke();
        
        // Call the Interact method, which can be overridden by subclasses to define specific behavior
        Interact();
    }

    // Protected virtual method meant to be overridden by subclasses
    // This method doesn't do anything in the base class, it serves as a template for specific interaction logic
    protected virtual void Interact(){
        // No code written here - this method is intended to be customized by subclasses
    }
}
