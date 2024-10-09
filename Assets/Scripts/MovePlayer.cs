using UnityEngine;

public class MovePlayer : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float rotationSpeed = 700f;

    private CharacterController controller;
    private Vector3 moveDirection;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        MovingPlayer();
    }

    void MovingPlayer()
    {
        // Get input for movement (WASD or arrow keys)
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // Create movement direction based on the player's input
        Vector3 inputDirection = new Vector3(horizontal, 0, vertical).normalized;

        // Convert local movement direction based on player's rotation
        moveDirection = transform.TransformDirection(inputDirection);

        // Apply movement
        controller.Move(moveDirection * moveSpeed * Time.deltaTime);

        // Rotate the player based on mouse input
        float mouseX = Input.GetAxis("Mouse X");
        transform.Rotate(0, mouseX * rotationSpeed * Time.deltaTime, 0);
    }
}