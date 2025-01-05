using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int bulletDamage;  // The amount of damage the bullet does when it hits a target
    public GameManager gameManager;  // Reference to the GameManager for updating the game state
    public float bulletLifetime = 5f; // Bullet lifetime in seconds (how long the bullet exists before it is destroyed)

    private void Start()
    {
        // If GameManager is not already assigned, find it in the scene
        if (gameManager == null)
        {
            gameManager = FindObjectOfType<GameManager>();  // Automatically finds the GameManager object in the scene
        }

        // Destroy the bullet after the specified lifetime, to ensure it doesn't stay around forever
        Destroy(gameObject, bulletLifetime);
    }

    // This function is called when the bullet collides with another object
    private void OnCollisionEnter(Collision objectWeHit)
    {
        // If the bullet hits an object tagged as "Target"
        if (objectWeHit.gameObject.CompareTag("Target"))
        {
            print("Hit " + objectWeHit.gameObject.name + "!");  // Log to console
            CreateBulletImpactEffect(objectWeHit);  // Create impact effect at the collision point
            Destroy(gameObject);  // Destroy the bullet after it hits the target
        }

        // If the bullet hits a wall
        if (objectWeHit.gameObject.CompareTag("Wall"))
        {
            print("Hit a wall!");  // Log to console
            CreateBulletImpactEffect(objectWeHit);  // Create impact effect at the collision point
            Destroy(gameObject);  // Destroy the bullet after it hits the wall
        }

        // If the bullet hits a zombie
        if (objectWeHit.gameObject.CompareTag("Zombie"))
        {
            // Apply damage to the zombie and call a method to update the game state
            objectWeHit.gameObject.GetComponent<Zombie>().TakeDamage(bulletDamage);
            gameManager.OnZombieHit();  // Update game state with zombie hit count
            Destroy(gameObject);  // Destroy the bullet after it hits the zombie
        }

        // Update the game state when the bullet is fired (no matter what it hits)
        gameManager.OnBulletFired();  // This tracks how many bullets have been fired in the game
    }

    // Creates a visual effect where the bullet impacts the object
    void CreateBulletImpactEffect(Collision objectWeHit)
    {
        // Get the contact point where the bullet collided with the object
        ContactPoint contact = objectWeHit.contacts[0];

        // Instantiate the bullet impact effect (like a bullet hole or particle effect) at the point of contact
        GameObject hole = Instantiate(
            GlobalReferences.Instance.bulletImpactEffectPrefab,  // The bullet impact effect prefab from GlobalReferences
            contact.point,  // The point of contact (where the bullet hit the object)
            Quaternion.LookRotation(contact.normal)  // Align the impact effect with the normal of the surface
        );

        // Set the impact effect as a child of the object the bullet hit, so it moves with the object
        hole.transform.SetParent(objectWeHit.gameObject.transform);
    }
}
