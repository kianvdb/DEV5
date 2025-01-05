using System; // Import for handling system-related functionalities (not directly used here)
using System.Collections; // Import for collections like arrays and lists
using System.Collections.Generic; // Import for using generic collections like List<T>
using Unity.VisualScripting; // Import for VisualScripting, though not used directly in this code
using UnityEngine; // Import UnityEngine for general Unity functionality like GameObject, Input, etc.

public class WeaponManager : MonoBehaviour
{
    // Singleton pattern to ensure only one instance of WeaponManager exists
    public static WeaponManager Instance { get; set;}

    // List to store weapon slots (used for switching weapons)
    public List<GameObject> weaponSlots;

    // Reference to the currently active weapon slot
    public GameObject activeWeaponSlot;

    // Ammo counters for rifle and pistol
    [Header("Ammo")]
    public int totalRifleAmmo = 0;
    public int totalPistolAmmo = 0;

    // Awake is called when the script instance is being loaded
    private void Awake()
    {
        // Singleton pattern to ensure only one instance of WeaponManager exists
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

    // Start is called before the first frame update
    private void Start()
    {
        // Set the first weapon slot as the active weapon slot on start
        activeWeaponSlot = weaponSlots[0];
    }

    // Update is called once per frame
    private void Update()
    {
        // Loop through all weapon slots and activate or deactivate them based on which one is active
        foreach(GameObject weaponSlot in weaponSlots)
        {
            if (weaponSlot == activeWeaponSlot)
            {
                // Activate the weapon in the active weapon slot
                weaponSlot.SetActive(true);
            }
            else
            {
                // Deactivate all other weapon slots
                weaponSlot.SetActive(false);
            }
        }

        // Switch between weapon slots based on user input (1 or 2 key press)
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            SwitchActiveSlot(0); // Switch to weapon slot 0
        }
        if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            SwitchActiveSlot(1); // Switch to weapon slot 1
        }
    }

    // PickupWeapon is called when a weapon is picked up
    public void PickupWeapon(GameObject pickedupWeapon)
    {
        // Add the weapon into the active slot
        AddWeaponIntoActiveSlot(pickedupWeapon);
    }

    // AddWeaponIntoActiveSlot adds a picked-up weapon into the active weapon slot
    private void AddWeaponIntoActiveSlot(GameObject pickedupWeapon)
    {
        // Drop the current active weapon if any
        DropCurrentWeapon(pickedupWeapon);

        // Set the picked-up weapon's parent to the active weapon slot
        pickedupWeapon.transform.SetParent(activeWeaponSlot.transform, false);

        // Get the Weapon component of the picked-up weapon
        Weapon weapon = pickedupWeapon.GetComponent<Weapon>();

        // Set the local position and rotation of the weapon based on its spawn position and rotation
        pickedupWeapon.transform.localPosition = new Vector3(weapon.spawnPosition.x, weapon.spawnPosition.y, weapon.spawnPosition.z);
        pickedupWeapon.transform.localRotation = Quaternion.Euler(weapon.spawnRotation.x, weapon.spawnRotation.y, weapon.spawnRotation.z);

        // Set the weapon as the active weapon and enable its animator
        weapon.isActiveWeapon = true;
        weapon.animator.enabled = true;
    }

    // PickupAmmo is called when an AmmoBox is picked up
    internal void PickupAmmo(AmmoBox ammo)
    {
        // Switch based on the type of ammo (Pistol or Rifle) and add the ammo to the respective total
        switch (ammo.ammoType)
        {
            case AmmoType.PistolAmmo:
                totalPistolAmmo += ammo.ammoAmount;
                break;
            case AmmoType.RifleAmmo:
                totalRifleAmmo += ammo.ammoAmount;
                break;
        }
    }

    // DropCurrentWeapon is called to drop the currently active weapon
    private void DropCurrentWeapon(GameObject pickedupWeapon)
    {
        // If there is a weapon in the active weapon slot, drop it
        if(activeWeaponSlot.transform.childCount > 0)
        {
            // Get the currently equipped weapon
            var weaponToDrop = activeWeaponSlot.transform.GetChild(0).gameObject;

            // Set the weapon as no longer active and disable its animator
            weaponToDrop.GetComponent<Weapon>().isActiveWeapon = false;
            weaponToDrop.GetComponent<Weapon>().animator.enabled = false;

            // Move the dropped weapon to the same position as the picked-up weapon
            weaponToDrop.transform.SetParent(pickedupWeapon.transform.parent);
            weaponToDrop.transform.localPosition = pickedupWeapon.transform.localPosition;
            weaponToDrop.transform.localRotation = pickedupWeapon.transform.localRotation;
        }
    }

    // SwitchActiveSlot is called to switch to a different weapon slot
    public void SwitchActiveSlot(int slotNumber)
    {
        // If there is a currently equipped weapon, make it inactive
        if(activeWeaponSlot.transform.childCount > 0)
        {
            Weapon currentWeapon = activeWeaponSlot.transform.GetChild(0).GetComponent<Weapon>();
            currentWeapon.isActiveWeapon = false;
        }

        // Switch the active weapon slot to the one specified by slotNumber
        activeWeaponSlot = weaponSlots[slotNumber];

        // If the new slot contains a weapon, make it the active weapon
        if(activeWeaponSlot.transform.childCount > 0)
        {
            Weapon newWeapon = activeWeaponSlot.transform.GetChild(0).GetComponent<Weapon>();
            newWeapon.isActiveWeapon = true;
        }
    }

    // DecreaseTotalAmmo decreases the total ammo for the specified weapon type
    internal void DecreaseTotalAmmo(int bulletsToDecrease, Weapon.WeaponModel thisWeaponModel)
    {
        // Switch based on the weapon model and decrease the corresponding ammo count
        switch (thisWeaponModel)
        {
            case Weapon.WeaponModel.Pistol:
                totalPistolAmmo -= bulletsToDecrease;
                break;
            case Weapon.WeaponModel.Rifle:
                totalRifleAmmo -= bulletsToDecrease;
                break;
        }
    }

    // CheckAmmoLeftFor returns the total ammo left for the specified weapon model
    public int CheckAmmoLeftFor(Weapon.WeaponModel thisWeaponModel)
    {
        // Switch based on the weapon model and return the corresponding ammo count
        switch (thisWeaponModel)
        {
            case Weapon.WeaponModel.Rifle:
                return totalRifleAmmo;
            case Weapon.WeaponModel.Pistol:
                return totalPistolAmmo;
            default:
                return 0;
        }
    }
}
