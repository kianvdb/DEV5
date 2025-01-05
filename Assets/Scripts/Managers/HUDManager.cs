using System; // Import System namespace for general utility functions like handling exceptions and date/time
using System.Collections; // Import System.Collections namespace for handling collections like arrays and lists
using System.Collections.Generic; // Import System.Collections.Generic for working with generic collections like List<T>
using TMPro; // Import TMPro for handling text UI elements using TextMeshPro
using UnityEngine; // Import UnityEngine for general Unity functionality like MonoBehaviour and UI components
using UnityEngine.UI; // Import UnityEngine.UI for working with UI components like Images and Texts

// Define the HUDManager class responsible for managing and updating the heads-up display (HUD) in the game
public class HUDManager : MonoBehaviour
{
    // Singleton instance of the HUDManager class for global access
    public static HUDManager Instance { get; set; }

    [Header("Ammo")]
    // UI elements for displaying magazine ammo and total ammo count
    public TextMeshProUGUI magazineAmmoUI;
    public TextMeshProUGUI totalAmmoUI;
    public Image ammoTypeUI;

    [Header("Weapon")]
    // UI elements for displaying active and inactive weapon sprites
    public Image activeWeaponUI;
    public Image unActiveWeaponUI;

    [Header("Throwables")]
    // UI elements for lethal and tactical throwables
    public Image lethalUI;
    public TextMeshProUGUI lethalAmountUI;

    public Image tacticalUI;
    public TextMeshProUGUI tacticalAmountUI;

    // Sprite to represent empty slots in the HUD
    public Sprite emptySlot;

    // Awake is called when the script instance is being loaded
    private void Awake()
    {
        // Singleton pattern to ensure only one instance of HUDManager exists
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // Destroy this instance if another one already exists
        }
        else
        {
            Instance = this; // Assign this instance to the singleton
        }
    }

    // Update is called once per frame
    private void Update()
    {
        // Get the active and inactive weapon slots from the WeaponManager
        Weapon activeWeapon = WeaponManager.Instance.activeWeaponSlot.GetComponentInChildren<Weapon>();
        Weapon unActiveWeapon = GetUnActiveWeaponSlot().GetComponentInChildren<Weapon>();

        // If an active weapon exists, update the HUD with the weapon and ammo information
        if (activeWeapon)
        {
            // Display ammo count based on the active weapon
            magazineAmmoUI.text = $"{activeWeapon.bulletsLeft / activeWeapon.bulletsPerBurst}";
            totalAmmoUI.text = $"{WeaponManager.Instance.CheckAmmoLeftFor(activeWeapon.thisWeaponModel)}";

            // Update the ammo type icon
            Weapon.WeaponModel model = activeWeapon.thisWeaponModel;
            ammoTypeUI.sprite = GetWeaponSprite(model);

            // If an inactive weapon exists, update its icon in the HUD
            if (unActiveWeapon)
            {
                unActiveWeaponUI.sprite = GetWeaponSprite(unActiveWeapon.thisWeaponModel);
            }
        }
        else
        {
            // If no active weapon exists, clear all UI elements related to weapon and ammo
            magazineAmmoUI.text = "";
            totalAmmoUI.text = "";
            ammoTypeUI.sprite = emptySlot;
            activeWeaponUI.sprite = emptySlot;
            unActiveWeaponUI.sprite = emptySlot;
        }
    }

    // Get the sprite for the weapon based on its model
    private Sprite GetWeaponSprite(Weapon.WeaponModel model)
    {
        // Load the appropriate weapon prefab from Resources and return its sprite
        switch (model)
        {
            case Weapon.WeaponModel.Pistol:
                return Instantiate(Resources.Load<GameObject>("Pistol_Weapon")).GetComponent<SpriteRenderer>().sprite;

            case Weapon.WeaponModel.Rifle:
                return Instantiate(Resources.Load<GameObject>("Rifle_Weapon")).GetComponent<SpriteRenderer>().sprite;

            default:
                return null; // Return null if the weapon model doesn't match any cases
        }
    }

    // Get the sprite for the ammo based on the weapon model
    private Sprite GetAmmoSprite(Weapon.WeaponModel model)
    {
        // Load the appropriate ammo prefab from Resources and return its sprite
        switch (model)
        {
            case Weapon.WeaponModel.Pistol:
                return Instantiate(Resources.Load<GameObject>("Pistol_Ammo")).GetComponent<SpriteRenderer>().sprite;

            case Weapon.WeaponModel.Rifle:
                return Instantiate(Resources.Load<GameObject>("Rifle_Ammo")).GetComponent<SpriteRenderer>().sprite;

            default:
                return null; // Return null if the ammo model doesn't match any cases
        }
    }

    // Get the slot for the inactive weapon (the one not currently active)
    private GameObject GetUnActiveWeaponSlot()
    {
        // Loop through all weapon slots in the WeaponManager
        foreach (GameObject weaponSlot in WeaponManager.Instance.weaponSlots)
        {
            // Return the slot that is not the active weapon slot
            if (weaponSlot != WeaponManager.Instance.activeWeaponSlot)
            {
                return weaponSlot;
            }
        }

        // This will never happen if there are at least two weapon slots, but return null as a fallback
        return null;
    }
}
