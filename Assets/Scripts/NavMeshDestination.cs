
using UnityEngine;
using UnityEngine.AI;

public class NavMeshDestination : MonoBehaviour
{
    private NavMeshAgent _agent;
    [SerializeField] private Transform _destination;

    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();

        _agent.destination = _destination.position;
    }
}
