using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    public Camera cam; // Reference to the camera used for looking around.
    private float xRotation = 0f; // Keeps track of the camera's vertical rotation (up/down).
    public float xSensitivity = 30f; // Sensitivity for horizontal (left/right) camera movement.
    public float ySensitivity = 30f; // Sensitivity for vertical (up/down) camera movement.

    public void ProcessLook(Vector2 input)
    {
        // Extract horizontal (x) and vertical (y) input from the input vector.
        float mouseX = input.x;
        float mouseY = input.y;

        // Calculate the vertical camera rotation.
        xRotation = (mouseY * Time.deltaTime) * ySensitivity;
        xRotation = Mathf.Clamp(xRotation, -80f, 80f); // Clamp the rotation to prevent the camera from rotating too far up/down.

        // Apply the vertical rotation to the camera.
        cam.transform.localRotation = Quaternion.Euler(xRotation, 0, 0);

        // Rotate the player horizontally based on the mouse movement (looking left and right).
        transform.Rotate(Vector3.up * (mouseX * Time.deltaTime) * xSensitivity);
    }
}
