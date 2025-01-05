using System.Collections; // Import for collections like arrays and lists
using System.Collections.Generic; // Import for using generic collections like List<T>
using System.Runtime.CompilerServices; // Import for using advanced runtime features (not directly used here)
using UnityEngine; // Import UnityEngine for general Unity functionality like MonoBehaviour, Raycasting, etc.
using UnityEngine.InputSystem.Interactions; // Import for input system interactions (not directly used in this code)

public class InteractionManager : MonoBehaviour
{
    // Singleton instance of InteractionManager for global access
    public static InteractionManager Instance { get; set;}

    // Currently hovered weapon and ammo box objects
    public Weapon hoveredWeapon = null;
    public AmmoBox hoveredAmmoBox = null;

    // Awake is called when the script instance is being loaded
    private void Awake()
    {
        // Singleton pattern to ensure only one instance of InteractionManager exists
        if(Instance != null && Instance != this)
        {
            // Destroy this instance if another one already exists
            Destroy(gameObject);
        }
        else
        {
            // Set this instance as the singleton instance
            Instance = this;
        }
    }

    // Update is called once per frame
    private void Update()
    {
        // Cast a ray from the center of the screen to detect interactable objects
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0)); // Ray originates from the center of the camera view (viewport)
        RaycastHit hit;

        // Perform the raycast and check if it hits any object
        if(Physics.Raycast(ray, out hit))
        {
            // Get the game object that the ray hit
            GameObject objectHitByRaycast = hit.transform.gameObject;

            // Check if the hit object is a Weapon (and not the active weapon)
            if(objectHitByRaycast.GetComponent<Weapon>() && objectHitByRaycast.GetComponent<Weapon>().isActiveWeapon == false)
            {
                // If it's a valid weapon, store it as the hoveredWeapon and enable the outline effect to highlight it
                hoveredWeapon = objectHitByRaycast.gameObject.GetComponent<Weapon>();
                hoveredWeapon.GetComponent<Outline>().enabled = true;

                // Check if the "F" key is pressed to pick up the weapon
                if(Input.GetKeyDown(KeyCode.F))
                {
                    // Call the WeaponManager to pick up the weapon
                    WeaponManager.Instance.PickupWeapon(objectHitByRaycast.gameObject);
                }
            }
            else
            {
                // If a previously hovered weapon is not valid anymore, disable the outline effect
                if(hoveredWeapon)
                {
                    hoveredWeapon.GetComponent<Outline>().enabled = false;
                }
            }

            // Check if the hit object is an AmmoBox
            if(objectHitByRaycast.GetComponent<AmmoBox>())
            {
                // If it's an AmmoBox, store it as the hoveredAmmoBox and enable the outline effect to highlight it
                hoveredAmmoBox = objectHitByRaycast.gameObject.GetComponent<AmmoBox>();
                hoveredAmmoBox.GetComponent<Outline>().enabled = true;

                // Check if the "F" key is pressed to pick up ammo
                if(Input.GetKeyDown(KeyCode.F))
                {
                    // Call the WeaponManager to pick up the ammo
                    WeaponManager.Instance.PickupAmmo(hoveredAmmoBox);

                    // Destroy the AmmoBox after it has been picked up
                    Destroy(objectHitByRaycast.gameObject);
                }
            }
            else
            {
                // If a previously hovered AmmoBox is not valid anymore, disable the outline effect
                if(hoveredAmmoBox)
                {
                    hoveredAmmoBox.GetComponent<Outline>().enabled = false;
                }
            }
        }
    }
}
