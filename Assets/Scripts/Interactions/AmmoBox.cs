using System.Collections; 
using System.Collections.Generic; 
using UnityEngine; 

// Define the AmmoBox class, which is a MonoBehaviour that can be attached to GameObjects in Unity
public class AmmoBox : MonoBehaviour
{
    // Public integer to store the amount of ammo in the ammo box, default set to 200
    public int ammoAmount = 200;

    // Enum variable to specify the type of ammo (e.g., rifle ammo or pistol ammo)
    public AmmoType ammoType;
}

// Enum to define possible types of ammo that the AmmoBox can hold
public enum AmmoType{
    RifleAmmo,  // Represents ammo for a rifle
    PistolAmmo  // Represents ammo for a pistol
}
