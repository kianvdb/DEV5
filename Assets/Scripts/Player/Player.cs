using System.Collections; // For using IEnumerator and coroutines
using System.Collections.Generic; // For using collections like Lists
using UnityEngine; // Unity Engine core functionality like GameObject, MonoBehaviour, etc.
using UnityEngine.UI; // To work with UI components, like Image for bloody screen

public class Player : MonoBehaviour
{
    public int HP = 100; // Player's health points
    public GameObject bloodyScreen; // Reference to the bloody screen effect UI element
    public GameManager gameManager;  // Reference to the GameManager script

    // Start is called before the first frame update
    private void Start()
    {
        // If GameManager is not already assigned in the Inspector, find it in the scene
        if (gameManager == null)
        {
            gameManager = FindObjectOfType<GameManager>();  // Finds the GameManager in the scene
        }
    }

    // Method to handle the player taking damage
    public void TakeDamage(int damageAmount)
    {
        HP -= damageAmount; // Subtract damage from the player's HP

        // Check if the player's HP drops to 0 or below
        if (HP <= 0)
        {
            // Randomly decide whether the player dies or just gets a hit effect
            int randomValue = Random.Range(0, 2); // Randomly returns 0 or 1
            if (randomValue == 0)
            {
                print("Player Dead"); // Print a message to the console
                PlayerDead(); // Call method to handle player death
            }
            else
            {
                print("Player Hit"); // Print a message to the console
                StartCoroutine(BloodyScreenEffect()); // Start the bloody screen effect coroutine
            }
        }
    }

    // Method that handles the player's death
    private void PlayerDead()
    {
        // Disable player movement and look controls
        GetComponent<PlayerLook>().enabled = false;
        GetComponent<PlayerMotor>().enabled = false;

        // Play the death animation
        GetComponentInChildren<Animator>().enabled = true;

        // Notify the GameManager that the game is over and pass the player's survival time
        gameManager.EndGame(gameManager.survivalTime); // Pass survival time to the GameManager's EndGame method
    }

    // Coroutine to create the bloody screen effect
    private IEnumerator BloodyScreenEffect()
    {
        // Ensure that the bloody screen is active in the hierarchy
        if (!bloodyScreen.activeInHierarchy)
        {
            bloodyScreen.SetActive(true); // Enable the bloody screen UI element
        }

        var image = bloodyScreen.GetComponentInChildren<Image>(); // Get the Image component within bloodyScreen

        // Set the initial alpha value of the image to 1 (fully visible)
        Color startColor = image.color;
        startColor.a = 1f;
        image.color = startColor;

        float duration = 3f; // Duration for the effect
        float elapsedTime = 0f;

        // Gradually decrease the alpha value of the bloody screen over time
        while (elapsedTime < duration)
        {
            // Use Lerp to interpolate the alpha value from 1 to 0
            float alpha = Mathf.Lerp(1f, 0f, elapsedTime / duration);

            // Update the alpha of the image
            Color newColor = image.color;
            newColor.a = alpha;
            image.color = newColor;

            // Increment elapsed time
            elapsedTime += Time.deltaTime;

            yield return null; // Wait for the next frame
        }

        // After the effect is finished, hide the bloody screen
        if (bloodyScreen.activeInHierarchy)
        {
            bloodyScreen.SetActive(false);
        }
    }

    // Trigger method that is called when the player collides with a trigger collider
    private void OnTriggerEnter(Collider other)
    {
        // Check if the object the player collided with is a ZombieHand
        if (other.CompareTag("ZombieHand"))
        {
            // Get the damage from the ZombieHand and call TakeDamage
            TakeDamage(other.gameObject.GetComponent<ZombieHand>().damage);
        }
    }
}
