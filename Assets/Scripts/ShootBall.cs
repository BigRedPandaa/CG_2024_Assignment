using System;
using UnityEngine;
using UnityEngine.UIElements;

public class BallShooter : MonoBehaviour
{
    public GameObject ballPrefab; // Assign the ball prefab in the inspector
    public Transform shootPoint; // The position where the ball will be instantiated (e.g., player's hand)
    public float shootForce = 500f; // Adjust the shooting force

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        // Instantiate the ball at the shootPoint's position and rotation
        GameObject ball = Instantiate(ballPrefab, shootPoint.position, shootPoint.rotation);

        // Get the direction from the shootPoint to the center of the camera's view
        Camera cam = Camera.main;
        Vector3 direction = cam.transform.forward;

        // Apply force to the ball in the direction of the camera's forward vector
        Rigidbody rb = ball.GetComponent<Rigidbody>();
        rb.AddForce(direction * shootForce);
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("detection");
        if (other.CompareTag("Wall")) // Ensure the walls have the "Wall" tag
        {
            Debug.Log("hit");
            Destroy(gameObject); // Destroy the ball when it hits the wall
        }
    }

}
