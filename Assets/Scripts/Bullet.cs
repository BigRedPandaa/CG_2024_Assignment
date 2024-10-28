using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
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
