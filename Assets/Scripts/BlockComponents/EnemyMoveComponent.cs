using UnityEngine;
using UnityEngine.AI;

public class EnemyMoveComponent : BaseComponent
{
    public override ComponentType Type => ComponentType.Move;

    [SerializeField] private float _speed;

    public GameObject Target { get; set; }

    private NavMeshAgent _agent;


    private void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _agent.speed = _speed;
    }

    private void Update()
    {
        if (Target != null) {
            try {
                _agent.SetDestination(Target.transform.position);
            } catch { 
                Target = null;
                blockBehaviour.SelfDestruct();
            }
        }
    }
}
