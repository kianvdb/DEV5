using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class GameManager : MonoBehaviour
{
    private int bulletsFired = 0;  // Track the number of bullets fired
    private int zombieHits = 0;    // Track the number of zombies hit
    public float survivalTime = 0f;
    private bool gameOver = false; // To check if the game is over

    // Call this method whenever the player fires a bullet
    public void OnBulletFired()
    {
        if (!gameOver) 
        {
            bulletsFired++;  // Increment bullet fired count
        }
    }

    // Call this method when a bullet hits a zombie
    public void OnZombieHit()
    {
        if (!gameOver) 
        {
            zombieHits++;  // Increment zombie hit count
        }
    }

    // Call this method when the player dies or game ends
    public void EndGame(float survivalTime)
    {
        gameOver = true; // Mark the game as over
        this.survivalTime = survivalTime;

        // After the game ends, calculate and save the stats
        SaveGameStats("Player1", survivalTime); // "Player1" is a placeholder, replace with actual player name
    }

    // Method to save game stats to the backend
    private void SaveGameStats(string playerName, float survivalTime)
    {
        StartCoroutine(SendStatsToBackend(playerName, survivalTime));
    }

    // Coroutine to send stats to the backend server
    IEnumerator SendStatsToBackend(string playerName, float survivalTime)
    {
        string url = "http://localhost:3000/api/stats";  // Replace with your backend server URL

        // Prepare the data to send
        var data = new
        {
            playerName = playerName,
            survivalTime = survivalTime,
            bulletsFired = bulletsFired,
            zombieHits = zombieHits,
            accuracy = CalculateAccuracy(),  // Calculate accuracy
            timestamp = System.DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ss.fffZ")  // ISO 8601 format timestamp
        };

        string jsonData = JsonUtility.ToJson(data);  // Convert to JSON

        // Create a POST request with raw JSON data
        UnityWebRequest request = new UnityWebRequest(url, "POST");
        byte[] jsonBytes = System.Text.Encoding.UTF8.GetBytes(jsonData);  // Convert string to byte array
        request.uploadHandler = new UploadHandlerRaw(jsonBytes);  // Upload the raw JSON data
        request.downloadHandler = new DownloadHandlerBuffer();  // Handle the response
        request.SetRequestHeader("Content-Type", "application/json");  // Set the header as JSON

        // Wait for the response
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            Debug.Log("Stats saved successfully!");
        }
        else
        {
            Debug.LogError("Error saving stats: " + request.error);
        }
    }

    // Calculate accuracy (percentage of hits out of bullets fired)
    private float CalculateAccuracy()
    {
        if (bulletsFired == 0) return 0f; // Avoid division by zero
        return (float)zombieHits / bulletsFired * 100f; // Accuracy as a percentage
    }

    // Call this in the Update function to track survival time
    public void UpdateSurvivalTime()
    {
        if (!gameOver)
        {
            survivalTime += Time.deltaTime;
        }
    }
}
