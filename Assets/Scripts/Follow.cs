using UnityEngine;

public class Follow : MonoBehaviour
{
    [SerializeField] private GameObject _target;
    [SerializeField] private Vector3 _offset;

    public void SetTarget(GameObject target)
    {
        _target = target;
    }

    private void Update()
    {
        if (_target != null) {
            transform.position = _target.transform.position + _offset;
        }
    }
}
