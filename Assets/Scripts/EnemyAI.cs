using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public Transform player;            // Reference to the player's transform
    public GameObject deathEffect;      // Optional effect to spawn on death (like an explosion or particle effect)

    private NavMeshAgent agent;         // NavMeshAgent component for pathfinding

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();   // Get the NavMeshAgent component
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;  // Finds the player by tag
        }
    }

    void Update()
    {
        agent.SetDestination(player.position);  // Continuously update destination to the player's current position
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))  // Checks if the collided object has the tag "Bullet"
        {
            Die();  // Calls the die function if hit by a bullet
            Destroy(other.gameObject);  // Destroy the bullet on impact
            GameManager.instance.AddScore();
        }
    }

    void Die()
    {
        if (deathEffect != null)
        {
            Instantiate(deathEffect, transform.position, Quaternion.identity);  // Spawn a death effect, if available
        }
        Destroy(gameObject);  // Destroy the enemy
    }
}