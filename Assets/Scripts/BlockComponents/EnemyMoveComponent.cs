using UnityEngine;
using UnityEngine.AI;

public class EnemyMoveComponent : BaseComponent
{
    public override ComponentType Type => ComponentType.Move;

    [SerializeField] private float _speed;
    [SerializeField] private GameObject _target;

    private NavMeshAgent _agent;


    private void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _agent.speed = _speed;
    }

    private void Update()
    {
        if (_target == null) {

        }
        else {
            _agent.SetDestination(_target.transform.position);
        }
    }
}
