using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;         // Reference to the player object
    public Vector3 offset = new Vector3(0, 2, -4); // Camera offset behind the player
    public float lookSensitivity = 2f; // Sensitivity for mouse looking up and down
    public float minYAngle = -30f;    // Min vertical angle for the camera
    public float maxYAngle = 60f;     // Max vertical angle for the camera

    private float currentPitch = 0f;  // Stores the current vertical camera rotation

    void Update()
    {
        if (player == null)
        {
            Debug.LogWarning("Player object is not assigned!");
            return;
        }

        // Get player rotation (without changing the camera's vertical rotation)
        Quaternion playerRotation = Quaternion.Euler(0, player.eulerAngles.y, 0);

        // Update the camera position to follow the player from behind
        transform.position = player.position + playerRotation * offset;

        // Get mouse input for looking up and down
        float mouseY = Input.GetAxis("Mouse Y") * lookSensitivity;

        // Adjust the pitch (vertical angle) of the camera
        currentPitch = Mathf.Clamp(currentPitch - mouseY, minYAngle, maxYAngle);

        // Apply rotation: match the player's yaw and adjust pitch independently
        transform.rotation = playerRotation * Quaternion.Euler(currentPitch, 0, 0);
    }
}
