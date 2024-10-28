using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public Transform player;         
    private NavMeshAgent agent;       

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();   
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform; 
        }
    }

    void Update()
    {
        agent.SetDestination(player.position); 
    }

    public void Die()
    {
        GameManager.instance.AddScore();
        Debug.Log("Is bullet");
        Destroy(gameObject);
     }
}