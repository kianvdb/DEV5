using UnityEngine.Events; 
using UnityEngine; 

// Define the InteractionEvent class, which will be used to trigger events when an object is interacted with
public class InteractionEvent : MonoBehaviour
{
    // A UnityEvent that will be invoked when an interaction occurs
    // This allows other objects to listen to this event and take action when the interaction happens
    public UnityEvent OnInteract;
}
