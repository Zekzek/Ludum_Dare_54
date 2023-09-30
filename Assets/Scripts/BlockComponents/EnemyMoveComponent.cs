using UnityEngine;
using UnityEngine.AI;

public class EnemyMoveComponent : BaseComponent
{
    public override ComponentType Type => ComponentType.Move;

    [SerializeField] private float speed;
    [SerializeField] private GameObject target;

    private NavMeshAgent _agent;


    private void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _agent.speed = speed;
    }

    private void Update()
    {
        _agent.SetDestination(target.transform.position);
    }
}
