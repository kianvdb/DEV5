using System.Collections; // Import for collections like arrays or lists
using System.Collections.Generic; // Import for using generic collections like List<T>
using UnityEngine; // Import UnityEngine for general Unity functionality like AudioSource and AudioClip
using static Weapon; // Import the Weapon class to access the WeaponModel enum (Pistol, Rifle, etc.)

// SoundManager handles playing various sound effects related to weapons (shooting, reloading, etc.)
public class SoundManager : MonoBehaviour
{
    // Singleton pattern to ensure only one instance of SoundManager exists
    public static SoundManager Instance { get; set;}

    // AudioSources for handling shooting and reloading sounds
    public AudioSource ShootingChannel; // The audio source that plays shooting sounds
    public AudioClip PistolShot; // Sound clip for pistol shots
    public AudioClip RifleShot; // Sound clip for rifle shots

    // AudioSources for reloading sounds
    public AudioSource reloadingSoundRifle; // Sound for reloading the rifle
    public AudioSource reloadingSoundPistol; // Sound for reloading the pistol
    public AudioSource emptyMagazineSoundPistol; // Sound for empty magazine (if implemented later)

    // Awake is called when the script instance is being loaded
    private void Awake()
    {
        // Singleton pattern to ensure only one instance of SoundManager exists
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

    // PlayShootingSound is called to play the shooting sound depending on the weapon used
    public void PlayShootingSound(WeaponModel weapon)
    {
        // Switch based on the weapon model to play the appropriate sound
        switch(weapon)
        {
            case WeaponModel.Pistol:
                // Play shooting sound for the pistol (first play general shooting, then specific shot sound)
                ShootingChannel.Play();
                ShootingChannel.PlayOneShot(PistolShot);
                break;

            case WeaponModel.Rifle:
                // Play shooting sound for the rifle (first play general shooting, then specific shot sound)
                ShootingChannel.Play();
                ShootingChannel.PlayOneShot(RifleShot);
                break;
        }
    }

    // PlayReloadSound is called to play the reload sound depending on the weapon used
    public void PlayReloadSound(WeaponModel weapon)
    {
        // Switch based on the weapon model to play the appropriate reload sound
        switch(weapon)
        {
            case WeaponModel.Pistol:
                // Play reloading sound for the pistol
                reloadingSoundPistol.Play();
                break;

            case WeaponModel.Rifle:
                // Play reloading sound for the rifle
                reloadingSoundRifle.Play();
                break;
        }
    }
}
