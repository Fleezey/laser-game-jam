using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    private Transform m_Target = null;

    private void Start()
    {
        m_Target = GameObject.FindWithTag("Player").transform;
    }

    private void Update()
    {
        NavMeshAgent agent = GetComponent<NavMeshAgent>();
        agent.destination = m_Target.position;
    }
}
