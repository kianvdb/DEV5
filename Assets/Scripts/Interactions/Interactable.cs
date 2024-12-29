using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
   //Add or remove an InteractionEvent component to this gameObject.
 public bool useEvents;
 [SerializeField]
 public string promptMessage;


public virtual string Onlook(){
   return promptMessage;
}


//This function will be called from our player.
 public void BaseInteract(){
   if (useEvents)
   GetComponent<InteractionEvent>().OnInteract.Invoke();
    Interact();
 }


protected virtual void Interact(){
//We wont have any code written in this function.
//This is a template function to be overridden by our subclass.
}
}
